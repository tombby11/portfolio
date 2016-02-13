using System.Threading.Tasks;
using System.Windows.Controls;
using WeatherApp.PresentationContext.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.WPF.Views
{
    /// <summary>
    ///     Interaction logic for ForecastListView.xaml
    /// </summary>
    public partial class ForecastListView : UserControl
    {
        private readonly ForecastViewModel _viewModel;

        public ForecastListView()
        {
            _viewModel = new ForecastViewModel();
            InitializeComponent();
            DataContext = _viewModel;
        }

        public async Task RefreshList(CityModel city)
        {
            await _viewModel.RefreshForecastListAsync(city);
        }
    }
}