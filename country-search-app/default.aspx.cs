using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace country_search_app
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        public class Name
        {
            public string common { get; set; }
        }

        public class Flags
        {
            public string png { get; set; }
            public string svg { get; set; }
            public string alt { get; set; }
        }

        public class Country
        {
            public Name name { get; set; }
            public Flags flags { get; set; }
            public List<string> capital { get; set; }
            public long population { get; set; }
            public double area { get; set; }
        }

        public List<string> capital { get; set; }
        public long population { get; set; }
        public double area { get; set; }
        

        private async Task<string> searchCountry()
        {
            string name = txtCountry.Text;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://restcountries.com/v3.1/name/{name}");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return "error";
                }
            }
        }

        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            string countryName = txtCountry.Text;
            string searchResult = await searchCountry();

            if (searchResult == "error")
            {
                imgFlag.ImageUrl = "https://placehold.co/300x200?text=Country%20Not%20Found";
                lblCountry.Text = "Country searched: " + countryName;
                lblCapital.Text = "";
                lblPopulation.Text = "";
                lblArea.Text = "";
            }
            else
            {
                var countries = JsonConvert.DeserializeObject<List<Country>>(searchResult);
                var country = countries[0];
                var flag = country.flags.png;

                imgFlag.ImageUrl = flag;
                lblCountry.Text = country.name.common;
                lblCapital.Text = "Capital: " + (country.capital != null && country.capital.Count > 0 ? country.capital[0] : "No disponible");
                lblPopulation.Text = "Population: " + country.population.ToString("N0");
                lblArea.Text = "Area: " + country.area.ToString("N0") + " km²";
            }
        }

    }
}