using System;
using System.IO;
using System.Reflection;
using OpenTK.Mathematics;
using Poggers.GameObjects.Entities;
using Poggers.GameObjects.Entities.Enemies;
using Poggers.GameObjects.Items;
using Poggers.GameObjects.Weapons;
using Poggers.Interfaces;
using Poggers.Pathfinding;
using Poggers.Textures.JSOn_parser;

namespace Poggers
{
    public class World
    {
        public const float CELLWIDTH = GRIDSIZE * POV;
        private const float GRIDSIZE = 0.5f;
        private const float POV = 0.25f;
        private IModel model;
        private TilemapModel level;
        private TilesetProps tileset;
        private string tileSetstr = "Poggers.Textures.parser.set.json";
        private string[] levels =
        {
            "Poggers.Textures.parser.neutotrial.json",
            "Poggers.Textures.parser.neulevel1.json",
            "Poggers.Textures.parser.boss.json",
        };

        public World(IModel model, int level)
        {
            if (level > this.levels.Length)
            {
                Console.WriteLine("Error on loading level");
                return;
            }

            this.model = model;

            Stream[] stream = new Stream[this.levels.Length];
            Stream tileSetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(this.tileSetstr);

            for (int s = 0; s < this.levels.Length; s++)
            {
                stream[s] = Assembly.GetExecutingAssembly().GetManifestResourceStream(this.levels[s]);
            }

            this.level = TilemapParser.ParseTilemap(stream[level]);
            this.tileset = TilemapParser.ParseTileset(tileSetStream);

            this.CreateGrid(CELLWIDTH);  // box size
        }

        private void CreateGrid(float width)
        {
            Vector2 cent = (0, 0);
            int b = 0;
            bool[,] collisionGrid = new bool[this.level.width, this.level.height];
            bool background;
            bool end = false;

            for (int z = 0; z < this.level.layers.Count; z++)
            {
                for (int y = 0; y < this.level.height; y++)
                {
                    for (int x = 0; x < this.level.width; x++)
                    {
                        background = true;
                        end = false;
                        foreach (Tiles tiles in this.tileset.tiles)
                        {
                            if (tiles.id == (this.level.layers[z].data[b] - 1))
                            {
                                collisionGrid[x, y] = tiles.properties[0].name.Contains("wall");
                                if (collisionGrid[x, y])
                                {
                                    background = false;
                                    Wall bottom = new Wall(cent, width, width, false, this.level.layers[z].data[b]);
                                    this.model.GameObjects.Add(bottom);
                                }

                                if (tiles.properties[0].name.Contains("player"))
                                {
                                    this.model.Player = new Player(cent, this.model);
                                    WeaponBroadsword testSword = new WeaponBroadsword(this.model, this.model.Player);
                                    this.model.Player.Weapon = testSword;
                                    this.model.Player.Items = new IItem[] { null, new HealthPotion((0, 0), 0), null };
                                }

                                if (tiles.properties[0].name.Contains("enemy2"))
                                {
                                    // The width of the enemy has to be less or equal, than the CELLWIDTH
                                    MushroomEnemy enemy = new MushroomEnemy(cent, this.model);
                                    enemy.Weapon = new WeaponLance(this.model, enemy);
                                    this.model.GameObjects.Insert(0, enemy);
                                }

                                if (tiles.properties[0].name.Contains("enemy1"))
                                {
                                    // The width of the enemy has to be less or equal, than the CELLWIDTH
                                    FlowerEnemy enemy = new FlowerEnemy(cent, this.model);
                                    this.model.GameObjects.Insert(0, enemy);
                                }

                                if (tiles.properties[0].name.Contains("boss1"))
                                {
                                    // The width of the enemy has to be less or equal, than the CELLWIDTH
                                    this.model.Boss = new BossCeolmaer(cent, this.model);
                                    this.model.GameObjects.Insert(0, this.model.Boss);
                                }

                                if (tiles.properties[0].name.Contains("end"))
                                {
                                    end = true;
                                }
                            }
                        }

                        if (background)
                        {
                            GridBox bg = new GridBox(cent, width, width, this.level.layers[z].data[b]);
                            bg.SetEnd(end);
                            this.model.GameObjects.Add(bg);
                        }

                        cent.X += width;
                        b++;
                    }

                    cent.X = 0;
                    cent.Y -= width;
                }
            }

            this.model.GameObjects.Reverse();
            this.model.GameObjects.Add(this.model.Player);
            Pathprovider.Instance = new Pathprovider(collisionGrid, CELLWIDTH);
        }
    }
}
