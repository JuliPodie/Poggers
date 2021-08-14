using System;

namespace Poggers.Collision
{
    public abstract class ColliderFunctions
    {
        public static bool Collides(ICollidable collidable1, ICollidable collidable2)
        {
            if (collidable1 is ICollidableCircle && collidable2 is ICollidableCircle)
            {
                return Collides(collidable1 as ICollidableCircle, collidable2 as ICollidableCircle);
            }
            else if (collidable1 is ICollidableRectangle && collidable2 is ICollidableRectangle)
            {
                return Collides(collidable1 as ICollidableRectangle, collidable2 as ICollidableRectangle);
            }
            else if (collidable1 is ICollidableCircle && collidable2 is ICollidableRectangle)
            {
                return Collides(collidable1 as ICollidableCircle, collidable2 as ICollidableRectangle);
            }
            else if (collidable1 is ICollidableRectangle && collidable2 is ICollidableCircle)
            {
                return Collides(collidable2 as ICollidableCircle, collidable1 as ICollidableRectangle);
            }

            throw new NotImplementedException();
        }

        private static bool Collides(ICollidableCircle collidable1, ICollidableCircle collidable2)
        {
            return Math.Pow(collidable1.Center.X - collidable2.Center.X, 2) + Math.Pow(collidable1.Center.Y - collidable2.Center.Y, 2) <= Math.Pow(collidable1.Radius + collidable2.Radius, 2);
        }

        private static bool Collides(ICollidableRectangle collidable1, ICollidableRectangle collidable2)
        {
            float width1 = collidable1.Width / 2, width2 = collidable2.Width / 2, height1 = collidable1.Height / 2, height2 = collidable2.Height / 2;

            bool xOverlap = !(collidable1.Center.X - width1 > collidable2.Center.X + width2 || collidable2.Center.X - width2 > collidable1.Center.X + width1);
            bool yOverlap = !(collidable1.Center.Y - height1 > collidable2.Center.Y + height2 || collidable2.Center.Y - height2 > collidable1.Center.Y + height1);

            return xOverlap && yOverlap;
        }

        private static bool Collides(ICollidableCircle circle, ICollidableRectangle rectangle)
        {
            double distanceSquared = 0;
            if (circle.Center.X < rectangle.Center.X - (rectangle.Width / 2))
            {
                distanceSquared += Math.Pow(circle.Center.X - (rectangle.Center.X - (rectangle.Width / 2)), 2);
            }
            else if (circle.Center.X > rectangle.Center.X + (rectangle.Width / 2))
            {
                distanceSquared += Math.Pow(circle.Center.X - (rectangle.Center.X + (rectangle.Width / 2)), 2);
            }

            if (circle.Center.Y < rectangle.Center.Y - (rectangle.Height / 2))
            {
                distanceSquared += Math.Pow(circle.Center.Y - (rectangle.Center.Y - (rectangle.Height / 2)), 2);
            }
            else if (circle.Center.Y > rectangle.Center.Y + (rectangle.Height / 2))
            {
                distanceSquared += Math.Pow(circle.Center.Y - (rectangle.Center.Y + (rectangle.Height / 2)), 2);
            }

            return distanceSquared <= (circle.Radius * circle.Radius);
        }
    }
}
