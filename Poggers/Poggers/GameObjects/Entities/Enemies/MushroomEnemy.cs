using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.EntityStates;
using Poggers.Interfaces;
using Poggers.Textures;

namespace Poggers.GameObjects.Entities.Enemies
{
    public class MushroomEnemy : BasicEnemy, IArmedEntity
    {
        private const float WIDTH = 0.2f;
        private const float HEIGHT = 0.2f;
        private Animator ani;

        private IWeapon weapon;

        public MushroomEnemy(Vector2 center, IModel model)
            : base(center, WIDTH, HEIGHT, model)
        {
            this.Health = new ValueWithBounds(2, 1);
            this.Endurance = new ValueWithBoundsAndTimer(100);
            this.ani = new Animator();
        }

        public IWeapon Weapon { get => this.weapon; set => this.weapon = value; }

        public override void Die()
        {
            base.Die();
            this.weapon.StopAttack();
        }

        public void Attack()
        {
            float distance = Vector2.Subtract(this.Center, this.Model.Player?.Center ?? (0, 0)).Length;
            if (!this.State.IsAttacking() && this.Weapon.IsUsable && distance < (this.Weapon?.Range ?? 0) && this.Endurance.Subtract(this.Weapon.EnduranceCost))
            {
                this.Weapon.StartAttack(0);
            }
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            GL.Color4(this.SpriteColor);
            GL.BindTexture(TextureTarget.Texture2D, this.ani.GetEnemy1Frame(this.State, this.Direction));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2((offset.X - (this.Width / 2)) * windowRatio, offset.Y - (this.Height / 2)); // draw first quad corner
            GL.TexCoord2(1, 1);
            GL.Vertex2((offset.X + (this.Width / 2)) * windowRatio, offset.Y - (this.Height / 2));
            GL.TexCoord2(1, 0);
            GL.Vertex2((offset.X + (this.Width / 2)) * windowRatio, offset.Y + this.Height);
            GL.TexCoord2(0, 0);
            GL.Vertex2((offset.X - (this.Width / 2)) * windowRatio, offset.Y + this.Height);
            GL.End();
        }
    }
}
