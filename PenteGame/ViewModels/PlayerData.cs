using PenteGame.Lib.Enums;

namespace PenteGame.ViewModels
{
    internal class PlayerData : ViewModelBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set { _name = value; PropertyChanging(); }
        }

        private int _numberOfWins;

        public int NumberOfWins
        {
            get => _numberOfWins;
            set { _numberOfWins = value; PropertyChanging(); }
        }


        private int _numberOfCaptures;

        public int NumberOfCaptures
        {
            get => _numberOfCaptures;
            set { _numberOfCaptures = value; PropertyChanging(); }
        }

        private PieceColor _color;

        public PieceColor Color
        {
            get { return _color; }
            set { _color = value; PropertyChanging(); }
        }


    }
}
