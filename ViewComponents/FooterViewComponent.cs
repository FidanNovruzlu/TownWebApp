using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TownWebApp.DAL;
using TownWebApp.Models;

namespace TownWebApp.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly TownDbContext _context;
        public FooterViewComponent(TownDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, Setting> setting = await _context.Settings.ToDictionaryAsync(k => k.Key);
            return View(setting);
        }
    }
}
