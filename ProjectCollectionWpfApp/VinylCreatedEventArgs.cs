using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCollectionClassLibrary;

namespace ProjectCollectionWpfApp
{
    public class VinylCreatedEventArgs : EventArgs
    {
        public Vinyl Vinyl { get; set; }

        public VinylCreatedEventArgs(string? name, float price, DateTime? date, string? image, string? genre, string? label, string? country, string? composer)
        {
            Vinyl = new Vinyl(name, price, date, image, genre, label, country, composer);
        }
    }
}
