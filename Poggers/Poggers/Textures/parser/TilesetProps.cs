using System.Collections.Generic;
using System.IO;

namespace Poggers.Textures.JSOn_parser
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<Ausstehend>")]
    public class TilesetProps
    {
        public int columns { get; set; }

        public string image { get; set; }

        public int imageheight { get; set; }

        public int imagewidth { get; set; }

        public int margin { get; set; }

        public string name { get; set; }

        public int spacing { get; set; }

        public int tilecount { get; set; }

        public string tiledversion { get; set; }

        public int tileheight { get; set; }

        public Tiles[] tiles { get; set; }

        public int tilewidth { get; set; }

        public string type { get; set; }

        public double version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<Ausstehend>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<Ausstehend>")]
    public class Tiles
    {
        public int id { get; set; }

        public Properties[] properties { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<Ausstehend>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<Ausstehend>")]

    public class Properties
    {
        public string name { get; set; }

        public string type { get; set; }

        public bool value { get; set; }
    }
}