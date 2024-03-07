using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace AllAboutWeezer.Models
{


    public class Message
    {

        [Key]
        public int MessageId { get; set; }
        public AppUser From { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public int YearDate { get; set; }
        public string StoryBody { get; set; }
       
        public DateOnly Date { get; set; }


        [ForeignKey("OriginalMessageId")]
        public List<Message> Replies { get; set; } = new List<Message>();
        public int? OriginalMessageId { get; set; } = null;
    }
}