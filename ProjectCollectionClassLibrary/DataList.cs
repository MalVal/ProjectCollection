using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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

        private string _comicsPath;

        public string ComicsPath
        {
            get { return _comicsPath; }
            set { _comicsPath = value; }
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

        private string _vinylPath;

        public string VinylPath
        {
            get { return _vinylPath; }
            set { _vinylPath = value; }
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

        #region Methods

        public void AddComics(Comics comics)
        {
            this.ComicsList.Add(comics);
            this.ElementsList.Add(comics);
        }

        public void AddVinyl(Vinyl vinyl)
        {
            this.VinylList.Add(vinyl);
            this.ElementsList.Add(vinyl);
        }

        public void DeleteComics(Comics comics)
        {
            this.ComicsList.Remove(comics);
            this.ElementsList.Remove(comics);
        }

        public void DeleteVinyls(Vinyl vinyl)
        {
            this.VinylList.Remove(vinyl);
            this.ElementsList.Remove(vinyl);
        }

        public void OrderList(int choice)
        {
            List<Element> listElem = ElementsList.ToList();
            List<Comics> listComics = ComicsList.ToList();
            List<Vinyl> listVinyls = VinylList.ToList();

            switch(choice)
            {
                case 1:
                    listElem.Sort(new ElementNameAComparer());
                    listComics.Sort(new ElementNameAComparer());
                    listVinyls.Sort(new ElementNameAComparer());
                    break;
                case 2:
                    listElem.Sort(new ElementNameDComparer());
                    listComics.Sort(new ElementNameDComparer());
                    listVinyls.Sort(new ElementNameDComparer());
                    break;
                case 3:
                    listElem.Sort(new ElementPriceAComparer());
                    listComics.Sort(new ElementPriceAComparer());
                    listVinyls.Sort(new ElementPriceAComparer());
                    break;
                case 4:
                    listElem.Sort(new ElementPriceDComparer());
                    listComics.Sort(new ElementPriceDComparer());
                    listVinyls.Sort(new ElementPriceDComparer());
                    break;
                case 5:
                    listElem.Sort(new ElementDateAddedAComparer());
                    listComics.Sort(new ElementDateAddedAComparer());
                    listVinyls.Sort(new ElementDateAddedAComparer());
                    break;
                case 6:
                    listElem.Sort(new ElementDateAddedDComparer());
                    listComics.Sort(new ElementDateAddedDComparer());
                    listVinyls.Sort(new ElementDateAddedDComparer());
                    break;
                default:
                    listElem.Sort();
                    listComics.Sort();
                    listVinyls.Sort();
                    break;
            }

            ElementsList.Clear();
            foreach (var item in listElem)
            {
                ElementsList.Add(item);
            }

            ComicsList.Clear();
            foreach (var item in listComics)
            {
                ComicsList.Add(item);
            }

            VinylList.Clear();
            foreach (var item in listVinyls)
            {
                VinylList.Add(item);
            }
        }

        #endregion
    }
}
