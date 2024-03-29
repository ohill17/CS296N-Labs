﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace AllAboutWeezer.Models
{


    public class Message : IdentityUser
    {
       
        public int MessageId { get; set; }
        public AppUser From { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public int YearDate { get; set; }
        public string StoryBody { get; set; }
       
        public DateOnly Date { get; set; }
    }
}