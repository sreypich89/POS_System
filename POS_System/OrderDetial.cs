using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class OrderDetial : Form
    {
        public OrderDetial()
        {
            InitializeComponent();
            
        }

        public DataGridViewRowCollection Rows
        {
            get => dataGridView.Rows;
        }

        private void OrderDetial_Load(object sender, EventArgs e)
        {
            txtTotalAmount.Text = $"{Product.TotalAmount:c2}";
        }
    }
}
