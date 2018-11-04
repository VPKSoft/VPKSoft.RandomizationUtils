﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1587 // XML comment is not placed on a valid language element
/// <summary>
/// A name space for the VPKSoft.RandomizationUtils.
/// </summary>
namespace VPKSoft.RandomizationUtils // the Sandcastle did require the name space to be commented as well once..
{
    /// <summary>
    /// Some utilities for this library to be used.
    /// </summary>
    public static class RandomAdditionalUtil
    {
        /// <summary>
        /// Normalizes a floating-point number from either negative or positive infinity to minimum or maximum value depending on the infinity type.
        /// </summary>
        /// <param name="value">A reference to a floating-point number to be made finite.</param>
        internal static void NormalizeDouble(ref double value)
        {
            // negative infinity is normalized to the minimum value..
            if (double.IsNegativeInfinity(value))
            {
                value = double.MinValue;
            }
            // positive infinity is normalized to the maximum value..
            else if (double.IsPositiveInfinity(value))
            {
                value = double.MaxValue;
            }
        }
    }
}
