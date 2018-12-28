using D3vS1m.Domain.Data.Scene;

namespace D3vS1m.Application.Channel
{
    /// <summary>
    /// Stores the data of the cuboid that is used as input for the wireless radio channel simulation
    /// </summary>
    public class RadioCuboid
    {
        public RadioCuboid()
        {
            this.MinCorner = new Vector();
            this.MaxCorner = new Vector();
            this.Resolution = 1;
        }

        // --- public methoden

        public override string ToString()
        {
            return $"Min: {MinCorner} Max: {MaxCorner}";
        }

        // --- properties

        public Vector MinCorner { get; set; }
        public Vector MaxCorner { get; set; }
        public float Resolution { get; set; }
        public int[] DataDimension { get; set; }
        public int TotalData { get; set; }
    }
}
