using LaSpesa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaSpesa.Core.Interfaces
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll(Func<T, bool> filter = null); 
        public T GetById(int id);
    }
}
