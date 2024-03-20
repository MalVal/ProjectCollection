using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCollectionClassLibrary
{
    public class DataList : INotifyPropertyChanged
    {
        private ObservableCollection<Comics> _comicsList;

        public ObservableCollection<Comics> ComicsList
        {
            get { return _comicsList; }
            set
            {
                if (_comicsList != value)
                {
                    _comicsList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Comics _currentComics;

        public Comics CurrentComics
        {
            get { return _currentComics; }
            set
            {
                if (_currentComics != value)
                {
                    _currentComics = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<Vinyl> _vinylList;

        public ObservableCollection<Vinyl> VinylList
        {
            get { return _vinylList; }
            set
            {
                if (_vinylList != value)
                {
                    _vinylList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Vinyl _currentVinyl;

        public Vinyl CurrentVinyl
        {
            get { return _currentVinyl; }
            set
            {
                if (_currentVinyl != value)
                {
                    _currentVinyl = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DataList()
        {
            ComicsList = new ObservableCollection<Comics>();
            VinylList = new ObservableCollection<Vinyl>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

    }
}
