using System.Collections.Generic;
using OpenTK.Mathematics;
using Poggers.GameObjects.Weapons.Attacks;
using Poggers.Interfaces;

namespace Poggers.GameObjects.Weapons
{
    public class WeaponLance : Weapon
    {
        public const int ATTACKCOOLDOWN = 1000;
        public const int ATTACKWINDUP = 500;
        private const float ENDURANCECOST = 40;

        public WeaponLance(IModel model, IArmedEntity owner)
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
                new SwingAttack(this, 1f, 0.25f, 0.5f, 300, Color4.Red),
            };
        }
    }
}
