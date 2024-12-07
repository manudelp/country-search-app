using System;
using System.Collections.Generic;
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

        // Clases para deserializar la respuesta JSON  
        public class Name
        {
            public string common { get; set; }
            public string official { get; set; }
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

        // Propiedades para almacenar datos del país  
        public List<string> capital { get; set; }
        public long population { get; set; }
        public double area { get; set; }

        // Método para buscar un país por nombre  
        private async Task<string> searchCountry()
        {
            string name = txtCountry.Text;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://restcountries.com/v3.1/name/{name}?fullText=true");

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

        // Método que se ejecuta al hacer clic en el botón de búsqueda  
        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            string countryName = txtCountry.Text.Trim();

            if (string.IsNullOrWhiteSpace(countryName))
            {
                lblError.Text = "Please input a country";
                return;
            }

            btnSearch.Enabled = false;
            lblError.Text = "";

            try
            {
                string searchResult = await searchCountry();

                if (searchResult == "error")
                {
                    imgFlag.ImageUrl = "https://placehold.co/300x200?text=Not%20found";
                    lblCountry.Text = $"Searched country: {countryName}";
                    lblOfficial.Text = "Please ensure the country name is spelled correctly.";
                    lblCapital.Text = "";
                    lblPopulation.Text = "";
                    lblArea.Text = "";
                }
                else
                {
                    var countries = JsonConvert.DeserializeObject<List<Country>>(searchResult);

                    if (countries == null || countries.Count == 0)
                    {
                        lblError.CssClass = lblError.CssClass.Replace("d-none", "").Trim();
                        lblError.Text = "No country has been found.";
                        return;
                    }

                    var country = countries[0];
                    imgFlag.ImageUrl = country.flags?.png ?? "https://placehold.co/300x200?text=Sin%20Bandera";
                    lblCountry.Text = country.name?.common ?? "Unknown";
                    lblOfficial.Text = country.name?.official ?? "Unknown";
                    lblCapital.Text = "Capital: " + (country.capital?.Count > 0 ? country.capital[0] : "Not available");
                    lblPopulation.Text = "Population: " + country.population.ToString("N0");
                    lblArea.Text = "Area: " + country.area.ToString("N0") + " km²";
                }
            }
            catch (Exception ex)
            {
                lblError.CssClass = lblError.CssClass.Replace("d-none", "").Trim();
                lblError.Text = "An error ocurred, please try again";
                System.Diagnostics.Trace.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }
    }
}