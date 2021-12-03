using LaSpesa.Core.Entities;
using LaSpesa.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaSpesa.Mock
{
    public class RepositoryMockCategoria : ICategoria
    {

        public IEnumerable<Categoria> GetAll(Func<Categoria, bool> filter = null)
        {
            if (filter != null)
            {
                return InMemoryStorage.categorie.Where(filter).ToList();
            }
            return InMemoryStorage.categorie.ToList();
        }

        public Categoria GetById(int id)
        {
            var categoria = InMemoryStorage.categorie.FirstOrDefault(c => c.Id == id);
            return categoria;
        }
    }
}
