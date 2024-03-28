using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectCollectionClassLibrary
{
    public class DataList : INotifyPropertyChanged
    {
        #region Elements
        private ObservableCollection<Element> _elementsList;

        public ObservableCollection<Element> ElementsList
        {
            get { return _elementsList; }
            set
            {
                if (_elementsList != value)
                {
                    _elementsList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Element _currentElement;

        public Element CurrentElement
        {
            get { return _currentElement; }
            set
            {
                if (_currentElement != value)
                {
                    _currentElement = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Comics
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
        #endregion

        #region Vinyls
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
        #endregion

        #region Constructor
        public DataList()
        {
            ElementsList = new ObservableCollection<Element>();
            ComicsList = new ObservableCollection<Comics>();
            VinylList = new ObservableCollection<Vinyl>();
        }
        #endregion

        #region Interfaces
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        #endregion
    }
}
