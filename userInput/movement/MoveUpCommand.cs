using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underdark.movement
{
    class MoveUpCommand : ICommand
    {
        public void execute(Actor actor)
        {
            actor.moveUp();
        }
    }
}
