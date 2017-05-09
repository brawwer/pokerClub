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
    public class PokerTableValuationsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly ApplicationDbContext _applicationContext;

        public PokerTableValuationsController(DatabaseContext context, ApplicationDbContext applicationContext)
        {
            _context = context;
            _applicationContext = applicationContext;
        }

        // GET: PokerTableValuations
        [Authorize]
        public async Task<IActionResult> Index(int? id)
        {
            var databaseContext = _context.PokerTableValuations
                .Where(s => s.PokerTableId == id)
                .Include(p => p.PokerTable).Where(p => p.PokerTable.Id == id)
                .Include(p => p.ValueCategory);

            var valueCategoryQuery = from d in _context.ValueCategories
                                     orderby d.Id.ToString()
                                     select d;

            //Show Value Categories in dropdown
            ViewBag.ValueCategoryId = new SelectList(valueCategoryQuery, "Id", "Name");

            var memberQuery = from e in _applicationContext.Users
                                  orderby e.Name
                                  select e;
                      
            ViewBag.MemberId = new SelectList(memberQuery, "Id", "Name");

            var pokerTableQuery = from z in _context.PokerTables
                                  select z;

            
            PokerTable pokerTable = pokerTableQuery.First();
            ViewData["PokerTableDate"] = pokerTable.Date.ToString("MMMM") + " " + pokerTable.Date.Day + "," + pokerTable.Date.Year;
            ViewData["PokerTableId"] = pokerTable.Id;

            //TODO: Will this show the POT for only this Poker Table?
            var pot = _context.PokerTableValuations
                .Where(s => s.PokerTableId == id)
                .Include(p => p.ValueCategory).Where(p => p.ValueCategoryId != 4);

            ViewData["Pot"] = (pot.ToList().Select(x => x.Amount).Sum()).ToString("C");

            return View(await databaseContext.ToListAsync());
        }
        
        // POST: PokerTableValuations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,PokerTableId,ValueCategoryId,Amount")] PokerTableValuation pokerTableValuation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokerTableValuation);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction("Index", new { id = pokerTableValuation.PokerTableId });
            
            //return RedirectToAction("Index/" + pokerTableValuation.PokerTableId);
        }
    

        // GET: PokerTableValuations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var pokerTableValuation = await _context.PokerTableValuations.SingleOrDefaultAsync(m => m.Id == id);
            _context.PokerTableValuations.Remove(pokerTableValuation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = pokerTableValuation.PokerTableId });
        }

        private bool PokerTableValuationExists(int id)
        {
            return _context.PokerTableValuations.Any(e => e.Id == id);
        }
    }
}
