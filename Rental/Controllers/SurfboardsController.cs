using Microsoft.AspNetCore.Mvc;
using Lib.Products;
using Rental.Data;
using Microsoft.AspNetCore.Authorization;
using Rental.Services.ApiCall;

namespace Rental.Controllers
{
    public class SurfboardsController : Controller
    {
        #region Dependency Injections
        private readonly RentalContext rentalContext;
        private readonly SurfboardApi surfboardApi;

        public SurfboardsController(RentalContext context)
        {
            rentalContext = context;
        }
        #endregion

        #region Index
        [HttpGet]
        // Route: Surfboards
        //public async Task<IActionResult> Index() => View("\"https://localhost:7051/api/surfboards\"");


        [HttpGet]
        // Route: Surfboards
        public async Task<IActionResult> Index()
        {
            IEnumerable<Surfboard>? surfboards = await surfboardApi.GetAllAsync("https://localhost:7051/api/surfboards") as IEnumerable<Surfboard>;

            if (surfboards.Any())
            {
                // Sets the modified context
                rentalContext.Surfboard = surfboards;

                return View(surfboards);
            }

            return NotFound("No Surfboards was found");
        }
        #endregion

        #region Create Methods
        // Route: Surfboards/Create
        [HttpGet]
        public IActionResult Create() => View(); // Think as the acualty page

        // Route: Surfboards/Create
        [HttpPost, Authorize(Roles = "Employee")]
        public async Task<IActionResult> Create([Bind] Surfboard surfboard) // Think as the button-action
        {
            await surfboardApi.AddAsync("https://localhost:7051/api/surfboard/add", surfboard);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Methods
        // Route: Surfboards/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                //Surfboard? selected = await rentalContext.Surfboard.FindAsync(id);
                Surfboard? selected = rentalContext.Surfboard.FirstOrDefault(surfboard => surfboard.Id == id);

                if (selected != null)
                {
                    return View(selected);
                }
                else
                {
                    return NotFound("The product that you are trying to edit was not found");
                }
            }
            return BadRequest();
        }

        // Route: Surfboards/Edit/id
        [HttpPost, Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit([FromBody, Bind] Surfboard surfboard)
        {
            await surfboardApi.EditAsync("", surfboard);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete Methods
        // Route: Surfboards/Delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                //Surfboard? selected = await rentalContext.Surfboard.FindAsync(id);
                Surfboard? selected = rentalContext.Surfboard.FirstOrDefault(surfboard => surfboard.Id == id);

                if (selected != null)
                {
                    return View(selected);
                }
                else
                {
                    return NotFound("The product that you are trying to delete was not found");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // Route: Surfboards/Delete/id
        [HttpPost, Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            await surfboardApi.DeleteAsync("", id);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
