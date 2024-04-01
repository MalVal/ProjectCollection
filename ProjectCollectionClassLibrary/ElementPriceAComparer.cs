using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCollectionClassLibrary
{
    public class ElementPriceAComparer : IComparer<Element>
    {
        public int Compare(Element elem1, Element elem2)
        {
            return elem1.Price.CompareTo(elem2.Price);
        }
    }
}
