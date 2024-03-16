using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace AllAboutWeezer.Models
{


    public class Message
    {

        [Key]
        public int MessageId { get; set; }

        public AppUser? From { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(48, ErrorMessage = "Title cannot be longer than 48 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Topic is required.")]
        [StringLength(48, ErrorMessage = "Topic cannot be longer than 48 characters.")]
        public string Topic { get; set; }

        public int YearDate { get; set; }
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Story body is required.")]
        [StringLength(528, ErrorMessage = "Story body cannot be longer than 528 characters.")]
        public string StoryBody { get; set; }
    


    [ForeignKey("OriginalMessageId")]
        public List<Message> Replies { get; set; } = new List<Message>();
        public int? OriginalMessageId { get; set; } = null;
    }
}