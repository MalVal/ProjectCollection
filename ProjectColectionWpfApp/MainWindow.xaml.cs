using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectCollectionClassLibrary;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectColectionWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Element> _listElem;

        public List<Element> ListElem
        {
            get { return _listElem; }
            set { _listElem = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            ListElem = new List<Element>();
        }

        private void BtnCreateComics_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            string? name, isbn, autor, topic;
            float price;
            DateTime? date = null;

            name = ComName.Text;
            if(string.IsNullOrWhiteSpace(name))
            {
                name = null;
            }
            
            if(string.IsNullOrWhiteSpace(ComPrice.Text))
            {
                price = 0;
            }
            else
            {
                if (!float.TryParse(ComPrice.Text, out price))
                {
                    error = true;
                }
            }

            if(string.IsNullOrWhiteSpace(ComDate.Text))
            {
                date = null;
            }
            else
            {
                DateTime parsedDate;
                if (!DateTime.TryParse(ComDate.Text, out parsedDate))
                {
                    error = true;
                }
                else
                {
                    date = parsedDate;
                }
            }

            isbn = ComIsbn.Text;
            if (string.IsNullOrWhiteSpace(isbn))
            {
                isbn = null;
            }

            autor = ComName.Text;
            if (string.IsNullOrWhiteSpace(autor))
            {
                autor = null;
            }

            topic = ComName.Text;
            if (string.IsNullOrWhiteSpace(topic))
            {
                topic = null;
            }

            if (error == false)
            {
                Comics newComics = new(name, price, date, isbn, autor, topic);
                ListElem.Add(newComics);
                ComError.Content = "The comic book was saved successfull !";
            }
            else
            {
                ComError.Content = "The comic book was not saved because some fields are invalid !";
            }
        }

        private void BtnCreateVinyl_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            string? name, genre, label, country, composer;
            float price;
            DateTime? date = null;

            name = VinName.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = null;
            }

            if (string.IsNullOrWhiteSpace(VinPrice.Text))
            {
                price = 0;
            }
            else
            {
                if (!float.TryParse(VinPrice.Text, out price))
                {
                    error = true;
                }
            }

            if (string.IsNullOrWhiteSpace(VinDate.Text))
            {
                date = null;
            }
            else
            {
                DateTime parsedDate;
                if (!DateTime.TryParse(VinDate.Text, out parsedDate))
                {
                    error = true;
                }
                else
                {
                    date = parsedDate;
                }
            }

            genre = VinGenre.Text;
            if (string.IsNullOrWhiteSpace(genre))
            {
                genre = null;
            }

            label = VinLabel.Text;
            if (string.IsNullOrWhiteSpace(label))
            {
                label = null;
            }

            country = VinCountry.Text;
            if (string.IsNullOrWhiteSpace(country))
            {
                country = null;
            }

            composer = VinComposer.Text;
            if (string.IsNullOrWhiteSpace(composer))
            {
                composer = null;
            }

            if (error == false)
            {
                Vinyl newComics = new(name, price, date, genre, label, country, composer);
                ListElem.Add(newComics);
                VinError.Content = "The vinyl was saved successfull !";
            }
            else
            {
                VinError.Content = "The vinyl was not saved because some fields are invalid !";
            }
        }
    }
}