using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslamSalat
{
    public partial class Form1 : Form
    {
        public static string Priere_Une;
        public static string Priere_Deux;
        public static string Priere_Trois;
        public static string Priere_Quatre;
        public static string Priere_Cinq;
        class SalatTotal
        {
            public string Salat_One;
            public string Salat_Two;
            public string Salat_Three;
            public string Salat_Four;
            public string Salat_Five;
        }

        static async Task<string> GetHtmlAsync(string results)
        {

            const string url = "https://www.muslimpro.com/en/prayer-times";
            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);

            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(html);


            var salat = htmlDocument.DocumentNode.Descendants("span")
             .Where(node => node.GetAttributeValue("class", "")
             .Equals("jam-solat")).ToList();


            List<SalatTotal> newsLinks = new List<SalatTotal>();

            foreach (var link in salat)
            {
                string Fajr = link.SelectSingleNode("/html/body/div[1]/div[7]/div/div/div/div/div[2]/div[1]/div[2]/ul/li[1]/span[2]").InnerText;
                string Dohr = link.SelectSingleNode("/html/body/div[1]/div[7]/div/div/div/div/div[2]/div[1]/div[2]/ul/li[3]/span[2]").InnerText;
                string Asr = link.SelectSingleNode("/html/body/div[1]/div[7]/div/div/div/div/div[2]/div[1]/div[2]/ul/li[4]/span[2]").InnerText;
                string Maghrib = link.SelectSingleNode("/html/body/div[1]/div[7]/div/div/div/div/div[2]/div[1]/div[2]/ul/li[5]/span[2]").InnerText;
                string Isha = link.SelectSingleNode("/html/body/div[1]/div[7]/div/div/div/div/div[2]/div[1]/div[2]/ul/li[6]/span[2]").InnerText;
                SalatTotal item = new SalatTotal();

                var un = item.Salat_One = Fajr.ToString();
                var deux = item.Salat_Two = Dohr.ToString();
                var trois = item.Salat_Three = Asr.ToString();
                var quatre = item.Salat_Four = Maghrib.ToString();
                var cinq = item.Salat_Five = Isha.ToString();
                newsLinks.Add(item);
                break;

            }
            results = JsonConvert.SerializeObject(newsLinks);
            return results;

        }

        public Form1()
        {
            InitializeComponent();
        }

        public async void Form1_Load(object sender, EventArgs e)
        {
            var results = "";
            results = await GetHtmlAsync(results);
            Priere_Une = results.Substring(15, 5); // Priere 1
            Priere_Deux = results.Substring(35, 5); // Priere 2
            Priere_Trois = results.Substring(57, 5);// Priere 3
            Priere_Quatre = results.Substring(78, 5);// Priere 4
            Priere_Cinq = results.Substring(99, 5);// Priere 5

        }
    }
    
}