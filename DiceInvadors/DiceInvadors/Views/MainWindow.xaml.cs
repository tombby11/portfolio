using System.Windows;
using System.Windows.Input;
using DiceInvaders.AnimationLayer;

namespace DiceInvaders.Views
{

    public  partial class MainWindow : Window
    {
        private readonly GameEngine _gameEngine;
        public MainWindow()
        {
            _gameEngine = new GameEngine();
            DataContext = _gameEngine; 
            InitializeComponent();
        }
      
        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (_gameEngine.ViewModel.GameOver)
            {
                if (e.Key == Key.Return)
                {
                    _gameEngine.StartGame();
                }
            }
            else
            {
                _gameEngine.KeyDown(e.Key);
            }
        }

        private void KeyUpHandler(object sender, KeyEventArgs e)
        {
            _gameEngine.KeyUp(e.Key);
        }

        private void UpdatePlayAreaSize(Size newPlayAreaSize)
        {
            double targetWidth;
            double targetHeight;
            if (newPlayAreaSize.Width > newPlayAreaSize.Height)
            {
                targetWidth = newPlayAreaSize.Height * 4 / 3;
                targetHeight = newPlayAreaSize.Height;
                double leftRightMargin = (newPlayAreaSize.Width - targetWidth) / 2;
                playArea.Margin = new Thickness(leftRightMargin, 0, leftRightMargin, 0);
            }
            else
            {
                targetHeight = newPlayAreaSize.Width * 3 / 4;
                targetWidth = newPlayAreaSize.Width;
                double topBottomMargin = (newPlayAreaSize.Height - targetHeight) / 2;
                playArea.Margin = new Thickness(0, topBottomMargin, 0, topBottomMargin);
            }
            playArea.Width = targetWidth;
            playArea.Height = targetHeight;
            _gameEngine.PlayAreaSize = new Size(targetWidth - 30, targetHeight - 30);
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlayAreaSize(playArea.RenderSize);
        }

        private void pageRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePlayAreaSize(new Size(e.NewSize.Width-100, e.NewSize.Height - 160));

        }
    }
}
