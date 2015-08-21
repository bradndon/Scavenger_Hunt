using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScavengerHunt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int num = 0;
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.Resources["Color400"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF7043"));

            Application.Current.Resources["Color500"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF5722"));
            Application.Current.Resources["Color600"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4511E"));
            Application.Current.Resources["Color700"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E64A19"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double dWidth = -1;
            double dHeight = -1;
            FrameworkElement pnlClient = this.Content as FrameworkElement;
            if (pnlClient != null)
            {
                dWidth = pnlClient.ActualWidth + 10;
                dHeight = pnlClient.ActualHeight;
                appBar1.Width = dWidth;
                appBar2.Width = dWidth;
                scroller.Height = dHeight - 95;
            }
        }

   
        private void openFile(object sender, RoutedEventArgs e)
        {
            
            var dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Filles (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
            }



        }

        private void addRow(object sender, RoutedEventArgs e)
        {
            //increase number of rows
            //num++;

            var newCanvas = new Canvas();
            newCanvas.Width = 1280;
            newCanvas.Height = 64;
            stack.Children.Add(newCanvas);


            //create button dynamically
            var newBtn = new Button();

            //Text on button
            newBtn.Content = "OPEN FILE";

            //Properties of button
            newBtn.Width = 100;
            newBtn.Height = 35;
            newBtn.Style = (Style)this.Resources["GoogleGreyButton"];

            //Location on Screen
            Canvas.SetTop(newBtn, 0);
            Canvas.SetLeft(newBtn, 370d);

            newBtn.Click += openFile;

            //Add to Screen
            newCanvas.Children.Add(newBtn);

            //create Label dynamically
            var dynamicLabel = new Label();

            //Text on Label
            dynamicLabel.Content = "Row " + num;

            //Properties of label
            dynamicLabel.Width = 100;
            dynamicLabel.Height = 30;
            dynamicLabel.Foreground = new SolidColorBrush(Colors.Black);

            //Location on Screen
            Canvas.SetTop(dynamicLabel, 0);
            Canvas.SetLeft(dynamicLabel, 250d);

            //Add to Screen
            newCanvas.Children.Add(dynamicLabel);
        }

        private void Size_Changed(object sender, SizeChangedEventArgs e)
        {
            double dWidth = -1;
            double dHeight = -1;
            FrameworkElement pnlClient = this.Content as FrameworkElement;
            if (pnlClient != null)
            {
                dWidth = pnlClient.ActualWidth + 10;
                dHeight = pnlClient.ActualHeight;
                appBar1.Width = dWidth;
                appBar2.Width = dWidth;
                scroller.Height = dHeight - 95;
            }
        }
    }
}
