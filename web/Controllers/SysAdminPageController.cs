using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Db;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using System.Text;
using System.Security.Cryptography;

namespace web.Controllers;

public class SysAdminPageController : Controller
{
    private readonly ILogger<SysAdminPageController> _logger;
    private readonly MyContext _context;
    private readonly QRPulseConfig _qrpulseConfig;

    public SysAdminPageController(
        ILogger<SysAdminPageController> logger,
        IOptions<QRPulseConfig> qrpulseConfig,
        MyContext myContext)
    {
        _logger = logger;
        _context = myContext;
        _qrpulseConfig = qrpulseConfig.Value;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> List()
    {           
        var claims = HttpContext.User.Claims;
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser;

        List <User> users = await _context.User
            .Where(x => x.companyId == userData.companyId)
            .ToListAsync();
        return View(users);
    }

    [HttpGet]
    public async Task<ActionResult> Delete(string id)
    {
        string companyId = _qrpulseConfig.SysCompanyId;

        User? userDb = await _context.User
            .Where(x => x.id == id && x.companyId == companyId)
            .FirstOrDefaultAsync();
        if (userDb == null)
        {
            return BadRequest($"User {id} not found");
        }

        _context.Remove(userDb);
        await _context.SaveChangesAsync();

        return RedirectToAction("SysAdminPage");
    }


    [HttpGet]
    public async Task<ActionResult> Edit(string id)
    {
        string companyId = _qrpulseConfig.SysCompanyId;

        User? userDb = await _context.User
            .Where(x => x.id == id && x.companyId == companyId)
            .FirstOrDefaultAsync();
        if (userDb == null)
        {
            return BadRequest($"User {id} not found");
        }

        return View(userDb);
    }

    [HttpPost("SysAdminPage/Edit/{userId}")]
    public async Task<ActionResult> Edit(User user, string userId)
    {
        string companyId = _qrpulseConfig.SysCompanyId;

        User? userDb = await _context.User
            .Where(x => x.id == user.id && x.companyId == companyId)
            .FirstOrDefaultAsync();
        if (userDb == null)
        {
            return BadRequest($"User {user.id} not found");
        }

        userDb.login = user.login;
        userDb.surname = user.surname;
        userDb.name = user.name;
        userDb.contactEmail = user.contactEmail;
        userDb.contactPhone = user.contactPhone;
        userDb.dateBirth = user.dateBirth;
        userDb.gender = user.gender;


        _context.Update(userDb);
        await _context.SaveChangesAsync();

        return View(userDb);
    }

    [HttpGet]
    public async Task<ActionResult> NewUser(string id)
    {
        NewUser user = new NewUser();

        return View(user);
    }


    [HttpPost]
    public async Task<ActionResult> NewUser(NewUser user)
    {
        string companyId = _qrpulseConfig.SysCompanyId;

        User user1 = new User();

        user1.gender = user.gender;
        user1.login = user.login;
        user1.name = user.name;
        user1.contactEmail = user.contactEmail;
        user1.contactPhone = user.contactPhone;
        user1.companyId = user.companyId;
        user1.surname = user.surname ?? "";
        user1.dateBirth = user.dateBirth;

        user1.salt = Nanoid.Nanoid.Generate(size: 31);
        var password = user.password;

        user1.hash = ComputePasswordSHA256(password, user1.salt);



//password salt??????????


        _context.Add(user1);
        await _context.SaveChangesAsync();


        return View(user);
    }


    protected string ComputePasswordSHA256(string data, string salt)
    {
        if (string.IsNullOrEmpty(data)) return "";
        try
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Error("Ошибка вычисления SHA256.", ex.Message);
        }
        return "";
    }
}

