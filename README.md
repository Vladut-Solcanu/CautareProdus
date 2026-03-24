CautareProdus - Găsește Produsul la Raft
📝 Descriere scurtă
CautareProdus este o aplicație consolă dezvoltată în C# (.NET 8.0) care ajută utilizatorii să găsească rapid poziția exactă a unui produs (culoar și raft) în diferite filiale ale supermarketurilor.

Aplicația este proiectată respectând arhitectura pe 3 straturi (N-Tier Architecture), separând interfața cu utilizatorul de logica de business și de modelele de date, conform bunelor practici de Programare Orientată pe Obiecte (POO).

🚀 Funcționalități principale
Căutare inteligentă după nume (LINQ): - Utilizatorul introduce numele (sau o parte din numele) produsului căutat.

Aplicația filtrează și returnează instantaneu lista de produse compatibile din toate magazinele și filialele înregistrate, folosind expresii LINQ.

Afișarea detaliată a locației produsului:

Pentru fiecare produs găsit, aplicația afișează:

Magazinul și filiala (ex: Lidl (Zamca)).

Detalii produs: Nume, Categorie (ex: Lactate, gestionat prin enum) și Etichete speciale (ex: Produs Local, Ofertă Specială, gestionate prin enum cu atributul [Flags]).

Locația fizică exactă: Culoarul și Raftul.

Gestiunea stocurilor / locațiilor (Mod Angajat):

Interfață simplă tip meniu pentru înregistrarea produselor noi la raft.

Validarea datelor de intrare (ex: tratarea excepțiilor de tip FormatException cu blocuri try-catch pentru a preveni blocarea aplicației la introducerea literelor în loc de cifre pentru raft/culoar).

🏗️ Structura Proiectului (Arhitectura pe 3 straturi)
Soluția este împărțită în 3 proiecte distincte pentru o mentenanță și o scalabilitate ușoară:

ModeleMagazin (Class Library): Conține definițiile entităților (LocatieProdus.cs) și enumerările (CategorieProdus, EticheteProdus).

LogicaMagazine (Class Library): Reprezintă stratul de manipulare a datelor. Aici este gestionată colecția generică de date în memorie (List<LocatieProdus>) și tot aici se află logica de căutare (extensiile .Where() din LINQ).

CautareProdus (Console App): Este punctul de intrare în aplicație (Program.cs). Gestionează interacțiunea cu utilizatorul (UI), citește datele de la tastatură și afișează rezultatele.
