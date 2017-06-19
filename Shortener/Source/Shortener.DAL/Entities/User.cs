using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TestTask.Shortener.DAL.Entities
{
    public class User
    {
        public Int32 Id { get; set; }

        [Index(IsUnique = true)]
        public Guid ShortenerUserId { get; set; }

        public virtual List<UserLink> Links { get; set; }
    }
}
