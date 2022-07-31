using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            //Source -> Target
            //To read data from the model to the DTO
            CreateMap<Platform, PlatformReadDto>();
            //To create data is passed from the DTO to the Model, so as not to expose our Data(Privacy), and also to avoid contract binding
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}