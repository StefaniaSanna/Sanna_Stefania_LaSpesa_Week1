using LaSpesa.Core.BusinessLayer;
using LaSpesa.Core.Entities;
using LaSpesa.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaSpesa.Client
{
    public class Menu
    {
        private static readonly SpesaBusinessLayer mainBL = new SpesaBusinessLayer(new RepositoryMockSpesa(), new RepositoryMockCategoria(), new RepositoryMockUtente());
        public static void Start()
        {
            Console.WriteLine("Benvenuto!");
            char choice;
            do
            {
                Console.WriteLine("**********Menù**********");
                Console.WriteLine("Scegli [1] per inserire una nuova spesa" +
                   "\nScegli [2] per approvare una spesa" +
                   "\nScegli [3] per visualizzare l'elenco delle spese approvate nell'ultimo mese" +
                   "\nScegli [4] per visualizzare l'elenco delle spese di un utente" +
                   "\nScegli [5] per visualizzare il totale delle spese per categoria nell'ultimo mese" +
                   "\nScegli [6] per visualizzare le spese registrate dalla più recente alla meno recente" +
                   "\nScegli [7] per visualizzare le spese totali per tutte le categorie nell'ultimo mese" +

                   "\nScegli Q per uscire\n");
                choice = Console.ReadKey().KeyChar;
                switch (choice)
                {
                    case '1':
                        AddSpesa();
                        break;
                    case '2':
                        ApproveSpesa();
                        break;
                    case '3':
                        GetLastMonthApprovedSpese();
                        break;
                    case '4':
                        GetSpesePerUtente();
                        break;
                    case '5':
                        GetTotalPaymentForCategoriesLastMonth();
                        break;
                    case '6':
                        GetOrderedSpese();
                        break;
                    case '7':
                        GetMoneyForEveryCategory();
                        break;
                    case 'Q':
                        Console.WriteLine("Arrivederci");
                        break;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;
                }
            }
            while (choice != 'Q');
        }

        private static void GetMoneyForEveryCategory()
        {
            var categorie = mainBL.GetAllCategorie().ToList();
            foreach (var item in categorie)
            {
                (decimal,bool) spese = mainBL.GetTotalMoneyForCategoriesLastMonth(item.Id);
                Console.WriteLine($"Categoria {item.Nome}: spese {spese.Item1} euro nell'ultimo mese");
            }
        }
        private static void GetTotalPaymentForCategoriesLastMonth() 
        
        {
            var categorie =mainBL.GetAllCategorie();
            foreach (var categorieItem in categorie)
            {
                Console.WriteLine(categorieItem);
            }

            int idCategoria =CheckNumber("Inserire l'id della categoria");
            (decimal,bool) totalPayment = mainBL.GetTotalMoneyForCategoriesLastMonth(idCategoria);
            if (totalPayment.Item2 == true)
            {
                if (totalPayment.Item1 > 0)
                {
                    Console.WriteLine($"Il mese scorso sono stati spesi {totalPayment.Item1} euro per la categoria scelta");
                }
                else
                {
                    Console.WriteLine("Per questa categoria non sono stati spesi soldi l'ultimo mese");
                }
            }            
        }
        private static void GetLastMonthApprovedSpese()
        {         
            var speseApprovateUltimoMese = mainBL.GetLastMonthApprovedSpese();
            if(speseApprovateUltimoMese.ToList().Count > 0)
            {
                Console.WriteLine("Le spese approvate nell'ultimo mese sono:");
                foreach (var item in speseApprovateUltimoMese)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("Il mese scrorso non sono state effettuate spese");
            }
        }
        private static void GetSpesePerUtente()
        {
            int id = CheckNumber("Inserire l'id dell'utente");
            var spesePerUtente = mainBL.GetAllSpesePerUtente(id);
            if(spesePerUtente.Item2 == true)
            {
                if (spesePerUtente.Item1.ToList().Count > 0)
                {
                    foreach (var item in spesePerUtente.Item1)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine("L'utente selezionato è registrato ma non ha effettuato ancora spese");
                }               
            }
        }
        private static void GetOrderedSpese()
        {
            var orderedSpese = mainBL.GetOrderedSpese();
            
            Console.WriteLine("Spese dalla più recente alla meno recente: ");

            foreach (var o in orderedSpese)
            {
                Console.WriteLine(o);
            }
        }
        private static void ApproveSpesa()
        {
            
            var speseNonApprovate = mainBL.GetAllNotApproved();            
            if(speseNonApprovate.ToList().Count > 0)
            {
                Console.WriteLine("Le spese non ancora approvate sono:");
                foreach (var item in speseNonApprovate)
                {
                    Console.WriteLine(item);
                }
                int id = CheckNumber("Inserire l'id della spesa che si desidera approvare");
                Spesa spesaRecuperato = mainBL.GetSpesaById(id);
                if (spesaRecuperato != null)
                {
                    bool IsUpdate = mainBL.UpdateSpesa(spesaRecuperato);
                }
            }
            else
            {
                Console.WriteLine("Non ci sono spese non approvate");
            }
            
        }
        private static void AddSpesa()
        {
            DateTime data = CheckData();  
            string descrizione = GetString("la descrizione");
            decimal importo = CheckDecimal("l'importo");
            Console.WriteLine("Le categorie disponibili sono:");
            var categorie = mainBL.GetAllCategorie();
            foreach (var c in categorie)
            {
                Console.WriteLine(c); 
            }
            int categoriaId = CheckNumber("Inserisci l'id della categoria");
            Console.WriteLine("Gli utenti registrati sono:");

            var utenti = mainBL.GetAllUtenti();
            foreach (var u in utenti)
            {
                Console.WriteLine(u);
            }
            int utenteId = CheckNumber("Inserisci l'id dell'utente");
            bool isApprovato = false;                                             
            Spesa nuovaSpesa = new Spesa();
            nuovaSpesa.Data = data;
            nuovaSpesa.Approvato = isApprovato;
            nuovaSpesa.CategoriaId = categoriaId;
            nuovaSpesa.Importo = importo;
            nuovaSpesa.Descrizione = descrizione;
            nuovaSpesa.UtenteId = utenteId;
            bool isAdded = mainBL.AddSpesa(nuovaSpesa);

        }
        private static DateTime CheckData()
        {
            DateTime data;
            do
            {
                Console.WriteLine("Inserire la data");
            }
            while(!DateTime.TryParse(Console.ReadLine(), out data));
            return data;
        }
        private static int CheckNumber(string message)
        {
            Console.WriteLine($"{message}: ");

            int number;

            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Puoi inserire solo numeri interi. Riprova: ");
            }

            return number;
        }
        private static decimal CheckDecimal(string message)
        {
            Console.WriteLine($"Inserisci {message}: ");

            decimal number;

            while (!decimal.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Puoi inserire solo numeri interi. Riprova: ");
            }

            return number;
        }
        private static string GetString(string v)
        {
            string text;
            do
            {
                Console.WriteLine("Inserisci " + v);
                text = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(text));

            return text;
        }
    }
    
}

