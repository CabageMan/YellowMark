using AutoMapper;
using YellowMark.Contracts.Comments;
using YellowMark.Domain.Comments.Entity;

namespace YellowMark.ComponentRegistrar.MapperProfiles;

/// <summary>
/// Comment mapping profile.
/// </summary>
public class CommentProfile : Profile
{
    /// <summary>
    /// Constructor for comment mapping profile.
    /// </summary>
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.IsEdited, opt => opt.MapFrom(src => src.CreatedAt != src.UpdatedAt))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.UserInfo.FirstName))
            .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.UserInfo.LastName))
            .ForMember(dest => dest.AdId, opt => opt.MapFrom(src => src.Ad.Id))
            .ForMember(dest => dest.AdTitle, opt => opt.MapFrom(src => src.Ad.Title));

        var creationDate = DateTime.UtcNow;
        CreateMap<CreateCommentRequest, Comment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => creationDate))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => creationDate))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserInfo, opt => opt.Ignore())
            .ForMember(dest => dest.AdId, opt => opt.MapFrom(src => src.AdId))
            .ForMember(dest => dest.Ad, opt => opt.Ignore());

        CreateMap<UpdateCommentRequest, Comment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserInfo, opt => opt.Ignore())
            .ForMember(dest => dest.AdId, opt => opt.Ignore())
            .ForMember(dest => dest.Ad, opt => opt.Ignore());
    }
}
