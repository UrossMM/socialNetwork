using AutoMapper;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models.Profiles
{
    public class PostProfile : Profile
    {

        public PostProfile()
        {
            CreateMap<Post, PostDTO>().ReverseMap();
        }
    }
}
