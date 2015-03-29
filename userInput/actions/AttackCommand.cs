using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underdark.movement
{
    class AttackCommand : ICommand
    {
        public void execute(Actor actor)
        {
            actor.attack();
        }
    }
}
