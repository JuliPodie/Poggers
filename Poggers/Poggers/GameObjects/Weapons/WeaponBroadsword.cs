using System.Collections.Generic;
using OpenTK.Mathematics;
using Poggers.GameObjects.Weapons.Attacks;
using Poggers.Interfaces;

namespace Poggers.GameObjects.Weapons
{
    public class WeaponBroadsword : Weapon
    {
        public const int ATTACKCOOLDOWN = 500;
        public const int ATTACKWINDUP = 10;
        private const float ENDURANCECOST = 20;

        public WeaponBroadsword(IModel model, IArmedEntity owner)
            : base(model, owner)
        {
            this.EnduranceCost = ENDURANCECOST;
            this.Cooldowntime = ATTACKCOOLDOWN;
            this.Winduptime = ATTACKWINDUP;
        }

        public override void InitializeAttacks()
        {
            this.Attacks = new List<Attack>
            {
                new SwingAttack(this, 1f, 0.3f, 0.55f, 300, Color4.White),
            };
        }
    }
}
