using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
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
using System.IO;
using System;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace ProjectColectionWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables and properties
        private DataList DataList { get; set; }

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            DataList = new DataList();
            DataContext = DataList;

            if (File.Exists(".\\comics.json") && File.Exists(".\\vinyls.json"))
            {
                Load();
            }
        }

        #endregion

        #region Exterior Event functions
        private void WindowCreateElement_ComicsCreated(object sender, ComicsCreatedEventArgs e)
        {
            Comics newComics = new Comics(e.ComicBook.Name, e.ComicBook.Price, e.ComicBook.Date, e.ComicBook.Image, e.ComicBook.Isbn, e.ComicBook.Autor, e.ComicBook.Topic);
            DataList.ComicsList.Add(newComics);
        }

        private void WindowCreateElement_VinylCreated(object sender, VinylCreatedEventArgs e)
        {
            Vinyl newVinyl = new Vinyl(e.Vinyl.Name, e.Vinyl.Price, e.Vinyl.Date, e.Vinyl.Image, e.Vinyl.Genre, e.Vinyl.Label, e.Vinyl.Country, e.Vinyl.Composer);
            DataList.VinylList.Add(newVinyl);
        }
        #endregion

        #region Event functions

        private void BtnImageComics_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers d'images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                if(DataList.CurrentComics != null)
                    DataList.CurrentComics.Image = openFileDialog.FileName;
            }
        }

        private void BtnImageVinyl_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers d'images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                if (DataList.CurrentVinyl != null)
                    DataList.CurrentVinyl.Image = openFileDialog.FileName;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Voulez-vous sauvegarder?", "Quitter?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (result == MessageBoxResult.Yes)
            {
                Save();
            }
        }

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
                if (optionChoisie == "Vinyls")
                {
                    dataGridVinyls.Visibility = Visibility.Visible;
                    GridVinyls.Visibility = Visibility.Visible;
                    dataGridComics.Visibility = Visibility.Hidden;
                    GridComics.Visibility = Visibility.Hidden;
                }
                else if (optionChoisie == "Comics")
                {
                    dataGridVinyls.Visibility = Visibility.Hidden;
                    GridVinyls.Visibility = Visibility.Hidden;
                    dataGridComics.Visibility = Visibility.Visible;
                    GridComics.Visibility = Visibility.Visible;
                }
            }
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            this.Save();
        }

        private void MenuItem_ResetComics_Click(object sender, RoutedEventArgs e)
        {
            this.DataList.ComicsList.Clear();
        }

        private void MenuItem_ResetVinyls_Click(object sender, RoutedEventArgs e)
        {
            this.DataList.VinylList.Clear();
        }

        #endregion

        #region Save + Load

        private void Save()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            File.WriteAllText(".\\comics.json", JsonSerializer.Serialize(DataList.ComicsList, options));
            File.WriteAllText(".\\vinyls.json", JsonSerializer.Serialize(DataList.VinylList, options));
        }

        private void Load()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            DataList.ComicsList = JsonSerializer.Deserialize<ObservableCollection<Comics>>(File.ReadAllText(".\\comics.json"), options)!;
            DataList.VinylList = JsonSerializer.Deserialize<ObservableCollection<Vinyl>>(File.ReadAllText(".\\vinyls.json"), options)!;
        }

        #endregion
    }
}