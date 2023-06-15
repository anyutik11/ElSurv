using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Db;
using web.Models;


namespace web.Controllers;

[Authorize]
[Route("{Controller}/{Action}")]
public class CompanyPageController : Controller
{
    private readonly ILogger<CompanyPageController> _logger;
    private readonly MyContext _context;

    public CompanyPageController(
        ILogger<CompanyPageController> logger,
        MyContext myContext)
    {
        _logger = logger;
        _context = myContext;
    }


    [HttpGet]
    public async Task<ActionResult> List()
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        List<Company> companies = await _context.Company
            .IgnoreQueryFilters()
            .Where(x => userData.sysUser || x.id == userData.companyId)
            .AsNoTracking()
            .ToListAsync();

        return View(companies);
    }

    [HttpGet("{companyId}")]
    public async Task<ActionResult> Block(string companyId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        Company? company = await _context.Company
            .IgnoreQueryFilters()
            .Where(x => x.id == companyId && (userData.sysUser || x.id == userData.companyId))
            .FirstOrDefaultAsync();
        company.deleted = true;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(List));
    }

    [HttpGet("{companyId}")]
    public async Task<ActionResult> Unblock(string companyId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        Company? company = await _context.Company
            .IgnoreQueryFilters()
            .Where(x => x.id == companyId && (userData.sysUser || x.id == userData.companyId))
            .FirstOrDefaultAsync();
        company.deleted = false;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(List));
    }

    [HttpGet("{companyId}")]
    public async Task<ActionResult> Delete(string companyId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        Company? company = await _context.Company
            .IgnoreQueryFilters()
            .Where(x => x.id == companyId && (userData.sysUser || x.id == userData.companyId))
            .FirstOrDefaultAsync();
        _context.Company.Remove(company);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public async Task<ActionResult> Edit(string companyId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;

        CompanyDto? company = await _context.Company
            .IgnoreQueryFilters()
            .Where(x => x.id == companyId && (userData.sysUser || x.id == userData.companyId))
            .Select(s => new CompanyDto(s))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        
        return View(company);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(string companyId, CompanyDto companyDto)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        if (!ModelState.IsValid)
        {
            return View();
        }

        Company? company = await _context.Company
            .IgnoreQueryFilters()
            .Where(x => x.id == companyDto.id)
            .FirstOrDefaultAsync();

        companyDto.Update(company);
        _context.Company.Update(company);        
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List)); ;
    }

    [HttpGet]
    public ActionResult AddCompany()
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        CompanyDto company = new CompanyDto();
        return View("Edit", company);
    }

    [HttpPost]
    public async Task<ActionResult> AddCompany(CompanyDto companyDto)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        if (!ModelState.IsValid)
        {
            return View("Edit");
        }

        Company company = new Company();
        companyDto.Update(company);
        _context.Add(company);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List)); ;
    }


}
