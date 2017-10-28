using AutoMapper;
using BrandLoyalty.WebApp.Infrastructure.Automapper.Profiles;
using EndecoDemo.DAL;
using EndecoDemo2017.ViewModels.Member;

namespace EndecoDemo2017.Infrastructure.Automapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //Mapper.Initialize(x =>
            //{
            //    x.AddProfile<ModelsToViewModelsProfile>();
            //});

            Mapper.Initialize(x =>
            {
                x.CreateMap<Member, MemberViewModel>()
                .ForMember(g => g.MemberId, map => map.MapFrom(vm => vm.Id))
                .ForMember(g => g.Email, map => map.MapFrom(vm => vm.Email))
                .ForMember(g => g.Stock, map => map.MapFrom(vm => vm.StockHeaders))
                .ReverseMap();
            });
        }
    }
}