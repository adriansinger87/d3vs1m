using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.System.Constants
{
    /// <summary>
    /// The partial class contains constant fields and methods that should not be applied as extension methods or
    /// within outer layers inside the simulation models.
    /// The other partial classes have a separate scope in line with the simulation models
    /// </summary>
    public partial class Const
    {
        /// <summary>
        /// Calculates the Degree from a given float the represents an angle as Radian
        /// </summary>
        /// <param name="rad">the input angle as Radian value</param>
        /// <returns>The Degree value</returns>
        public static float ToDegree(float rad)
        {
            return ((rad * 180) / (float)Math.PI);
        }

        /// <summary>
        /// Calculates the Radian from a given float the represents an angle as Degree
        /// </summary>
        /// <param name="deg">the input angle as Degree value</param>
        /// <returns>The Radian value</returns>
        public static float ToRadian(float deg)
        {
            return ((deg * (float)Math.PI) / 180);
        }
    }
}
