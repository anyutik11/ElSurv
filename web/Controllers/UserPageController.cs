using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Db;
using web.Models;

namespace web.Controllers;

[Authorize]
[Route("{Controller}/{Action}")]
public class UserPageController : Controller
{
    private readonly ILogger<UserPageController> _logger;
    private readonly MyContext _context;

    public UserPageController(
        ILogger<UserPageController> logger,
        MyContext myContext)
    {
        _logger = logger;
        _context = myContext;
    }

    [HttpGet("{companyId}")]
    public async Task<ActionResult> List(string companyId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        string? companyName = await _context.Company
            .Where(x => x.id == companyId)
            .Select(s => s.shortName)
            .FirstOrDefaultAsync();

        List<User> users = await _context.User
            .IgnoreQueryFilters()
            .Where(x => userData.sysUser || x.id == userData.companyId)
            .AsNoTracking()
            .ToListAsync();

        ViewData["companyName"] = companyName;

        return View(users);
    }

    [HttpGet("{companyId}")]
    public async Task<ActionResult> Block(string companyId, string userId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        User? user = await _context.User
            .IgnoreQueryFilters()
            .Where(x => x.id == userId && x.companyId == companyId && (userData.sysUser || x.companyId == userData.companyId))
            .FirstOrDefaultAsync();
        user.deleted = true;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(List), new {companyId});
    }

    [HttpGet("{companyId}")]
    public async Task<ActionResult> Unblock(string companyId, string userId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        User? user = await _context.User
            .IgnoreQueryFilters()
            .Where(x => x.id == userId && x.companyId == companyId && (userData.sysUser || x.companyId == userData.companyId))
            .FirstOrDefaultAsync();
        user.deleted = false;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(List), new { companyId });
    }

    [HttpGet]
    public async Task<ActionResult> Delete(string userId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        User? user = await _context.User
            .IgnoreQueryFilters()
            .Where(x => x.id == userId && (userData.sysUser || x.companyId == userData.companyId))
            .FirstOrDefaultAsync();
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(List), new { user.companyId });
    }


    [HttpGet]
    public async Task<ActionResult> Edit(string userId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        ViewData["SubTitle"] = "Edit User";
        UserDto? userDto = await _context.User
            .IgnoreQueryFilters()
            .Where(x => x.id == userId && (userData.sysUser || x.companyId == userData.companyId))
            .Select(s => new UserDto(s))
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return View(userDto);
    }

    [HttpPost]
    public async Task<ActionResult> Edit([FromQuery] string userId, [FromForm] UserDto userDto)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        if (!ModelState.IsValid)
        {
            return View();
        }

        User? user = await _context.User
            .IgnoreQueryFilters()
            .Where(x => x.id == userDto.id && (userData.sysUser || x.companyId == userData.companyId))
            .FirstOrDefaultAsync();

        userDto.Update(user);
        _context.User.Update(user);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List), new { user.companyId }); ;
    }





    [HttpGet("{companyId}")]
    public ActionResult AddUser(string companyId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        ViewData["SubTitle"] = "Add User";
        UserDto user = new UserDto();       
        return View("Edit", user);
    }

    [HttpPost("{companyId}")]
    public async Task<ActionResult> AddUser(string companyId, UserDto userDto)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;
        if (!userData.sysUser) return BadRequest();

        if (!ModelState.IsValid)
        {
            return View("Edit");
        }

        User user = new User();
        userDto.Update(user);
        user.companyId = companyId;
        user.salt = Nanoid.Nanoid.Generate(size: 31);
        user.hash = Utils.ComputePasswordSHA256(userDto.password, user.salt);
        _context.Add(user);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List), new { user.companyId }); ;
    }

}
