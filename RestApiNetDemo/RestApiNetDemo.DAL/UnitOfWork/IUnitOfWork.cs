using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiNetDemo.DAL.UnitOfWork
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext,new()
    {
        TContext Context { get; }
        void Save();
    }
}
