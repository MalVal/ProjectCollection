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
using System.Windows.Media.Animation;

namespace ProjectCollectionWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables and properties
        private DataList DataList { get; set; }

        private Color ColorBackground { get; set; }

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            DataList = new DataList();
            DataContext = DataList;

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ProjectCollection")!;
            if (registryKey != null)
            {
                int RGBvalue = (int)registryKey.GetValue("color", 0);
                DataList.ComicsPath = (string)registryKey.GetValue("comicsPath", 0);
                DataList.VinylPath = (string)registryKey.GetValue("vinylPath", 0);

                registryKey.Close();

                byte r = (byte)((RGBvalue & 0xFF0000) >> 16);
                byte g = (byte)((RGBvalue & 0x00FF00) >> 8);
                byte b = (byte)(RGBvalue & 0x0000FF);

                ColorBackground = Color.FromRgb(r, g, b);
            }
            else
            {
                registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ProjectCollection");

                if (registryKey != null)
                {
                    Color color = Colors.Aqua;
                    int RGBvalue = color.R << 16 | color.G << 8 | color.B;

                    registryKey.SetValue("color", RGBvalue);
                    registryKey.SetValue("comicsPath", ".\\comics.json");
                    registryKey.SetValue("vinylPath", ".\\vinyls.json");

                    registryKey.Close();

                    ColorBackground = color;
                    DataList.ComicsPath = ".\\comics.json";
                    DataList.VinylPath = ".\\vinyls.json";
                }
            }
            LeftBox.Background = new SolidColorBrush(ColorBackground);

            if (File.Exists(DataList.ComicsPath))
            {
                this.LoadComics(DataList.ComicsPath);
            }

            if (File.Exists(DataList.VinylPath))
            {
                this.LoadVinyls(DataList.VinylPath);
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

        private void WindowOption_OptionChanged(OptionChangedEventArgs options)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ProjectCollection", true)!;
            if (registryKey != null)
            {
                if(options.Color.HasValue)
                {
                    Color color = options.Color.Value;
                    int RGBvalue = color.R << 16 | color.G << 8 | color.B;
                    registryKey.SetValue("color", RGBvalue);
                    ColorBackground = color;
                    LeftBox.Background = new SolidColorBrush(ColorBackground);
                }

                if(options.ComicsPath != null)
                {
                    registryKey.SetValue("comicsPath", options.ComicsPath);
                    DataList.ComicsPath = options.ComicsPath;
                }

                if(options.VinylPath != null)
                {
                    registryKey.SetValue("vinylPath", options.VinylPath);
                    DataList.VinylPath = options.VinylPath;
                }

                registryKey.Close();
            }
        }
        #endregion

        #region Event functions

        private void BtnImageComics_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                if (DataList.CurrentComics != null)
                {
                    DataList.CurrentComics.Image = openFileDialog.FileName;
                    ImgComics.Source = new BitmapImage(new Uri(DataList.CurrentComics.Image));
                }
            }
        }

        private void BtnImageVinyl_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                if (DataList.CurrentVinyl != null)
                {
                    DataList.CurrentVinyl.Image = openFileDialog.FileName;
                    ImgVinyl.Source = new BitmapImage(new Uri(DataList.CurrentVinyl.Image));
                }
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
            var result = MessageBox.Show("Do you want to save ?", "Exit ?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (result == MessageBoxResult.Yes)
            {
                this.SaveComics(DataList.ComicsPath);
                this.SaveVinyls(DataList.VinylPath);
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

        private void RadioButtonOrder_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButtonElement = sender as RadioButton;

            if (radioButtonElement.IsChecked == true)
            {
                string optionChoisie = radioButtonElement.Content.ToString();
                if(optionChoisie == "Name ascending")
                {
                    DataList.OrderList(1);
                }
                else if(optionChoisie == "Name descending")
                {
                    DataList.OrderList(2);
                }
                else if(optionChoisie == "Price ascending")
                {
                    DataList.OrderList(3);
                }
                else if(optionChoisie == "Price descending")
                {
                    DataList.OrderList(4);
                }
                else if(optionChoisie == "Date added ascending")
                {
                    DataList.OrderList(5);
                }
                else if(optionChoisie == "Date added descending")
                {
                    DataList.OrderList(6);
                }
            }
        }

        private void dataGridVinyls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataList.CurrentVinyl != null)
            {
                ImgVinyl.Source = new BitmapImage(new Uri(DataList.CurrentVinyl.Image));
                GridVinyls.Visibility = Visibility.Visible;;
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
                ImgComics.Source = new BitmapImage(new Uri(DataList.CurrentComics.Image));
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
            WindowOptions windowOptions = new WindowOptions();
            windowOptions.OptionChanged += WindowOption_OptionChanged;
            windowOptions.ShowDialog();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_SaveComics_Click(object sender, RoutedEventArgs e)
        {
            this.SaveComics(DataList.ComicsPath);

            MessageBox.Show("Save successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_SaveVinyls_Click(object sender, RoutedEventArgs e)
        {
            this.SaveVinyls(DataList.VinylPath);

            MessageBox.Show("Save successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_SaveAll_Click(object sender, RoutedEventArgs e)
        {
            this.SaveComics(DataList.ComicsPath);
            this.SaveVinyls(DataList.VinylPath);

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

        #region Save

        private void SaveComics(string path)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            File.WriteAllText(path, JsonSerializer.Serialize(DataList.ComicsList, options));
        }

        private void SaveVinyls(string path)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            File.WriteAllText(path, JsonSerializer.Serialize(DataList.VinylList, options));
        }

        #endregion

        #region Load

        private void LoadComics(string path = ".\\comics.json")
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            DataList.ComicsList = JsonSerializer.Deserialize<ObservableCollection<Comics>>(File.ReadAllText(path), options)!;
            foreach (Comics comics in DataList.ComicsList)
            {
                DataList.ElementsList.Add(comics);
            }
        }

        private void LoadVinyls(string path = ".\\vinyls.json")
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            DataList.VinylList = JsonSerializer.Deserialize<ObservableCollection<Vinyl>>(File.ReadAllText(path), options)!;
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
                openFileDialog.Filter = "xml files (*.xml)|*.xml";
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
                openFileDialog.Filter = "xml files (*.xml)|*.xml";
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

        private void MenuItem_ImportListComics_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "json files (*.json)|*.json";
                if (openFileDialog.ShowDialog() == true)
                {
                    string filename = openFileDialog.FileName;
                    JsonSerializerOptions options = new()
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true
                    };

                    ObservableCollection<Comics> list = JsonSerializer.Deserialize<ObservableCollection<Comics>>(File.ReadAllText(filename), options)!;
                    foreach (Comics comics in list)
                    {
                        DataList.ElementsList.Add(comics);
                        DataList.ComicsList.Add(comics);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the file : " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_ImportListVinyls_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "json files (*.json)|*.json";
                if (openFileDialog.ShowDialog() == true)
                {
                    string filename = openFileDialog.FileName;
                    JsonSerializerOptions options = new()
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true
                    };

                    ObservableCollection<Vinyl> list = JsonSerializer.Deserialize<ObservableCollection<Vinyl>>(File.ReadAllText(filename), options)!;
                    foreach (Vinyl vinyl in list)
                    {
                        DataList.ElementsList.Add(vinyl);
                        DataList.VinylList.Add(vinyl);
                    }
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
                saveFileDialog.Filter = "xml files (*.xml)|*.xml";
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
                saveFileDialog.Filter = "xml files (*.xml)|*.xml";
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

        private void MenuItem_ExportAllComics_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "json files (*.json)|*.json";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string fileName = saveFileDialog.FileName;

                if (!fileName.ToLower().EndsWith(".json"))
                {
                    fileName += ".json";
                }
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                File.WriteAllText(fileName, JsonSerializer.Serialize(DataList.ComicsList, options));
            }
        }

        private void MenuItem_ExportAllVinyls_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "json files (*.json)|*.json";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string fileName = saveFileDialog.FileName;

                if (!fileName.ToLower().EndsWith(".json"))
                {
                    fileName += ".json";
                }
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                File.WriteAllText(fileName, JsonSerializer.Serialize(DataList.VinylList, options));
            }
        }

        #endregion
    }
}