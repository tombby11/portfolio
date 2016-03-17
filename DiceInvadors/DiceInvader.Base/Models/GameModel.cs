using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiceInvader.Base.Helpers;

namespace DiceInvader.Base.Models
{
    public class GameModel
    {
        public GameModel(IGameModelHelper helper, Player player = null)
        {
            _helper = helper;
            _player = player ??
                      new Player(new Point(Player.PlayerSize.Width, PlayAreaSize.Height - Player.PlayerSize.Height*3));
        }

        #region Constants

        public const int MaximumPlayerShots = 3;
        public const int InitialStarCount = 50;

        #endregion

        #region Fields

        private readonly Player _player;
        private bool _gameOver = true;
        private bool _isPlayerDead;
        private Direction _invadersDirection = Direction.Left;
        private bool _justMovedDown;
        private int _score;
        private int _lives;
        private readonly IGameModelHelper _helper;
        private DateTime _lastUpdated = DateTime.MinValue;
        private Direction _invaderDirection;

        #endregion

        #region Events

        /// <summary>
        ///     This event is fired on game over state
        /// </summary>
        public event EventHandler<bool> GameOverChanged;

        /// <summary>
        ///     This event is fired on the movement of any ship
        /// </summary>
        public event EventHandler<ShipChangedEventArgs> ShipChanged;

        /// <summary>
        ///     This event is fired on the movement of any shot
        /// </summary>
        public event EventHandler<ShotMovedEventArgs> ShotMoved;
        public event EventHandler<int> ScoreChanged;
        public event EventHandler<int> LivesChanged;

        #endregion

        #region Properties

        public int Score
        {
            get { return _score; }
            private set
            {
                _score = value;
                var evt = ScoreChanged;
                evt?.Invoke(this, value);
            }
        }

        public int Wave { get; private set; }

        public int Lives
        {
            get { return _lives; }
            private set
            {
                _lives = value;
                var evt = LivesChanged;
                evt?.Invoke(this, value);
            }
        }

        public bool GameOver
        {
            get { return _gameOver; }
            set
            {
                if (_gameOver == value)
                {
                    return;
                }

                _gameOver = value;
                var evnt = GameOverChanged;
                evnt?.Invoke(this, value);
            }
        }

        public List<Invader> Invaders { get; } = new List<Invader>();

        public List<Shot> PlayerShots { get; } = new List<Shot>();

        public List<Shot> InvaderShots { get; } = new List<Shot>();

        public Size PlayAreaSize { set; get; } = new Size(110, 360);

        #endregion

        #region Public methods

        /// <summary>
        ///     Ends the game and resets the score to zero
        /// </summary>
        public void EndGame()
        {
            Score = 0;
            GameOver = true;
        }

        /// <summary>
        ///     Sets the game from the initial state
        /// </summary>
        public void InitializeGameStatus()
        {
            GameOver = false;
            Lives = 2;
            Wave = 0;
            PlayerShots.Clear();
            InvaderShots.Clear();
            Invaders.Clear();
        }

        /// <summary>
        ///     Initializes the player ship and sets it in the initial location
        /// </summary>
        public void InitializePlayerShip()
        {
            _player.Location = new Point(Player.PlayerSize.Width, PlayAreaSize.Height - Player.PlayerSize.Height*3);
            TriggerShipChanged(_player, false);
        }

        /// <summary>
        ///     Creates a rocket shot by the player
        /// </summary>
        public void RocketShot(Point startingPoint)
        {
            if (GameOver)
                return;
            if (PlayerShots.Count >= MaximumPlayerShots)
                return;

            var shot = new Shot(startingPoint, Direction.Up, ShotType.Rocket);
            PlayerShots.Add(shot);
            TriggerShotMoved(shot, false);
        }

        /// <summary>
        ///     Updates the location of the player
        /// </summary>
        /// <param name="direction"> Left or Right</param>
        public void MovePlayer(Direction direction)
        {
            if (_isPlayerDead) return;
            switch (direction)
            {
                case Direction.Left:
                    if (_player.Location.X > _player.Size.Width)
                        _player.Move(direction);
                    break;
                case Direction.Right:
                    if (_player.Location.X < PlayAreaSize.Width - _player.Size.Width - 5)
                    {
                        _player.Move(direction);
                    }

                    break;
            }
            TriggerShipChanged(_player, false);
        }

