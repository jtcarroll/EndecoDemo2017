
using AutoMapper;
using EndecoDemo.DAL;
using EndecoDemo.Models.Member;
using EndecoDemo.Models.Stock;
using System;
using System.Collections.Generic;
using System.Text;
namespace EndecoDemo.Services.Automapper.Profiles
{

    public class DtoModelsToModels : Profile
    {
        protected void Configure()
        {
            this.CreateMemberDtoToModelMap();
            this.CreateStockHeaderDtoToModelMap();
            this.CreateStockDetailsDtoToModelMap();
        }

        #region Member
        private void CreateMemberDtoToModelMap()
        {
            CreateMap<Member, MemberModel>().ReverseMap();
        }
        #endregion

        #region StockHeader
        private void CreateStockHeaderDtoToModelMap()
        {
            CreateMap<StockHeader, StockHeaderModel>().ReverseMap();
        }
        #endregion

        #region StockDetails
        private void CreateStockDetailsDtoToModelMap()
        {
            CreateMap<StockDetail, StockDetailModel>().ReverseMap();
        }
        #endregion
    }
}
