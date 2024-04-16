using AutoMapper;
using YellowMark.Contracts.Files;

namespace YellowMark.ComponentRegistrar.MapperProfiles;

/// <summary>
/// File mapping profile.
/// </summary>
public class FileProfile : Profile
{
    /// <summary>
    /// Constructor for file mapping profile.
    /// </summary>
    public FileProfile()
    {
        CreateMap<Domain.Files.Entity.File, FileDto>();

        CreateMap<Domain.Files.Entity.File, FileInfoDto>()
            .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<FileDto, Domain.Files.Entity.File>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Content.Length))
            .ForMember(dest => dest.AdId, opt => opt.MapFrom(src => src.AdId))
            .ForMember(dest => dest.Ad, opt => opt.Ignore());
    }
}