        /// <summary>
        ///     Moves all the shots (rockets and bombs) in the screen
        /// </summary>
        public void MoveShots()
        {
            if (_isPlayerDead) return;

            foreach (var shot in PlayerShots)
            {
                shot.Move();
                TriggerShotMoved(shot, false);
            }

            foreach (var shot in InvaderShots)
            {
                shot.Move();
                TriggerShotMoved(shot, false);
            }
        }

        /// <summary>
        ///     Removes all shots went out of bounds
        /// </summary>
        public void RemoveOutOfBoundShots(double playerShotsBound, double invadorShotBound)
        {
            var outOfBoundsPlayerShots = (from shot in PlayerShots
                where shot.Location.Y < playerShotsBound
                select shot).ToList();


            var outOfBoundsInvadersShots =
                (from shot in InvaderShots
                    where shot.Location.Y > invadorShotBound
                    select shot).ToList();

            if (outOfBoundsPlayerShots != null)
            {
                foreach (var shot in outOfBoundsPlayerShots)
                {
                    PlayerShots.Remove(shot);
                    TriggerShotMoved(shot, true);
                }
            }

            if (outOfBoundsInvadersShots == null) return;


            foreach (var shot in outOfBoundsInvadersShots)
            {
                InvaderShots.Remove(shot);
                TriggerShotMoved(shot, true);
            }
        }

        /// <summary>
        /// New stage in the game where new invaders created 
        /// </summary>
        public void NextWave()
        {
            Wave++;
            Invaders.Clear();
            for (var row = 0; row <= 1; row++)
                for (var column = 0; column < 11; column++)
                {
                    var location = new Point(column*Invader.InvaderSize.Width*1.4, row*Invader.InvaderSize.Height*1.4);
                    Invader invader;
                    switch (row)
                    {
                        case 0:
                            invader = new Invader(InvaderType.Enemy2, location, 20);
                            break;
                        default:
                            invader = new Invader(InvaderType.Enemy1, location, 10);
                            break;
                    }
                    Invaders.Add(invader);
                    TriggerShipChanged(invader, false);
                }
        }

        /// <summary>
        /// Check if the player got shot by a bomb
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerHit()
        {
            // If the player got shot 
            var shotsHit =
                from shot in InvaderShots
                where _player.Area.Contains(shot.Location)
                select shot;

            return shotsHit.Any();
        }

        /// <summary>
        /// Removes all the shots from the screen 
        /// </summary>
        public void RemoveShots()
        {
            foreach (var shot in PlayerShots.ToList()) {
                PlayerShots.Remove(shot);
                TriggerShotMoved(shot, true);
            }
            foreach (var shot in InvaderShots.ToList()) {
                InvaderShots.Remove(shot);
                TriggerShotMoved(shot, true);
            }
        }

        /// <summary>
        /// Checks if an invador has reaches the buttom 
        /// </summary>
        /// <returns></returns>
        public bool IsInvadorsReachedTheButtom()
        {
            var result = from invader in Invaders
                         where invader.Area.Bottom > _player.Area.Top + _player.Size.Height
                         select invader;
            return result.Any();
        }

        /// <summary>
        /// Checks if there is a collision between the player with an invador
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerInvadorCollision()
        {
            if (_isPlayerDead) return false;

            var collidedInvaders = (from invader in Invaders
                where invader.Area.IntersectsWith(_player.Area)
                select invader).ToList();

            if (!collidedInvaders.Any()) return false;

            foreach (var invader in collidedInvaders.ToList())
            {
                Score += invader.Score;
                Invaders.Remove(invader);
                TriggerShipChanged(invader, true);
            }
            return true;
        }
        /// <summary>
        /// Removes one life from the player and puts the player in dying state for 2.5 seconds 
        /// </summary>
        public async void HitPlayer()
        {
            Lives--;
            if (Lives >= 0)
            {
                _isPlayerDead = true;
                TriggerShipChanged(_player, true);
            }
            else
                EndGame();
            await Task.Delay(TimeSpan.FromSeconds(2.5));

            _isPlayerDead = false;
            TriggerShipChanged(_player, false);
        }

