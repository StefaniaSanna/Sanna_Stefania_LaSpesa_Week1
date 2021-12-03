using LaSpesa.Core.Entities;
using LaSpesa.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaSpesa.Mock
{
    public class RepositoryMockUtente : IUtente
    {      
        public IEnumerable<Utente> GetAll(Func<Utente, bool> filter = null)
        {
            if (filter != null)
            {
                return InMemoryStorage.utenti.Where(filter).ToList();
            }
            return InMemoryStorage.utenti.ToList();
        }

        public Utente GetById(int id)
        {
            var utente = InMemoryStorage.utenti.SingleOrDefault(x => x.Id == id);
            return utente;
        }
    }
}
