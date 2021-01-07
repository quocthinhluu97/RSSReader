using AutoMapper;
using RSSReader.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace RSSReader.API
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<SyndicationItem, Post>()
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Title.Text))
                .ForMember(dst => dst.Summary, opt => opt.MapFrom(src => src.Summary.Text))
                .ForMember(dst => dst.Uri, opt => opt.MapFrom(src => src.Links.First().Uri))
                .ForMember(dst => dst.Author, opt => opt.MapFrom(src => src.Authors.First().Email))
                .ForMember(dst => dst.PublishDate, opt => opt.MapFrom(src => src.PublishDate.DateTime));
        }
    }
}
