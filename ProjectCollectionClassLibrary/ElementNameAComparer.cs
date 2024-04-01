using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCollectionClassLibrary
{
    public class ElementNameAComparer : IComparer<Element>
    {
        public int Compare(Element elem1, Element elem2)
        {
            return elem1.Name.CompareTo(elem2.Name);
        }
    }
}
