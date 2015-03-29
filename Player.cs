using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underdark
{
    class Player : Actor,IFocusable
    {
        private float playerSpeed;    

        public override void Initialize(Texture2D actorTexture, Vector2 actorPosition)
        {
            this.actorTexture = actorTexture;
            playerPosition = actorPosition;
            playerIsActive = true;
            health = 100;
            playerSpeed = actorTexture.Width/2;
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(actorTexture, playerPosition, null, Color.White, 0.0f, new Vector2(0, 0), 0.5f, SpriteEffects.FlipHorizontally, 0.0f);
        }

        public override void moveLeft()
        {
            if (TileMap.GetTileAtPixel(((int)playerPosition.X - width / 2), ((int)playerPosition.Y)) != Tile.Black)
            {
                playerPosition.X -= playerSpeed;
                //playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, 4000 - width);
            }
        }

        public override void moveRight()
        {
            if (TileMap.GetTileAtPixel(((int)playerPosition.X + width / 2), ((int)playerPosition.Y)) != Tile.Black)
            {
                playerPosition.X += playerSpeed;
                //playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, 4000 - width);
            }
        }

        public override void moveDown()
        {
            if (TileMap.GetTileAtPixel((int)playerPosition.X, ((int)playerPosition.Y + height / 2)) != Tile.Black)
            {
                playerPosition.Y += playerSpeed;
                //playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, 4000 - height);
            }            
        }

        public override void moveUp()
        {
            if (TileMap.GetTileAtPixel((int)playerPosition.X, ((int)playerPosition.Y - height / 2)) != Tile.Black)
            {
                playerPosition.Y -= playerSpeed;
                //playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, 4000 - height);
            }
        }

        public override void attack()
        {
            System.Console.Out.Write("RAWR ATTACK\n");
        }

        public void setPlayerSpeed(float playerSpeed){
            this.playerSpeed = playerSpeed;
        }


        public Vector2 Position
        {
            get { return new Vector2(playerPosition.X + width/4, playerPosition.Y); }
        }

        public override void interact()
        {
            System.Console.Out.Write("RAWR INTERACT\n");
        }
    }
}
