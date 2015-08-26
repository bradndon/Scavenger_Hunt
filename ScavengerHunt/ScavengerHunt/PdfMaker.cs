using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ScavengerHunt
{
    public class Scavenger
    {
        public Scavenger(Image[] questions, Image[] answers)
        {
            Make(questions, answers);
        }

        static void Make(Image[] questions, Image[] answers)
        {
            //VARIABLE DEFINITIONS
            var doc = new Document(PageSize.LETTER.Rotate());
            var writer = PdfWriter.GetInstance(doc, new FileStream("E:/foxb4/Documents/Doc1.pdf", FileMode.Create));
            Image ansImg, queImg; //ansImg top right, queImg bottom center
            string[] alpha = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //Image[] questions = new Image[26]; //image array for questions
            //Image[] answers = new Image[26]; //image array for answers

            //find all the images
            //for (int i = 1; i <= 26; i++)
            //{
            //    questions[i - 1] = Image.GetInstance("E:/Downloads/" + i + ".jpg");
            //    answers[i - 1] = Image.GetInstance("E:/Downloads/" + i + ".jpg");
            //}

            //shuffle alphabet array
            new Random().Shuffle(alpha);

            //open document
            doc.Open();
            var cb = writer.DirectContent;
            StringBuilder order = new StringBuilder();
            //main loop, creates pages and follows the template for the layout
            for (int i = 0; i < answers.Length; i++)
            {
                //add a new page
                doc.NewPage();

                //boxes for refrence REMOVE FOR RELEASE
                cb.Rectangle(doc.PageSize.Width - 36f - 240f, doc.PageSize.Height - 36f - 200f, 240f, 200f);
                cb.Rectangle(36f, doc.PageSize.Height - 36f - 200f, 240f, 200f);
                cb.Rectangle(36f, 36f, 720f, 302f);
                cb.Stroke();

                //formatting for letter
                var myText = new Phrase(alpha[i]);
                myText.Font.Size = 108f;
                var ct = new ColumnText(cb);
                ct.SetSimpleColumn(myText, 36f, doc.PageSize.Height - 36f - 100f, 277, 200, 32, Element.ALIGN_CENTER);
                ct.Go();

                //format image for top right
                ansImg = answers[i];
                ansImg.ScaleToFit(240f, 200f);
                ansImg.SetAbsolutePosition(doc.PageSize.Width - 36f - 240f, doc.PageSize.Height - 36f - 100f - ansImg.ScaledHeight / 2);
                doc.Add(ansImg);

                //format image for bottom middle
                queImg = questions[((i + 1) % answers.Length)];
                queImg.ScaleToFit(720f, 302f);
                queImg.SetAbsolutePosition(36f, 36f + 151f - queImg.ScaledHeight / 2);
                doc.Add(queImg);
                order.Append(alpha[i] + " -> ");
                
                
            }
            order.Append(alpha[0]);
            doc.NewPage();
            string o = order.ToString();
            var orders = new Paragraph(o);
            
            orders.Font.Size = 36f;
            doc.Add(new Paragraph());
            doc.Add(orders);
            

            //closethe document
            doc.Close();

            //open finished document
            System.Diagnostics.Process.Start("E:/foxb4/Documents/Doc1.pdf");
        }
    }
    //extension for shuffling an array (based on the Fisher-Yates Algorithm)
    static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

}
