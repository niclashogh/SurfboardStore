using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Lib.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase, IController<Rent>
    {
        #region Dependency Injections
        private readonly ProductContext productContext;

        public RentsController(ProductContext productContext)
        {
            this.productContext = productContext;
        }
        #endregion

        #region [Post] AddAsync **Checkout CreatedAtAction
        // Route: api/rent/id
        [HttpPost("rent/{id}")]
        public async Task<ActionResult<Rent>> AddAsync([FromBody] Rent rental)
        {
            if (productContext.Surfboard == null)
            {
                return NotFound("Due to the automatic dataseed of Surfboard items, there is an error loading or initializing the items");
            }

            bool rentalIsAvailable = await productContext.Rent.AnyAsync(obj => obj.SurfboardId != rental.Id);

            if (!rentalIsAvailable)
            {
                return BadRequest("Surfboard is already rented out");
            }
            else if (User != null)
            {
                productContext.Rent.Add(rental);
                await productContext.SaveChangesAsync();

                return CreatedAtAction("GetRental", new { Id = rental.Id }, rental); //??
            }
            else if (rental.GuestEmail != null && rental.Surfboard.Price <= 200)
            {
                productContext.Rent.Add(rental);
                await productContext.SaveChangesAsync();

                return CreatedAtAction("GetRental", new { Id = rental.Id }, rental); //??
            }
            else
            {
                return BadRequest("Guest or Customer login is required");
            }
        }
        #endregion

        #region [Put] EditAsync
        // Route: api/rental/edit/id
        [HttpPut("rental/edit/{id}"), Authorize]
        public async Task<ActionResult<Rent>> EditAsync([Bind] Rent rental)
        {
            return BadRequest("You are not allowed to edit your rentals, this is a professional store, not your grandma's market stall");
        }
        #endregion

        #region [Delete] DeleteAsync
        // Route: api/rental/delete/id
        [HttpDelete("/rental/delete/{id}"), Authorize]
        public async Task<ActionResult<Rent>> DeleteAsync([FromQuery] int id)
        {
            if (productContext.Rent == null)
            {
                return Ok("No surfboard is rented out at the moment");
            }
            else if (User != null)
            {
                var rental = await productContext.Rent.FindAsync(id);
                productContext.Rent.Remove(rental);
                await productContext.SaveChangesAsync();

                return Ok();
            }
            else // if user is logged in as Guest
            {
                return BadRequest("This current item is rented out to a Guest, the options to confirm and cancel this rental is provided in the email we send you");
            }

        }
        #endregion

        #region [Get] GetAllAsync
        // Route: api/rentals
        [HttpGet("rentals"), Authorize]
        public async Task<ActionResult<IEnumerable<Rent>>> GetAllAsync()
        {
            if (productContext.Rent == null)
            {
                return Ok("No surfboard is rented out at the moment");
            }

            if (User.IsInRole("Employee"))
            {
                IEnumerable<Rent> rentals = await productContext.Rent.ToListAsync();
                var jsonSerialize = JsonSerializer.Serialize(rentals);
                return Ok(jsonSerialize);
            }
            else if (User != null)
            {
                IEnumerable<Rent> rentals = await productContext.Rent
                    .Include(obj => obj.Surfboard)
                    .Include(obj => obj.Customer)
                    .Where(rental => rental.CustomerId == User.FindFirstValue("Id"))
                    .ToListAsync();

                var jsonSerialize = JsonSerializer.Serialize(rentals);

                return Ok(jsonSerialize);
            }
            else // If User is not logged in
            {
                return BadRequest("You need to be logged in to see your rented items, if you have rented an item as Guest, please check your email that you privided when you rented the board");
            }
        }
        #endregion
    }
}
