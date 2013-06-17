using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public interface IDbList<T> : IDbSet<T>, IQueryable<T>
        where T: class
    {
    }
}
