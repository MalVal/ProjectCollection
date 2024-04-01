using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCollectionClassLibrary
{
    public class ElementDateAddedDComparer : IComparer<Element>
    {
        public int Compare(Element elem1, Element elem2)
        {
            return -1 * elem1.DateAdded.CompareTo(elem2.DateAdded);
        }
    }
}
