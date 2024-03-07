namespace ProjectCollectionClassLibrary
{
    public abstract class Element
    {
        #region Variables & properties

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

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

        #endregion

        #region Constructors

        public Element(String? name, float price, DateTime? date)
        {
            Id = Sequence.Next();
            Name = name;
            DateAdded = DateTime.Now;
            Price = price;
            Date = date;
        }

        public Element() : this(null, 0, null)
        {
        }

        #endregion

        #region Methods

        public virtual void Save(BinaryWriter bw)
        {
            bw.Write(Id);
            if(Name != null)
            {
                bw.Write(Name);
            }
            else
            {
                bw.Write("".ToString());
            }
            bw.Write(DateAdded.Ticks);
            bw.Write(Price);
            if(Date.HasValue)
            {
                bw.Write(Date.Value.Ticks);
            }
            else
            {
                bw.Write((long)-1);
            }
        }

        public virtual void Load(BinaryReader br)
        {
            Id = br.ReadInt32();
            Name = br.ReadString();
            if(Name == "")
            {
                Name = null;
            }
            DateAdded = new DateTime(br.ReadInt64());
            Price = br.ReadSingle();
            long ticksDate = br.ReadInt64();
            if(ticksDate != -1)
            {
                Date = new DateTime(ticksDate);
            }
            else
            {
                Date = null;
            }
        }

        #endregion
    }
}
