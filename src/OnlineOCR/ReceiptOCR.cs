using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace OnlineOCR
{
    public class ReceiptOCR
    {
        private string Username { get; set; }
        private string Password { get; set; }

        public ReceiptOCR(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Input receipt image filepath and taxid,
        /// Output receipt information.
        /// Need a Receipt class to received.
        /// </summary>
        public Receipt.Information StartOCR(string FilePath, bool Saveimg, string TaxID)
        {

            var fileStream = File.OpenRead(FilePath);
            var fileName = Path.GetFileName(FilePath);

            using (var client = new HttpClient())

            {
                var url = "";

                clientInit(client);

                MultipartFormDataContent form = new MultipartFormDataContent();

                form.Add(new StreamContent(fileStream), "UpLoadFile", fileName);
                form.Add(new StringContent(Saveimg.ToString()), "Saveimg");
                form.Add(new StringContent(TaxID), "TaxID");

                var response = client.PostAsync(url, form).Result.Content.ReadAsStringAsync().Result;

                string result = Regex.Unescape(response);

                Receipt.Information receipt = JsonConvert.DeserializeObject<Receipt.Information>(result);

                return receipt;

            }
        }


        private void clientInit(HttpClient client)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"{Username}:{Password}");
            string val = System.Convert.ToBase64String(plainTextBytes);
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
            client.DefaultRequestHeaders.Add("ContentType", "application/json");
            client.DefaultRequestHeaders.Add("charset", "UTF-8");

        }
    }
    public class Receipt
    {
        public class Result
        {
            public string Date { get; set; }
            public string ReceiptNum { get; set; }
            public string SellerNum { get; set; }
            public string BuyerNum { get; set; }
            public string NoTaxCharge { get; set; }
            public string Charge { get; set; }
            public string Tax { get; set; }
            public string ReceiptFormat { get; set; }
        }

        public class Information
        {
            public Sysinfo Sysinfo { get; set; }
            public Result Result { get; set; }
        }

        public class Sysinfo
        {
            public string Sentence { get; set; }
            public string Saveimg { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
        }


    }
}
