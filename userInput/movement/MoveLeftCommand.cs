using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underdark
{
    class MoveLeftCommand : ICommand
    {
        public void execute(Actor actor)
        {
            actor.moveLeft();
        }
    }
}
