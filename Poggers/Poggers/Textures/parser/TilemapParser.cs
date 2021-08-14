using System;
using System.IO;
using Newtonsoft.Json;

namespace Poggers.Textures.JSOn_parser
{
    public class TilemapParser
    {
        /// <summary>
        /// Parses Tilemap json files into a Tilemap model.
        /// </summary>
        /// <param name="resource">The Tilemap json stream.</param>
        /// <returns>The Tilemap model.</returns>
        public static TilemapModel ParseTilemap(Stream resource)
        {
            if (resource == null)
            {
                throw new ArgumentException("resource cannot be null.");
            }

            StreamReader reader = new StreamReader(resource);
            string json = reader.ReadToEnd();
            TilemapModel model = JsonConvert.DeserializeObject<TilemapModel>(json);
            return model;
        }

        public static TilesetProps ParseTileset(Stream resource)
        {
            if (resource == null)
            {
                throw new ArgumentException("resource cannot be null.");
            }

            StreamReader reader = new StreamReader(resource);
            string json = reader.ReadToEnd();
            TilesetProps model = JsonConvert.DeserializeObject<TilesetProps>(json);
            return model;
        }
    }
}