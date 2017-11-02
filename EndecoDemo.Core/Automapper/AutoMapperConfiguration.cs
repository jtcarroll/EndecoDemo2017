using AutoMapper;
using EndecoDemo.DAL;
using EndecoDemo.Models.Member;
using EndecoDemo.Models.Stock;
using EndecoDemo.Models.ViewModels;
using EndecoDemo.Services.Automapper.Profiles;
using System;

namespace EndecoDemo.Services.Automapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            // add profiles one time
            Mapper.Initialize(x =>
            {
                x.CreateMap<Member, MemberModel>().ReverseMap();
                x.CreateMap<StockHeaderModel, StockHeader>().ReverseMap();
                x.CreateMap<StockDetail, StockDetailModel>().ReverseMap();
                x.CreateMap<StockDetailViewModel, StockDetailModel>()
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Value))
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.StockHeaderId, o => o.Ignore()).ReverseMap();
            });
        }
    }
}