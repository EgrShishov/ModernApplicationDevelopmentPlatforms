using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_253505_Shishov.API.Data;
using WEB_253505_Shishov.Domain.Entities;

namespace WEB_253505_Shishov.API.Controllers
{
    public class ConstructorsController : Controller
    {
        private readonly AppDbContext _context;

        public ConstructorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Constructors
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Constructors.Include(c => c.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Constructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructor = await _context.Constructors
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (constructor == null)
            {
                return NotFound();
            }

            return View(constructor);
        }

        // GET: Constructors/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Constructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Picies,CategoryId,Description,Image,Price,Id")] Constructor constructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(constructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", constructor.CategoryId);
            return View(constructor);
        }

        // GET: Constructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructor = await _context.Constructors.FindAsync(id);
            if (constructor == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", constructor.CategoryId);
            return View(constructor);
        }

        // POST: Constructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Picies,CategoryId,Description,Image,Price,Id")] Constructor constructor)
        {
            if (id != constructor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(constructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConstructorExists(constructor.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", constructor.CategoryId);
            return View(constructor);
        }

        // GET: Constructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructor = await _context.Constructors
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (constructor == null)
            {
                return NotFound();
            }

            return View(constructor);
        }

        // POST: Constructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var constructor = await _context.Constructors.FindAsync(id);
            if (constructor != null)
            {
                _context.Constructors.Remove(constructor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConstructorExists(int id)
        {
            return _context.Constructors.Any(e => e.Id == id);
        }
    }
}
