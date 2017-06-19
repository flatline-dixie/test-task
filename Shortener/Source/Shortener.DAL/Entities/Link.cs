using System;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Shortener.DAL.Entities
{
    public class Link
    {
        public Int32 Id { get; set; }

        [Required]
        public string OriginalLink { get; set; }

        [Required]
        public Int32 ClickCount { get; set; }
    }
}
