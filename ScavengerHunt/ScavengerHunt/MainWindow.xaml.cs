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
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ScavengerHunt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> imgList;
        List<Button> btnList;
        List<Label> lblList;
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
            changeSize();
            btnList = new List<Button>();
            imgList = new List<string>();
            lblList = new List<Label>();
        }

   
        private void openFile(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(btnList.IndexOf((Button)sender));


            var dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (*.png)|*.png|JPEG Filles (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                imgList[btnList.IndexOf((Button)sender)] = filename;
                lblList[btnList.IndexOf((Button)sender)].Content = filename;
            }



        }

        private void addRow(object sender, RoutedEventArgs e)
        {
            //increase number of rows
            num++;
            imgList.Add(null);
            imgList.Add(null);
           
            var newCanvas = new Canvas();
            newCanvas.Width = 940;
            newCanvas.Height = 72;
            stack.Children.Add(newCanvas);


            //create button dynamically
            var newBtn1 = new Button();

            //Text on button
            newBtn1.Content = "OPEN FILE";

            //Properties of button
            newBtn1.Width = 100;
            newBtn1.Height = 36;
            newBtn1.Style = (Style)this.Resources["MaterialAccentButton"];
            

            //Location on Screen
            Canvas.SetTop(newBtn1, 18d);
            Canvas.SetLeft(newBtn1, 172d);

            newBtn1.Click += openFile;

            //Add to Screen
            newCanvas.Children.Add(newBtn1);
            btnList.Add(newBtn1);

            var newBtn = new Button();

            //Text on button
            newBtn.Content = "OPEN FILE";

            //Properties of button
            newBtn.Width = 100;
            newBtn.Height = 36;
            newBtn.Style = (Style)this.Resources["MaterialAccentButton"];


            //Location on Screen
            Canvas.SetTop(newBtn, 18d);
            Canvas.SetLeft(newBtn, 444d);

            newBtn.Click += openFile;

            //Add to Screen
            newCanvas.Children.Add(newBtn);
            btnList.Add(newBtn);


            //create Label dynamically
            var dynamicLabel = new Label();

            //Text on Label
            dynamicLabel.Content = "Question " + num + " Image Path:";

            //Properties of label
            dynamicLabel.Width = 140;
            dynamicLabel.Height = 36;
            dynamicLabel.Foreground = new SolidColorBrush(Colors.Black);

            //Location on Screen
            Canvas.SetTop(dynamicLabel, 20d);
            Canvas.SetLeft(dynamicLabel, 16d);

            //Add to Screen
            newCanvas.Children.Add(dynamicLabel);
            lblList.Add(dynamicLabel);

            //create Label dynamically
            var dynamicLabel2 = new Label();

            //Text on Label
            dynamicLabel2.Content = "Answer " + num + " Image Path:";

            //Properties of label
            dynamicLabel2.Width = 140;
            dynamicLabel2.Height = 36;
            dynamicLabel2.Foreground = new SolidColorBrush(Colors.Black);

            //Location on Screen
            Canvas.SetTop(dynamicLabel2, 20d);
            Canvas.SetLeft(dynamicLabel2, 288d);

            //Add to Screen
            newCanvas.Children.Add(dynamicLabel2);
            lblList.Add(dynamicLabel2);
            
        }

        private void Size_Changed(object sender, SizeChangedEventArgs e)
        {
            changeSize();
            
        }

        private void changeSize()
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
                scroller.Height = dHeight - 79;
                menu.Height = dHeight - 79;
                Canvas.SetTop(openButton, dHeight - 54);
                Canvas.SetLeft(openButton, dWidth - 64);
            }
        }

        private void makeButton_Click(object sender, RoutedEventArgs e)
        {
            iTextSharp.text.Image[] questions = new iTextSharp.text.Image[imgList.Count / 2];
            iTextSharp.text.Image[] answers = new iTextSharp.text.Image[imgList.Count/ 2];
            int i = 0;
            for (i = 0; i < btnList.Count() ; i ++)
            {
                string s = imgList[i];
                if (s != null)
                {
                    if (i %2 == 0)
                    {
                        questions[i/2] = iTextSharp.text.Image.GetInstance(s);
                        Trace.WriteLine("Question: " + s);
                    } else
                    {
                        answers[i/2] = iTextSharp.text.Image.GetInstance(s);
                        Trace.WriteLine("Answer: " + s);
                    }
                    
                    
                } else
                {
                    Trace.WriteLine("NOT READY");
                    break;
                }
            }
            if (i == btnList.Count())
            {
                Scavenger pdf = new Scavenger(questions, answers);
            }
        }
        
    }
}
