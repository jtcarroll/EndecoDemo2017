﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndecoDemo.DAL.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}