        public void CheckForInvaderHit()
        {
            if (_isPlayerDead) return;

            var shotsHit = new List<Shot>();
            var invadersKilled = new List<Invader>();
            foreach (var shot in PlayerShots)
            {
                var invadersShot = (from invader in Invaders
                    where invader.Area.Contains(shot.Location) && shot.Direction == Direction.Up
                    select new {InvaderKilled = invader, ShotHit = shot}).ToList();

                if (!invadersShot.Any())
                    continue;

                foreach (var invadorshot in invadersShot)
                {
                    shotsHit.Add(invadorshot.ShotHit);
                    invadersKilled.Add(invadorshot.InvaderKilled);
                }
            }
            foreach (var invader in invadersKilled)
            {
                Score += invader.Score;
                Invaders.Remove(invader);
                TriggerShipChanged(invader, true);
            }
            foreach (var shot in shotsHit)
            {
                PlayerShots.Remove(shot);
                TriggerShotMoved(shot, true);
            }
        }


        public void MoveInvaders()
        {
            if (_isPlayerDead) return;


            if (!_helper.CanInvadorsMove(Wave, Invaders.Count, _lastUpdated)) return;
            _lastUpdated = DateTime.Now;



            var invadersTouchingLeftBoundary = from invader in Invaders
                                               where invader.Area.Left < Invader.HorizontalInterval
                                               select invader;
            var invadersTouchingRightBoundary = from invader in Invaders
                                                where invader.Area.Right > PlayAreaSize.Width - Invader.HorizontalInterval * 2
                                                select invader;


            if (!_justMovedDown)
            {
                if (invadersTouchingLeftBoundary.Any())
                {
                    foreach (var invader in Invaders)
                    {
                        invader.Move(Direction.Down);
                        TriggerShipChanged(invader, false);
                    }
                    _invaderDirection = Direction.Right;
                }
                else if (invadersTouchingRightBoundary.Any())
                {
                    foreach (var invader in Invaders)
                    {
                        invader.Location = new Point(
                            PlayAreaSize.Width - (PlayAreaSize.Width - invader.Location.X), invader.Location.Y);
                        invader.Move(Direction.Down);
                        TriggerShipChanged(invader, false);
                    }
                    _invaderDirection = Direction.Left;
                }
                _justMovedDown = true;
            }
            else
            {
                _justMovedDown = false;
                foreach (var invader in Invaders)
                {
                    invader.Move(_invaderDirection);
                    TriggerShipChanged(invader, false);
                }
            }
        }


        /// <summary>
        ///     Adds a bomb from an invader
        /// </summary>
        /// <param name="bombLocation">
        ///     the location where the bomb should be dropped from, if it is not assigned then a random
        ///     location will be assigned
        /// </param>
        public void FireBomb(Point bombLocation = new Point())
        {
            if (_isPlayerDead)
                return;
            if (!Invaders.Any())
                return;

            if (!_helper.CanFireBomb(InvaderShots.Count, Wave))
                return;

            if (!bombLocation.Initialized)
            {
                bombLocation = _helper.GetRandomBombLocation(Invaders);
            }

            var invaderShot = new Shot(bombLocation, Direction.Down, ShotType.Bomb);

            InvaderShots.Add(invaderShot);
            TriggerShotMoved(invaderShot, false);
        }

        #endregion

        #region Private methods

        private void TriggerShipChanged(Ship shipUpdated, bool killed)
        {
            var shipChanged = ShipChanged;
            shipChanged?.Invoke(this, new ShipChangedEventArgs(shipUpdated, killed));
        }

        private void TriggerShotMoved(Shot shot, bool disappeared)
        {
            var shotMoved = ShotMoved;
            shotMoved?.Invoke(this, new ShotMovedEventArgs(shot, disappeared));
        }

        #endregion
    }
}