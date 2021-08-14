using System.Collections.Generic;
using System.Timers;
using Poggers.Directions;
using Poggers.EntityStates;

namespace Poggers.Textures
{
    public class Animator
    {
        private const double SpriteIntervalMs = 200;

        private static List<int>[] player = new List<int>[8];
        private static List<int> playerD = new List<int>();
        private static List<int>[] enemy1 = new List<int>[4]; // 0front 1back 2left 3right
        private static List<int> enemy2 = new List<int>();

        private Timer timer;
        private int sprite = 0;

        public Animator()
        {
            this.timer = new Timer();
            this.timer.Elapsed += this.SwitchSprites;
            this.timer.AutoReset = true;
            this.timer.Interval = SpriteIntervalMs;
            this.timer.Start();
        }

        public static void Init()
        {
            for (int i = 0; i < 4; i++)
            {
                enemy1[i] = new List<int>();
            }

            for (int i = 0; i < 8; i++)
            {
                player[i] = new List<int>();
            }
        }

        public static void PlayerAdd(int tex, int i)
        {
            player[i].Add(tex);
        }

        public static void PlayerDodgeAdd(int i)
        {
            playerD.Add(i);
        }

        public static void Enemy1Add(int i, int i1)
        {
            enemy1[i1].Add(i);
        }

        public static void Enemy1Add(int v)
        {
            enemy2.Add(v);
        }

        public int GetEnemy2Frame(EntityState state)
        {
            switch (state)
            {
                case EntityState.Moving:
                    if (this.sprite == 0 || this.sprite == 3 || this.sprite == 4)
                    {
                        this.sprite = 1;
                    }

                    return enemy2[this.sprite];
                default:
                    return enemy2[0];
            }
        }

        public int GetEnemy1Frame(EntityState state, Direction dir)
        {
            switch (state)
            {
                case EntityState.Moving:
                    if (this.sprite == 0)
                    {
                        this.sprite = 1;
                    }

                    switch (dir)
                    {
                        case Direction.S:
                            return enemy1[0][this.sprite];

                        case Direction.W:
                            return enemy1[1][this.sprite];

                        case Direction.A:
                            return enemy1[2][this.sprite];

                        case Direction.D:
                            return enemy1[3][this.sprite];

                        case Direction.WA:
                            return enemy1[2][this.sprite];

                        case Direction.WD:
                            return enemy1[3][this.sprite];

                        case Direction.SD:
                            return enemy1[3][this.sprite];

                        case Direction.SA:
                            return enemy1[2][this.sprite];
                    }

                    break;

                default:
                    switch (dir)
                    {
                        case Direction.S:
                            return enemy1[0][0];

                        case Direction.W:
                            return enemy1[1][0];

                        case Direction.A:
                            return enemy1[2][0];

                        case Direction.D:
                            return enemy1[3][0];

                        case Direction.WA:
                            return enemy1[2][0];

                        case Direction.WD:
                            return enemy1[3][0];

                        case Direction.SD:
                            return enemy1[3][0];

                        case Direction.SA:
                            return enemy1[2][0];
                    }

                    break;
            }

            return -1;
        }

        // down up left right backrigh backleft frontright frontleft
        public int GetPlayerFrame(EntityState state, Direction dir)
        {
            switch (state)
            {
                case EntityState.Moving:
                    if (this.sprite == 0)
                    {
                        this.sprite = 1;
                    }

                    switch (dir)
                    {
                        case Direction.S:
                            return player[0][this.sprite];

                        case Direction.W:
                            return player[1][this.sprite];

                        case Direction.A:
                            return player[2][this.sprite];

                        case Direction.D:
                            return player[3][this.sprite];

                        case Direction.WD:
                            return player[4][this.sprite];

                        case Direction.WA:
                            return player[5][this.sprite];

                        case Direction.SD:
                            return player[6][this.sprite];

                        case Direction.SA:
                            return player[7][this.sprite];
                    }

                    break;

                case EntityState.Dodging:
                    if (this.sprite == 0 || this.sprite == 3 || this.sprite == 4)
                    {
                        this.sprite = 1;
                    }

                    return playerD[this.sprite];

                default:
                    switch (dir)
                    {
                        case Direction.S:
                            return player[0][0];

                        case Direction.W:
                            return player[1][0];

                        case Direction.A:
                            return player[2][0];

                        case Direction.D:
                            return player[3][0];

                        case Direction.WD:
                            return player[4][0];

                        case Direction.WA:
                            return player[5][0];

                        case Direction.SD:
                            return player[6][0];

                        case Direction.SA:
                            return player[7][0];
                    }

                    break;
            }

            return -1;
        }

        private void SwitchSprites(object source, ElapsedEventArgs e)
        {
            // PlayerWalkDownAni.Count)
            if (this.sprite == 4)
            {
                this.sprite = 0;
            }
            else
            {
                this.sprite++;
            }
        }
    }
}