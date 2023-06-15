using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Db;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;
using web.Migrations;

namespace web.Controllers;

[Route("{controller}/{action}")]
public class MobileApiController : Controller
{
    private readonly ILogger<MobileApiController> _logger;
    private readonly MyContext _context;
    private readonly QRPulseConfig _qrpulseConfig;

    public MobileApiController(
        ILogger<MobileApiController> logger,
        IOptions<QRPulseConfig> qrpulseConfig,
        MyContext myContext)
    {
        _logger = logger;
        _context = myContext;
        _qrpulseConfig = qrpulseConfig.Value;
    }

    // https://localhost:44324/MobileApi/CompanyList
    [EnableCors(PolicyName = "AllowCors")] 
    [HttpGet]
    public async Task<ActionResult> CompanyList(string key)
    {
     
        List<Company> companies = await _context.Company
            .AsNoTracking()
            .ToListAsync();

        return new JsonResult(companies);
    }

    // https://localhost:44324/MobileApi/ReviewList
    [EnableCors(PolicyName = "AllowCors")]
    [HttpGet]
    [Route("{companyId}")]
    public async Task<ActionResult> ReviewList(string key, string companyId)
    {

        List<Review> reviews = await _context.Review
            .Where(x => x.companyId == companyId)
            .AsNoTracking()
            .ToListAsync();

        return new JsonResult(reviews);
    }


    [EnableCors(PolicyName = "AllowCors")]
    [HttpPost]
    [Route("{reviewId}")]
    public async Task<ActionResult> postResult(string key, string reviewId, 
        [FromBody] List<string> answers)
    {

        Review? review = await _context.Review
            .Where(x => x.id == reviewId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (review == null) return BadRequest("Not found");

        Answer answer = new Answer
        {
            companyId = review.companyId,
            reviewId = reviewId,
            date = DateTime.Now,
            text1 = answers[0],
            text2 = answers[1],
            text3 = answers[2],

        };

        _context.Answer.Add(answer);
        await _context.SaveChangesAsync();

        return Ok();
    }

    // https://localhost:44324/MobileApi/BonuceList
    [EnableCors(PolicyName = "AllowCors")]
    [HttpGet]
    [Route("{guestId}")]
    public async Task<ActionResult> BonuceList(string key, string guestId)
    {

        List<Bonuce> bonuces = await _context.Bonuce
            .Where(x => x.guestId == guestId)
            .AsNoTracking()
            .ToListAsync();

        return new JsonResult(bonuces);
    }


    // https://localhost:44324/MobileApi/Bot 
    [EnableCors(PolicyName = "AllowCors")]
    [HttpGet]
    public async Task<ActionResult> Bot(string key)
    {
        return View();
    }


    [EnableCors(PolicyName = "AllowCors")]
    [HttpPost]    
    public async Task<ActionResult> login(string key, [FromBody] List<string> dto)
    {
        Guest? guest = await _context.Guest
            .Where(x => x.login == dto[0])
            .FirstOrDefaultAsync();
        if (guest == null) return NotFound();

        if (Utils.ComputePasswordSHA256(dto[1], guest.salt) != guest.hash)
            return BadRequest();

        guest.salt = "";
        guest.hash = "";
        return new JsonResult(guest);
    }
}

