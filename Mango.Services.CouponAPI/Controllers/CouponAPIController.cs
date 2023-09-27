using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private readonly IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> obj = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable< CouponDto >> (obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x=> x.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(obj) ;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;

        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponCode == code);
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;

        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;

        }


    }
}