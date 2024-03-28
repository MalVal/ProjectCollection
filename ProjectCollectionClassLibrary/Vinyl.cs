using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCollectionClassLibrary
{
    public class Vinyl : Element
    {
        #region Variables and properties

        private String? _genre;

        public String? Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        private String? _label;

        public String? Label
        {
            get { return _label; }
            set { _label = value; }
        }

        private String? _country;

        public String? Country
        {
            get { return _country; }
            set { _country = value; }
        }

        private String? _composer;

        public String? Composer
        {
            get { return _composer; }
            set { _composer = value; }
        }

        #endregion

        #region Constructors

        public Vinyl(String? name, float price, DateTime? date, String? image, String? genre, String? label, String? country, String? composer) : base(name, price, date, image)
        {
            Genre = genre;
            Label = label;
            Country = country;
            Composer = composer;
        }

        public Vinyl() : this(null, 0, null, null, null, null, null, null)
        {
        }

        #endregion
    }
}
