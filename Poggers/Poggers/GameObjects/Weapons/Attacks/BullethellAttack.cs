using System;
using OpenTK.Mathematics;
using Poggers.Randomize;

namespace Poggers.GameObjects.Weapons.Attacks
{
    public class BullethellAttack : Attack
    {
        private readonly float projectileWidth;
        private readonly int count;

        public BullethellAttack(IWeapon weapon, float damage, int duration, int count, float projectileWidth, float range)
            : base(weapon, damage, duration, range)
        {
            this.projectileWidth = projectileWidth;
            this.count = count;
        }

        public override void InitializeHitboxes()
        {
            for (int i = 0; i < this.count; i++)
            {
                int angle = Randomizer.GetInt(360);
                Vector2 direction = ((float)Math.Cos(angle), (float)Math.Sin(angle));
                Vector2 center = new Vector2(this.Weapon.Owner.Center.X, this.Weapon.Owner.Center.Y + (this.Weapon.Owner.Height * 0.6f));
                this.Hitboxes.Add(new Projectile(center, this.projectileWidth / 2, direction, this.Range / this.UpdateRepetitions, 21));
            }
        }
    }
}
