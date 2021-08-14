namespace Poggers.Textures.JSOn_parser
{
    using System.Collections.Generic;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Model class")]
    public class TilemapModel
    {
        public int tileheight { get; set; }

        public int tilewidth { get; set; }

        public int height { get; set; }

        public int width { get; set; }

        public IList<TilemapLayerModel> layers { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Model class")]
    public class TilemapLayerModel
    {
        public uint[] data { get; set; }

        public int height { get; set; }

        public int width { get; set; }

        public int id { get; set; }

        public float opacity { get; set; }

        public string name { get; set; }

        public TilemapLayerPropertiesModel[] properties { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Model class")]
    public class TilemapLayerPropertiesModel
    {
        public string name { get; set; }

        public string type { get; set; }

        public object value { get; set; }
    }
}