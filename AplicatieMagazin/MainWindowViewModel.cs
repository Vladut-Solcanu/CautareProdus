using System.ComponentModel;
using System.Runtime.CompilerServices;
using ModeleMagazin;

namespace AplicatieMagazin
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Produs produsCurent;

        public Produs ProdusCurent
        {
            get => produsCurent;
            set
            {
                produsCurent = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            ProdusCurent = new Produs();
        }

        public bool EsteFormularValid =>
            string.IsNullOrEmpty(ProdusCurent[nameof(ProdusCurent.Nume)]) &&
            string.IsNullOrEmpty(ProdusCurent[nameof(ProdusCurent.Pret)]) &&
            string.IsNullOrEmpty(ProdusCurent[nameof(ProdusCurent.Culoar)]) &&
            string.IsNullOrEmpty(ProdusCurent[nameof(ProdusCurent.Raft)]);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}