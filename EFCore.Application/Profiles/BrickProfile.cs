using AutoMapper;
using EFCore.Application.Dtos;
using EFCore.Application.Models;
using EFCore.Application.Models.Bricks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Profiles
{
    public class BrickProfile : Profile
    {
        public BrickProfile()
        {
            CreateMap<BrickAvailability, BrickAvailabilityDto>().ReverseMap();
        }
    }
}
