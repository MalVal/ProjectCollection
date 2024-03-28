using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectCollectionClassLibrary
{
    public class Comics : Element
    {
        #region Variables and properties

        private String? _isbn;

        public String? Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        private String? _autor;

        public String? Autor
        {
            get { return _autor; }
            set { _autor = value; }
        }

        private String? _topic;

        public String? Topic
        {
            get { return _topic; }
            set { _topic = value; }
        }

        #endregion

        #region Constructors

        public Comics(String? name, float price, DateTime? date, String? image, String? isbn, String? autor, String? topic) : base(name, price, date, image)
        {
            Isbn = isbn;
            Autor = autor;
            Topic = topic;
        }

        public Comics() : this(null, 0, null, null, null, null, null)
        {
        }

        #endregion
    }
}
