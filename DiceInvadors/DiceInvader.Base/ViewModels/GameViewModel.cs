using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DiceInvader.Base.Helpers;
using DiceInvader.Base.Models;

namespace DiceInvader.Base.ViewModels
{
    /// <summary>
    ///     This class  represents the state of the game (e.g. Score, lives , Gameover)
    /// </summary>
    public sealed class GameViewModel : INotifyPropertyChanged
    {

        #region Fields

        private bool _gameOver;
        private ObservableCollection<object> _lives;
        private int _score;
        private readonly GameModel _model;
        private readonly Random _random ;
        #endregion


        public GameViewModel(GameModel gameModel)
        {
            Lives = new ObservableCollection<object>();
            _random = new Random();
            _model = gameModel;
            _model.GameOverChanged += OnGameOverChanged;
            _model.ScoreChanged += OnScoreChanged;
            _model.LivesChanged += OnLivesChanged;
            _model.EndGame();
        }

        private void OnLivesChanged(object sender, int lives)
        {
            if (lives < 0) return;


            while (Lives.Count > lives)
                Lives.RemoveAt(0);
            while (Lives.Count < lives)
                Lives.Add(new object());
        }

        #region Events

        public event EventHandler<ShipChangedEventArgs> ShipChanged
        {
            add { _model.ShipChanged += value; }

            remove { _model.ShipChanged -= value; }
        }

        public event EventHandler<ShotMovedEventArgs> ShotMoved
        {
            add { _model.ShotMoved += value; }

            remove { _model.ShotMoved -= value; }
        }

        #endregion

        #region Properties

        public bool GameOver
        {
            get { return _model.GameOver; }
            set
            {
                if (value == _gameOver) return;

                _gameOver = value;
                OnPropertyChanged(nameof(GameOver));
            }
        }

        public ObservableCollection<object> Lives
        {
            get { return _lives; }
            set
            {
                if (value == null || value == _lives) return;

                _lives = value;
                OnPropertyChanged(nameof(Lives));
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                if (value == _score) return;
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public bool PlayerFlashing { get; set; } = false;

        public DateTime? LeftAction { get; set; } = null;

        public DateTime? RightAction { get; set; } = null;

        #endregion

        #region Public methods


        public void MoveInvaders()
        {
            _model.MoveInvaders();
        }

   

        public void MoveShots()
        {
            _model.MoveShots();
        }

        public void CheckForInvaderHit()
        {
            _model.CheckForInvaderHit();
        }

        public bool CheckForPlayerHit()
        {
            var result = _model.CheckForPlayerHit();
            if (result)
            {
                _model.HitPlayer(DateTime.Now);
            }
            return result;
        }

        public bool CheckForCollision()
        {
            var result = _model.CheckForCollision();
            if (result)
            {
                _model.HitPlayer(DateTime.Now);
            }
            return result;
        }


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
        /// <summary>
        ///  Fire a bomb from an invador 
        /// </summary>
        /// <param name="bombLocation"> the location where the bomb should be dropped from</param>
        public void FireBomb(Point bombLocation)
        {
           _model.FireBomb( bombLocation);
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

        #endregion

        #region Private methods


        private void OnScoreChanged(object sender, int score)
        {
            Score = score;
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
            var propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public void NextWave()
        {
             _model.NextWave();
        }
    }
}