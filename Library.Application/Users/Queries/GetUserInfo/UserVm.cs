using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain.Entities;

namespace KDrom.Application.Users.Queries.GetUserInfo
{
    public class UserVm : IMapWith<User>
    {
        public string Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserVm>();
        }
    }
}
