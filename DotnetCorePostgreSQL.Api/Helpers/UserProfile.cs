using AutoMapper;
using DotnetCorePostgreSQL.Api.Data.DTO;
using DotnetCorePostgreSQL.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCorePostgreSQL.Api.Helpers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Post, PostDto>();
        }
    }
}
