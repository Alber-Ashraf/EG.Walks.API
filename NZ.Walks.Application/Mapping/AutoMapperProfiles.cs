using AutoMapper;
using EG.Walks.Domain.DTOs.Requests;
using EG.Walks.Domain.DTOs.Responses;
using EG.Walks.Domain.Entities;

namespace EG.Walks.Domain.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDtoResponse>().ReverseMap();
            CreateMap<Region, CreateRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();
            CreateMap<Walk, WalkDtoResponse>().ReverseMap();
            CreateMap<Walk, CreateWalkRequestDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDtoResponse>().ReverseMap();
        }
    }
}
