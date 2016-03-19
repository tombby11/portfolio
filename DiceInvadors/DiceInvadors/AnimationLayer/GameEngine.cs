using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DiceInvader.Base.Models;
using DiceInvader.Base.ViewModels;
using DiceInvaders.Views;
using Point = DiceInvader.Base.Helpers.Point;

namespace DiceInvaders.AnimationLayer
{
    /// <summary>
    ///     Responsible for moving the elements on the screen
    /// </summary>
    public class GameEngine : INotifyPropertyChanged
    {
        #region Constructor

        public GameEngine()
        {
            Scale = 1;
            Sprites = new ObservableCollection<FrameworkElement>();
            ViewModel = new GameViewModel(new GameModel(new GameModelHelper()) );

            ViewModel.ShipChanged += ModelShipChangedEventHandler;
            ViewModel.ShotMoved += ModelShotMovedEventHandler;

            _timer.Interval = TimeSpan.FromMilliseconds(66);
            _timer.Tick += TimerTickEventHandler;
        }

        #endregion

        public static double Scale { get; private set; }

        #region Fields
        private readonly Random _random = new Random();

        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private FrameworkElement _playerControl;
        private static ObservableCollection<FrameworkElement> _sprites;
        private readonly Dictionary<Invader, FrameworkElement> _invaders = new Dictionary<Invader, FrameworkElement>();

        private readonly Dictionary<FrameworkElement, DateTime> _shotInvaders =
            new Dictionary<FrameworkElement, DateTime>();

        private readonly Dictionary<Shot, FrameworkElement> _shots = new Dictionary<Shot, FrameworkElement>();

        #endregion

        #region Properties

        public Size PlayAreaSize { get; set; }

        public ObservableCollection<FrameworkElement> Sprites
        {
            get { return _sprites; }
            set
            {
                if (value == null || value == _sprites)
                    return;

                _sprites = value;
                OnPropertyChanged(nameof(Sprites));
            }
        }

