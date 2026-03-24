using System.Collections.Generic;
using System.Linq; // Necesit pentru LINQ
using ModeleMagazin;

namespace LogicaMagazine
{
    public class AdministrareLocatiiMemorie
    {
        // Colecție generică pentru stocare
        private List<LocatieProdus> _locatii;

        public AdministrareLocatiiMemorie()
        {
            _locatii = new List<LocatieProdus>();
        }

        public void AdaugaLocatie(LocatieProdus locatie)
        {
            _locatii.Add(locatie);
        }

        public List<LocatieProdus> GetToateLocatiile()
        {
            return _locatii;
        }

        // CERINȚA: Căutare folosind LINQ
        public List<LocatieProdus> CautaProdusInToateMagazinele(string numeCautat)
        {
            // LINQ cu metoda de extensie .Where
            var rezultate = _locatii
                .Where(loc => loc.NumeProdus.ToLower().Contains(numeCautat.ToLower()))
                .ToList();

            return rezultate;
        }
    }
}