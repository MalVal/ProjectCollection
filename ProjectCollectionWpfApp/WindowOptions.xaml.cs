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

namespace ProjectCollectionWpfApp
{
    /// <summary>
    /// Interaction logic for WindowOptions.xaml
    /// </summary>
    public partial class WindowOptions : Window
    {
        public delegate void EventHandler(OptionChangedEventArgs options);

        public event EventHandler OptionChanged;

        public WindowOptions()
        {
            InitializeComponent();
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color? selectedColor = colorPicker.SelectedColor;
            if (selectedColor.HasValue)
            {
                Color color = selectedColor.Value;
                OptionChanged?.Invoke(new OptionChangedEventArgs(color));
            }
        }
    }

    public class OptionChangedEventArgs : EventArgs
    {
        public Color Color { get; set; }

        public OptionChangedEventArgs(Color color)
        {
            Color = color;
        }
    }
}
