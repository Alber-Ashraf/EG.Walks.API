using AutoMapper;
using EG.Walks.Contracts.Responses;
using EG.Walks.Domain.DTOs.Requests;
using EG.Walks.Domain.DTOs.Responses;
using EG.Walks.Domain.Entities;

namespace EG.Walks.Domain.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionResponseDto>().ReverseMap();
            CreateMap<Region, CreateRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();
            CreateMap<Walk, WalkResponseDto>().ReverseMap();
            CreateMap<Walk, CreateWalkRequestDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyResponseDto>().ReverseMap();
            CreateMap<Image, ImageUploadResponseDto>().ReverseMap();
        }
    }
}
