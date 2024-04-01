using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using ProjectCollectionClassLibrary;
using System.IO;
using System.Xml.Serialization;

namespace ProjectCollectionWpfApp
{
    /// <summary>
    /// Interaction logic for WindowOptions.xaml
    /// </summary>
    public partial class WindowOptions : Window
    {
        #region Events
        public delegate void EventHandler(OptionChangedEventArgs options);

        public event EventHandler OptionChanged;
        #endregion

        #region Variables and properties

        DataOptions DataOptions { get; set; }

        #endregion

        #region Constructor
        public WindowOptions()
        {
            InitializeComponent();

            DataOptions = new DataOptions();

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ProjectCollection")!;
            if (registryKey != null)
            {
                this.DataOptions.ComicsPathL = "Comics path : " + (string)registryKey.GetValue("comicsPath", 0);
                this.DataOptions.VinylPathL = "Vinyl path : " + (string)registryKey.GetValue("vinylPath", 0);
                registryKey.Close();

                DataContext = this.DataOptions;
            }
        }
        #endregion

        #region Event functions
        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color? selectedColor = colorPicker.SelectedColor;
            if (selectedColor.HasValue)
            {
                Color color = selectedColor.Value;
                OptionChanged?.Invoke(new OptionChangedEventArgs(color, null, null));
            }
        }

        private void ComicsPath_Click(object sender, RoutedEventArgs e)
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

                this.DataOptions.ComicsPathL = "Comics path : " + fileName;
                OptionChanged?.Invoke(new OptionChangedEventArgs(null, fileName, null));
            }
        }

        private void VinylPath_Click(object sender, RoutedEventArgs e)
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

                this.DataOptions.VinylPathL = "Vinyl path : " + fileName;
                OptionChanged?.Invoke(new OptionChangedEventArgs(null, null, fileName));
            }
        }
        #endregion
    }

    public class OptionChangedEventArgs : EventArgs
    {
        public Color? Color { get; set; }
        public string? ComicsPath { get; set; }
        public string? VinylPath { get; set; }

        public OptionChangedEventArgs(Color? color, string? comicsPath, string? vinylPath)
        {
            Color = color;
            ComicsPath = comicsPath;
            VinylPath = vinylPath;
        }
    }
}
