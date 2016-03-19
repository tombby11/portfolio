using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media.Animation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DiceInvaders.Views
{
    public sealed partial class AnimatedImage : UserControl
    {
        public AnimatedImage()
        {
            this.InitializeComponent();
        }

        public AnimatedImage(string imageName): this()
        {
            
            Image.Source = new BitmapImage(new Uri(@"/DiceInvaders;component/Images/"+ imageName,UriKind.Relative)); 
        }
       
        public void InvaderShot()
        {
            var storyBoard = FindResource("InvaderShotStoryboard") as Storyboard;
            storyBoard?.Begin();
        }

        public void StartFlashing()
        {
            var storyBoard = FindResource("FlashStoryboard") as Storyboard;
            storyBoard?.Begin();
        }

        public void StopFlashing()
        {
            var storyBoard = FindResource("FlashStoryboard") as Storyboard;
            storyBoard?.Stop();
        }
    }
}
