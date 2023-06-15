using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Db;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace web.Controllers;

[AllowAnonymous]
[Route("{controller}/{action}")]
public class ReviewAskController : Controller
{
    private readonly ILogger<ReviewAskController> _logger;
    private readonly MyContext _context;
    private readonly QRPulseConfig _qrpulseConfig;

    public ReviewAskController(
        ILogger<ReviewAskController> logger,
        IOptions<QRPulseConfig> qrpulseConfig,
        MyContext myContext)
    {
        _logger = logger;
        _context = myContext;
        _qrpulseConfig = qrpulseConfig.Value;
    }

    [HttpGet]
    [Route("/tr/{reviewKey}")]
    public IActionResult Index(string reviewKey)
    {
        return RedirectToAction(nameof(Questions), "ReviewAsk", new { id = reviewKey});
    }

    [HttpGet("{reviewKey}")]
    public async Task<ActionResult> Questions(string reviewKey)
    {
        ReviewAskDto? reviewAskDto = await _context.Review
            .Include(i => i.Company)
            .Where(x => x.key == reviewKey)
            .Select(s => new ReviewAskDto(s, s.Company.shortName))
            .FirstOrDefaultAsync();
        if (reviewAskDto == null)
        {
            return BadRequest($"Review {reviewKey} not found");
        }
        return View(reviewAskDto);
    }


    [HttpPost("{reviewKey}")]
    public async Task<ActionResult> Questions(string reviewKey, [FromForm] List<string> result)
    {
        Review? reviewDb = await _context.Review
            .Where(x => x.key == reviewKey)
            .FirstOrDefaultAsync();
        if (reviewDb == null)
        {
            return BadRequest($"Review {reviewKey} not found");
        }
        List<string> ans = JsonConvert.DeserializeObject<List<string>>(result[0]);
        Answer answer = new Answer
        {
            reviewId = reviewDb.id,
            companyId = reviewDb.companyId,
            date = DateTime.Now,
            text1 = ans[0],
            text2 = ans[1],
            text3 = ans[2],
        };
        _context.Add(answer);
        await _context.SaveChangesAsync();

        return Ok(); // RedirectToAction(nameof(ThankYou));
    }


    [HttpGet]
    public ActionResult ThankYou()
    {
        return View();
    }


}
