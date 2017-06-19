using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Shortener.DAL.Entities
{
    public class UserLink
    {
        [Key, Column(Order = 0)]
        public Int32 UserId { get; set; }

        public virtual User User { get; set; }

        [Key, Column(Order = 1)]
        public Int32 LinkId { get; set; }

        public virtual Link Link { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public Int32 ClickCount { get; set; }

        [Required]
        [Index]
        [StringLength(200)]
        public string ShortLink { get; set; }
    }
}
