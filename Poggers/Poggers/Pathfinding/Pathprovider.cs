using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Poggers.Pathfinding
{
    public class Pathprovider
    {
        private static Pathprovider instance;
        private bool[,] grid;
        private float cellWidth;
        private ConcurrentDictionary<Tuple<Vector2i, Vector2i>, Vector2> path;

        public Pathprovider(bool[,] grid, float cellWidth)
        {
            this.grid = grid;
            this.cellWidth = cellWidth;
            this.GeneratePath();
        }

        public static Pathprovider Instance { get => instance; set => instance = value; }

        public static bool IsTraversable(bool[,] grid, Vector2i vector)
        {
            return grid.GetLowerBound(0) <= vector.X && grid.GetUpperBound(0) >= vector.X && grid.GetLowerBound(1) <= vector.Y && grid.GetUpperBound(1) >= vector.Y && !grid[vector.X, vector.Y];
        }

        public Vector2 GetDirectionVector(Vector2 position, Vector2 playerPos)
        {
            Vector2i start = this.TranslateToGrid(position), target = this.TranslateToGrid(playerPos);

            Vector2 direction = this.GetDirectionVector(start, target);
            Vector2 correction = Vector2.Subtract(position, Vector2.Multiply(start, (this.cellWidth, -this.cellWidth)));

            Vector2i offset = (0, 0);

            if (direction.Y != 0)
            {
                if (correction.X > 0)
                {
                    offset = (1, 0);
                }
                else if (correction.X < 0)
                {
                    offset = (-1, 0);
                }
            }

            if (direction.X != 0)
            {
                if (correction.Y > 0)
                {
                    offset = (0, -1);
                }
                else if (correction.Y < 0)
                {
                    offset = (0, 1);
                }
            }

            Vector2i tmp = ((int)Math.Ceiling(direction.X), (int)Math.Ceiling(direction.Y));
            Vector2i testPos = Vector2i.Add(Vector2i.Add(start, tmp), offset);

            if (!Pathprovider.IsTraversable(this.grid, testPos))
            {
                return Vector2.Multiply(offset, (-1, 1));
            }

            // Invert at the x-axis
            return Vector2.Multiply(direction, (1, -1));
        }

        public Vector2 GetDirectionVector(Vector2i start, Vector2i target)
        {
            if (!this.path.TryGetValue(new Tuple<Vector2i, Vector2i>(start, target), out Vector2 direction))
            {
                return (0, 0);
            }

            return direction;
        }

        private void GeneratePath()
        {
            List<Vector2i> vectorsToExpand = new List<Vector2i>();
            this.path = new ConcurrentDictionary<Tuple<Vector2i, Vector2i>, Vector2>();
            for (int targetX = 0; targetX < this.grid.GetLength(0); targetX++)
            {
                for (int targetY = 0; targetY < this.grid.GetLength(1); targetY++)
                {
                    Vector2i target = (targetX, targetY);

                    if (IsTraversable(this.grid, target))
                    {
                        vectorsToExpand.Add(target);
                    }
                }
            }

            Parallel.ForEach(vectorsToExpand, targetVector => this.FillDictionary(targetVector));
        }

        private void FillDictionary(Vector2i target)
        {
            HashSet<Node> results = new HashSet<Node>(new NodePositionEquality());
            this.FillResults(target, results);

            for (int startX = 0; startX < this.grid.GetLength(0); startX++)
            {
                for (int startY = 0; startY < this.grid.GetLength(1); startY++)
                {
                    Vector2i start = (startX, startY);
                    if (results.TryGetValue(new Node(null, start), out Node resultNode))
                    {
                        this.path.TryAdd(new Tuple<Vector2i, Vector2i>(start, target), resultNode.Direction);
                    }
                }
            }
        }

        private void FillResults(Vector2i target, HashSet<Node> results)
        {
            Queue<Node> open = new Queue<Node>();
            Node initialNode = new Node(null, target);

            open.Enqueue(initialNode);
            while (open.Count > 0)
            {
                List<Node> tmp = open.Dequeue().Expand(this.grid);
                foreach (Node node in tmp)
                {
                    Node original = null;
                    if (!results.Contains(node) || (results.TryGetValue(node, out original) && original.Costs > node.Costs))
                    {
                        results.Remove(original);
                        results.Add(node);
                        open.Enqueue(node);
                    }
                }
            }
        }

        private Vector2i TranslateToGrid(Vector2 vector)
        {
            // Invert at x-axis
            vector = Vector2.Multiply(vector, (1, -1));
            return ((int)Math.Round(vector.X / this.cellWidth), (int)Math.Round(vector.Y / this.cellWidth));
        }
    }
}
