using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Shortener.BE
{
    public class ShortenerUserLink
    {
        public Int32 UserId { get; set; }
        public String OriginalLink { get; set; }
        public String ShortLink { get; set; }
        public Int32 ClickCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
