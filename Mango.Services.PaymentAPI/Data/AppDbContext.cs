using Mango.Services.PaymentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.PaymentAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

    }
}
