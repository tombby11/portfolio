using System.Windows.Controls;
using WeatherApp.PresentationContext.ViewModels;
using WPFAutoCompleteTextbox;

namespace WeatherApp.WPF.Views
{
    /// <summary>
    ///     Interaction logic for CitySearchView.xaml
    /// </summary>
    public partial class CitySearchView : UserControl
    {
        private CitiesViewModel _viewModel; 
        public CitySearchView()
        {
            _viewModel = new CitiesViewModel();
            DataContext = _viewModel;
            InitializeComponent();
            foreach (var city in _viewModel.Cities)
            {
                searchBox.AddItem(new AutoCompleteEntry(city.CityName,city.CityName.ToLower(), city.CityName.ToUpper()));
            }
        }
    }
}