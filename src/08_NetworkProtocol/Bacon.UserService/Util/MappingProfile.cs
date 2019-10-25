using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Bacon.ServiceDefine1.Dto;
using Bacon.ServiceDefine1.Entity;

namespace Bacon.Service1.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, UserEntity>();
            CreateMap<UserEntity, UserDto>();
        }
    }
}
