using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokerClub.Data;
using PokerClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace PokerClub.Controllers
{
    public class ValueCategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public ValueCategoriesController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: ValueCategories
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ValueCategories.ToListAsync());
        }

        // GET: ValueCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valueCategory = await _context.ValueCategories
                .SingleOrDefaultAsync(m => m.Id == id);
            if (valueCategory == null)
            {
                return NotFound();
            }

            return View(valueCategory);
        }

        // GET: ValueCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ValueCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ValueCategory valueCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valueCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(valueCategory);
        }

        // GET: ValueCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valueCategory = await _context.ValueCategories.SingleOrDefaultAsync(m => m.Id == id);
            if (valueCategory == null)
            {
                return NotFound();
            }
            return View(valueCategory);
        }

        // POST: ValueCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ValueCategory valueCategory)
        {
            if (id != valueCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valueCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValueCategoryExists(valueCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(valueCategory);
        }

        // GET: ValueCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valueCategory = await _context.ValueCategories
                .SingleOrDefaultAsync(m => m.Id == id);
            if (valueCategory == null)
            {
                return NotFound();
            }

            return View(valueCategory);
        }

        // POST: ValueCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valueCategory = await _context.ValueCategories.SingleOrDefaultAsync(m => m.Id == id);
            _context.ValueCategories.Remove(valueCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ValueCategoryExists(int id)
        {
            return _context.ValueCategories.Any(e => e.Id == id);
        }
    }
}
