using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace For_Gen_QR
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerateQR_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.AppSettings["Connectstring"].ToString());
            sqlconn.Open();
            SqlCommand command = new SqlCommand("select QR from [EBA_QR] where QR LIKE '%" + txtInfo.Text +"%'", sqlconn);
            object result = command.ExecuteScalar(); //to excute command select on sqlquery
            if (result != null)
            {
                string QR = result.ToString();
                //generate QR code 
                QRCodeGenerator qr = new QRCodeGenerator();
                QRCodeData data = qr.CreateQrCode(QR, QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(data);
                pictureBoxQR.Image = code.GetGraphic(5);
            }
            else
            {
                MessageBox.Show("Wallet ID does not exits, please input again!", "OK");
            }
        }
    }
}
