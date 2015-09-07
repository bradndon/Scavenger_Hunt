using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

namespace ScavengerHunt
{
    public class Scavenger
    {
        public Scavenger(Image[] questions, Image[] answers, string filename)
        {
            Make(questions, answers, filename);
        }

        static void Make(Image[] questions, Image[] answers,  string filename)
        {
            //VARIABLE DEFINITIONS
            var doc = new Document(PageSize.LETTER.Rotate());
            var writer = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
            Image ansImg, queImg; //ansImg top right, queImg bottom center
            string[] alpha = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //create an array of so that it will only use parts of the alphabet that correspond to the number of questions
            int[] nums = Enumerable.Range(0,answers.Length).ToArray();
            new Random().Shuffle(nums);

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
                var myText = new Phrase(alpha[nums[i]]);
                myText.Font.Size = 108f;
                var ct = new ColumnText(cb);
                ct.SetSimpleColumn(myText, 36f, doc.PageSize.Height - 36f - 100f, 277, 200, 32, Element.ALIGN_CENTER);
                ct.Go();

                //format image for top right
                ansImg = answers[i];
                ansImg.ScaleToFit(240f, 200f);
                ansImg.SetAbsolutePosition(doc.PageSize.Width - 36f - 120f - ansImg.ScaledWidth / 2, doc.PageSize.Height - 36f - 100f - ansImg.ScaledHeight / 2);
                doc.Add(ansImg);

                //format image for bottom middle
                queImg = questions[((i + 1) % answers.Length)];
                queImg.ScaleToFit(720f, 302f);
                queImg.SetAbsolutePosition(36f + 360f- queImg.ScaledWidth / 2, 36f + 151f - queImg.ScaledHeight / 2);
                doc.Add(queImg);
                order.Append(alpha[nums[i]] + " -> ");
                
                
            }
            order.Append(alpha[nums[0]]);
            doc.NewPage();
            string o = order.ToString();
            var orders = new Paragraph(o);
            
            orders.Font.Size = 36f;
            doc.Add(new Paragraph());
            doc.Add(orders);
            

            //closethe document
            doc.Close();

            //open finished document
            System.Diagnostics.Process.Start(filename);
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
