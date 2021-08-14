using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Interfaces;
using Poggers.Textures;

namespace Poggers.Overlays
{
    public class Hud : IOverlay
    {
        private IModel model;

        // BOT LEFT
        private Vector2 infoPos = new Vector2(-0.97f, -0.97f);
        private float infoHeight = 0.5f;
        private float infoWidth = 0.959f;

        private Vector2 infoPicPos = new Vector2(0.09f, 0.11f);
        private float infoPicSize = 0.27f;

        private Vector2 infoHealthPos = new Vector2(0.4f, 0.29f);
        private float infoHealthOff = 0.008f;
        private float infoHealthSize = 0.07f;

        private Vector2 infoEndurancePos = new Vector2(0.37f, 0.145f);
        private float infoEnduranceHeight = 0.04f;
        private float infoEnduranceWidth = 0.35f;

        // BOT RIGHT
        private Vector2 itembarPos = new Vector2(0.97f, -0.97f);
        private float itembarHeight = 0.45f;
        private float itembarWidth = 0.45f;

        private Vector2 itemPos = new Vector2(0.17f, 0.085f);
        private float itemHeight = 0.1f;
        private float itemWidth = 0.1f;
        private float itemOffX = 0.085f;
        private float itemOffY = 0.19f;

        // BOT MID
        private Vector2 bossHpPos = new Vector2(-0.4f, -0.95f);
        private float bossHPH = 0.07f;
        private float bossHPW = 0.8f;
        private bool boss;

        private Vector2 bossNamePos = new Vector2(-0.2f, -0.87f);
        private float bossNameH = 0.07f;
        private float bossNameW = 0.4f;

        public Hud(IModel model)
        {
            this.model = model;
        }

        public void Draw(float windowRatio)
        {
            float temp;
            float tempHP;
            float infoPosX = -1 + ((1 + this.infoPos.X) * windowRatio);
            float itembarPosX = 1 - (((1 - this.itembarPos.X) + this.itembarWidth) * windowRatio);

            if (this.model.Boss != null && !this.boss && this.model.Boss.IsAttacking)
            {
                this.boss = true;
            }

            GL.Color4(Color4.White);

            // BOT LEFT

            // PlayerPic
            temp = infoPosX + (this.infoPicPos.X * windowRatio);
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(8));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(temp, this.infoPos.Y + this.infoPicPos.Y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(temp + (this.infoPicSize * windowRatio), this.infoPos.Y + this.infoPicPos.Y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(temp + (this.infoPicSize * windowRatio), this.infoPos.Y + this.infoPicPos.Y + this.infoPicSize);
            GL.TexCoord2(0, 0);
            GL.Vertex2(temp, this.infoPos.Y + this.infoPicPos.Y + this.infoPicSize);
            GL.End();

            // Backgr Info
            temp = -1 + ((1 + this.infoPos.X) * windowRatio);
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(7));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(temp, this.infoPos.Y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(temp + (this.infoWidth * windowRatio), this.infoPos.Y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(temp + (this.infoWidth * windowRatio), this.infoPos.Y + this.infoHeight);
            GL.TexCoord2(0, 0);
            GL.Vertex2(temp, this.infoPos.Y + this.infoHeight);
            GL.End();

            // Health
            tempHP = this.model.Player.Health.Value;

            for (float i = 0; i < 3; i++)
            {
                switch (tempHP)
                {
                    case 1:
                    case 2:
                    case 3:
                        GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(5));
                        break;
                    case 4:
                    case 5:
                    case 6:
                        GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(6));
                        break;
                    default:
                        GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(4));
                        break;
                }

                tempHP--;

                temp = infoPosX + ((this.infoHealthPos.X + ((this.infoHealthSize + this.infoHealthOff) * i)) * windowRatio);
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 1);
                GL.Vertex2(temp, this.infoPos.Y + this.infoHealthPos.Y);
                GL.TexCoord2(1, 1);
                GL.Vertex2(temp + (this.infoHealthSize * windowRatio), this.infoPos.Y + this.infoHealthPos.Y);
                GL.TexCoord2(1, 0);
                GL.Vertex2(temp + (this.infoHealthSize * windowRatio), this.infoPos.Y + this.infoHealthPos.Y + this.infoHealthSize);
                GL.TexCoord2(0, 0);
                GL.Vertex2(temp, this.infoPos.Y + this.infoHealthPos.Y + this.infoHealthSize);
                GL.End();
            }

