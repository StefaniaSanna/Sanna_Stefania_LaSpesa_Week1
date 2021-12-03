using LaSpesa.Core.Entities;
using LaSpesa.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaSpesa.Mock
{
    public class RepositoryMockSpesa : ISpesa
    {
        public bool Add(Spesa spesa)
        {
            spesa.Id = InMemoryStorage.spese.Max(x => x.Id) + 1;
            InMemoryStorage.spese.Add(spesa);
            return true;
        }

        public IEnumerable<Spesa> GetAll(Func<Spesa, bool> filter = null)
        {
            if (filter != null)
            {
                return InMemoryStorage.spese.Where(filter).ToList();
            }
            return InMemoryStorage.spese.ToList();
        }

        public Spesa GetById(int id)
        {
            var spesaTrovata = InMemoryStorage.spese.SingleOrDefault(x => x.Id == id);
            return spesaTrovata;
        }

        public bool Update(Spesa spesaModificata)
        {
            var indice = InMemoryStorage.spese.IndexOf(spesaModificata);
            if (indice != -1)
            {
                spesaModificata.Approvato = true;
                InMemoryStorage.spese[indice] = spesaModificata;
                return true;
            }
            return false;
        }
    }
}
