namespace PenteGame.ViewModels
{
    internal class MainPageData : ViewModelBase
    {
        private PlayerData _playerOne;

        public PlayerData PlayerOne
        {
            get => _playerOne;
            set { _playerOne = value; PropertyChanging(); }
        }

        private PlayerData _playerTwo;

        public PlayerData PlayerTwo
        {
            get => _playerTwo;
            set { _playerTwo = value; PropertyChanging(); }
        }


    }
}
