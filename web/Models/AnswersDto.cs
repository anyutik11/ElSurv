using web.Db;

namespace web.Models;

public class AnswersDto
{
    public Review review { get; set; }
    public List<Answer> answers { get; set; }

}
