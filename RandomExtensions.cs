﻿#region License
/*
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using System;

#pragma warning disable CS1587 // XML comment is not placed on a valid language element
/// <summary>
/// A name space for the VPKSoft.RandomizationUtils.
/// </summary>
namespace VPKSoft.RandomizationUtils // the Sandcastle did require the name space to be commented as well once..
{
    /// <summary>
    /// Extensions for the Random class.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to <paramref name="minDouble"/>, and less than <paramref name="maxDouble"/>.
        /// </summary>
        /// <param name="random">A Random class instance for this extension method.</param>
        /// <param name="minDouble">The minimum randomization value.</param>
        /// <param name="maxDouble">The maximum randomization value.</param>
        /// <returns>A random floating-point number that is greater than or equal to <paramref name="minDouble"/>, and less than <paramref name="maxDouble"/>.</returns>
        public static double NextDouble(this Random random, double minDouble, double maxDouble)
        {
            // just return the calculated value..
            return minDouble + ((maxDouble - minDouble) * random.NextDouble());
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than <paramref name="maxDouble"/>.
        /// </summary>
        /// <param name="random">A Random class instance for this extension method.</param>
        /// <param name="maxDouble">The maximum randomization value.</param>
        /// <returns>A random floating-point number that is greater than or equal to 0.0, and less than <paramref name="maxDouble"/>.</returns>
        public static double NextDouble(this Random random, double maxDouble)
        {
            // just return the calculated value..
            return random.NextDouble(0, maxDouble);
        }

        /// <summary>
        /// Returns a non-negative random floating-point number.
        /// </summary>
        /// <param name="random">A Random class instance for this extension method.</param>
        /// <returns>A non-negative random floating-point number.</returns>
        public static double NextDoubleNormal(this Random random)
        {
            // just return the calculated value..
            return random.NextDouble(0, double.MaxValue);
        }
    }
}
