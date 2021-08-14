using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace Poggers.Directions
{
    public static class DirectionExtension
    {
        public static Vector2i GetDirectionVector(this Direction direction)
        {
            switch (direction)
            {
                case Direction.A:
                    return (-1, 0);
                case Direction.D:
                    return (1, 0);
                case Direction.W:
                    return (0, 1);
                case Direction.S:
                    return (0, -1);
                case Direction.WA:
                    return (-1, 1);
                case Direction.WD:
                    return (1, 1);
                case Direction.SA:
                    return (-1, -1);
                case Direction.SD:
                    return (1, -1);
            }

            throw new InvalidOperationException();
        }

        public static Vector2 GetDirectionNormalized(this Direction direction)
        {
            return Vector2.Normalize(GetDirectionVector(direction));
        }

        public static Vector2 GetDirectionWithLength(this Direction direction, float length)
        {
            if (length <= 0)
            {
                throw new ArgumentException();
            }

            return Vector2.Multiply(GetDirectionNormalized(direction), length);
        }

        public static List<Direction> Deconstruct(this Direction direction)
        {
            List<Direction> tmp = new List<Direction>(2);

            if (direction == Direction.WA)
            {
                tmp.Add(Direction.W);
                tmp.Add(Direction.A);
            }
            else if (direction == Direction.WD)
            {
                tmp.Add(Direction.W);
                tmp.Add(Direction.D);
            }
            else if (direction == Direction.SA)
            {
                tmp.Add(Direction.S);
                tmp.Add(Direction.A);
            }
            else if (direction == Direction.SD)
            {
                tmp.Add(Direction.S);
                tmp.Add(Direction.D);
            }

            return tmp;
        }

        /// <summary>
        /// Returns the Direction with the smalles angle between the vector and the normalized direction vector.
        /// </summary>
        /// <param name="vector">The vector, from which the nearest direction is requested</param>
        /// <returns>The direction with the smalles angle.</returns>
        public static Direction GetDirectionFromVector2(Vector2 vector)
        {
            vector.Normalize();

            // Random direction
            Tuple<Direction, double> minimum = new Tuple<Direction, double>(Direction.D, double.MaxValue);
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                double value = Math.Acos(Vector2.Dot(dir.GetDirectionNormalized(), vector));
                if (value < minimum.Item2)
                {
                    minimum = new Tuple<Direction, double>(dir, value);
                }
            }

            return minimum.Item1;
        }
    }
}
