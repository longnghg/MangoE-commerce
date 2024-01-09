using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.LazyTest;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dtos;
using Mango.Services.ShoppingCartAPI.Services.IServices.Coupon;
using Mango.Services.ShoppingCartAPI.Services.IServices.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;
        IStringLocalizer _stringLocalizer;


        private readonly Lazy<CouponLazy> couponLazy;

        private  CouponLazy couponLazyInstance { get { return couponLazy.Value; } }

        public CartAPIController(AppDbContext dbContext,
            IMapper mapper,
            IProductService productService,
            ICouponService couponService,
            IStringLocalizer stringLocalizer
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _response = new ResponseDto();
            _stringLocalizer = stringLocalizer;

            _productService = productService;
            _couponService = couponService;
            // test lazy
            couponLazy = new Lazy<CouponLazy>(() =>
            {
                CouponLazy instance = new CouponLazy();
                return instance;
            },LazyThreadSafetyMode.ExecutionAndPublication);
        }
        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _dbContext.CartHeaders.AsNoTracking()
                    .FirstOrDefaultAsync(x=> x.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    // create
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _dbContext.CartHeaders.Add(cartHeader);
                    await _dbContext.SaveChangesAsync();

                    cartDto.CartDetails.First().CartHeaderId = cartHeader.Id;
                    _dbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    var cartDetailsFromDb = await _dbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        x => x.ProductId == cartDto.CartDetails.First().ProductId &&
                        x.CartHeaderId == cartHeaderFromDb.Id);

                    if (cartDetailsFromDb == null)
                    {
                        // create new
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.Id;
                        _dbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        // update count                             
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().Id = cartDetailsFromDb.Id;
                        _dbContext.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _dbContext.SaveChangesAsync();
                    }

                }
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
                throw;
            }
            return _response;
        }

        [HttpDelete("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                var cartDetails = _dbContext.CartDetails
                    .First(x=> x.Id == cartDetailsId);

                int totalCountOfCartItem = _dbContext.CartDetails
                    .Where(x => x.CartHeaderId == cartDetails.CartHeaderId)
                    .Count();


                _dbContext.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItem == 1)
                {
                    var cartHeaderToRemove = await _dbContext.CartHeaders
                        .FirstOrDefaultAsync(x => x.Id == cartDetails.CartHeaderId);

                    _dbContext.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _dbContext.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
                throw;
            }
            return _response;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCartAsync(string userId)
        {
            try
            {
                var name = _stringLocalizer["hello2"];

                var te = new ContrucChain();
                //int getage = couponLazyInstance.AgeCouponLazy;
                return new ResponseDto();

                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_dbContext.CartHeaders.First(x => x.UserId == userId))
                };
                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_dbContext.CartDetails.Where(x => x.CartHeaderId == cart.CartHeader.Id));

                IEnumerable<ProductDto> productDtos = await _productService.GetProductDtosAsync();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(x=> x.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                // apply coupon if any
                if (!String.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCouponDtoAsync(cart.CartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal >= coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount; // total price
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }


                _response.Result = cart;
            }

            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
                throw;
            }
            return _response;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<ResponseDto> ApplyCouponAsync([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _dbContext.CartHeaders.FirstAsync(x => x.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _dbContext.CartHeaders.Update(cartFromDb);
                await _dbContext.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPut("RemoveCoupon")]
        public async Task<ResponseDto> RemoveCouponAsync([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _dbContext.CartHeaders.FirstAsync(x => x.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = string.Empty;
                _dbContext.CartHeaders.Update(cartFromDb);
                await _dbContext.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }

    }

}
