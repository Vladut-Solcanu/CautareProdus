using System;
using System.Collections.Generic;
using ModeleMagazin;
using LogicaMagazine;

namespace SistemMagazine
{
    class Program
    {
        static void Main()
        {
            AdministrareLocatiiMemorie adminLocatii = new AdministrareLocatiiMemorie();

            while (true)
            {
                Console.WriteLine("\n=== GASESTE PRODUSUL LA RAFT ===");
                Console.WriteLine("1. Inregistreaza locatia unui produs nou");
                Console.WriteLine("2. Afiseaza toate inregistrarile");
                Console.WriteLine("3. Cauta unde se afla un produs");
                Console.WriteLine("0. Iesire");
                Console.Write("Alege o optiune: ");

                string optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        CitesteSiAdauga(adminLocatii);
                        break;
                    case "2":
                        Afiseaza(adminLocatii.GetToateLocatiile());
                        break;
                    case "3":
                        Console.Write("Ce produs cauti?: ");
                        string cautare = Console.ReadLine();
                        var rezultate = adminLocatii.CautaProdusInToateMagazinele(cautare);
                        Afiseaza(rezultate);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
        }

        static void CitesteSiAdauga(AdministrareLocatiiMemorie admin)
        {
            // CERINȚA: Tratarea excepțiilor
            try
            {
                Console.Write("Nume Magazin (ex: Lidl Scheia, Lidl Zamca): ");
                string magazin = Console.ReadLine();

                Console.Write("Nume Produs: ");
                string produs = Console.ReadLine();

                Console.Write("Culoar (numar): ");
                int culoar = int.Parse(Console.ReadLine());

                Console.Write("Raft (numar): ");
                int raft = int.Parse(Console.ReadLine());

                // Selectare Enum simplu
                Console.WriteLine("Categorii: 1-Lactate, 2-Bauturi, 3-Dulciuri, 4-Ingrijire");
                Console.Write("Alege ID categorie: ");
                CategorieProdus categorie = (CategorieProdus)int.Parse(Console.ReadLine());

                // Setare Enum cu Flags (ex: Produsul e și Local, și la Ofertă)
                EticheteProdus etichete = EticheteProdus.ProdusLocal | EticheteProdus.OfertaSpeciala;

                LocatieProdus locatieNoua = new LocatieProdus(magazin, produs, categorie, etichete, culoar, raft);
                admin.AdaugaLocatie(locatieNoua);

                Console.WriteLine("Produsul a fost inregistrat la raft cu succes!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Eroare: Culoarul si Raftul trebuie sa fie numere (ex: 3)!");
            }
        }

        static void Afiseaza(List<LocatieProdus> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nu s-au gasit rezultate.");
                return;
            }

            Console.WriteLine("\n--- Locatii ---");
            foreach (var item in lista)
            {
                Console.WriteLine(item.Info());
            }
        }
    }
}