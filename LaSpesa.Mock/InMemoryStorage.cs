using LaSpesa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaSpesa.Mock
{
    public class InMemoryStorage
    {
        public static List<Spesa> spese = new List<Spesa>()
        {
            new Spesa()
            {
                Id = 1,
                Data= new DateTime(2021,11,10),
                Descrizione = "Nuovo computer",
                Importo = 200,
                Approvato = true,
                CategoriaId = 1,
                UtenteId = 1
            },
            new Spesa()
            {
                Id = 2,
                Data= new DateTime(2021,11,1), 
                Descrizione = "Pranzo da asporto",
                Importo = 30,
                Approvato = true,
                CategoriaId = 2,
                UtenteId = 2
            },
            new Spesa()
            {
                Id = 3,
                Data= new DateTime(2021,12,3), 
                Descrizione = "Rossetto",
                Importo = 20,
                Approvato = false,
                CategoriaId = 3,
                UtenteId = 3 
            },
            new Spesa()
            {
                Id = 4,
                Data= new DateTime(2021,11,11),
                Descrizione = "Fondotinta",
                Importo = 15,
                Approvato = false,
                CategoriaId = 3,
                UtenteId = 2
            }


        };
        public static List<Utente> utenti = new List<Utente>()
        {
            new Utente()
            {
                Id=1,
                Nome = "Mario",
                Cognome = "Rossi"
            },
            new Utente()
            {
                Id=2,
                Nome = "Stefania",
                Cognome = "Sanna"
            },
            new Utente()
            {
                Id=3,
                Nome = "Mauro",
                Cognome = "Sanna"
            }
        };
        public static List<Categoria> categorie = new List<Categoria>()
        {
            new Categoria()
            {
                Id = 1,
                Nome = "Elettronica"
            },
            new Categoria()
            {
                Id = 2,
                Nome = "Alimentare"
            },
            new Categoria()
            {
                Id = 3,
                Nome = "Cosmetica"
            },

        };

    }
}
