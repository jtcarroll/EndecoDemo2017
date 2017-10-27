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
            : base(dbFactory) { }
    }

    public interface IMemberRepository : IRepository<Member>
    {

    }
}
