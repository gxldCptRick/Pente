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
            _gridSize = 19;
            PlayerOne = new PlayerData(PieceColor.Black);
            PlayerTwo = new PlayerData(PieceColor.White);
            _mainGameController = new PenteController();
        }

    }
}
