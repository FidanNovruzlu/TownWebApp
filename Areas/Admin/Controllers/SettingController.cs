using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TownWebApp.DAL;
using TownWebApp.Models;
using TownWebApp.ViewModels.SettingVM;

namespace TownWebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SettingController : Controller
{

    private readonly TownDbContext _context;
    public SettingController(TownDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        List<Setting> settings = _context.Settings.ToList();
        return View(settings);
    }
    public IActionResult Update(int id)
    {
        Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);
        if (setting == null) return NotFound();

        UpdateSettingVM updateSettingVM = new UpdateSettingVM()
        {
            Value=setting.Value,
        };

        return View(updateSettingVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id,UpdateSettingVM settingVM)
    {
        if (!ModelState.IsValid) return View();

        Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);
        if (setting == null) return NotFound();

        setting.Value = settingVM.Value;
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}

