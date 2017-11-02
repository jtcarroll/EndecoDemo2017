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
    public interface IStockHeaderService
    {
        IEnumerable<StockHeaderModel> GetStockHeaders();
        IEnumerable<StockHeaderModel> GetStockHeadersForMember(int id, int fetchCount);
        StockHeaderModel GetLastStockHeaderForMember(int memberId);
        StockHeaderModel GetStockHeader(int id);
        int CreateStockHeader(StockHeaderModel StockHeader);
        int CommitStockHeader();

    }

    public class StockHeaderService : IStockHeaderService
    {
        private readonly IStockHeaderRepository _stockHeaderRepository;
        private readonly IUnitOfWork unitOfWork;

        public StockHeaderService(IStockHeaderRepository stockHeaderRepository, IUnitOfWork unitOfWork)
        {
            this._stockHeaderRepository = stockHeaderRepository;
            this.unitOfWork = unitOfWork;
        }

        #region StockHeaderService

        public IEnumerable<StockHeaderModel> GetStockHeaders()
        {
            var stockHeaders = _stockHeaderRepository.GetAll();
            return Mapper.Map<IEnumerable<StockHeaderModel>>(stockHeaders);
        }

        public StockHeaderModel GetStockHeader(int id)
        {
            var stockHeader = _stockHeaderRepository.GetById(id);
            return Mapper.Map<StockHeaderModel>(stockHeader);
        }

        public IEnumerable<StockHeaderModel> GetStockHeadersForMember(int memberId, int fetchCount)
        {
            var stockHeaders = _stockHeaderRepository.GetStockHeadersForMember(memberId, fetchCount);
            return Mapper.Map<IEnumerable<StockHeaderModel>>(stockHeaders);
        }

        public StockHeaderModel GetLastStockHeaderForMember(int memberId)
        {
            var stockHeader = _stockHeaderRepository.GetLastStockHeaderForMember(memberId);
            return Mapper.Map<StockHeaderModel>(stockHeader);
        }

        public int CreateStockHeader(StockHeaderModel stockHeaderModel)
        {
            var stockHeader = Mapper.Map<StockHeader>(stockHeaderModel);
            var entity =  _stockHeaderRepository.Add(stockHeader);
           return entity.Id;
        }

        public int CommitStockHeader()
        {
            return unitOfWork.Commit();
        }

        #endregion

    }
}



