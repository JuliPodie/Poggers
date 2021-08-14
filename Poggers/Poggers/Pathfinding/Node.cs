using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using OpenTK.Mathematics;
using Poggers.Directions;

namespace Poggers.Pathfinding
{
    public class Node
    {
        private readonly float costs;
        private Vector2i position;
        private Node parent;

        public Node(Node parent, Vector2i position)
        {
            this.Position = position;
            this.parent = parent;
            this.costs = parent is null ? 0 : parent.costs + CalculateCosts(parent.Position, position);
        }

        public Vector2i Position { get => this.position; set => this.position = value; }

        public Node Parent { get => this.parent; set => this.parent = value; }

        public float Costs { get => this.costs; }

        public Vector2 Direction { get => Vector2.Subtract(this.parent?.Position ?? this.position, this.position); }

        public List<Node> Expand(bool[,] map)
        {
            List<Node> expandedNodes = new List<Node>(8);

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                Vector2i resultPosition = Vector2i.Add(this.Position, direction.GetDirectionVector());
                if (resultPosition.Equals(this.parent?.Position))
                {
                    continue;
                }

                List<Direction> directionComponents = direction.Deconstruct();
                directionComponents.Add(direction);

                if (directionComponents.TrueForAll(d => Pathprovider.IsTraversable(map, Vector2i.Add(this.Position, d.GetDirectionVector()))))
                {
                    expandedNodes.Add(new Node(this, resultPosition));
                }
            }

            return expandedNodes;
        }

        private static float CalculateCosts(Vector2i parentPos, Vector2i ownPos)
        {
            return Vector2i.Subtract(ownPos, parentPos).EuclideanLength;
        }
    }
}
