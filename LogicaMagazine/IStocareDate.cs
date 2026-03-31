using System.Collections.Generic;
using ModeleMagazin;

namespace LogicaMagazine
{
   
    public interface IStocareProduse
    {
        void AddProdus(Produs p);
        List<Produs> GetProduse();
        void UpdateProdus(Produs produsActualizat);
    }


    public interface IStocareMagazine
    {
        void AddMagazin(Magazin m);
        List<Magazin> GetMagazine();
    }
}