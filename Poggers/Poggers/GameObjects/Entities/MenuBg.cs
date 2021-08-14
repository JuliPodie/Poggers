using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Textures;

namespace Poggers.GameObjects.Entities
{
    public class MenuBg : GameRectangle
    {
        public MenuBg(Vector2 center, float width, float height)
          : base(center, width, height)
        {
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(11));
            GL.Color4(Color4.White);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(-1, -1); // draw first quad corner
            GL.TexCoord2(1, 1);
            GL.Vertex2(1, -1);
            GL.TexCoord2(1, 0);
            GL.Vertex2(1, 1);
            GL.TexCoord2(0, 0);
            GL.Vertex2(-1, 1);
            GL.End();
        }
    }
}