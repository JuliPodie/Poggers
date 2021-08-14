using System;
using OpenTK.Mathematics;
using Poggers.Directions;

namespace Poggers.GameObjects.Weapons.Attacks
{
    public class SwingAttack : Attack
    {
        private readonly float radius;
        private readonly float width;
        private Color4 color;

        public SwingAttack(IWeapon weapon, float damage, float range, float width, int duration, Color4 color)
            : base(weapon, damage, duration, range)
        {
            this.color = color;
            this.radius = width / 8;
            this.width = width;
        }

        public override void InitializeHitboxes()
        {
            this.Hitboxes.Add(new Projectile(this.GetProjectileCenter(this.Weapon.Owner.Direction), this.radius, this.GetProjectileDirection(this.Weapon.Owner.Direction), this.width / this.UpdateRepetitions, 0, this.color, this.Duration));
        }

        private Vector2 GetProjectileCenter(Direction direction)
        {
            // Vector of Direction.W
            Vector2 offset = ((this.width / 2) - this.radius, this.Range - this.radius);
            double angle = Math.PI * 45 / 180.0;

            angle *= direction switch
            {
                Direction.W => 0,
                Direction.WA => 1,
                Direction.A => 2,
                Direction.SA => 3,
                Direction.S => 4,
                Direction.SD => 5,
                Direction.D => 6,
                Direction.WD => 7,
                _ => throw new NotImplementedException(),
            };

            // Rotate the vector "offset" counterclockwise with the angle "angle"
            return Vector2.Add(this.Weapon.Owner.Center, ((float)((Math.Cos(angle) * offset.X) - (Math.Sin(angle) * offset.Y)), (float)((Math.Sin(angle) * offset.X) + (Math.Cos(angle) * offset.Y))));
        }

        private Direction GetProjectileDirection(Direction attackDirection)
        {
            return attackDirection switch
            {
                Direction.A => Direction.S,
                Direction.D => Direction.W,
                Direction.W => Direction.A,
                Direction.S => Direction.D,
                Direction.WA => Direction.SA,
                Direction.WD => Direction.WA,
                Direction.SA => Direction.SD,
                Direction.SD => Direction.WD,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
