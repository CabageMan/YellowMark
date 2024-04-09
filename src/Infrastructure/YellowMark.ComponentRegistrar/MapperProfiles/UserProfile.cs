using AutoMapper;
using YellowMark.Contracts.Users;
using YellowMark.Domain.Users.Entity;

namespace YellowMark.ComponentRegistrar;

/// <summary>
/// User mapping profile.
/// </summary>
public class UserProfile : Profile
{
    /// <summary>
    /// Constructor for user mapping profile.
    /// </summary>
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(s => s.FullName, map => map.MapFrom(s => 
                $"{s.LastName} {s.MiddleName} {s.FirstName}"
            ));

        CreateMap<CreateUserRequest, User>()
            .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
            .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
            .ForMember(s => s.UpdatedAt, map => map.MapFrom(s => DateTime.UtcNow))
            .ForMember(s => s.Ads, map => map.Ignore())
            .ForMember(s => s.Comments, map => map.Ignore());
    }
}
