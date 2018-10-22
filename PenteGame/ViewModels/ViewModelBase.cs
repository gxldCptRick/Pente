using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PenteGame.ViewModels
{
    abstract class ViewModelBase : INotifyPropertyChanged //An Event to see if a property changed.
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void PropertyChanging([CallerMemberName] string propertyName = null){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
