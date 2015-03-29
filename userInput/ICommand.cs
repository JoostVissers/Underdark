using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underdark
{
    interface ICommand
    {
       void execute(Actor actor);
    }
}
