// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

using System.Windows.Controls;
using System.Windows.Media;

namespace DiceInvaders.Views
{
    public sealed partial class StarControl : UserControl
    {
        public StarControl()
        {
            this.InitializeComponent();
        }

        public void SetFill(SolidColorBrush solidColorBrush)
        {
            polygon.Fill = solidColorBrush;
        }
    }
}
