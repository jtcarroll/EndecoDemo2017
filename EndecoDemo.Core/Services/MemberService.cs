using AutoMapper;
using EndecoDemo.DAL;
using EndecoDemo.DAL.Infrastructure.Interfaces;
using EndecoDemo.DAL.Repositories;
using EndecoDemo.Models.Member;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndecoDemo.Services.Services
{
    public interface IMemberService
    {
        IEnumerable<MemberModel> GetMembers();
        MemberModel GetMember(int id);
        MemberModel GetMemberByEmail(string email);
        int CreateMember(MemberModel Member);
        void CommitMember();
    }

    public class MemberService : IMemberService
    {
        private readonly IMemberRepository membersRepository;
        private readonly IUnitOfWork unitOfWork;

        public MemberService(IMemberRepository membersRepository, IUnitOfWork unitOfWork)
        {
            this.membersRepository = membersRepository;
            this.unitOfWork = unitOfWork;
        }

        #region MemberService

        public IEnumerable<MemberModel> GetMembers()
        {
            var members = membersRepository.GetAll();
            return Mapper.Map<IEnumerable<MemberModel>>(members);
        }

        public MemberModel GetMember(int id)
        {
            var member = membersRepository.GetById(id);
            return Mapper.Map<MemberModel>(member);
        }

        public MemberModel GetMemberByEmail(string email)
        {
            var member = membersRepository.GetByEmail(email);
            return Mapper.Map<MemberModel>(member);
        }

        public int CreateMember(MemberModel memberModel)
        {
            var member = Mapper.Map<Member>(memberModel);
            var entity = membersRepository.Add(member);
            return entity.Id;
        }

        public void CommitMember()
        {
            unitOfWork.Commit();
        }

        #endregion

    }
}