        public GameViewModel ViewModel { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        ///     Removed all visual elements on screen and starts the game from the begining
        /// </summary>
        public void StartGame()
        {
            foreach (var invader in _invaders.Values) Sprites.Remove(invader);
            foreach (var shot in _shots.Values) Sprites.Remove(shot);
            ViewModel.StartGame();
            _timer.Start();
        }

        public void KeyDown(Key key)
        {

            var x = (double) _playerControl.GetValue(Canvas.LeftProperty);
            var width = _playerControl.ActualWidth;
            var startingX = x + width/2;
            var startingY = (double) _playerControl.GetValue(Canvas.TopProperty);

            if (key == Key.Space)
                ViewModel.FireRocket(startingX, startingY);

            if (key == Key.Left)
                ViewModel.LeftAction = DateTime.Now;

            if (key == Key.Right)
                ViewModel.RightAction = DateTime.Now;
        }

        public void KeyUp(Key key)
        {
            if (key == Key.Left)
                ViewModel.LeftAction = null;

            if (key == Key.Right)
                ViewModel.RightAction = null;
        }

        #endregion

        #region Private methods 

        private void ModelShotMovedEventHandler(object sender, ShotMovedEventArgs e)
        {
            if (!e.Disappeared)
            {
                if (!_shots.ContainsKey(e.Shot))
                {
                    var shotControl = GameHelper.ShotControlFactory(e.Shot, Scale);
                    _shots.Add(e.Shot, shotControl);
                    Sprites.Add(shotControl);
                }
                else
                {
                    var shotControl = _shots[e.Shot];
                    GameHelper.MoveElementOnCanvas(shotControl, e.Shot.Location.X, e.Shot.Location.Y, Scale);
                }
            }
            else
            {
                if (_shots.ContainsKey(e.Shot))
                {
                    var shotControl = _shots[e.Shot];
                    Sprites.Remove(shotControl);
                    _shots.Remove(e.Shot);
                }
            }
        }

        private void ModelShipChangedEventHandler(object sender, ShipChangedEventArgs e)
        {
            if (!e.Killed) //If the ship is alive then move it to the next destination 
            {
                if (e.ShipUpdated is Invader)
                {
                    var invader = (Invader) e.ShipUpdated;
                    if (!_invaders.ContainsKey(invader))
                    {
                        var invaderControl = GameHelper.InvaderControlFactory(invader, Scale);
                        _invaders.Add(invader, invaderControl);
                        Sprites.Add(invaderControl);
                    }
                    else
                    {
                        var invaderControl = _invaders[invader];
                        GameHelper.MoveElementOnCanvas(invaderControl, invader.Location.X, invader.Location.Y, Scale);
                        GameHelper.ResizeElement(invaderControl, invader.Size.Width, invader.Size.Height, Scale);
                    }
                }
                else if (e.ShipUpdated is Player)
                {
                    if (ViewModel.PlayerFlashing)
                    {
                        ViewModel.PlayerFlashing = false;
                        var control = _playerControl as AnimatedImage;
                        control?.StopFlashing();
                    }

                    var player = e.ShipUpdated as Player;
                    if (_playerControl == null)
                    {
                        _playerControl = GameHelper.PlayerControlFactory(player, Scale);
                        Sprites.Add(_playerControl);
                    }
                    else
                    {
                        GameHelper.MoveElementOnCanvas(_playerControl, player.Location.X, player.Location.Y, Scale);
                        GameHelper.ResizeElement(_playerControl, player.Size.Width, player.Size.Height, Scale);
                    }
                }
            }
            else
            {
                //If the ship died 
                if (e.ShipUpdated is Invader)
                {
                    var invader = (Invader) e.ShipUpdated ;
                    if (!_invaders.ContainsKey(invader)) return;
                    var invaderControl = _invaders[invader] as AnimatedImage;
                    if (invaderControl == null) return;
                    invaderControl.InvaderShot();
                    _shotInvaders[invaderControl] = DateTime.Now;
                    _invaders.Remove(invader);
                }
                else if (e.ShipUpdated is Player)
                {
                    var control = _playerControl as AnimatedImage;
                    control?.StartFlashing();
                    ViewModel.PlayerFlashing = true;
                }
            }
        }

        private async void TimerTickEventHandler(object sender, EventArgs e)
        {
            //we convert to Windows.Size to the local Size stuct that the pcl can deal with 
            var baseSize = new DiceInvader.Base.Helpers.Size(PlayAreaSize.Width, PlayAreaSize.Height);
            ViewModel.UpdatePlayAreaSize(baseSize);

            if (_invaders.Count == 0)
            {
                ViewModel.NextWave();
            }
            ViewModel.MovePlayer();
            ViewModel.MoveInvaders(DateTime.Now);

            //Todo: put this part in the helper 
            var result =
                from invader in _invaders
                group invader by invader.Value.GetValue(Canvas.LeftProperty)
                into invaderGroup
                orderby invaderGroup.Key descending
                select invaderGroup;

            var randomGroup = result.ElementAt(_random.Next(result.ToList().Count));
            var bottomInvader = randomGroup.Last();

            var shotLocation =
                new Point(
                    (double) bottomInvader.Value.GetValue(Canvas.LeftProperty) + bottomInvader.Value.ActualWidth/2,
                    (double) bottomInvader.Value.GetValue(Canvas.TopProperty) + 2);

            ViewModel.FireBomb(shotLocation);
            ViewModel.MoveShots();
            ViewModel.RemoveOutOfBoundShots(10, PlayAreaSize.Height-10);
            ViewModel.CheckForInvaderHit();
            if (ViewModel.IsInvadorsReachedTheButtom())
            {
                ViewModel.EndGame();
            }
            if (ViewModel.IsPlayerHit() || ViewModel.IsPlayerInvadorCollision())
            {
                
                ViewModel.RemoveShots();
                await ViewModel.HitPlayer();              
            }
      
            foreach (var control in _shotInvaders.Keys.ToList())
            {
                var elapsed = _shotInvaders[control];
                if (DateTime.Now - elapsed <= TimeSpan.FromSeconds(.5)) continue;
                Sprites.Remove(control);
                _shotInvaders.Remove(control);
            }

            if (ViewModel.GameOver)
            {
                _timer.Stop();
            }
        }

        #endregion

        #region PropertyChanged Notification Implementation 

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}