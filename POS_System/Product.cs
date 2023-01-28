using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class Product : UserControl
    {
        public Product()
        {
            InitializeComponent();
            btnCancel.Visible = false;
        }

        public static DataGridViewRowCollection Rows { get; set; }
        public static double TotalAmount 
        {
            get
            {
                double total = 0;
                foreach(DataGridViewRow row in Rows)
                {
                    string a = row.Cells[4].Value.ToString();
                    total += double.Parse(a, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency);
                }
                return total;
            }
        }

        public int ID { get; set; }
        public int Qty { get; set; }
        public string PName { 
            get => txtPName.Text; 
            set => txtPName.Text = value; 
        }
        public Image Picture { 
            get => picture.Image; 
            set => picture.Image = value; 
        }

        private double price;
        public double Price { 
            get => price;
            set
            {
                price = value;
                txtPrice.Text = price.ToString("c2");
            }
        }
        private int CheckID(int id)
        {
            for(int i=0; i < Rows.Count; i++)
            {
                int tid = int.Parse(Rows[i].Cells[0].Value.ToString());
                if(tid==id)
                    return i;
            }
            return -1;
        }


        private void btnOrder_Click(object sender, EventArgs e)
        {
            Qty++;
            btnOrder.Text = $"Order({Qty})";
            btnCancel.Visible = true;

            int index = CheckID(ID);
            if (index == -1)
            {
                Rows.Add(ID, PName, Qty, $"{ Price:c2}", $"{Qty * Price:c2}");
            }
            else
            {
                Rows[index].Cells[2].Value = Qty;
                Rows[index].Cells[4].Value = $"{Qty * price:c2}";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Qty--;
            int index = CheckID(ID);
            if(Qty == 0)
            {
                btnOrder.Text = "Order";
                btnCancel.Visible=false;
                Rows.RemoveAt(index);
            }
            else
            {
                btnOrder.Text = $"Order({Qty})";
                Rows[index].Cells[2].Value = Qty;
                Rows[index].Cells[4].Value = $"{Qty * price:c2}";
            }
        }
    }
}
