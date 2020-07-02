using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MangaSoep
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEntry SelectedEntry { get; set; }

        //public ObservableCollection<IEntry> Entries { get; } = new ObservableCollection<IEntry>();

        public IReadOnlyList<MangaStatus> MangaStatusOptions { get; } = Enumeration.GetAll<MangaStatus>().ToList();

        public MainWindowViewModel()
        {
        }
    }
}
