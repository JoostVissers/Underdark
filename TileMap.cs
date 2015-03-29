using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underdark
{
    class TileMap
    {
        public const int TileWidth = 32;
        public const int TileHeight = 32;
        public const int MapWidth = 128;
        public const int MapHeight = MapWidth;
        private const int corridor_length = 2;
        private Random r = new Random();

        private Rectangle[,] tilesMapRectangles = new Rectangle[MapWidth,MapHeight];
        private Dictionary<int,Texture2D> tiles;
        private static int[,] map = new int[MapWidth,MapHeight];

        public void Initialize(Dictionary<int,Texture2D> tiles)
        {
            if (this.tiles != null)
            {
                this.tiles.Clear();
            }
            this.tiles = tiles;

            mapSetup();

            generateRandomMap(MapWidth, MapHeight);
        }

        private void mapSetup()
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    tilesMapRectangles[x, y] = new Rectangle(TileWidth * x, TileHeight * y, TileWidth, TileHeight);
                    map[x, y] = Tile.Black;
                }
            }
        }

        public void generateRandomMap(int mapWidth, int mapHeight)
        {
            int xRoomStart = mapWidth / 2;
            int yRoomStart = mapHeight / 2;

            //Starting room
            setNewRoom(1,xRoomStart, yRoomStart, 3, 3);

            int roomChance = 35;

            for (int i = 0; i < 400; i++)
            {
                int[] wallPosition = findRandomWallTile();
                if (r.Next(0, 101) <= roomChance)
                {
                    newRoom(wallPosition[0], wallPosition[1]);
                }
                else
                {
                    newCorridor(wallPosition[0], wallPosition[1]);
                }
            }

        }

        public void newCorridor(int startX,int startY)
        {
            int chancesToFindSpace = 1000;

            for (int i = 0; i < chancesToFindSpace; i++)
            {
                int corridorNewDirection = r.Next(Directions.North, Directions.West);

                int randomCorridorL = r.Next(corridor_length,6);

                if (checkSpaceForNewCorridor(corridorNewDirection, startX, startY, randomCorridorL))
                {
                    setNewCorridor(corridorNewDirection, startX, startY, randomCorridorL);
                    chancesToFindSpace = 1001;
                }
            }
        }

        public void newRoom(int startX,int startY)
        {
            int roomWidth = r.Next(4, 11);
            int roomHeight = r.Next(4, 11);
            int chancesToFindSpace = 1000;

            for (int i = 0; i < chancesToFindSpace; i++)
            {
                int roomNewDirection = r.Next(Directions.North, Directions.West);

                if (checkSpaceForNewRoom(roomNewDirection, startX, startY, roomWidth, roomHeight))
                {
                    setNewRoom(roomNewDirection, startX, startY, roomWidth, roomHeight);
                    i = 1001;
                }
            }
        }

        public void setNewRoom(int direction,int startX, int startY,int width,int height)
        {
            if (direction == Directions.North)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        map[startX + x, startY - y] = Tile.White;
                    }
                }
            }
            else if (direction == Directions.East)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        map[startX + x, startY + y] = Tile.White;
                    }
                }
            }
            else if (direction == Directions.South)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        map[startX - x, startY + y] = Tile.White;
                    }
                }
            }
            else if (direction == Directions.West)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        map[startX - x, startY - y] = Tile.White;
                    }
                }
            }
        }

        public Boolean checkSpaceForNewRoom(int direction, int x, int y, int width,int height)
        {
            if (direction == Directions.North)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (map[x + j + 1, y - i + 1] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x + j, y - i] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x + j -1, y - i -1] == Tile.White)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else if (direction == Directions.East)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (map[x + i + 1, y + j + 1] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x + i, y + j] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x + i - 1, y + j -1] == Tile.White)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else if (direction == Directions.South)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (map[x - j + 1, y + i + 1] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x - j, y + i] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x - j - 1, y + i -1] == Tile.White)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else if (direction == Directions.West)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (map[x - i+1, y - j+1] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x - i, y - j] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x - i-1, y - j-1] == Tile.White)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public void setNewCorridor(int direction, int x, int y, int length)
        {
            if (direction == Directions.North)
            {
                for (int i = 0; i < length; i++)
                {
                    map[x,y - i] = Tile.White;
                }
            }
            else if (direction == Directions.East)
            {
                for (int i = 0; i < length; i++)
                {
                    map[x+i, y] = Tile.White;
                }
            }
            else if (direction == Directions.South)
            {
                for (int i = 0; i < length; i++)
                {
                    map[x, y + i] = Tile.White;
                }
            }
            else if (direction == Directions.West)
            {
                for (int i = 0; i < length; i++){
                    map[x - i, y] = Tile.White;
                }
            }
        }

        public Boolean checkSpaceForNewCorridor(int direction,int x,int y,int length)
        {

            if(direction == Directions.North){
                for(int i = 0; i < length; i++){
                        if (map[x, y - i] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x+1, y - i] == Tile.White)
                        {
                            return false;
                        }
                        if (map[x-1, y - i] == Tile.White)
                        {
                            return false;
                        }
                }
                return true;
            }
            else if(direction == Directions.East){
                for(int i = 0; i < length; i++){
                    if(map[x+i,y-1] == Tile.White){
                        return false;
                    }
                    if (map[x + i, y] == Tile.White)
                    {
                        return false;
                    }
                    if (map[x + i, y+1] == Tile.White)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if(direction == Directions.South){
                for(int i = 0; i < length; i++){
                    if(map[x -1,y+i] == Tile.White){
                        return false;
                    }
                    if (map[x, y + i] == Tile.White)
                    {
                        return false;
                    }
                    if (map[x +1, y + i] == Tile.White)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if(direction == Directions.West){
                for(int i = 0; i < length; i++){
                    
                    if(map[x-i,y -1] == Tile.White){
                        return false;
                    }
                    if (map[x - i, y] == Tile.White)
                    {
                        return false;
                    }
                    if (map[x - i, y +1] == Tile.White)
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        public int[] findRandomWallTile()
        {
            Boolean wallFound = false;
            int[] wallPos = new int[2];
            while (wallFound == false)
            {
                int xWallTilePosition = r.Next(1, MapWidth - 1);
                int yWallTilePosition = r.Next(1, MapHeight - 1);
                if (map[xWallTilePosition, yWallTilePosition] != Tile.White)
                {
                    //Check left
                    if (map[xWallTilePosition - 1, yWallTilePosition] == Tile.White)
                    {
                        
                        wallPos[0] = xWallTilePosition;
                        wallPos[1] = yWallTilePosition;
                        wallFound = true;
                    }
                    //Check right
                    if (map[xWallTilePosition + 1, yWallTilePosition] == Tile.White)
                    {
                        
                        wallPos[0] = xWallTilePosition;
                        wallPos[1] = yWallTilePosition;
                        wallFound = true;
                    }
                    //Check top
                    if (map[xWallTilePosition, yWallTilePosition - 1] == Tile.White)
                    {
                        
                        wallPos[0] = xWallTilePosition;
                        wallPos[1] = yWallTilePosition;
                        wallFound = true;
                    }
                    //Check down
                    if (map[xWallTilePosition, yWallTilePosition + 1] == Tile.White)
                    {
                        
                        wallPos[0] = xWallTilePosition;
                        wallPos[1] = yWallTilePosition;
                        wallFound = true;
                    }
                }
            }
            return wallPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = null;
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    if (map[y, x] == Tile.Black)
                    {
                        texture = tiles[Tile.Black];
                    }
                    else if (map[y, x] == Tile.White)
                    {
                        texture = tiles[Tile.White];
                    }
    
                    spriteBatch.Draw(texture,tilesMapRectangles[y,x],Color.White);
                }
            }
        }

        static public int GetTileAtSquare(int tileX, int tileY)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                return map[tileX, tileY];
            }
            else
            {
                return -1;
            }
        }

        static public void SetTileAtSquare(int tileX, int tileY, int tile)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                map[tileX, tileY] = tile;
            }
        }

        static public int GetTileAtPixel(int pixelX, int pixelY)
        {
            return GetTileAtSquare(
                GetSquareByPixelX(pixelX),
                GetSquareByPixelY(pixelY));
        }

        static public int GetTileAtPixel(Vector2 pixelLocation)
        {
            return GetTileAtPixel(
                (int)pixelLocation.X,
                (int)pixelLocation.Y);
        }

        static public bool IsWallTile(int tileX, int tileY)
        {
            int tileIndex = GetTileAtSquare(tileX, tileY);

            if (tileIndex == -1)
            {
                return false;
            }

            return tileIndex == Tile.Black;
        }

        static public bool IsWallTile(Vector2 square)
        {
            return IsWallTile((int)square.X, (int)square.Y);
        }

        static public bool IsWallTileByPixel(Vector2 pixelLocation)
        {
            return IsWallTile(
                GetSquareByPixelX((int)pixelLocation.X),
                GetSquareByPixelY((int)pixelLocation.Y));
        }

        static public int GetSquareByPixelX(int pixelX)
        {
            return pixelX / TileWidth;
        }

        static public int GetSquareByPixelY(int pixelY)
        {
            return pixelY / TileHeight;
        }
    }
}
