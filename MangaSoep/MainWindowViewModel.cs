using System.Collections.ObjectModel;

namespace MangaSoep
{
    class MainWindowViewModel
    {

        ObservableCollection<IEntry> Entries { get; } = new ObservableCollection<IEntry>();

        public MainWindowViewModel()
        {
        }

    }
}
