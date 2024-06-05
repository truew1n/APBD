using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LAB10.DTO;
using LAB10.Models;
using LAB10.Data;

namespace LAB10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ShoppingCartContext _context;

        public AccountsController(ShoppingCartContext context)
        {
            _context = context;
        }

        [HttpGet("{accountId:int}")]
        public async Task<ActionResult<AccountDTO>> GetAccount(int accountId)
        {
            var account = await _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.ShoppingCarts)
                .ThenInclude(sc => sc.Product)
                .FirstOrDefaultAsync(a => a.AccountId == accountId);

            if (account == null)
            {
                return NotFound();
            }

            var accountDTO = new AccountDTO
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                Phone = account.Phone,
                Role = account.Role.Name,
                Cart = account.ShoppingCarts.Select(sc => new CartDetailDTO
                {
                    ProductId = sc.ProductId,
                    ProductName = sc.Product.Name,
                    Amount = sc.Amount
                }).ToList()
            };

            return accountDTO;
        }
    }
}
