using System;

namespace ModeleMagazin
{
 
    public enum CategorieProdus
    {
        Lactate = 1,
        Bauturi = 2,
        Dulciuri = 3,
        Ingrijire = 4
    }

    
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