using TestTask.Shortener.BE;
using System;
using System.Linq;
using TestTask.Common.DAL;
using TestTask.Shortener.DAL;
using TestTask.Shortener.DAL.Entities;
using System.Threading.Tasks;

namespace TestTask.Shortener.BL
{
    public interface IShortenerBL
    {
        Task<ShortenerUser> GetUser(Guid shortenerUserId);

        String GetOriginalLink(string shortLink);

        Task<ShortenerUserLink> GetShortenerUserLink(Guid shortenerUserId, string originalLink);

        Task<IPagedList<ShortenerUserLink>> GetUserLinks(Guid shortenerUserId, int page, int pageSize);

        Task ClickInc(string shortLink);
    }

    public class ShortenerBL : IShortenerBL
    {
        protected readonly ICommonRepository<ShortenerContext, User, Guid> UserRepository;

        protected readonly ICommonRepository<ShortenerContext, Link, Guid> LinkRepository;

        protected readonly ICommonRepository<ShortenerContext, UserLink, Guid> UserLinkRepository;

        protected readonly IMapper<User, ShortenerUser> UserMapper;

        protected readonly IMapper<UserLink, ShortenerUserLink> ShortenerUserMapper;

        public ShortenerBL(
            ICommonRepository<ShortenerContext, User, Guid> userRepository,
            ICommonRepository<ShortenerContext, Link, Guid> linkRepository,
            ICommonRepository<ShortenerContext, UserLink, Guid> userLinkRepository,
            IMapper<User, ShortenerUser> userMapper,
            IMapper<UserLink, ShortenerUserLink> shortenerUserMapper)
        {
            UserRepository = userRepository;
            LinkRepository = linkRepository;
            UserLinkRepository = userLinkRepository;
            UserMapper = userMapper;
            ShortenerUserMapper = shortenerUserMapper;
        }

        public async Task<ShortenerUser> GetUser(Guid shortenerUserId)
        {
            var user = await GetUserByShortenerUserId(shortenerUserId);
            return UserMapper.Map(user);
        }

        private async Task<User> GetUserByShortenerUserId(Guid shortenerUserId)
        {
            User user;
            user = UserRepository.Get(x => x.ShortenerUserId.Equals(shortenerUserId)).SingleOrDefault();

            if (user == null)
            {
                user = new User { ShortenerUserId = shortenerUserId };
                UserRepository.AddOrUpdate(user);
                await UserRepository.SaveChangesAsync();
            }
            return user;
        }

        public String GetOriginalLink(string shortLink)
        {
            var link = UserLinkRepository.Get(x => x.ShortLink.Equals(shortLink)).SingleOrDefault();
            if (link == null || string.IsNullOrEmpty(link.Link.OriginalLink))
                return null;
            return link.Link.OriginalLink;
        }

        public async Task<ShortenerUserLink> GetShortenerUserLink(Guid shortenerUserId, string originalLink)
        {
            var user = await GetUserByShortenerUserId(shortenerUserId);

            int nextVal = GetNextSiquenceValue();

            Link link = null;
            link = LinkRepository.Get(x => x.OriginalLink.Equals(originalLink)).SingleOrDefault();

            if (link == null)
            {
                link = new Link { OriginalLink = originalLink, ClickCount = 0};
                LinkRepository.Insert(link);
                await LinkRepository.SaveChangesAsync();

            }

            UserLink uLink = null;
            uLink = UserLinkRepository.Get(
                x => x.UserId.Equals(user.Id) && x.LinkId.Equals(link.Id)
                ).SingleOrDefault();

            if (uLink == null)
            {
                var shortLink = StringShortener.Encode(nextVal);

                uLink = new UserLink
                {
                    LinkId = link.Id,
                    CreateDate = DateTime.Now,
                    ClickCount = 0,
                    UserId = user.Id,
                    ShortLink = shortLink
                };

                UserLinkRepository.Insert(uLink);
            }
            await UserLinkRepository.SaveChangesAsync();
            uLink.User = user;
            uLink.Link = link;
            return ShortenerUserMapper.Map(uLink);
        }

        public async Task<IPagedList<ShortenerUserLink>> GetUserLinks(Guid shortenerUserId, int page, int pageSize)
        {
            var user = GetUser(shortenerUserId).Result;

            var links = UserLinkRepository.Get(x => x.UserId.Equals(user.UserId));

            return (await links.OrderByDescending(x => x.CreateDate).ToPagedListAsync(pageSize, page))
                .SelectFromPageList(x => ShortenerUserMapper.Map(x));

        }

        public async Task ClickInc(string shortLink)
        {
            if (string.IsNullOrEmpty(shortLink))
                return;

            var userLink = UserLinkRepository.Get(x => x.ShortLink.Equals(shortLink)).FirstOrDefault();
            if (userLink == null)
                return;

            userLink.ClickCount++;
            userLink.Link.ClickCount++;
            UserLinkRepository.Update(userLink);
            await UserLinkRepository.SaveChangesAsync();
        }

        private int GetNextSiquenceValue()
        {
            var query =
                LinkRepository.RepositoryContext.Database.
                SqlQuery<Int64>("SELECT NEXT VALUE FOR dbo.SQNLink;");
            var task = query.SingleAsync();
            Int32 nextVal = (Int32)task.Result;
            return nextVal;
        }
    }
}

