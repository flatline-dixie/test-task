using TestTask.Common.DAL;
using TestTask.Shortener.DAL.Entities;
using TestTask.Shortener.BE;

namespace TestTask.Shortener.BL.Mapper
{
    public class ShortenerLinkMapper : IMapper<UserLink, ShortenerUserLink>
    {
        public UserLink Map(ShortenerUserLink blEntity)
        {
            var link = new UserLink
            {
                CreateDate = blEntity.CreateDate,
                ClickCount = blEntity.ClickCount,
                UserId = blEntity.UserId,
                Link = new Link
                {
                    OriginalLink = blEntity.OriginalLink
                }
            };
            return link;
        }

        public ShortenerUserLink Map(UserLink dbEntity)
        {
            var link = new ShortenerUserLink
            {
                UserId = dbEntity.UserId,
                ClickCount = dbEntity.ClickCount,
                CreateDate = dbEntity.CreateDate,
                ShortLink = dbEntity.ShortLink
            };

            if (dbEntity.Link != null)
            {
                link.OriginalLink = dbEntity.Link.OriginalLink;
            }

            return link;
        }
    }
}