            // Endurance
            temp = infoPosX + (this.infoEndurancePos.X * windowRatio);

            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(10));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(temp, this.infoPos.Y + this.infoEndurancePos.Y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(temp + (this.infoEnduranceWidth * windowRatio), this.infoPos.Y + this.infoEndurancePos.Y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(temp + (this.infoEnduranceWidth * windowRatio), this.infoPos.Y + this.infoEndurancePos.Y + this.infoEnduranceHeight);
            GL.TexCoord2(0, 0);
            GL.Vertex2(temp, this.infoPos.Y + this.infoEndurancePos.Y + this.infoEnduranceHeight);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(11));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(temp, this.infoPos.Y + this.infoEndurancePos.Y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(temp + ((this.infoEnduranceWidth * this.model.Player.Endurance.Value / 100) * windowRatio), this.infoPos.Y + this.infoEndurancePos.Y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(temp + ((this.infoEnduranceWidth * this.model.Player.Endurance.Value / 100) * windowRatio), this.infoPos.Y + this.infoEndurancePos.Y + this.infoEnduranceHeight);
            GL.TexCoord2(0, 0);
            GL.Vertex2(temp, this.infoPos.Y + this.infoEndurancePos.Y + this.infoEnduranceHeight);
            GL.End();

            // BOT RIGHT

            // Itembar
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(9));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(itembarPosX, this.itembarPos.Y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(itembarPosX + (this.itembarWidth * windowRatio), this.itembarPos.Y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(itembarPosX + (this.itembarWidth * windowRatio), this.itembarPos.Y + this.itembarHeight);
            GL.TexCoord2(0, 0);
            GL.Vertex2(itembarPosX, this.itembarPos.Y + this.itembarHeight);
            GL.End();

            // Items
            temp = itembarPosX + (this.itemPos.X * windowRatio);

            // Weapon
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(15)); // maybe just hardcode it?
            if (this.model.Player.Weapon.IsUsable)
            {
                GL.Color4(Color4.White);
            }
            else
            {
                GL.Color4(255, 255, 255, 0.5f);
            }

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(temp, this.itembarPos.Y + this.itemPos.Y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(temp + (this.itemWidth * windowRatio), this.itembarPos.Y + this.itemPos.Y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(temp + (this.itemWidth * windowRatio), this.itembarPos.Y + this.itemPos.Y + this.itemHeight);
            GL.TexCoord2(0, 0);
            GL.Vertex2(temp, this.itembarPos.Y + this.itemPos.Y + this.itemHeight);
            GL.End();

            GL.Color4(Color4.White);

            // Item1
            for (int i = 0; i < 3; i++)
            {
                float tempX = 0f;
                float tempY = 0f;
                switch (i)
                {
                    case 0:
                        tempX = -this.itemOffX;
                        tempY = this.itemOffY / 2;
                        break;
                    case 1:
                        tempY = this.itemOffY;
                        break;
                    case 2:
                        tempX = this.itemOffX;
                        tempY = this.itemOffY / 2;
                        break;
                }

                if (this.model.Player.Items[i] is null)
                {
                    continue;
                }

                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(this.model.Player.Items[i].Texture));
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 1);
                GL.Vertex2(temp + (tempX * windowRatio), this.itembarPos.Y + this.itemPos.Y + tempY);
                GL.TexCoord2(1, 1);
                GL.Vertex2(temp + ((this.itemWidth + tempX) * windowRatio), this.itembarPos.Y + this.itemPos.Y + tempY);
                GL.TexCoord2(1, 0);
                GL.Vertex2(temp + ((this.itemWidth + tempX) * windowRatio), this.itembarPos.Y + this.itemPos.Y + this.itemHeight + tempY);
                GL.TexCoord2(0, 0);
                GL.Vertex2(temp + (tempX * windowRatio), this.itembarPos.Y + this.itemPos.Y + this.itemHeight + tempY);
                GL.End();
            }

            if (this.boss)
            {
                // BOT MID
                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(10));
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 1);
                GL.Vertex2(this.bossHpPos.X, this.bossHpPos.Y);
                GL.TexCoord2(1, 1);
                GL.Vertex2(this.bossHpPos.X + this.bossHPW, this.bossHpPos.Y);
                GL.TexCoord2(1, 0);
                GL.Vertex2(this.bossHpPos.X + this.bossHPW, this.bossHpPos.Y + this.bossHPH);
                GL.TexCoord2(0, 0);
                GL.Vertex2(this.bossHpPos.X, this.bossHpPos.Y + this.bossHPH);
                GL.End();

                GL.Color4(Color4.Red);
                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(11));
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 1);
                GL.Vertex2(this.bossHpPos.X, this.bossHpPos.Y);
                GL.TexCoord2(1, 1);
                GL.Vertex2(this.bossHpPos.X + (this.bossHPW * this.model.Boss.Health.Value / 10), this.bossHpPos.Y);
                GL.TexCoord2(1, 0);
                GL.Vertex2(this.bossHpPos.X + (this.bossHPW * this.model.Boss.Health.Value / 10), this.bossHpPos.Y + this.bossHPH);
                GL.TexCoord2(0, 0);
                GL.Vertex2(this.bossHpPos.X, this.bossHpPos.Y + this.bossHPH);
                GL.End();
                GL.Color4(Color4.White);

                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(20));
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 1);
                GL.Vertex2(this.bossNamePos.X, this.bossNamePos.Y);
                GL.TexCoord2(1, 1);
                GL.Vertex2(this.bossNamePos.X + this.bossNameW, this.bossNamePos.Y);
                GL.TexCoord2(1, 0);
                GL.Vertex2(this.bossNamePos.X + this.bossNameW, this.bossNamePos.Y + this.bossNameH);
                GL.TexCoord2(0, 0);
                GL.Vertex2(this.bossNamePos.X, this.bossNamePos.Y + this.bossNameH);
                GL.End();
            }
        }
    }
}
