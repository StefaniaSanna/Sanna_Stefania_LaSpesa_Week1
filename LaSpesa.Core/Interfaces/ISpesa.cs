using LaSpesa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaSpesa.Core.Interfaces
{
    public interface ISpesa : IRepository<Spesa>
    {
        public bool Add(Spesa spesa);
        public bool Update(Spesa spesa);
    }
}
