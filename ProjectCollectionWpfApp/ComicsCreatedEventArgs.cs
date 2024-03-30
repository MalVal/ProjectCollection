using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCollectionClassLibrary;

namespace ProjectCollectionWpfApp
{
    public class ComicsCreatedEventArgs : EventArgs
    {
        public Comics ComicBook { get; set; }

        public ComicsCreatedEventArgs(string? name, float price, DateTime? date, string? image, string? isbn, string? autor, string? topic)
        {
            ComicBook = new Comics(name, price, date, image, isbn, autor, topic);
        }
    }
}
