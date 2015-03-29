using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underdark
{
    public abstract class Actor
    {
        public Texture2D actorTexture;

        public int height
        {
            get { return actorTexture.Height; }
        }

        public int width
        {
            get { return actorTexture.Width; }
        }

        protected int health;
        protected Boolean playerIsActive;
        public Vector2 playerPosition;


        abstract public void Draw(SpriteBatch sb);
        abstract public void Update();
        abstract public void moveLeft();
        abstract public void moveRight();
        abstract public void moveUp();
        abstract public void moveDown();
        abstract public void attack();
        abstract public void Initialize(Texture2D ActorTexture, Vector2 ActorPosition);
        abstract public void interact();
    }
}
