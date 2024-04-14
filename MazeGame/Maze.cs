using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MazeGame
{
    public class Maze
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Cell[,] Cells { get; private set; }

        private Texture2D wallTexture;
        private Texture2D pathTexture;
        private int cellSize = 50;

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            Cells = new Cell[width, height];
        }

        public void GenerateMaze()
        {
            // Initialize all cells as walls
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells[x, y] = new Cell(x, y, true);
                }
            }

            // Choose a random cell to start from
            var rng = new Random();
            var startCell = Cells[rng.Next(Width), rng.Next(Height)];
            startCell.IsWall = false;

            // List of walls adjacent to the cells in the maze
            var walls = new List<Cell>();

            // Add the walls of the starting cell to the list
            AddWalls(startCell, walls);

            while (walls.Count > 0)
            {
                // Choose a random wall from the list
                var wall = walls[rng.Next(walls.Count)];

                // Check if the wall separates two cells, one in the maze and one not in the maze
                var adjacentCells = GetAdjacentCells(wall).Where(c => !c.IsWall).ToList();
                if (adjacentCells.Count == 1)
                {
                    // Make the wall a path and add its walls to the list
                    wall.IsWall = false;
                    AddWalls(wall, walls);
                }

                // Remove the wall from the list
                walls.Remove(wall);
            }
        }

        private void AddWalls(Cell cell, List<Cell> walls)
        {
            var adjacentWalls = GetAdjacentCells(cell).Where(c => c.IsWall).ToList();
            foreach (var wall in adjacentWalls)
            {
                if (!walls.Contains(wall))
                {
                    walls.Add(wall);
                }
            }
        }

        private IEnumerable<Cell> GetAdjacentCells(Cell cell)
        {
            if (cell.X > 0)
                yield return Cells[cell.X - 1, cell.Y];
            if (cell.X < Width - 1)
                yield return Cells[cell.X + 1, cell.Y];
            if (cell.Y > 0)
                yield return Cells[cell.X, cell.Y - 1];
            if (cell.Y < Height - 1)
                yield return Cells[cell.X, cell.Y + 1];
        }

        public void LoadContent(ContentManager contentManager)
        {
            wallTexture = contentManager.Load<Texture2D>("wall");
            pathTexture = contentManager.Load<Texture2D>("path");
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            for (int x = 0; x < spriteBatch.GraphicsDevice.Viewport.Width; x++)
            {
                // Cast a ray from the player's position
                var rayDirection = player.Direction + player.CameraPlane * ((2f * x / spriteBatch.GraphicsDevice.Viewport.Width) - 1);
                var rayPosition = player.Position;

                // Step the ray through the maze until it hits a wall
                while (!Cells[(int)rayPosition.X, (int)rayPosition.Y].IsWall)
                {
                    rayPosition += rayDirection;
                }

                // Calculate the distance to the wall
                var distanceToWall = Vector2.Distance(player.Position, rayPosition);

                // Calculate the height of the wall slice
                var wallHeight = spriteBatch.GraphicsDevice.Viewport.Height / distanceToWall;

                // Draw the wall slice
                var wallSlice = new Rectangle(x, (int)((spriteBatch.GraphicsDevice.Viewport.Height - wallHeight) / 2), 1, (int)wallHeight);
                spriteBatch.Draw(wallTexture, wallSlice, Color.White);
            }
        }
    }
}
