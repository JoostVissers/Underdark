using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Underdark.movement;

namespace Underdark
{
    class InputHandler
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        private ICommand moveLeftCommand;
        private ICommand moveRightCommand;
        private ICommand moveUpCommand;
        private ICommand moveDownCommand;
        private ICommand attackCommand;

        public InputHandler()
        {
            moveLeftCommand = new MoveLeftCommand();
            moveRightCommand = new MoveRightCommand();
            moveUpCommand = new MoveUpCommand();
            moveDownCommand = new MoveDownCommand();
            attackCommand = new AttackCommand();
        }

        public ICommand handleInput()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            Keys movementLeft = Keys.A;
            Keys movementRight = Keys.D;
            Keys movementUp = Keys.W;
            Keys movementDown = Keys.S;
            Keys actionAttack = Keys.Space;

            if (currentKeyboardState.IsKeyDown(movementLeft) && previousKeyboardState.IsKeyUp(movementLeft))
            {
                return moveLeftCommand;
            }
            else if (currentKeyboardState.IsKeyDown(movementRight) && previousKeyboardState.IsKeyUp(movementRight))
            {
                return moveRightCommand;
            }
            else if (currentKeyboardState.IsKeyDown(movementUp) && previousKeyboardState.IsKeyUp(movementUp))
            {
                return moveUpCommand;
            }
            else if (currentKeyboardState.IsKeyDown(movementDown) && previousKeyboardState.IsKeyUp(movementDown))
            {
                return moveDownCommand;
            }
            else if (currentKeyboardState.IsKeyDown(actionAttack) && previousKeyboardState.IsKeyUp(actionAttack))
            {
                return attackCommand;
            }
            else
            {
                return null;
            }
        }
    }
}
