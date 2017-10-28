namespace BrandLoyalty.WebApp.Infrastructure.Automapper.Profiles
{
    using AutoMapper;
    using EndecoDemo.DAL;
    using EndecoDemo2017.ViewModels.Member;
    using System.Linq;

    public class ModelsToViewModelsProfile : Profile
    {
        protected void Configure()
        {
            this.CreateMemberMap();
        }

        #region Member
        private void CreateMemberMap()
        {
            CreateMap<Member, MemberViewModel>()
                .ForMember(g => g.MemberId, map => map.MapFrom(vm => vm.Id))
                .ForMember(g => g.Email, map => map.MapFrom(vm => vm.Email))
                .ForMember(g => g.Stock, map => map.MapFrom(vm => vm.StockHeaders))
                .ReverseMap();
        }
    }

}
#endregion