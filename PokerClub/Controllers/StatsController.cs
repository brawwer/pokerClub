using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokerClub.Data;
using PokerClub.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerClub.Controllers
{
    public class StatsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly ApplicationDbContext _applicationContext;

        public StatsController(DatabaseContext context, ApplicationDbContext applicationContext)
        {
            _context = context;
            _applicationContext = applicationContext;
        }

        // GET: PokerTableValuations
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new StatsViewModel();
            model.StatsViews = new List<StatsView>();

            var pokerTableValuations = _context.PokerTableValuations;

            var members = await _applicationContext.Users.ToListAsync();
            foreach(var member in members)
            {
                StatsView statsView = new StatsView();
                statsView.MemberName = member.Name;
                statsView.TotalGamesPlayed = pokerTableValuations.ToList()
                    .Where(x => x.MemberId == member.Id)
                    .Select(z => z.PokerTableId).Distinct().Count();
                statsView.ParticipationPercentage = (int)((double)statsView.TotalGamesPlayed / pokerTableValuations.ToList()
                                                         .Select(z => z.PokerTableId).Distinct().Count()*100);
                statsView.TotalBuyin = pokerTableValuations.ToList()
                    .Where(x => x.MemberId == member.Id)
                    .Where(y => y.ValueCategoryId == 1)
                    .Select(z => z.Amount).Sum();
                statsView.TotalRebuy = pokerTableValuations.ToList()
                    .Where(x => x.MemberId == member.Id)
                    .Where(z => z.ValueCategoryId == 2)
                    .Select(z => z.Amount).Sum();
                statsView.TotalAddon = pokerTableValuations.ToList()
                    .Where(x => x.MemberId == member.Id)
                    .Where(y => y.ValueCategoryId == 3)
                    .Select(z => z.Amount).Sum();
                statsView.TotalPrizes = pokerTableValuations.ToList()
                    .Where(x => x.MemberId == member.Id)
                    .Where(y => y.ValueCategoryId == 4)
                    .Select(z => z.Amount).Sum();
                statsView.TotalNetAmt = statsView.TotalPrizes - statsView.TotalBuyin - statsView.TotalRebuy - statsView.TotalAddon;
                model.StatsViews.Add(statsView);

            }


            return View(model.StatsViews.OrderByDescending(x => x.TotalNetAmt));
        }
    }
}