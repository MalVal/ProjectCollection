using System.ComponentModel;

namespace ProjectCollectionClassLibrary
{
    public abstract class Element
    {
        #region Variables & properties

        private String? _name;

        public String? Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private DateTime _dateAdded;

        public DateTime DateAdded
        {
            get { return _dateAdded; }
            set { _dateAdded = value; }
        }

        private float _price;

        public float Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private DateTime? _date;

        public DateTime? Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private String? _image;

        public String? Image
        {
            get { return _image;  }
            set
            { 
                _image = value;
            }
        }

        #endregion

        #region Constructors

        public Element(String? name, float price, DateTime? date, string? image)
        {
            Name = name;
            DateAdded = DateTime.Now;
            Price = price;
            Date = date;
            Image = image;
        }

        public Element() : this(null, 0, null, null)
        {
        }

        #endregion
    }
}
