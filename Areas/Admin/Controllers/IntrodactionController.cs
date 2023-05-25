using MessagePack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TownWebApp.DAL;
using TownWebApp.Models;
using TownWebApp.ViewModels;
using TownWebApp.ViewModels.IntrodactionVM;

namespace TownWebApp.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class IntrodactionController : Controller
{
    private readonly TownDbContext _context;
    public IntrodactionController(TownDbContext context)
    {
        _context = context;
    }
    public IActionResult Index(int page = 1, int take=5)
    {
        List<Introdaction> introdactions=_context.Introdactions.Skip((page-1)*take).Take(take).ToList();
        int allPageCount = _context.Introdactions.Count();
        
        PaginationVM<Introdaction> introdactionVM= new PaginationVM<Introdaction>()
        {
            PageCount=(int)(Math.Ceiling((double)allPageCount/take)),
            CurrentPage=page,
            Introdactions=introdactions
        };

        return View(introdactionVM);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]

    public IActionResult Create(CreateIntrodactionVM introdactionVM)
    {
        if (!ModelState.IsValid)
        {
            return View(introdactionVM);
        }

        Introdaction introdaction=new Introdaction()
        {
            IconName=introdactionVM.IconName,
            Title=introdactionVM.Title,
            Description=introdactionVM.Description
        };

        _context.Introdactions.Add(introdaction);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Update(int id)
    {
        Introdaction? introdaction= _context.Introdactions.Find(id);
        if(introdaction==null) return NotFound();

        UpdateIntrodactionVM updateIntrodactionVM = new UpdateIntrodactionVM()
        {
            IconName = introdaction.IconName,
            Title = introdaction.Title,
            Description = introdaction.Description
        };
        return View(updateIntrodactionVM);
    }
    [HttpPost]
    public IActionResult Update(int id,UpdateIntrodactionVM updateIntrodactionVM)
    {
        if (!ModelState.IsValid)
        {
            return View(updateIntrodactionVM);
        }

        Introdaction? introdaction = _context.Introdactions.Find(id);
        if (introdaction == null) return NotFound();

        introdaction.IconName = updateIntrodactionVM.IconName;
       introdaction.Title=updateIntrodactionVM.Title;
        introdaction.Description = updateIntrodactionVM.Description;

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        Introdaction? introdaction = _context.Introdactions.Find(id);
        if (introdaction == null) return NotFound();

        _context.Introdactions.Remove(introdaction);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Read(int id)
    {
        Introdaction? introdaction = _context.Introdactions.Find(id);
        if (introdaction == null) return NotFound();

        return View(introdaction);
    }
}
