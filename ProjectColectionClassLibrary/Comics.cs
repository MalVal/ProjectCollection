using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Comics(String? name, float price, DateTime? date, String? isbn, String? autor, String? topic) : base(name, price, date)
        {
            Isbn = isbn;
            Autor = autor;
            Topic = topic;
        }

        public Comics() : this(null, 0, null, null, null, null)
        {
        }

        #endregion

        #region Methods

        public override void Save(BinaryWriter bw)
        {
            base.Save(bw);
            if(Isbn != null)
            {
                bw.Write(Isbn);
            }
            else
            {
                bw.Write("");
            }
            if (Autor != null)
            {
                bw.Write(Autor);
            }
            else
            {
                bw.Write("");
            }
            if (Topic != null)
            {
                bw.Write(Topic);
            }
            else
            {
                bw.Write("");
            }
        }

        public override void Load(BinaryReader br)
        {
            base.Load(br);
            Isbn = br.ReadString();
            if (Isbn == "")
            {
                Isbn = null;
            }
            Autor = br.ReadString();
            if (Autor == "")
            {
                Autor = null;
            }
            Topic = br.ReadString();
            if (Topic == "")
            {
                Topic = null;
            }
        }

        #endregion
    }
}
