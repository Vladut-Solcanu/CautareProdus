using System.Collections.Generic;
using ModeleMagazin;

namespace LogicaMagazine
{
    public interface IStocareProduse
    {
        void AddProdus(Produs p);
        List<Produs> GetProduse();
        void UpdateProdus(Produs p);
    }

    public interface IStocareMagazine
    {
        void AddMagazin(Magazin m);          // Create
        List<Magazin> GetMagazine();         // Read
        void UpdateMagazin(Magazin m);       // Update
        void DeleteMagazin(Magazin m);       // Delete (Noua metodă adăugată)
    }
}