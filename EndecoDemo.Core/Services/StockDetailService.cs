using AutoMapper;
using EndecoDemo.DAL;
using EndecoDemo.DAL.Infrastructure.Interfaces;
using EndecoDemo.DAL.Repositories;
using EndecoDemo.Models.Stock;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndecoDemo.Services.Services
{
    public interface IStockDetailService
    {
        IEnumerable<StockDetailModel> GetStockDetails();
        IEnumerable<StockDetailModel> GetStockDetailsForStockHeader(int headerId);
        StockDetailModel GetStockDetail(int id);
        void CreateStockDetails(IEnumerable<StockDetailModel> stockDetails);
        int CommitStockDetail();
    }

    public class StockDetailService : IStockDetailService
    {
        private readonly IStockDetailRepository _stockDetailRepository;
        private readonly IUnitOfWork unitOfWork;

        public StockDetailService(IStockDetailRepository stockDetailRepository, IUnitOfWork unitOfWork)
        {
            this._stockDetailRepository = stockDetailRepository;
            this.unitOfWork = unitOfWork;
        }

        #region StockHeaderService

        public IEnumerable<StockDetailModel> GetStockDetails()
        {
            var stockHeaders = _stockDetailRepository.GetAll();
            return Mapper.Map<IEnumerable<StockDetailModel>>(stockHeaders);
        }

        public IEnumerable<StockDetailModel> GetStockDetailsForStockHeader(int headerId)
        {
            var stockDetails = _stockDetailRepository.GetStockDetailsForStockHeader(headerId);
            return Mapper.Map<IEnumerable<StockDetailModel>>(stockDetails);
        }

        public StockDetailModel GetStockDetail(int id)
        {
            var stockDetail = _stockDetailRepository.GetById(id);
            return Mapper.Map<StockDetailModel>(stockDetail);
        }

        public void CreateStockDetails(IEnumerable<StockDetailModel> stockDetailModels)
        {
            var stockDetail = Mapper.Map< IEnumerable<StockDetail>>(stockDetailModels);
            _stockDetailRepository.SaveStockDetails(stockDetail);
        }

        public int CommitStockDetail()
        {
            return unitOfWork.Commit();
        }

        #endregion

    }
}




