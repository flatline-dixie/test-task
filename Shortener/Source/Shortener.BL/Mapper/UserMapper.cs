using TestTask.Common.DAL;
using TestTask.Shortener.DAL.Entities;
using TestTask.Shortener.BE;

namespace TestTask.Shortener.BL.Mapper
{
    public class UserMapper : IMapper<User, ShortenerUser>
    {
        public User Map(ShortenerUser blEntity)
        {
            return new User
            {
                Id = blEntity.UserId,
                ShortenerUserId = blEntity.StorageId
            };
        }

        public ShortenerUser Map(User dbEntity)
        {
            return new ShortenerUser
            {
                UserId = dbEntity.Id,
                StorageId = dbEntity.ShortenerUserId
            };
        }
    }
}
