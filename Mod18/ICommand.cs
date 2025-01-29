using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod18
{
    public interface ICommand
    {
        Task ExecuteAsync();
    }
}
