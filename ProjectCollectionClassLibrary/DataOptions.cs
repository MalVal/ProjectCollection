using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCollectionClassLibrary
{
    public class DataOptions : INotifyPropertyChanged
    {
        #region Variables and properties
        private string _comicsPathL;

        public string ComicsPathL
        {
            get { return _comicsPathL; }
            set
            {
                _comicsPathL = value;
                NotifyPropertyChanged();
            }
        }

        private string _vinylPathL;

        public string VinylPathL
        {
            get { return _vinylPathL; }
            set
            {
                _vinylPathL = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public DataOptions()
        {

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
