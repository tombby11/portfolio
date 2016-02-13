using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.PresentationContext.ViewModels;
using WeatherApp.ViewModels;

namespace WeatherApp.PresentationContexts.Test
{
    [TestClass]
  public class ViewModelTests
  {
    private class PersonViewModel : ViewModel
    {      
      private string _name;
      
      public string Name
      {
        get { return _name; }
        set { _name = value; NotifyPropertyChanged(); }
      }
    }
    
    [TestMethod]
    public void Should_notify_property_changed_when_changing_property()
    {
      // Arrange
      var vm = new PersonViewModel();
      bool propertyChanged = false;
      vm.PropertyChanged += (s, e) => propertyChanged = true;

      // Act
      vm.Name = "Uncle Bob";

      // Assert
      Assert.IsTrue(propertyChanged);
    }
  }
}
