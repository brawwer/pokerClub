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
    public class PokerTablesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly ApplicationDbContext _applicationContext;

        public PokerTablesController(DatabaseContext context, ApplicationDbContext applicationContext)
        {
            _context = context;
            _applicationContext = applicationContext;
        }

        // GET: PokerTables
        [Authorize]
        public async Task<IActionResult> Index()
        {

            return View(await _context.PokerTables.ToListAsync());
        }

        // GET: PokerTables/Create
        public IActionResult Create()
        {
            var memberQuery = from e in _applicationContext.Users
                              orderby e.Name
                              select e;

            ViewBag.MemberId = new SelectList(memberQuery, "Name", "Name");

            return View();
        }

        // POST: PokerTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Host")] PokerTable pokerTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokerTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pokerTable);
        }

        // GET: PokerTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokerTable = await _context.PokerTables.SingleOrDefaultAsync(m => m.Id == id);
            if (pokerTable == null)
            {
                return NotFound();
            }
            return View(pokerTable);
        }

        // POST: PokerTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Host")] PokerTable pokerTable)
        {
            if (id != pokerTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokerTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokerTableExists(pokerTable.Id))
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
            return View(pokerTable);
        }

        // GET: PokerTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokerTable = await _context.PokerTables
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pokerTable == null)
            {
                return NotFound();
            }

            return View(pokerTable);
        }

        // POST: PokerTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokerTable = await _context.PokerTables.SingleOrDefaultAsync(m => m.Id == id);
            _context.PokerTables.Remove(pokerTable);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PokerTableExists(int id)
        {
            return _context.PokerTables.Any(e => e.Id == id);
        }
    }
}
