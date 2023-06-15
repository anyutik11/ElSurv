using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Db;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace web.Controllers;

public class AdminResultPageV2Controller : Controller
{
    private readonly ILogger<AdminResultPageV2Controller> _logger;
    private readonly MyContext _context;
    private readonly QRPulseConfig _qrpulseConfig;

    public AdminResultPageV2Controller(
        ILogger<AdminResultPageV2Controller> logger,
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
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;

        List<ReviewDto> reviews = await _context.Review
            .IgnoreQueryFilters()
            .Include(i => i.Company)
            .Where(x => userData.sysUser || x.companyId == userData.companyId)
            .Select(s => new ReviewDto(s, s.Company.shortName))
            .AsNoTracking()
            .ToListAsync();
        return View(reviews);
    }

    [HttpGet("AdminResultPageV2/Answers/{reviewId}")]
    public async Task<ActionResult> Answers(string reviewId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;

        Review? reviewDb = await _context.Review
            .Where(x => x.id == reviewId && (userData.sysUser || x.companyId == userData.companyId))
            .AsNoTracking()
            .FirstOrDefaultAsync();

        List<Answer> answersDb = await _context.Answer
            .Where(x => x.reviewId == reviewId && (userData.sysUser || x.companyId == userData.companyId))
            .AsNoTracking()
            .ToListAsync();

        AnswersDto answersDto = new AnswersDto();
        answersDto.review = reviewDb ?? new Review();
        answersDto.answers = answersDb;

        return View(answersDto);
    }


    /*
    [HttpGet]
    public async Task<ActionResult> Edit(string id)
    {
        string companyId = _qrpulseConfig.SysCompanyId;

        Review? reviewDb = await _context.Review
            .Where(x => x.id == id && x.companyId == companyId)
            .FirstOrDefaultAsync();
        if (reviewDb == null)
        {
            return BadRequest($"User {id} not found");
        }

        return View(reviewDb);
    }

    [HttpPost("AdminResultPageV2/Edit/{reviewId}")]
    public async Task<ActionResult> Edit(Review review, string reviewId)
    {
        string companyId = _qrpulseConfig.SysCompanyId;

        Review? reviewDb = await _context.Review
            .Where(x => x.id == review.id && x.companyId == companyId)
            .FirstOrDefaultAsync();
        if (reviewDb == null)
        {
            return BadRequest($"User {review.id} not found");
        }

        reviewDb.description = review.description;


        _context.Update(reviewDb);
        await _context.SaveChangesAsync();

        return View(reviewDb);
    }
    */
}
