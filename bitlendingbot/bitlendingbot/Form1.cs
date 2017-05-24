using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            //// MSUT WAIT 220 MS AFTER EVERY REQUEST
            //// MAX 5 REQUESTS PER SECOND
            //// TODO: Send user client ip address
            //var uri = "https://poloniex.com/tradingApi?command=returnAvailableAccountBalances";

            ////returnAvailableAccountBalances
            ////account: lending

            ////returnOpenLoanOffers

            ////returnActiveLoans

            ////https://poloniex.com/public?command=returnLoanOrders&currency=BTC


            //NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
            //var ascii = new ASCIIEncoding();
            //var nonce = DateTime.Now.ToString("yyyyMMddhhmmssfff");

            //outgoingQueryString.Add("nonce", nonce);
            //outgoingQueryString.Add("command", "returnActiveLoans");
            //string postdata = outgoingQueryString.ToString();

            //byte[] keyByte = ascii.GetBytes("8eb64edfbba6820f5f64004d53fb22a5c7bc5671e8f80a3318460306ece40de8cc06c56ec2f39bb658c0bae823c4d74e0fc7434115be4e97742e6ecee2e0664d");
            //var sha512 = new HMACSHA512(keyByte);
            //byte[] messageBytes = ascii.GetBytes(postdata);
            //byte[] hashmessage = sha512.ComputeHash(messageBytes);
            
            //var request = WebRequest.Create(uri);
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            ////request.ContentLength = postBytes.Length;
            //request.Headers.Add("Key", "4GXNGN7G-OL8LZWU3-30GNEFXD-IOOW8QV2");
            //request.Headers.Add("Sign", ToHex(hashmessage, true));

            //// add post data to request
            //Stream postStream = request.GetRequestStream();
            //postStream.Write(hashmessage, 0, hashmessage.Length);
            //postStream.Flush();
            //postStream.Close();
            ////postStream.Write(postBytes, 0, postBytes.Length);
            ////postStream.Flush();
            ////postStream.Close();

            //var response = request.GetResponse();
            //var stream = response.GetResponseStream();

            //var reader = new StreamReader(stream, Encoding.UTF8);
            //var responseString = reader.ReadToEnd();
            ////var json = (JObject)JsonConvert.DeserializeObject(responseString);
            ////var email = GetEmail(json);
        }

        public string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }
    }
}
