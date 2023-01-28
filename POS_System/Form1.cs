using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Product.Rows = detial.Rows;

            string []fileName = Directory.GetFiles("D:\\C#\\Image\\Image");

            int id = 1;
            int c = 1, row = 0;
            Random r = new Random();
            foreach(string f in fileName)
            {
                Image image = Image.FromFile(f);
                string pname = Path.GetFileNameWithoutExtension(f);
                double price = r.Next(1, 20);
                Product p = new Product()
                {
                    ID = id++,
                    Picture = image,
                    PName = pname,
                    Price = price,
                };
                tableLayoutPanel.Controls.Add(p,c,row);

                c++;
                if (c > 5)
                {
                    c = 1;
                    row++;
                }
            }
        }

        private void fExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private OrderDetial detial = new OrderDetial();
        private void fOrderDetail_Click(object sender, EventArgs e)
        {
            detial.ShowDialog(this);
        }
        double discount,cashReceive;
        private string Proname;

        //private object pname;

        private void fPayment_Click(object sender, EventArgs e)
        {
            Payment payment= new Payment();
            DialogResult dialogResult = payment.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                //Print Report
                discount = payment.Discount;
                cashReceive = payment.CashReceive;
                printPreviewDialog.ShowDialog(this);
                //Re new Main form
                //Application.Restart();
            }
            
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int y = 84;
            e.Graphics.DrawImage(Properties.Resources.photo_2022_07_17_22_26_27, 6, 0, 60, 60);
            e.Graphics.DrawImage(Properties.Resources.caltex2, 60, 0, 120, 25);
            Font f=new Font("",6,FontStyle.Bold);
            e.Graphics.DrawString("#43, St. Monivong, Sk. Sensok, Kh. Daun Penh, Phnom Penh",f, Brushes.Black, 80, 35);
            e.Graphics.DrawLine(Pens.RosyBrown, 0, 60, 450, 60);
            string seller = Environment.UserName;
            e.Graphics.DrawString($"Seller: {seller}", f, Brushes.Black, 0, 68);
            e.Graphics.DrawLine(Pens.GreenYellow, 0, y, 450, y);

            f = new Font("Consolas", 8, FontStyle.Bold);
            string st = "ID".PadRight(10) + "ProName".PadRight(10) + "Qty".PadRight(10) + "Price".PadRight(10) + "Amount".PadRight(10);
            y += 15;
            e.Graphics.DrawString(st, f, Brushes.Brown, 0, 84);
            //int y = 84;
            foreach (DataGridViewRow row in Product.Rows)
            {
                y += 10 ;
                int id = int.Parse(row.Cells[0].Value.ToString());
                string st1 = $"{id:000}".PadRight(10) + "ProName".PadRight(10) + "Qty".PadRight(10) + "Price".PadRight(10) + "Amount".PadRight(10);
                string pname = row.Cells[1].Value.ToString();
                if (pname.Length>=20)
                {
                    pname = ProductName.Substring(2,20);
                }
                string stl = $"{id:00}".PadRight(5)
                + $"{pname:c2}".PadRight(10)
                + $"{row.Cells[1].Value}".PadRight(10)
                + $"{row.Cells[2].Value}".PadRight(10)
                + $"{row.Cells[3].Value}".PadRight(10)
                + $"{row.Cells[4].Value}".PadRight(10);
                e.Graphics.DrawString(stl, f, Brushes.Black, 0,y);
            }
            y += 15;
            e.Graphics.DrawLine(Pens.GreenYellow, 0, y, 450, y);
            y += 15;
            e.Graphics.DrawString($"Discount       ={discount}%", f, Brushes.Black, 140, y);
            double disprice = Product.TotalAmount * discount / 100;
            double payment = Product.TotalAmount - discount;

            y+= 15 ;
            e.Graphics.DrawString($"Discount Price ={disprice:c2}", f, Brushes.Black, 140, y);
            y += 15 ;
            e.Graphics.DrawString($"Payment        ={payment:c2}", f, Brushes.Black, 140, y);
            y += 15;
            e.Graphics.DrawString($"Cash Receive   ={cashReceive}",f,Brushes.Black,140,y);
            y += 15;
            double cashreturn =cashReceive - payment;
          
            e.Graphics.DrawString($"Cash Return    ={cashreturn}",f,Brushes.Black,140,y);
            //y += 15;
            //e.Graphics.DrawLine(Pens.Green, 30, 70, 450, 66);
           // e.Graphics.DrawLine(Pens.BurlyWood, 30, 70, 450, 66);
            //y += 15;

            y += 15;
            e.Graphics.DrawLine(Pens.Black, 0, y, 450, y);
            f = new Font("Kh content",8);
            e.Graphics.DrawString("សូមអរគុណចំពោះការទិញទំនិញនៅហាងរបស់យើងខ្ញុំ សូមអញ្ជើញមកម្តងទៀត",f,Brushes.Black,90,y);
            y+= 15;
            e.Graphics.DrawString("Thank you for shopping with us, please come again!", f, Brushes.Black, 90, y);
            y += 15;
            e.Graphics.DrawString("ទំនិញដែលទិញហើយមិនអាចដូរបានទេ", f, Brushes.Black, 90, y);
            y += 15;
            e.Graphics.DrawString("Goods sold are not returnable", f, Brushes.Black, 90, y);
            
        }
    }
}
