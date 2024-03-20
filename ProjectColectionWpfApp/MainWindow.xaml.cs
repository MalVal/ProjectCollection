using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
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
using System.Xml.Linq;
using ProjectCollectionClassLibrary;
using ProjectCollectionWpfApp;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectColectionWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataList DataList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataList = new DataList();
            DataContext = DataList;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Voullez-vous quitter?", "Quitter?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        #region Event functions
        private void WindowCreateElement_ComicsCreated(object sender, ComicsCreatedEventArgs e)
        {
            Comics newComics = new Comics(e.ComicBook.Name, e.ComicBook.Price, e.ComicBook.Date, e.ComicBook.Isbn, e.ComicBook.Autor, e.ComicBook.Topic);
            DataList.ComicsList.Add(newComics);
        }

        private void WindowCreateElement_VinylCreated(object sender, VinylCreatedEventArgs e)
        {
            Vinyl newVinyl = new Vinyl(e.Vinyl.Name, e.Vinyl.Price, e.Vinyl.Date, e.Vinyl.Genre, e.Vinyl.Label, e.Vinyl.Country, e.Vinyl.Composer);
            DataList.VinylList.Add(newVinyl);
        }
        #endregion

        private void BtnCreateElement_Click(object sender, RoutedEventArgs e)
        {
            WindowCreateElement windowCreateElement = new WindowCreateElement();

            windowCreateElement.ComicsCreated += WindowCreateElement_ComicsCreated;
            windowCreateElement.VinylCreated += WindowCreateElement_VinylCreated;

            windowCreateElement.ShowDialog();
        }

        private void RadioButtonElement_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButtonElement = sender as RadioButton;

            if (radioButtonElement.IsChecked == true)
            {
                string optionChoisie = radioButtonElement.Content.ToString();
                if(optionChoisie == "Vinyls")
                {
                    dataGrid.ItemsSource = DataList.VinylList;
                    dataGrid.SelectedItem = DataList.CurrentVinyl;
                }
                else if(optionChoisie == "Comics")
                { 
                    dataGrid.ItemsSource = DataList.ComicsList;
                    dataGrid.SelectedItem = DataList.CurrentComics;
                }
            }
        }
    }
}