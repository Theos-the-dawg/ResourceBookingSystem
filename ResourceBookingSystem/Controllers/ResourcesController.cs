using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ResourceBookingSystem.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResourcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// POST: Resources/Create creates a new resource.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Location,Capacity,IsAvailable")] Resource resource)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with validation errors if the model state is invalid
                return View(resource);
            }

            _context.Add(resource);
            await _context.SaveChangesAsync();
            // Redirect to the Index action (resource list) after successful creation
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        ///  GET: Resources Index displays a list of all resources.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Resources.ToListAsync());
        }

        /// <summary>
        /// GET: Resources/Details/ gets the details of a specific resource by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var resource = await _context.Resources
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (resource == null) return NotFound();

            return View(resource);
        }

        /// <summary>
        /// GET: Resources/Create displays the form to create a new resource.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        ///  GET: Resources/Edit/ enable the editing of a resource by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }
            return View(resource);
        }

        /// <summary>
        /// POST: Resources/Edit/ updates an existing resource by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Location,Capacity,IsAvailable")] Resource resource)
        {
            if (id != resource.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Log or inspect ModelState errors here
                return View(resource);
            }

            try
            {
                _context.Update(resource);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(resource.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: Resources/Delete/ deletes a resource by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        /// <summary>
        ///  POST: Resources/Delete/ deletes a resource by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// GET: Resources/Bookings/ gets the bookings for a specific resource by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}
