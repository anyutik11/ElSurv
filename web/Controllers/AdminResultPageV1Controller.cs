using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Db;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace web.Controllers;

public class AdminResultPageV1Controller : Controller
{
    private readonly ILogger<AdminResultPageV1Controller> _logger;
    private readonly MyContext _context;
    private readonly QRPulseConfig _qrpulseConfig;

    public AdminResultPageV1Controller(
        ILogger<AdminResultPageV1Controller> logger,
        IOptions<QRPulseConfig> qrpulseConfig,
        MyContext myContext)
    {
        _logger = logger;
        _context = myContext;
        _qrpulseConfig = qrpulseConfig.Value;
    }


    [Authorize]
    [HttpGet]
    public async Task<ActionResult> ListOfAllQuestions()
    {
        string companyId = _qrpulseConfig.SysCompanyId;
        UserData userData = HttpContext.GetUserData();

        List<Review> reviews = await _context.Review
            .Where(x => x.companyId == userData.companyId)
            .ToListAsync();
        return View(reviews);
    }

    [HttpGet("AdminResultPageV1/Answer1/{reviewId}")]
    public async Task<ActionResult> Answer1(string reviewId)
    {
        List<Answer> answers1 = await _context.Answer
           .Where(x => x.reviewId == reviewId)
           .ToListAsync();
        return View(answers1);
    }

    [HttpGet("AdminResultPageV1/Answer2/{reviewId}")]
    public async Task<ActionResult> Answer2(string reviewId)
    {
        List<Answer> answers2 = await _context.Answer
           .Where(x => x.reviewId == reviewId)
           .ToListAsync();


        return View(answers2);
    }

    [HttpGet("AdminResultPageV1/Answer3/{reviewId}")]
    public async Task<ActionResult> Answer3(string reviewId)
    {

        List<Answer> answers3 = await _context.Answer
           .Where(x => x.reviewId == reviewId)
           .ToListAsync();
        return View(answers3);
    }

}
