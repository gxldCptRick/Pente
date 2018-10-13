using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteGame.Views.Intefaces
{
    public interface IPageChanger
    {
        event Action<PageRequest> PageChangeRequested;
    }
}
