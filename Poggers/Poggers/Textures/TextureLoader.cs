using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Poggers.Textures
{
    public class TextureLoader
    {
        private const int DIRECTIONS = 8;
        private const int AMTFRAMES = 5;

        private static readonly string[] Pictures =
        {
            "Textures/MenuButtons/startGameNeu.png", // 3 1
            "Textures/MenuButtons/startGameNeu.png", // 4 2

            "Textures/lightSprite.png", // 22 3

            "Textures/HUD/heartgrey.png", // 23 4
            "Textures/HUD/heartred.png", // 24 5
            "Textures/HUD/heartshield.png", // 25 6
            "Textures/HUD/hudbase.png", // 26 7
            "Textures/HUD/icon.png", // 27 8
            "Textures/HUD/itembar.png", // 28 9
            "Textures/HUD/endgrey.png", // 30 10
            "Textures/HUD/endgreen.png", // 31 11

            "Textures/Items/healthpot.png", // 33 12
            "Textures/Items/endurancepot.png", // 34 13
            "Textures/Items/speedpot.png", // 35 14
            "Textures/Items/smolsword.png", // 43 15

            "Textures/Screens/youwon4k32.png", // 36 16
            "Textures/Screens/youdied4k32.png",  // 45 17

            "Textures/Boss/ceolmaerbody.png", // 37 18
            "Textures/Boss/ceolmaerstaff.png", // 38 19

            "Textures/HUD/ceolmaernamewhite.png", // 49 20

            "Textures/Boss/bol.png", // 39 21
            "Textures/Boss/tentacles1.png", // 40 22
            "Textures/Boss/tentacles2.png", // 41 23
            "Textures/Boss/tentacles3.png", // 42 24

            "Textures/Weapon/Attack/frontslash1.png", // 46 25
            "Textures/Weapon/Attack/frontslash2.png", // 47 26
            "Textures/Weapon/Attack/frontslash3.png", // 48 27
        };

        private static readonly string[,] PlayerWalk =
        {
            {
            "Textures/PlayerSprites/walk/frontstand.png",
            "Textures/PlayerSprites/walk/frontwalk1.png",
            "Textures/PlayerSprites/walk/frontwalk2.png",
            "Textures/PlayerSprites/walk/frontwalk3.png",
            "Textures/PlayerSprites/walk/frontwalk4.png",
            },
            {
            "Textures/PlayerSprites/walk/backstand.png",
            "Textures/PlayerSprites/walk/backwalk1.png",
            "Textures/PlayerSprites/walk/backwalk2.png",
            "Textures/PlayerSprites/walk/backwalk3.png",
            "Textures/PlayerSprites/walk/backwalk4.png",
            },
            {
            "Textures/PlayerSprites/walk/leftstand.png",
            "Textures/PlayerSprites/walk/leftwalk1.png",
            "Textures/PlayerSprites/walk/leftwalk2.png",
            "Textures/PlayerSprites/walk/leftwalk3.png",
            "Textures/PlayerSprites/walk/leftwalk4.png",
            },
            {
            "Textures/PlayerSprites/walk/siderightstand.png",
            "Textures/PlayerSprites/walk/siderightwalk1.png",
            "Textures/PlayerSprites/walk/siderightwalk2.png",
            "Textures/PlayerSprites/walk/siderightwalk3.png",
            "Textures/PlayerSprites/walk/siderightwalk4.png",
            },
            {
            "Textures/PlayerSprites/walk/34backrightstand.png",
            "Textures/PlayerSprites/walk/34backrightwalk1.png",
            "Textures/PlayerSprites/walk/34backrightwalk2.png",
            "Textures/PlayerSprites/walk/34backrightwalk3.png",
            "Textures/PlayerSprites/walk/34backrightwalk4.png",
            },
            {
            "Textures/PlayerSprites/walk/34backleftstand.png",
            "Textures/PlayerSprites/walk/34backleftwalk1.png",
            "Textures/PlayerSprites/walk/34backleftwalk2.png",
            "Textures/PlayerSprites/walk/34backleftwalk3.png",
            "Textures/PlayerSprites/walk/34backleftwalk4.png",
            },
            {
            "Textures/PlayerSprites/walk/34frontrightstand.png",
            "Textures/PlayerSprites/walk/34frontrightwalk1.png",
            "Textures/PlayerSprites/walk/34frontrightwalk2.png",
            "Textures/PlayerSprites/walk/34frontrightwalk3.png",
            "Textures/PlayerSprites/walk/34frontrightwalk4.png",
            },
            {
            "Textures/PlayerSprites/walk/34frontleftstand.png",
            "Textures/PlayerSprites/walk/34frontleftwalk1.png",
            "Textures/PlayerSprites/walk/34frontleftwalk2.png",
            "Textures/PlayerSprites/walk/34frontleftwalk3.png",
            "Textures/PlayerSprites/walk/34frontleftwalk4.png",
            },
        };

        private static readonly string[] PlayerDodge =
        {
            "Textures/PlayerSprites/dodge/dodgefront1.png",
            "Textures/PlayerSprites/dodge/dodgefront2.png",
            "Textures/PlayerSprites/dodge/dodgefront1.png",
        };

        private static readonly string[] Enemy2 =
        {
            "Textures/Enemy/Enemy2/flowerstand.png",
            "Textures/Enemy/Enemy2/flowerwalk2.png",
            "Textures/Enemy/Enemy2/flowerwalk3.png",
        };

        private static readonly string[,] Enemy1 =
        {
            {
            "Textures/Enemy/Enemy1/frontstand.png",
            "Textures/Enemy/Enemy1/frontwalk1.png",
            "Textures/Enemy/Enemy1/frontwalk2.png",
            "Textures/Enemy/Enemy1/frontwalk3.png",
            "Textures/Enemy/Enemy1/frontwalk4.png",
            },
            {
            "Textures/Enemy/Enemy1/backstand.png",
            "Textures/Enemy/Enemy1/backwalk1.png",
            "Textures/Enemy/Enemy1/backwalk2.png",
            "Textures/Enemy/Enemy1/backwalk3.png",
            "Textures/Enemy/Enemy1/backwalk4.png",
            },
            {
            "Textures/Enemy/Enemy1/leftstand.png",
            "Textures/Enemy/Enemy1/leftwalk1.png",
            "Textures/Enemy/Enemy1/leftwalk2.png",
            "Textures/Enemy/Enemy1/leftwalk3.png",
            "Textures/Enemy/Enemy1/leftwalk4.png",
            },
            {
            "Textures/Enemy/Enemy1/rightstand.png",
            "Textures/Enemy/Enemy1/rightwalk1.png",
            "Textures/Enemy/Enemy1/rightwalk2.png",
            "Textures/Enemy/Enemy1/rightwalk3.png",
            "Textures/Enemy/Enemy1/rightwalk4.png",
            },
        };

        private static readonly string[] Menu =
        {
            "Textures/MenuButtons/startGameNeu.png",

            // 1
            "Textures/MenuButtons/levelone.png",
            "Textures/MenuButtons/levelonehi.png",
            "Textures/MenuButtons/levelonegold.png",
            "Textures/MenuButtons/levelonegoldhi.png",

            // 5
            "Textures/MenuButtons/leveltwo.png",
            "Textures/MenuButtons/leveltwohi.png",
            "Textures/MenuButtons/leveltwogold.png",
            "Textures/MenuButtons/leveltwogoldhi.png",

            // 9
            "Textures/MenuButtons/levelthree.png",
            "Textures/MenuButtons/levelthreehi.png",
            "Textures/MenuButtons/levelthreegold.png",
            "Textures/MenuButtons/levelthreegoldhi.png",

            // 13
            "Textures/MenuButtons/continue.png",
            "Textures/MenuButtons/continuehi.png",
            "Textures/MenuButtons/backtomenu.png",
            "Textures/MenuButtons/backtomenuhi.png",

            "Textures/Screens/youwon4k32.png", // 36 16
            "Textures/Screens/youdied4k32.png",
        };

        private static int[] enemy2 = new int[3];
        private static BitmapData[] enemy2bmp = new BitmapData[3];

        private static int[,] enemy1Walk = new int[4, 5];
        private static BitmapData[,] enemy1Walkbmp = new BitmapData[4, 5];

        private static int[] menus = new int[Menu.Length];
        private static BitmapData[] menusbm = new BitmapData[Menu.Length];

        private static int[] texture = new int[50];
        private static BitmapData[] bm = new BitmapData[50];

        private static int[,] playerwalktex = new int[DIRECTIONS, AMTFRAMES];
        private static BitmapData[,] bmp = new BitmapData[DIRECTIONS, AMTFRAMES];

        private static int[] playerdodgetex = new int[3];
        private static BitmapData[] bmpd = new BitmapData[3];

        public static int GetTexture(int id) => texture[id];

        public static int GetMenuTexture(int id) => menus[id];

        public static void LoadTex()
        {
            Animator.Init();

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc((BlendingFactor)BlendingFactorSrc.SrcAlpha, (BlendingFactor)BlendingFactorDest.OneMinusSrcAlpha);

            GL.GenTextures(50, out texture[0]);
            GL.BindTexture(TextureTarget.Texture2D, texture[0]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            BitmapData texData = LoadImage("Textures/sprite.png");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, texData.Width, texData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, texData.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            for (int i = 0; i < Pictures.Length; i++)
            {
                GL.BindTexture(TextureTarget.Texture2D, texture[i + 1]);
                bm[i] = LoadImageB(Pictures[i]);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bm[i].Width, bm[i].Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bm[i].Scan0);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }

            GL.GenTextures(Menu.Length, out menus[0]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            for (int i = 0; i < Menu.Length; i++)
            {
                GL.BindTexture(TextureTarget.Texture2D, menus[i]);
                menusbm[i] = LoadImageB(Menu[i]);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, menusbm[i].Width, menusbm[i].Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, menusbm[i].Scan0);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }

            GL.GenTextures(DIRECTIONS * AMTFRAMES, out playerwalktex[0, 0]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            for (int i = 0; i < DIRECTIONS; i++)
            {
                for (int j = 0; j < AMTFRAMES; j++)
                {
                    GL.BindTexture(TextureTarget.Texture2D, playerwalktex[i, j]);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                    bmp[i, j] = LoadImageB(PlayerWalk[i, j]);
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp[i, j].Width, bmp[i, j].Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp[i, j].Scan0);
                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                    // down up left right backrigh backleft frontright frontleft
                    Animator.PlayerAdd(playerwalktex[i, j], i);
                }
            }

            GL.GenTextures(3, out playerdodgetex[0]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            for (int i = 0; i < 3; i++)
            {
                GL.BindTexture(TextureTarget.Texture2D, playerdodgetex[i]);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                bmpd[i] = LoadImageB(PlayerDodge[i]);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpd[i].Width, bmpd[i].Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpd[i].Scan0);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                // down up left right backrigh backleft frontright frontleft
                Animator.PlayerDodgeAdd(playerdodgetex[i]);
            }

            GL.GenTextures(4 * 5, out enemy1Walk[0, 0]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    GL.BindTexture(TextureTarget.Texture2D, enemy1Walk[i, j]);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                    enemy1Walkbmp[i, j] = LoadImageB(Enemy1[i, j]);
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, enemy1Walkbmp[i, j].Width, enemy1Walkbmp[i, j].Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, enemy1Walkbmp[i, j].Scan0);
                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                    Animator.Enemy1Add(enemy1Walk[i, j], i);
                }
            }

            GL.GenTextures(3, out enemy2[0]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            for (int i = 0; i < 3; i++)
            {
                GL.BindTexture(TextureTarget.Texture2D, enemy2[i]);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                enemy2bmp[i] = LoadImageB(Enemy2[i]);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, enemy2bmp[i].Width, enemy2bmp[i].Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, enemy2bmp[i].Scan0);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                Animator.Enemy1Add(enemy2[i]);
            }
        }

        private static BitmapData LoadImage(string t)
        {
            Bitmap bmp = new Bitmap(t);
            Rectangle re = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpdata = bmp.LockBits(re, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmp.UnlockBits(bmpdata);
            return bmpdata;
        }

        private static BitmapData LoadImageB(string t)
        {
            Bitmap bmp = new Bitmap(t);
            Rectangle re = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpdata = bmp.LockBits(re, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            bmp.UnlockBits(bmpdata);
            return bmpdata;
        }
    }
}
