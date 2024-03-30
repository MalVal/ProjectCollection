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
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using System;

namespace ProjectCollectionWpfApp
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

            if (File.Exists(".\\comics.json"))
            {
                this.LoadComics();
            }

            if (File.Exists(".\\vinyls.json"))
            {
                this.LoadVinyls();
            }
        }

        #endregion

        #region Exterior Event functions
        private void WindowCreateElement_ComicsCreated(object sender, ComicsCreatedEventArgs e)
        {
            Comics newComics = new Comics(e.ComicBook.Name, e.ComicBook.Price, e.ComicBook.Date, e.ComicBook.Image, e.ComicBook.Isbn, e.ComicBook.Autor, e.ComicBook.Topic);
            DataList.AddComics(newComics);
        }

        private void WindowCreateElement_VinylCreated(object sender, VinylCreatedEventArgs e)
        {
            Vinyl newVinyl = new Vinyl(e.Vinyl.Name, e.Vinyl.Price, e.Vinyl.Date, e.Vinyl.Image, e.Vinyl.Genre, e.Vinyl.Label, e.Vinyl.Country, e.Vinyl.Composer);
            DataList.AddVinyl(newVinyl);
        }
        #endregion

        #region Event functions

        private void BtnImageComics_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers d'images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                if (DataList.CurrentComics != null)
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

        private void BtnDeleteComics_Click(object sender, RoutedEventArgs e)
        {
            DataList.DeleteComics(DataList.CurrentComics);
            DataList.CurrentComics = null;
        }

        private void BtnDeleteVinyl_Click(object sender, RoutedEventArgs e)
        {
            DataList.DeleteVinyls(DataList.CurrentVinyl);
            DataList.CurrentVinyl = null;
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
                this.SaveComics();
                this.SaveVinyls();
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
                    dataGridElements.Visibility = Visibility.Hidden;
                    dataGridVinyls.Visibility = Visibility.Visible;
                    dataGridComics.Visibility = Visibility.Hidden;
                    GridComics.Visibility = Visibility.Hidden;
                    DataList.CurrentComics = null;
                    DataList.CurrentElement = null;
                }
                else if (optionChoisie == "Comics")
                {
                    dataGridElements.Visibility = Visibility.Hidden;
                    dataGridVinyls.Visibility = Visibility.Hidden;
                    GridVinyls.Visibility = Visibility.Hidden;
                    dataGridComics.Visibility = Visibility.Visible;
                    DataList.CurrentVinyl = null;
                    DataList.CurrentElement = null;
                }
                else if (optionChoisie == "All")
                {
                    dataGridElements.Visibility = Visibility.Visible;
                    dataGridVinyls.Visibility = Visibility.Hidden;
                    dataGridComics.Visibility = Visibility.Hidden;
                    GridComics.Visibility = Visibility.Hidden;
                    GridVinyls.Visibility = Visibility.Hidden;
                    DataList.CurrentComics = null;
                    DataList.CurrentVinyl = null;
                }
            }
        }

        private void dataGridVinyls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataList.CurrentVinyl != null)
            {
                GridVinyls.Visibility = Visibility.Visible;
            }
            else
            {
                GridVinyls.Visibility = Visibility.Hidden;
            }
        }

        private void dataGridComics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataList.CurrentComics != null)
            {
                GridComics.Visibility = Visibility.Visible;
            }
            else
            {
                GridComics.Visibility = Visibility.Hidden;
            }
        }

        private void dataGridElements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataList.CurrentElement != null)
            {
                if (DataList.CurrentElement is Comics)
                {
                    GridVinyls.Visibility = Visibility.Hidden;
                    DataList.CurrentComics = (Comics)DataList.CurrentElement;
                    GridComics.Visibility = Visibility.Visible;
                }
                else if (DataList.CurrentElement is Vinyl)
                {
                    GridComics.Visibility = Visibility.Hidden;
                    DataList.CurrentVinyl = (Vinyl)DataList.CurrentElement;
                    GridVinyls.Visibility = Visibility.Visible;
                }
            }
            else
            {
                GridVinyls.Visibility = Visibility.Hidden;
                GridComics.Visibility = Visibility.Hidden;
                DataList.CurrentComics = null;
                DataList.CurrentVinyl = null;
            }
        }

        #endregion

        #region Event Menu

        private void MenuItem_Options_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_SaveComics_Click(object sender, RoutedEventArgs e)
        {
            this.SaveComics();

            MessageBox.Show("Save successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_SaveVinyls_Click(object sender, RoutedEventArgs e)
        {
            this.SaveVinyls();

            MessageBox.Show("Save successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_SaveAll_Click(object sender, RoutedEventArgs e)
        {
            this.SaveComics();
            this.SaveVinyls();

            MessageBox.Show("Save successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_ResetComics_Click(object sender, RoutedEventArgs e)
        {
            foreach (Element elem in this.DataList.ComicsList)
            {
                this.DataList.ElementsList.Remove(elem);
            }
            this.DataList.ComicsList.Clear();

            MessageBox.Show("Reset successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_ResetVinyls_Click(object sender, RoutedEventArgs e)
        {
            foreach (Element elem in this.DataList.VinylList)
            {
                this.DataList.ElementsList.Remove(elem);
            }
            this.DataList.VinylList.Clear();

            MessageBox.Show("Reset successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_ResetAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Element elem in this.DataList.ComicsList)
            {
                this.DataList.ElementsList.Remove(elem);
            }
            this.DataList.ComicsList.Clear();

            foreach (Element elem in this.DataList.VinylList)
            {
                this.DataList.ElementsList.Remove(elem);
            }
            this.DataList.VinylList.Clear();

            MessageBox.Show("Reset successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region Save + Load

        private void SaveComics()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            File.WriteAllText(".\\comics.json", JsonSerializer.Serialize(DataList.ComicsList, options));
        }

        private void SaveVinyls()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            File.WriteAllText(".\\vinyls.json", JsonSerializer.Serialize(DataList.VinylList, options));
        }

        private void LoadComics()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            DataList.ComicsList = JsonSerializer.Deserialize<ObservableCollection<Comics>>(File.ReadAllText(".\\comics.json"), options)!;
            foreach (Comics comics in DataList.ComicsList)
            {
                DataList.ElementsList.Add(comics);
            }
        }

        private void LoadVinyls()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            DataList.VinylList = JsonSerializer.Deserialize<ObservableCollection<Vinyl>>(File.ReadAllText(".\\vinyls.json"), options)!;
            foreach (Vinyl vinyl in DataList.VinylList)
            {
                DataList.ElementsList.Add(vinyl);
            }
        }

        #endregion

        #region Import

        private void MenuItem_ImportComicBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Fichiers xml (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == true)
                {
                    StreamReader myReader = new StreamReader(openFileDialog.FileName);

                    XmlSerializer xmlSerializerRead = new XmlSerializer(typeof(Comics));

                    Comics c = (Comics)xmlSerializerRead.Deserialize(myReader)!;

                    DataList.ComicsList.Add(c);
                    DataList.ElementsList.Add(c);

                    myReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the file : " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_ImportVinyl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Fichiers xml (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == true)
                {
                    StreamReader myReader = new StreamReader(openFileDialog.FileName);

                    XmlSerializer xmlSerializerRead = new XmlSerializer(typeof(Vinyl));

                    Vinyl c = (Vinyl)xmlSerializerRead.Deserialize(myReader)!;

                    DataList.VinylList.Add(c);
                    DataList.ElementsList.Add(c);

                    myReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the file : " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Export

        private void MenuItem_ExportComicBook_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataList.CurrentComics != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Fichiers xml (*.xml)|*.xml";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                bool? result = saveFileDialog.ShowDialog();

                if (result == true)
                {
                    string fileName = saveFileDialog.FileName;

                    if (!fileName.ToLower().EndsWith(".xml"))
                    {
                        fileName += ".xml";
                    }

                    StreamWriter myWriter = new StreamWriter(fileName);

                    XmlSerializer xmlSerializerWrite = new XmlSerializer(typeof(Comics));

                    xmlSerializerWrite.Serialize(myWriter, this.DataList.CurrentComics);

                    myWriter.Close();
                }
            }
            else
            {
                MessageBox.Show("You must select a comic book!", "Error", MessageBoxButton.OK);
            }
        }

        private void MenuItem_ExportVinyl_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataList.CurrentVinyl != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Tous les fichiers (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                bool? result = saveFileDialog.ShowDialog();

                if (result == true)
                {
                    string fileName = saveFileDialog.FileName;

                    if (!fileName.ToLower().EndsWith(".xml"))
                    {
                        fileName += ".xml";
                    }

                    StreamWriter myWriter = new StreamWriter(fileName);

                    XmlSerializer xmlSerializerWrite = new XmlSerializer(typeof(Vinyl));

                    xmlSerializerWrite.Serialize(myWriter, this.DataList.CurrentVinyl);

                    myWriter.Close();
                }
            }
            else
            {
                MessageBox.Show("You must select a vinyl!", "Error", MessageBoxButton.OK);
            }
        }

        #endregion
    }
}