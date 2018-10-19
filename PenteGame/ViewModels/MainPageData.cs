using PenteGame.Lib.Controllers;
using PenteGame.Lib.Enums;

namespace PenteGame.ViewModels
{
    internal class MainPageData : ViewModelBase
    {
        private int _gridSize;
        private PlayerData _playerOne;
        private PlayerData _playerTwo;
        private PenteController _mainGameController;

        private int _timerCount;

        public int TimerCount
        {
            get => _timerCount;
            set
            {
                _timerCount = value;
                if (value == 0)
                {
                    Game.SwitchTurn();
                }
                PropertyChanging();
            }
        }

        public PieceColor CurrentTurn => Game.CurrentTurn;

        public PlayerData PlayerOne
        {
            get => _playerOne;
            set { _playerOne = value; PropertyChanging(); }
        }

        public PlayerData PlayerTwo
        {
            get => _playerTwo;
            set { _playerTwo = value; PropertyChanging(); }
        }

        public PenteController Game => _mainGameController;
        public int GridSize
        {
            get => _gridSize;
            set
            {
                if (value % 2 != 0)
                {
                    _gridSize = value;
                    PropertyChanging();
                    _mainGameController.Width = value;
                    _mainGameController.Height = value;
                }
            }
        }

        public MainPageData()
        {
            TimerCount = 20;
            _gridSize = 19;
            PlayerOne = new PlayerData(PieceColor.Black);
            PlayerOne.Name = "Player One";
            PlayerTwo = new PlayerData(PieceColor.White);
            PlayerTwo.Name = "Player Two";
            _mainGameController = new PenteController();
            UpdateCaptureValues();
            _mainGameController.TurnChanging += TurnChanged;
        }

        public void ResetGame()
        {
            Game.ResetGame();
            UpdateCaptureValues();
            TimerCount = 20;
        }

        public void UpdateCaptureValues()
        {
            PlayerOne.NumberOfCaptures = Game.GetTotalCaptures(PieceColor.Black);
            PlayerTwo.NumberOfCaptures = Game.GetTotalCaptures(PieceColor.White);
        }

        private void TurnChanged(PieceColor color)
        {
            PropertyChanging(nameof(CurrentTurn));
        }
    }
}
