using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShinobiRanks1._0.Data;

namespace ShinobiRanks1._0.Pages.hvgenin
{
    public class IndexModel : PageModel
    {
        private readonly ShinobiRanks1._0.Data.ApplicationDbContext _context;

        public IndexModel(ShinobiRanks1._0.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<HVGenin> HVGenin { get;set; }

        public async Task OnGetAsync()
        {
            var vGenins = from m in _context.HVGenin select m;

            if (User.IsInRole("kage"))
            {   
                vGenins = vGenins.Where(s => s.GeninName.Contains("S"));    
            }else if (User.IsInRole("jounin"))
            {
                vGenins = vGenins.Where(s => s.GeninCNature.Contains("Wind"));
            }else if (User.IsInRole("chuunin"))
            {
                vGenins = vGenins.Where(s => s.GeninTeam.Contains("Team 7"));
            }
            else
            {
                vGenins = vGenins.Where(s => s.GeninName.Contains(" "));
            }


            HVGenin = await vGenins.ToListAsync();
        }
    }
}
