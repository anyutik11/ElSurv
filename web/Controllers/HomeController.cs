using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using System.Diagnostics;
using web.Db;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Cryptography;
using Serilog;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

namespace web.Controllers;

public class HomeController : Controller
{    
    private readonly ILogger<HomeController> _logger;
    private readonly MyContext _context;
    private readonly QRPulseConfig _qrpulseConfig;

    public HomeController(
        ILogger<HomeController> logger,
        IOptions<QRPulseConfig> qrpulseConfig,
        MyContext myContext)
    {
        _logger = logger;
        _context = myContext;
        _qrpulseConfig = qrpulseConfig.Value;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }


    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        LoginModel loginDto = new LoginModel();
        ViewData["Loggedin"] = false;
        return View(loginDto);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginDto)
    {
        loginDto.loginFailed = false;
        ViewData["Loggedin"] = false;
        if (!ModelState.IsValid)
        {               
            return View(loginDto);
        }

        User? userDb = await _context.User
            .Where(x => x.login == loginDto.Login)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (userDb == null)
        {
            loginDto.loginFailed = true;
            return View(loginDto);
        }

        if (Utils.ComputePasswordSHA256(loginDto.Password ?? "", userDb.salt) != userDb.hash)
        {
            loginDto.loginFailed = true;
            return View(loginDto);
        }
        ViewData["Loggedin"] = true;
        ViewData["Login"] = loginDto.Login;

        var claims = new List<Claim> { 
            new Claim(ClaimTypes.Name, userDb.name ?? ""),
            new Claim(ClaimTypes.Email, loginDto.Login ?? ""),
            new Claim("userId", userDb.id),
            new Claim("sysUser", (userDb.login == _qrpulseConfig.SysAdminLogin).ToString() ),
            new Claim("companyId", userDb.companyId),
        };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        await Task.Delay(1000);
        return RedirectToAction("List", "CompanyPage");     
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        ViewData["Loggedin"] = false;
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }



    public async Task<IActionResult> TestDb()
    {
        //List<Company> companies = await _context.Company.ToListAsync();
        await _context.Database.OpenConnectionAsync();
        await _context.Database.CloseConnectionAsync();
        return View();
    }

    // /home/initRaw?key=jghfY8678fgGFc
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> InitRaw([FromQuery] string key)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser;

        if (userData?.sysUser == true || key == "jghfY8678fgGFc") 
        {
            await web.Models.Init.WriteDb(_context, _qrpulseConfig);
        }
        else
        {
            return RedirectToAction("Login");
        }        
        return View("Init");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Init()
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser;

        if (!(userData?.sysUser==true))
        {
            return RedirectToAction("Login");
        }
        return View("Init");
    }



    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Init2()
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser;

        if (!userData.sysUser)
        {
            return Ok("SysUser only");
        }

        try
        {
            await web.Models.Init.WriteDb(_context, _qrpulseConfig);
        }
        catch (Exception ex)
        {
            Log.Error($"Возникло исключение: {ex.Message} {ex.InnerException?.Message}");
            return Ok($"Возникло исключение: {ex.Message} {ex.InnerException?.Message}");
        }

        await Task.Delay(6000);
        return View("InitResult");
    }







    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }






    
}