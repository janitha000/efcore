using AutoMapper;
using EFCore.Application.Dtos;
using EFCore.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.FullName, source => source.MapFrom(source => source.FirstName + " " + source.LastName))
                .ReverseMap();
                
        }
    }
}
