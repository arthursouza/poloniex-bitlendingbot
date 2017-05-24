using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bitlendingbot
{
    public partial class Form1 : Form
    {
        private int nonce = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            // MSUT WAIT 220 MS AFTER EVERY REQUEST
            // MAX 5 REQUESTS PER SECOND
            // TODO: Send user client ip address
            var uri = "https://poloniex.com/tradingApi";

            //returnAvailableAccountBalances
            //account: lending

            //returnOpenLoanOffers

            //returnActiveLoans

            //https://poloniex.com/public?command=returnLoanOrders&currency=BTC

            var request = WebRequest.Create(getPerson);
            request.Method = "POST";
            request.ContentLength = 0;
            request.ContentType = "application/json; charset=utf-8";
            var response = request.GetResponse();
            var stream = response.GetResponseStream();

            var reader = new StreamReader(stream, Encoding.UTF8);
            var responseString = reader.ReadToEnd();
            var json = (JObject)JsonConvert.DeserializeObject(responseString);
            var email = GetEmail(json);
        }
    }
}
