using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using web.Db;
using web.Models;

namespace web.Controllers;

public class TakeReviewPageController : Controller
{
    private readonly ILogger<TakeReviewPageController> _logger;
    private readonly MyContext _context;

    public TakeReviewPageController(
        ILogger<TakeReviewPageController> logger,
        MyContext myContext)
    {
        _logger = logger;
        _context = myContext;
    }


    [HttpGet]
    public async Task<ActionResult> List()
    {
        List<ReviewDto> reviews = await _context.Review
            .Include(i => i.Company)
            .Where(x => x.deleted == false)
            .Select(s => new ReviewDto(s, s.Company.shortName))
            .AsNoTracking()
            .ToListAsync();
        return View(reviews);
    }

}
