using PenteGame.Lib.Enums;

namespace PenteGame.ViewModels
{
    internal class PlayerData : ViewModelBase //Data that a player has
    {
        private int _numberOfCaptures;
        private int _numberOfWins;
        private string _name;
        private PieceColor _color;

        //Getters and Setters for each player property
        public string Name
        {
            get => _name;
            set { _name = value; PropertyChanging(); }
        }
        public int NumberOfCaptures
        {
            get => _numberOfCaptures;
            set { _numberOfCaptures = value; PropertyChanging(); }
        }
        public int NumberOfWins
        {
            get => _numberOfWins;
            set { _numberOfWins = value; PropertyChanging(); }
        }
        public PieceColor Color
        {
            get => _color;
            set { _color = value; PropertyChanging(); }
        }

        public PlayerData(PieceColor color)
        {
            Name = "";
            Color = color;
        }

    }
}
