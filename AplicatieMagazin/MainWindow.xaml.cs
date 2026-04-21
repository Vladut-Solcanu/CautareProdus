using System.Windows;
using ModeleMagazin;
using LogicaMagazine;

namespace AplicatieMagazin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IStocareProduse admin = new AdministrareProduseFisier("Produse.txt");

            Produs produsNou = new Produs()
            {
                Id = 999, 
                MagazinId = 1,
                Nume = "Ciocolată cu Lapte",
                
                Culoar = 3,
                Raft = 5
            };

            
            admin.AddProdus(produsNou);

            
            MessageBox.Show("Produsul a fost salvat cu succes în fișier!");

           
            lblId.Content = "ID Produs: " + produsNou.Id;
            lblNume.Content = "Nume: " + produsNou.Nume;
            lblCategorie.Content = "Locație: Culoarul " + produsNou.Culoar + ", Raftul " + produsNou.Raft;
        }
    }
}