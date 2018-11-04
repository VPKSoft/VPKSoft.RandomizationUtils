using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1587 // XML comment is not placed on a valid language element
/// <summary>
/// A name space for the VPKSoft.RandomizationUtils.
/// </summary>
namespace VPKSoft.RandomizationUtils // the Sandcastle did require the name space to be commented as well once..
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
{
    /// <summary>
    /// An utility to generate biased random numbers.
    /// </summary>
    public static class BiasedRandom
    {
        /// <summary>
        /// Gets or sets a Random class instance used as a helper with the BiasedRandom class.
        /// </summary>
        public static Random Random { get; set; } = new Random();

        /// <summary>
        /// Generates biased random numbers by given percentage weight and other parameters.
        /// </summary>
        /// <param name="min">The minimum value of the randomization result.</param>
        /// <param name="max">The maximum value of the randomization result.</param>
        /// <param name="percentage">The percentage to which end the randomization is biased to.</param>
        /// <param name="percentagePoint">A value of how much the biasing should increase on both sides of <paramref name="centralPercentage"/> percent within a step of ten percent change.</param>
        /// <param name="percentagePow">A value of how much the <paramref name="percentagePoint"/> should be powered into.</param>
        /// <param name="centralPercentage">The "center" percentage if not operating on 0% to 100 % scale.</param>
        /// <param name="centralPercentageTolerance">A value surrounding the value of <paramref name="centralPercentage"/> percent should be allowed as error tolerance.</param>
        /// <returns>A biased random number between the give <paramref name="min"/> and the <paramref name="max"/> values.</returns>
        public static double RandomBiasedBase(double min, double max, double percentage,
            double percentagePoint = 1, double percentagePow = 0.15, double centralPercentage = 50,
            double centralPercentageTolerance = 0.05)
        {
            RandomAdditionalUtil.NormalizeDouble(ref min); // avoid infinities..
            RandomAdditionalUtil.NormalizeDouble(ref max); // avoid infinities..

            // An internal check function to test if the "normal" randomization should be used.
            // This is determined by the percentage parameter value compared to the centralPercentage parameter value 
            // using the centralPercentageTolerance parameter value.
            bool InTolerance()
            {
                return percentage < centralPercentage + centralPercentageTolerance &&
                    percentage > centralPercentage - centralPercentageTolerance;
            }

            // a default randomization in the center of the percentage parameter value..
            if (InTolerance())
            {
                // ..so return a default unbiased random value..
                return min + (max - min) * Random.NextDouble();
            }

            // divide centralPercentage parameter's value by the given percentage to make the randomization "realistically" biased: 50% = 1, 100% = 0.5 and so on..
            double power = centralPercentage / (percentage < centralPercentage ? ((centralPercentage * 2) - percentage) : percentage);

            // to add more bias each ten point change in the percentage will yield to higher probability..
            double addBias = 1;

            // the centralPercentage parameter's value is not allowed as it's the center of all percentage calculations..
            if (percentagePoint > 0 && percentage != centralPercentage)
            {
                addBias += 1; // power to zero doesn't give an accurate calculation..
                addBias += (percentage > centralPercentage ? percentage : (centralPercentage * 2) - percentage) / 10.0; // add the ten point percentage increment or decrement..

                // multiply by the weight of the percentagePoint parameter value.. 
                addBias *= percentagePoint;
                addBias = Math.Pow(addBias, percentagePow); // again power it up (!)..
            }

            // do power with division with the addBias value..
            double result = min + (max - min) * Math.Pow(Random.NextDouble(), power / addBias);

            if (percentage > centralPercentage) // the percentage is higher than the balanced parameter centralPercentage value..
            {
                return result;
            }
            else // the percentage is lower than the balanced centralPercentage value..
            {
                result = max - result; // invert the result..
                return result;
            }
        }

        /// <summary>
        /// Generates biased random numbers by given percentage weight.
        /// </summary>
        /// <param name="min">The minimum value of the randomization result.</param>
        /// <param name="max">The maximum value of the randomization result.</param>
        /// <param name="percentage">The percentage to which end the randomization is biased to.</param>
        /// <returns>A biased random number between the give <paramref name="min"/> and the <paramref name="max"/> values.</returns>
        public static double RandomBiased(double min, double max, double percentage)
        {
            // just call the "hefty" main method with default values..
            return RandomBiasedBase(min, max, percentage); // ..and return the result..
        }

        /// <summary>
        /// Generates biased random numbers by given percentage weight.
        /// </summary>
        /// <param name="min">The minimum value of the randomization result.</param>
        /// <param name="max">The maximum value of the randomization result.</param>
        /// <param name="percentage">The percentage to which end the randomization is biased to.</param>
        /// <param name="percentagePoint">A value of how much the biasing should increase on both sides of 50 percent within a step of ten percent change.</param>
        /// <param name="percentagePow">A value of how much the <paramref name="percentagePoint"/> should be powered into.</param>
        /// <returns>A double-precision floating point number that is greater than or equal to <paramref name="min"/>, and less than <paramref name="max"/>.</returns>
        public static double RandomBiased(double min, double max, double percentage, double percentagePoint, double percentagePow)
        {
            // just call the "hefty" main method with default values..
            return RandomBiasedBase(min, max, percentage, percentagePoint, percentagePow); // ..and return the result..
        }

        /// <summary>
        /// Generates biased random numbers by given percentage weight.
        /// </summary>
        /// <param name="percentage">The percentage to which end the randomization is biased to.</param>
        /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        public static double NextDouble(double percentage)
        {
            double result = RandomBiasedBase(0, 100, percentage);
            result /= 100;
            return result;
        }

        /// <summary>
        /// Generates biased random numbers by given percentage weight.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">  The exclusive upper bound of the random number returned.</param>
        /// <param name="percentage">The percentage to which end the randomization is biased to.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue.</returns>
        public static int Next(int minValue, int maxValue, double percentage)
        {
            // just call the "hefty" main method with default values..
            return (int)RandomBiasedBase(minValue, maxValue, percentage); // ..and return the result..
        }

        /// <summary>
        /// Generates biased random numbers by given percentage weight.
        /// </summary>
        /// <param name="maxValue">  The exclusive upper bound of the random number returned.</param>
        /// <param name="percentage">The percentage to which end the randomization is biased to.</param>
        /// <returns>Returns a non-negative random integer that is less than the specified maximum.</returns>
        public static int Next(int maxValue, double percentage)
        {
            return Next(0, maxValue, percentage);
        }

        /// <summary>
        /// Generates biased random numbers by given percentage weight.
        /// </summary>
        /// <param name="percentage">The percentage to which end the randomization is biased to.</param>
        /// <returns>Returns a non-negative random integer.</returns>
        public static int Next(double percentage)
        {
            return Next(0, int.MaxValue, percentage);
        }
    }
}
