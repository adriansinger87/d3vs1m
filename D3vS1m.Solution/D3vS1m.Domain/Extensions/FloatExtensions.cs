using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Extensions
{
    public static class FloatExtensions
    {
        /// <summary>
        /// Calculates the Degree from a given float the represents an angle as Radian
        /// </summary>
        /// <param name="rad">the input angle as Radian value</param>
        /// <returns>The Degree value</returns>
        public static float ToDegree(this float rad)
        {
            return ((rad * 180) / (float)Math.PI);
        }

        /// <summary>
        /// Calculates the Radian from a given float the represents an angle as Degree
        /// </summary>
        /// <param name="deg">the input angle as Degree value</param>
        /// <returns>The Radian value</returns>
        public static float ToRadian(this float deg)
        {
            return ((deg * (float)Math.PI) / 180);
        }
    }
}
