using LaSpesa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaSpesa.Core.Interfaces
{
    public interface IBusinessLayer
    {
        public bool AddSpesa(Spesa s);
        public IEnumerable<Utente> GetAllUtenti();
        public IEnumerable<Categoria> GetAllCategorie();
        public IEnumerable<Spesa> GetOrderedSpese();
        public Spesa GetSpesaById(int id);
        public bool UpdateSpesa(Spesa s);
        public (IEnumerable<Spesa>,bool) GetAllSpesePerUtente(int id);
        public IEnumerable<Spesa> GetLastMonthApprovedSpese();
        public (decimal, bool) GetTotalMoneyForCategoriesLastMonth(int idCategoria);

    }
}
