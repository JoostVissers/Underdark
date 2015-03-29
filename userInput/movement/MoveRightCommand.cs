using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underdark
{
    class MoveRightCommand : ICommand
    {
        public void execute(Actor actor)
        {
            actor.moveRight();
        }
    }
}
