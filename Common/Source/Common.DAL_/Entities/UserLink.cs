using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.DAL.Entities
{
    public class UserLink
    {
        public Int32 UserId { get; set; }
        public virtual User User { get; set; }

        public Int32 LinkId { get; set; }
        public virtual Link Link { get; set; }

        public DateTime CreateDate { get; set; }
        public Int32 ClickCount { get; set; }
    }
}
