using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underdark.userInput.actions
{
    class InteractCommand
    {
        public void execute(Actor actor)
        {
            actor.interact();
        }
    }
}
