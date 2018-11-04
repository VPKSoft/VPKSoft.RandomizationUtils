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
    /// A class to use weighted randomization to select items from a item collection.
    /// </summary>
    public static class WieghtedRandom
    {
        /// <summary>
        /// Gets or sets a Random class instance used as a helper with the WieghtedRandom class.
        /// </summary>
        public static Random Random { get; set; } = new Random();

        /// <summary>
        /// Uses weighted randomization to return an item from the <paramref name="pairs"/> collection.
        /// </summary>
        /// <typeparam name="T">A type of the Value property member.</typeparam>
        /// <typeparam name="T2">The type of the AdditionalData property member.</typeparam>
        /// <param name="pairs">A collection of WeightedItem class instances to be used for the randomization.</param>
        /// <returns>An item from the <paramref name="pairs"/> collection randomized using weighted randomization.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the pairs collection is null.</exception>
        public static WeightedItem<T, T2> RandomWeighted<T, T2>(List<WeightedItem<T, T2>> pairs)
        {
            if (pairs == null)
            {
                throw new ArgumentNullException("pairs");
            }

            if (pairs.Count == 0) // nothing reasonable to return..
            {
                // ..so just return the default value..
                return default(WeightedItem<T, T2>);
            }

            pairs = pairs.OrderBy(f => f.Weight).ToList();

            // get the maximum cumulative value in the list..
            double cumulativeMax = pairs.Sum(f => f.Weight);

            // randomize with the maximum as the maximum value of the randomization..
            double randValue = Random.NextDouble(cumulativeMax);

            // set the current cumulative weight value to zero..
            double currentCumulativeValue = 0;

            // loop through the pairs in the list..
            for (int i = 0; i < pairs.Count; i++)
            {
                currentCumulativeValue += pairs[i].Weight;

                // if the random value is less than the cumulative sum of the iterated items Weight property values..
                if (randValue < currentCumulativeValue)
                {
                    // return the CountValuePair..
                    return pairs[i];
                }
            }

            // this shouldn't ever happen, but just in case return the default value..
            return default(WeightedItem<T, T2>);
        }
    }

    /// <summary>
    /// A class which is used by the RandomWeighted method.
    /// </summary>
    /// <typeparam name="T">A type of the Value property member.</typeparam>
    /// <typeparam name="T2">The type of the AdditionalData property member.</typeparam>
    public class WeightedItem<T, T2>
    {
        /// <summary>
        /// Gets or sets the weight value for an item.
        /// </summary>
        public double Weight { get; set; } = -1;

        /// <summary>
        /// Gets or sets the value of a weighted item.
        /// </summary>
        public T Value { get; set; } = default(T);

        /// <summary>
        /// Gets or sets the additional data of a weighted item. 
        /// This is an optional addition so if the property is not used in the code, just give it some type i.e. object.
        /// </summary>
        /// <value>
        /// The additional data.
        /// </value>
        public T2 AdditionalData { get; set; } = default(T2);
    }
}
