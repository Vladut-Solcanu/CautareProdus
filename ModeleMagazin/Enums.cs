using System;

namespace ModeleMagazin
{
    // Enum simplu pentru raion/categorie
    public enum CategorieProdus
    {
        Lactate = 1,
        Bauturi = 2,
        Dulciuri = 3,
        Ingrijire = 4
    }

    // Enum cu [Flags] pentru caracteristicile produsului la raft
    [Flags]
    public enum EticheteProdus
    {
        Niciuna = 0,
        FaraZahar = 1,
        FaraGluten = 2,
        ProdusLocal = 4,
        OfertaSpeciala = 8
    }
}