using System;
using System.ComponentModel.Design;
using web.Db;

namespace web.Models
{
	public class ReviewAskDto
	{
        public string? companyName { get; set; }
        public string? question1 { get; set; }
        public string? question2 { get; set; }
        public string? question3 { get; set; }
        public string? answer1 { get; set; }
        public string? answer2 { get; set; }
        public string? answer3 { get; set; }
        public string? reviewId { get; set; }

        public ReviewAskDto()
        {            
        }

        public ReviewAskDto(Review review, string compName)
        {
            companyName = compName;
            question1 = review.question1;
            question2 = review.question2;
            question3 = review.question3;            
        }

        /*
        public void Update(Answer answer, string reviewId, string companyId, string? guestId = null)
        {
            answer.text1 = answer1;
            answer.text2 = answer2;
            answer.text3 = answer3;
            answer.reviewId = reviewId;
            answer.companyId = companyId;

            answer.phone = "";
            answer.guestId = guestId;
        }
        */
    }
}

