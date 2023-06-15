using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Db;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace web.Controllers;

public class AdminReviewPageController : Controller
{
    private readonly ILogger<AdminReviewPageController> _logger;
    private readonly MyContext _context;
    private readonly QRPulseConfig _qrpulseConfig;

    public AdminReviewPageController(
        ILogger<AdminReviewPageController> logger,
        IOptions<QRPulseConfig> qrpulseConfig,
        MyContext myContext)
    {
        _logger = logger;
        _context = myContext;
        _qrpulseConfig = qrpulseConfig.Value;
    }

    public IActionResult Index()
    {
        return Ok("Ok");
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> List()
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

    [HttpGet]
    public async Task<ActionResult> Delete(string id)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;

        Review? reviewDb = await _context.Review
            .IgnoreQueryFilters()
            .Where(x => x.id == id && (userData.sysUser || x.companyId == userData.companyId))
            .FirstOrDefaultAsync();
        if (reviewDb == null)
        {
            return BadRequest($"Review {id} not found");
        }

        _context.Remove(reviewDb);
        await _context.SaveChangesAsync();

        return RedirectToAction("List");
    }


    [HttpGet]
    public async Task<ActionResult> Edit(string id)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;

        ReviewDto? reviewDb = await _context.Review
            .IgnoreQueryFilters()
            .Where(x => x.id == id && (userData.sysUser || x.companyId == userData.companyId))
            .Select(s => new ReviewDto(s, ""))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (reviewDb == null)
        {
            return BadRequest($"Review {id} not found");
        }

        return View(reviewDb);
    }

    [HttpPost("AdminReviewPage/Edit/{reviewId}")]
    public async Task<ActionResult> Edit(ReviewDto review, string reviewId)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;

        Review? reviewDb = await _context.Review
            .IgnoreQueryFilters()
            .Where(x => x.id == review.id && (userData.sysUser || x.companyId == userData.companyId))
            .FirstOrDefaultAsync();
        if (reviewDb == null)
        {
            return BadRequest($"Review {review.id} not found");
        }

        reviewDb.active = review.active ?? false;
        reviewDb.key = review.key;
        reviewDb.question1 = review.question1;
        reviewDb.question2 = review.question2;
        reviewDb.question3 = review.question3;

        _context.Update(reviewDb);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public async Task<ActionResult> NewReview()
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;

        ReviewDto review = new ReviewDto();
        return View(review);
    }


    [HttpPost]
    public async Task<ActionResult> NewReview(ReviewDto review)
    {
        UserData userData = HttpContext.GetUserData();
        ViewData["Loggedin"] = true; ViewData["sysUser"] = userData.sysUser; ViewData["Login"] = userData.login;

        if (!ModelState.IsValid)
        {
            return View(review);
        }

        Review review1 = new Review();
        review1.companyId = userData.companyId;
        review1.question1 = review.question1 ?? "";
        review1.question2 = review.question2 ?? "";
        review1.question3 = review.question3 ?? "";
        review1.description = review.description ?? "";
        review1.key = review.key ?? "";
        review1.active = false;

        _context.Add(review1);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(List)); ;
    }

}

