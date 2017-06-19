using System;
using System.Collections.Generic;

namespace Shortener.DAL.Entities
{
    public class User
    {
        public Int32 Id { get; set; }
        public Guid ShortenerUserId { get; set; }
        public virtual List<UserLink> Links{get; set; }
    }
}
