using LaSpesa.Core.Entities;
using LaSpesa.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaSpesa.Core.BusinessLayer
{
    public class SpesaBusinessLayer:IBusinessLayer
    {
        private readonly ISpesa _spesaRepository;
        private readonly ICategoria _categoriaRepository;
        private readonly IUtente _utenteRepository;
        public SpesaBusinessLayer(ISpesa spesaRepo, ICategoria categoriaRepo, IUtente utenteRepo)
        {
            _spesaRepository = spesaRepo;
            _categoriaRepository = categoriaRepo;
            _utenteRepository = utenteRepo;
        }

        public bool AddSpesa(Spesa s)
        {
            if (s == null)
            {
                Console.WriteLine("Impossibile aggiungere la spesa");
                return false;
            }
            else
            {
                Utente utente = _utenteRepository.GetById(s.UtenteId);
                if (utente == null)
                {
                    Console.WriteLine("Utente non trovato");                   
                }
                else
                {
                    Categoria categoria = _categoriaRepository.GetById(s.CategoriaId);
                    if (categoria == null)
                    {
                        Console.WriteLine("Categoria non trovata");                      
                    }                       
                    else
                    {
                        bool aggiunta = _spesaRepository.Add(s);
                        Console.WriteLine("Spesa aggiunta correttamente");
                        return aggiunta;
                    }
                }
            }
            return false;
            
        }

        public IEnumerable<Categoria> GetAllCategorie()
        {
            return _categoriaRepository.GetAll();
        }

        public IEnumerable<Utente> GetAllUtenti()
        {
            return _utenteRepository.GetAll();
        }

        public Spesa GetSpesaById(int id)
        {
            Spesa spesa = _spesaRepository.GetById(id);
            if(spesa == null)
            {
                Console.WriteLine("L'id inserito non corrisponde a nessuna spesa");
                return null;
            }
            else
            {
                return spesa;
            }
        }

        public bool UpdateSpesa(Spesa s)
        {
           var speseApprovate = _spesaRepository.GetAll(e => e.Approvato==false);
            foreach (var v in speseApprovate)
            {
                if (s.Id == v.Id)
                {
                    bool isUpdate = _spesaRepository.Update(v);
                    Console.WriteLine("Spesa approvata");
                    return isUpdate;
                }              
            }
            Console.WriteLine("Questa spesa è già stata approvata");
            return false;
        }

        public IEnumerable<Spesa> GetAllNotApproved()
        {
            return _spesaRepository.GetAll(e => e.Approvato == false);
        }

        public IEnumerable<Spesa> GetOrderedSpese()
        {
            var speseOrdinate = _spesaRepository.GetAll().ToList().OrderByDescending(s => s.Data);
            return speseOrdinate;
        }

        public (IEnumerable<Spesa>,bool) GetAllSpesePerUtente(int id)
        {
            Utente utenteTrovato = _utenteRepository.GetById(id);
            if (utenteTrovato == null)
            {
                Console.WriteLine("Nessun utente corrispondente a questo id");
                return(null,false);
            }
            var spesePerUtente = _spesaRepository.GetAll(e=> e.UtenteId == id);
            return (spesePerUtente,true);
        }

        public IEnumerable<Spesa> GetLastMonthApprovedSpese()
        {
            int LastMont = DateTime.Today.Month == 1 ? 12 : DateTime.Today.Month - 1;
            int LastYear = LastMont == 12 ? DateTime.Today.Year - 1 : DateTime.Today.Year;
            return _spesaRepository.GetAll(e => e.Data.Month == LastMont && e.Data.Year == LastYear && e.Approvato==true);
        }

        public (decimal,bool) GetTotalMoneyForCategoriesLastMonth(int idCategoria)
        {
            Categoria categoria = _categoriaRepository.GetById(idCategoria);
            decimal totale = 0;
            if (categoria != null)
            {
                int LastMont = DateTime.Today.Month == 1 ? 12 : DateTime.Today.Month - 1;
                int LastYear = LastMont == 12 ? DateTime.Today.Year - 1 : DateTime.Today.Year;
                var listaSPeseMeseScorsoPerCategoria = _spesaRepository.GetAll(e => e.Data.Month == LastMont && e.Data.Year == LastYear && e.CategoriaId == idCategoria);
               
                foreach (var item in listaSPeseMeseScorsoPerCategoria)
                {
                    totale += item.Importo;
                }
                return(totale,true);
            }
            else
            {
                Console.WriteLine("L'id non corrisponde ad alcuna categoria");
            }           
            return (totale,false);

        }
    }
}
