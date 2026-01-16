using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room_Reorder.Methods
{
    public static class UnitMethods
    {
        /// <summary>
        /// to convert radian to degree
        /// </summary>
        /// <param name="radian"></param>
        /// <returns>degree</returns>
        public static double ToDegrees(this double radian)
        {
            return radian * (180.0 / Math.PI);
        }

        /// <summary>
        /// to convert degree to radian
        /// </summary>
        /// <param name="degree"></param>
        /// <returns>radian</returns>
        public static double ToRadians(this double degree)
        {
            return degree * (Math.PI / 180.0);
        }

        /// <summary>
        /// radian to percent slope
        /// </summary>
        /// <param name="radian"></param>
        /// <returns>percent slope</returns>
        public static double Rad2Pct(this double radian)
        {
            return Math.Tan(radian) * 100;
        }
    }
}
