using System;
using System.Collections.Generic;
using System.Linq; // Folosit pentru cautarea cu LINQ
using ModeleMagazin;
using LogicaMagazine;

namespace CautareProdus
{
    class Program
    {
        static void Main()
        {
            // Instantiem clasele care se ocupa cu salvarea in fisiere pe hard disk
            string caleProduse = "Produse.txt";
            string caleMagazine = "Magazine.txt";

            // Folosim interfețele create conform laboratorului 5
            IStocareProduse adminProduse = new AdministrareProduseFisier(caleProduse);
            IStocareMagazine adminMagazine = new AdministrareMagazineFisier(caleMagazine);

            while (true)
            {
                Console.WriteLine("\n=== MENIU PRINCIPAL (SALVARE IN FISIERE) ===");
                Console.WriteLine("1. Adauga un PRODUS nou");
                Console.WriteLine("2. Afiseaza toate produsele");
                Console.WriteLine("3. Adauga un MAGAZIN nou");
                Console.WriteLine("4. Afiseaza toate magazinele");
                Console.WriteLine("5. Cauta produs dupa nume (LINQ)");
                Console.WriteLine("6. Modifica raftul unui produs (Update in fisier)");
                Console.WriteLine("0. Iesire");
                Console.Write("Alege o optiune: ");

                string optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        AdaugaProdus(adminProduse);
                        break;
                    case "2":
                        AfiseazaProduse(adminProduse.GetProduse());
                        break;
                    case "3":
                        AdaugaMagazin(adminMagazine);
                        break;
                    case "4":
                        AfiseazaMagazine(adminMagazine.GetMagazine());
                        break;
                    case "5":
                        Console.Write("Introdu numele produsului cautat: ");
                        string cautare = Console.ReadLine();

                        // Cerinta Lab: Folosim extensia LINQ (.Where) pentru filtrare rapida
                        var rezultate = adminProduse.GetProduse()
                            .Where(p => p.Nume.ToLower().Contains(cautare.ToLower()))
                            .ToList();

                        AfiseazaProduse(rezultate);
                        break;
                    case "6":
                        ModificaProdus(adminProduse);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
        }

        static void AdaugaProdus(IStocareProduse admin)
        {
            try
            {
                Console.Write("ID Produs (numar): ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Nume Produs: ");
                string nume = Console.ReadLine();

                Console.WriteLine("Categorii: 1-Lactate, 2-Bauturi, 3-Dulciuri, 4-Ingrijire");
                Console.Write("Alege ID categorie: ");
                CategorieProdus cat = (CategorieProdus)int.Parse(Console.ReadLine());

                Console.Write("Culoar (numar): ");
                int culoar = int.Parse(Console.ReadLine());

                Console.Write("Raft (numar): ");
                int raft = int.Parse(Console.ReadLine());

                Produs produsNou = new Produs(id, nume, cat, culoar, raft);
                admin.AddProdus(produsNou);

                Console.WriteLine("Produs salvat cu succes in fisierul Produse.txt!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Eroare: Trebuie sa introduci numere pentru ID, Culoar si Raft!");
            }
        }

        static void ModificaProdus(IStocareProduse admin)
        {
            try
            {
                Console.Write("Introdu ID-ul produsului pe care vrei sa il muti: ");
                int idCautat = int.Parse(Console.ReadLine());

                // Folosim LINQ (.FirstOrDefault) pentru a gasi rapid produsul dupa ID
                var produs = admin.GetProduse().FirstOrDefault(p => p.Id == idCautat);

                if (produs != null)
                {
                    Console.Write($"Produs gasit! Raftul curent este {produs.Raft}. Introdu noul raft: ");
                    produs.Raft = int.Parse(Console.ReadLine());

                    admin.UpdateProdus(produs);
                    Console.WriteLine("Locatia produsului a fost actualizata in fisier!");
                }
                else
                {
                    Console.WriteLine("Nu a fost gasit niciun produs cu acest ID.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Eroare: ID-ul si raftul trebuie sa fie numere!");
            }
        }

        static void AfiseazaProduse(List<Produs> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nu exista produse salvate.");
                return;
            }
            Console.WriteLine("\n--- Lista Produse ---");
            foreach (var p in lista)
            {
                Console.WriteLine(p.Info());
            }
        }

        static void AdaugaMagazin(IStocareMagazine admin)
        {
            try
            {
                Console.Write("ID Magazin (numar): ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Brand (ex: Lidl, Kaufland): ");
                string brand = Console.ReadLine();

                Console.Write("Filiala (ex: Zamca, Scheia): ");
                string filiala = Console.ReadLine();

                Magazin magazinNou = new Magazin(id, brand, filiala);
                admin.AddMagazin(magazinNou);

                Console.WriteLine("Magazin salvat cu succes in fisierul Magazine.txt!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Eroare: ID-ul magazinului trebuie sa fie un numar!");
            }
        }

        static void AfiseazaMagazine(List<Magazin> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nu exista magazine salvate.");
                return;
            }
            Console.WriteLine("\n--- Lista Magazine ---");
            foreach (var m in lista)
            {
                Console.WriteLine(m.Info());
            }
        }
    }
}