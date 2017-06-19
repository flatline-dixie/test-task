using System;

namespace Shortener.DAL.Entities
{
    public class Link
    {
        public Int32 Id { get; set; }
        public string OriginalLink { get; set; }
        public string ShortLink { get; set; }
    }
}
