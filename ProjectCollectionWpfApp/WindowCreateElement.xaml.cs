using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Win32;
using ProjectCollectionClassLibrary;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectCollectionWpfApp
{
    /// <summary>
    /// Interaction logic for WindowCreateElement.xaml
    /// </summary>
    public partial class WindowCreateElement : Window
    {
        #region Events

        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs args);

        public event EventHandler<ComicsCreatedEventArgs> ComicsCreated;
        public event EventHandler<VinylCreatedEventArgs> VinylCreated;

        #endregion

        #region Constructor

        public WindowCreateElement()
        {
            InitializeComponent();
        }

        #endregion

        #region ComboBox function

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem != null)
            {
                string contenuSelectionne = comboBox.SelectedItem.ToString();

                if (contenuSelectionne == "System.Windows.Controls.ComboBoxItem : Comics")
                {
                    SPvinyls.Visibility = Visibility.Hidden;
                    BtnCreateVinyl.Visibility = Visibility.Hidden;
                    SPcomics.Visibility = Visibility.Visible;
                    BtnCreateComics.Visibility = Visibility.Visible;
                }
                else if (contenuSelectionne == "System.Windows.Controls.ComboBoxItem : Vinyls")
                {
                    SPvinyls.Visibility = Visibility.Visible;
                    BtnCreateVinyl.Visibility = Visibility.Visible;
                    SPcomics.Visibility = Visibility.Hidden;
                    BtnCreateComics.Visibility = Visibility.Hidden;
                }
            }
        }

        #endregion

        #region Button create comics

        private void BtnCreateComics_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            string? name, isbn, autor, topic, image;
            float price;
            DateTime? date = null;

            name = ComName.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = null;
            }

            if (string.IsNullOrWhiteSpace(ComPrice.Text))
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

            if (ComDate.SelectedDate == null)
            {
                date = null;
            }
            else
            {
                date = ComDate.SelectedDate;
            }

            if (ComImage.Source == null)
            {
                image = null;
            }
            else
            {
                image = ComImage.Source.ToString();
            }

            isbn = ComIsbn.Text;
            if (string.IsNullOrWhiteSpace(isbn))
            {
                isbn = null;
            }

            autor = ComAutor.Text;
            if (string.IsNullOrWhiteSpace(autor))
            {
                autor = null;
            }

            topic = ComTopic.Text;
            if (string.IsNullOrWhiteSpace(topic))
            {
                topic = null;
            }

            if (error == false)
            {
                ComicsCreated?.Invoke(this, new ComicsCreatedEventArgs(name, price, date, image, isbn, autor, topic));
                ComError.Content = "The comic book was saved successfull !";
            }
            else
            {
                ComError.Content = "The comic book was not saved because some fields are invalid !";
            }
        }
        #endregion

        #region Button create vinyl

        private void BtnCreateVinyl_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            string? name, genre, label, country, composer, image;
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

            if (VinDate.SelectedDate == null)
            {
                date = null;
            }
            else
            {
                date = VinDate.SelectedDate;
            }

            if (VinImage.Source == null)
            {
                image = null;
            }
            else
            {
                image = VinImage.Source.ToString();
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
                VinylCreated?.Invoke(this, new VinylCreatedEventArgs(name, price, date, image, genre, label, country, composer));
                VinError.Content = "The vinyl was saved successfull !";
            }
            else
            {
                VinError.Content = "The vinyl was not saved because some fields are invalid !";
            }
        }

        private void BtnImageVinyl_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Tous les fichiers (*.*)|*.*"; // Filtre pour les images JPG et PNG
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                VinImage.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        private void BtnImageComics_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Tous les fichiers (*.*)|*.*"; // Filtre pour les images JPG et PNG
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                ComImage.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        #endregion
    }
}
