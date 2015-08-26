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
        List<TextBox> lblList;
        int num = 0;
        public MainWindow()
        {
            InitializeComponent();
           
            Application.Current.Resources["Color400"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF7043"));
            Application.Current.Resources["Color500"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF5722"));
            Application.Current.Resources["Color600"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4511E"));
            Application.Current.Resources["Color700"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E64A19"));
            Application.Current.Resources["ColorA500"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2196F3"));
            Application.Current.Resources["ColorA600"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#1E88E5"));
            Application.Current.Resources["ColorA700"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#1976D2"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            changeSize();
            btnList = new List<Button>();
            imgList = new List<string>();
            lblList = new List<TextBox>();
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
                lblList[btnList.IndexOf((Button)sender)].Text = filename;
                lblList[btnList.IndexOf((Button)sender)].Focus();
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
            Canvas.SetLeft(newBtn1, 202d);

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
            Canvas.SetLeft(newBtn, 504d);

            newBtn.Click += openFile;

            //Add to Screen
            newCanvas.Children.Add(newBtn);
            btnList.Add(newBtn);

            //create Label dynamically
            var dynamicText = new TextBox();

            //Properties of label
            dynamicText.Width = 170;
            dynamicText.Height = 32;
            dynamicText.FontSize = 16;
            dynamicText.Foreground = new SolidColorBrush(Colors.Black);
            dynamicText.TextChanged += textChanged;

            //Location on Screen
            Canvas.SetTop(dynamicText, 18d);
            Canvas.SetLeft(dynamicText, 16d);

            //Add to Screen
            newCanvas.Children.Add(dynamicText);
           
            lblList.Add(dynamicText);

            //create Label dynamically
            var dynamicText2 = new TextBox();

            //Text on Label
            

            //Properties of label
            dynamicText2.Width = 170;
            dynamicText2.Height = 32;
            dynamicText2.FontSize = 16;
            dynamicText2.Foreground = new SolidColorBrush(Colors.Black);
            dynamicText2.TextChanged += textChanged;

            //Location on Screen
            Canvas.SetTop(dynamicText2, 18d);
            Canvas.SetLeft(dynamicText2, 318d);

            //Add to Screen
            newCanvas.Children.Add(dynamicText2);
            lblList.Add(dynamicText2);

            //Create Watermarks for textBoxes

            //create TextBlock Dynamically
            var dynamicBlock = new TextBlock();

            //Properties of TextBlock
            dynamicBlock.Text = "Answer " + num + " Image Path";
            dynamicBlock.Width = 170;
            dynamicBlock.Height = 32;
            dynamicBlock.FontSize = 16;
            dynamicBlock.Foreground = new SolidColorBrush(Colors.DarkGray);
            dynamicBlock.IsHitTestVisible = false;

            //Create Watermark Style
            Style style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.VisibilityProperty, Visibility.Collapsed));

            //The trigger to make it visible
            var trigger = new DataTrigger();
            trigger.Binding = new Binding("Text") { Source = dynamicText2 };
            trigger.Value = "";
            trigger.Setters.Add(new Setter(TextBlock.VisibilityProperty, Visibility.Visible));
            style.Triggers.Add(trigger);

            //Set the style
            dynamicBlock.Style = style;

            //Location on Screen (Same as dynamicText2)
            Canvas.SetTop(dynamicBlock, 18d);
            Canvas.SetLeft(dynamicBlock, 318d);

            //Add to screen
            newCanvas.Children.Add(dynamicBlock);

            //create TextBlock Dynamically
            var dynamicBlock2 = new TextBlock();

            //Properties of TextBlock
            dynamicBlock2.Text = "Question " + num + " Image Path";
            dynamicBlock2.Width = 170;
            dynamicBlock2.Height = 32;
            dynamicBlock2.FontSize = 16;
            dynamicBlock2.Foreground = new SolidColorBrush(Colors.DarkGray);
            dynamicBlock2.IsHitTestVisible = false;

            //Create Watermark Style
            Style style2 = new Style(typeof(TextBlock));
            style2.Setters.Add(new Setter(TextBlock.VisibilityProperty, Visibility.Collapsed));

            //The trigger to make it visible
            var trigger2 = new DataTrigger();
            trigger2.Binding = new Binding("Text") { Source = dynamicText };
            trigger2.Value = "";
            trigger2.Setters.Add(new Setter(TextBlock.VisibilityProperty, Visibility.Visible));
            style2.Triggers.Add(trigger2);

            //Set the style
            dynamicBlock2.Style = style2;

            //Location on Screen (Same as dynamicText)
            Canvas.SetTop(dynamicBlock2, 18d);
            Canvas.SetLeft(dynamicBlock2, 16d);

            //Add to Screen
            newCanvas.Children.Add(dynamicBlock2);


        }

        private void Size_Changed(object sender, SizeChangedEventArgs e)
        {
            changeSize();
            
        }

        private void textChanged(object sender, TextChangedEventArgs e)
        {
            var t = (TextBox)sender;
            imgList[lblList.IndexOf(t)] = t.Text;

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
                Canvas.SetTop(openButton, dHeight - 54 - 24);
                Canvas.SetLeft(openButton, dWidth - 64 - 24);
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
