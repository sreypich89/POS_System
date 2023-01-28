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
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            comboDiscount.SelectedIndex = 0;
            txtTotalAmount.Text = $"{totalAmount:c2}";
        }
        double totalAmount = Product.TotalAmount;
        double[] dis = { 0, 5, 10, 15, 20 };
        double disPrice, payment, cashReturn;
        public double CashReceive { get;set; }
        
        private void btbCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.OK;
        }

        private void txtCashReceive_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                CashReceive=double.Parse(txtCashReceive.Text.Trim());
                cashReturn = CashReceive - payment;
                if (CashReceive >= payment )
                {
                    btnPayment.Enabled = true;
                    txtCashReturn.ForeColor= Color.Blue;
                }
                else
                {
                    btnPayment.Enabled = false;
                    txtCashReturn.ForeColor= Color.Red;
                }
                txtCashReturn.Text = $"{cashReturn:c2}";
            }
            catch (Exception)
            {
                txtCashReturn.Text = "Error";
                btnPayment.Enabled= false;
                txtCashReturn.ForeColor= Color.Red;
            }
        }

        public double Discount
        {
            get;set;
        }

        private void comboDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index=comboDiscount.SelectedIndex;
            Discount = dis[index];
            disPrice = totalAmount * Discount / 100;
            payment = totalAmount - disPrice;

            txtDisPrice.Text = $"{disPrice:c2}";
            txtPayment.Text=$"{payment:c2}";

            if (txtCashReceive.Text != "")
            {
                txtCashReceive_KeyUp(sender, null);
            }
        }
    }
}
