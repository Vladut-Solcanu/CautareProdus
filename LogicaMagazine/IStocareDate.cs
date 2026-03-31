using System.Collections.Generic;
using ModeleMagazin;

namespace LogicaMagazine
{
    // Interfata pentru Produse
    public interface IStocareProduse
    {
        void AddProdus(Produs p);
        List<Produs> GetProduse();
        void UpdateProdus(Produs produsActualizat);
    }

    // Interfata pentru Magazine
    public interface IStocareMagazine
    {
        void AddMagazin(Magazin m);
        List<Magazin> GetMagazine();
    }
}