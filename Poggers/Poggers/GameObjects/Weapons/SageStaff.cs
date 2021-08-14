using System.Collections.Generic;
using OpenTK.Mathematics;
using Poggers.GameObjects.Weapons.Attacks;
using Poggers.Interfaces;

namespace Poggers.GameObjects.Weapons
{
    public class SageStaff : Weapon
    {
        public const int ATTACKCOOLDOWN = 100;
        public const int ATTACKWINDUP = 800;

        public SageStaff(IModel model, IArmedEntity owner)
            : base(model, owner)
        {
            this.Cooldowntime = ATTACKCOOLDOWN;
            this.Winduptime = ATTACKWINDUP;
        }

        public override void InitializeAttacks()
        {
            this.Attacks = new List<Attack>
            {
                new BullethellAttack(this, 1f, 2000, 12, 0.1f, 2f),
                new FireAttack(this, 1, 3000, 0.2f, 1.5f),
                new SwingAttack(this, 1, 1f, 1.2f, 700, Color4.Black),
            };
        }
    }
}
