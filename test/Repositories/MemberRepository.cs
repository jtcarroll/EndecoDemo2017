using EndecoDemo.DAL.Infrastructure;
using EndecoDemo.DAL.Infrastructure.Implementations;
using EndecoDemo.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndecoDemo.DAL.Repositories
{
    public class MemberRepository : RepositoryBase<Member>, IMemberRepository
    {
        public MemberRepository(IDbFactory dbFactory)
            : base(dbFactory) {

        }

        public Member GetByEmail(string email)
        {
            this.DbContext.Configuration.LazyLoadingEnabled = false;
            return this.DbContext.Members.Where(m => m.Email.Equals(email)).FirstOrDefault();
        }


    }

    public interface IMemberRepository : IRepository<Member>
    {
        Member GetByEmail(string email);
    }
}
