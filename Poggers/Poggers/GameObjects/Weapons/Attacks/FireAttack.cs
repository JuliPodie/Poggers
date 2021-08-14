using System.Collections.Generic;

namespace Poggers.GameObjects.Weapons.Attacks
{
    public class FireAttack : Attack
    {
        private readonly float initalRadius;
        private List<int> tex = new List<int>();

        public FireAttack(IWeapon weapon, float damage, int duration, float initalRadius, float range)
            : base(weapon, damage, duration, range)
        {
            this.tex.Add(22);
            this.tex.Add(23);
            this.tex.Add(24);
            this.initalRadius = initalRadius;
        }

        public override void InitializeHitboxes()
        {
            ExpandingAttack expanding = new ExpandingAttack(this.Weapon.Owner.Center, this.initalRadius, (this.Range - this.initalRadius) / this.UpdateRepetitions, this.tex);
            this.Hitboxes.Add(expanding);
        }
    }
}
