using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace CSVReadingService
{
    public class CityCsvService : ICsvService
    {

        public CityCsvService()
        {
            Initialize();
        }

        public CsvReader CsvReader { get; set; }
        public List<City> Cities { get; set; }

        public void Initialize()
        {
            try
            {
                var assembly = typeof(CityCsvService).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("CSVReadingService.Resources.coords.csv");
                var textReader = new StreamReader(stream);
                CsvReader = new CsvReader(textReader);
                Cities = CsvReader.GetRecords<City>().ToList();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                CsvReader = null; 
            }
            

        }

        /// <summary>
        /// Find the city with the matching name
        /// </summary>
        /// <param name="cityName"> Name of the city to be found </param>
        /// <returns> The found city, otherwise it returns null </returns>
        public City GetCity(string cityName)
        {
          var found =  Cities.Find((c) => c.Name.ToLower() == cityName.ToLower());
            return found;
        }

       


    }
}
