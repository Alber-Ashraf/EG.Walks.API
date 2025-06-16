using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EG.Walks.Domain.DTOs;
using EG.Walks.Domain.Entities;

namespace EG.Walks.Domain.Mapping
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, CreateRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, CreateWalkRequestDto>().ReverseMap();
        }
    }
}
