using CsvHelper;

namespace CSVReadingService
{
    public interface ICsvService
    {

        /// <summary>
        /// This is the reader that will access the CSV file.
        /// </summary>
        CsvReader CsvReader { get; set; }
        /// <summary>
        /// Initializes the reader and sets it to the designated csv file 
        /// </summary>
        void Initialize();
    }
}