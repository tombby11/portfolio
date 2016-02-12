using DiceInvader.Base.Helpers;
using DiceInvader.Base.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DiceInvader.Base.ViewModels
{

    /// <summary>
    /// This class  represents the state of the game (e.g. Score, lives , Gameover) 
    /// </summary>
    public sealed class GameViewModel : INotifyPropertyChanged
    {
       
        #region Fields
        private bool _gameOver;
        private ObservableCollection<object> _lives;
        private int _Score;
        private readonly GameModel _model;
        private bool _playerFlashing = false;
        private DateTime? _leftAction = null;
        private DateTime? _rightAction = null;
        #endregion

        #region Events
        public event EventHandler<ShipChangedEventArgs> ShipChanged
        {
            add
            {
                _model.ShipChanged += value;
            }

            remove
            {
                _model.ShipChanged -= value;
            }
        }

        public event EventHandler<ShotMovedEventArgs> ShotMoved
        {
            add
            {
                _model.ShotMoved += value;
            }

            remove
            {
                _model.ShotMoved -= value;
            }
        }
        #endregion



      
        #region Properties
        public bool GameOver
        {
            get { return _model.GameOver; }
            set
            {
                if (value != _gameOver) _gameOver = value;
                OnPropertyChanged(nameof(GameOver));
            }
        }
        public ObservableCollection<object> Lives
        {
            get { return _lives; }
            set
            {
                if (value != null || value != _lives) _lives = value;
                OnPropertyChanged(nameof(Lives));
            }
        }
        public int Score
        {
            get { return _Score; }
            set
            {
                if ( value != _Score) _Score = value;
                OnPropertyChanged(nameof(Score));
            }
        }
        public bool PlayerFlashing
        {
            get
            {
                return _playerFlashing;
            }

            set
            {
                _playerFlashing = value;
            }
        }
        public DateTime? LeftAction
        {
            get
            {
                return _leftAction;
            }

            set
            {
                _leftAction = value;
            }
        }
        public DateTime? RightAction
        {
            get
            {
                return _rightAction;
            }

            set
            {
                _rightAction = value;
            }
        }
        #endregion

        #region Public methods

        public void StartGame()
        {
            _model.InitializeGameStatus();
            _model.InitializePlayerShip();
            _model.NextWave();
        }
        public void FireRocket(double fromX, double fromY)
        {
           var startingPoint = new Point(fromX, fromY);
            _model.RocketShot(startingPoint);
        }

        public void UpdatePlayAreaSize(Size playAreaSize)
        {
            _model.PlayAreaSize = playAreaSize;
        }
        
        public void MovePlayer()
        {
            if (LeftAction.HasValue && RightAction.HasValue)
                _model.MovePlayer(LeftAction > RightAction ? Direction.Left : Direction.Right);
            else if (LeftAction.HasValue)
                _model.MovePlayer(Direction.Left);
            else if (RightAction.HasValue)
                _model.MovePlayer(Direction.Right);
        }
        /// <summary>
        /// Update the status of game 
        /// </summary>
        public void Update()
        {

            //if (_model.Invaders.Count == 0)
            //{
            //    _model.NextWave();
            //}

            //if (!_playerDied.HasValue)
            //{
            //    MoveInvaders();
            //    FireBomb();
            //    MoveShots();
            //    CheckForInvaderHit();
            //    CheckForPlayerHit();
            //    CheckForCollision();
            //}
            //else if (DateTime.Now - _playerDied > TimeSpan.FromSeconds(2.5))
            //{
            //    _playerDied = null;
            //    TriggerShipChanged(_player, false);
            //}

            _model.Update();
            if (Score != _model.Score)
            {
                Score = _model.Score;
            }
            if (_model.Lives >= 0)
            {
                while (Lives.Count > _model.Lives)
                    Lives.RemoveAt(0);
                while (Lives.Count < _model.Lives)
                    Lives.Add(new object());
            }
        }

        #endregion

        #region Private methods
        public GameViewModel(GameModel gameModel)
        {            
            Lives = new ObservableCollection<object>();

            _model = gameModel;
            _model.GameOverChanged += OnGameOverChanged; ;
            _model.EndGame();
        }

        private void OnGameOverChanged(object sender, bool isGameOver)
        {
            GameOver = isGameOver;
        }

        #endregion  

        #region PropertyChanged Notification Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

       
        #endregion
    }
}
