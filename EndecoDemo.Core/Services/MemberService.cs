using EndecoDemo.DAL;
using EndecoDemo.DAL.Infrastructure.Interfaces;
using EndecoDemo.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndecoDemo.Services.Services
{
    public interface IMemberService
    {
        IEnumerable<Member> GetMembers();
        Member GetMember(int id);
        void CreateMember(Member Member);
        void SaveMember();
    }

    public class MemberService : IMemberService
    {
        private readonly IMemberRepository membersRepository;
        private readonly IStockHeaderRepository headerRepository;
        private readonly IUnitOfWork unitOfWork;

        public MemberService(IMemberRepository membersRepository, IUnitOfWork unitOfWork)
        {
            this.membersRepository = membersRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IMemberService Members

        public IEnumerable<Member> GetMembers()
        {
            var Members = membersRepository.GetAll();
            return Members;
        }

        public Member GetMember(int id)
        {
            var Member = membersRepository.GetById(id);
            return Member;
        }

        public void CreateMember(Member Member)
        {
            membersRepository.Add(Member);
        }

        public void SaveMember()
        {
            unitOfWork.Commit();
        }

        #endregion

    }
}


