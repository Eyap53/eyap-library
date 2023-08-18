namespace EyapLibrary.Extensions
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	public static class EnumerableExtensions
	{
		// The random number generator
		public static Random rng = new Random();

		public enum SelectionMode
		{
			Intersect,
			Except
		}

		/// <summary>
		/// Orders this enumerable randomly
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array">The enumerable to shuffle</param>
		/// <returns>An enumerable with its values shuffled</returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> e) => e.OrderBy(x => rng.Next());

		/// <summary>
		/// Orders this enumerable randomly
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable to shuffle</param>
		/// <param name="r"></param>
		/// <returns>An enumerable with its values shuffled</returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> e, Random r) => e.OrderBy(x => r.Next());

		/// <summary>
		/// Flattens an array of collections to a single collection
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable array to flatten</param>
		/// <returns>A flatten enumerable</returns>
		public static IEnumerable<T> Flatten<T>(this IEnumerable<T>[] e) => e.SelectMany(x => x);

		/// <summary>
		/// Returns the set without all instances of the given element.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="element">The element to omit</param>
		/// <returns>An enumerable without all the instances of the element</returns>
		public static IEnumerable<T> Except<T>(this IEnumerable<T> e, T element) => e.Except(new T[] { element });

		/// <summary>
		/// Returns the set without the first instance of the given element.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="element">The element to omit</param>
		/// <returns>An enumerable without the first instance of the element</returns>
		public static IEnumerable<T> ExceptFirst<T>(this IEnumerable<T> e, T element)
		{
			IEnumerable<T> firstPart = e.TakeWhile(x => !x.Equals(element));
			return firstPart.Concat(e.Skip(firstPart.Count() + 1));
		}

		/// <summary>
		/// Returns a random element from the enumerable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <returns>A random value from the enumerable</returns>
		public static T Random<T>(this IEnumerable<T> e) => e.Random(rng);

		/// <summary>
		/// Returns a random element from the enumerable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="r">RNG to use</param>
		/// <returns>A random value from the enumerable</returns>
		public static T Random<T>(this IEnumerable<T> e, Random r) => e.ElementAt(r.Next(0, e.Count()));

		/// <summary>
		/// Returns a random element from, but after transforming, the enumerable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="r">RNG to use</param>
		/// <param name="mode">Selection mode, for blackenumerableing or whitenumerableing</param>
		/// <param name="values">Values that will be added into the blackenumerable or whiteenumerable</param>
		/// <returns>A random value from the new processed enumerable</returns>
		public static T Random<T>(this IEnumerable<T> e, Random r, SelectionMode mode, params T[] values)
		{
			return e.ApplySelectionMode(mode, values)
					.Random(r);
		}

		/// <summary>
		/// Returns a random element from, but after transforming, the enumerable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="mode">Selection mode, for blackenumerableing or whitenumerableing</param>
		/// <param name="values">Values that will be added into the blackenumerable or whiteenumerable</param>
		/// <returns>A random value from the new processed enumerable</returns>
		public static T Random<T>(this IEnumerable<T> e, SelectionMode mode, params T[] values)
		{
			return e.ApplySelectionMode(mode, values)
					.Random();
		}

		/// <summary>
		/// Returns a random element from the enumerable or default(T) if the enumerable is empty
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <returns>A random value from the enumerable or default(T) if the enumerable is empty</returns>
		public static T RandomOrDefault<T>(this IEnumerable<T> e) => e.RandomOrDefault(rng);

		/// <summary>
		/// Returns a random element from the enumerable or default(T) if the enumerable is empty
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="r">RNG to use</param>
		/// <returns>A random value from the enumerable or default(T) if the enumerable is empty</returns>
		public static T RandomOrDefault<T>(this IEnumerable<T> e, Random r) => e.ElementAtOrDefault(r.Next(0, e.Count()));

		/// <summary>
		/// Returns a random element from, but after transforming, the enumerable or default(T) if the new enumerable is empty
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="r">RNG to use</param>
		/// <param name="mode">Selection mode, for blackenumerableing or whitenumerableing</param>
		/// <param name="values">Values that will be added into the blackenumerable or whiteenumerable</param>
		/// <returns>A random value from the new processed enumerable</returns>
		public static T RandomOrDefault<T>(this IEnumerable<T> e, Random r, SelectionMode mode, params T[] values)
		{
			return e.ApplySelectionMode(mode, values)
					.RandomOrDefault(r);
		}

		/// <summary>
		/// Returns a random element from, but after transforming, the enumerable or default(T) if the new enumerable is empty
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="mode">Selection mode, for blackenumerableing or whitenumerableing</param>
		/// <param name="values">Values that will be added into the blackenumerable or whiteenumerable</param>
		/// <returns>A random value from the new processed enumerable</returns>
		public static T RandomOrDefault<T>(this IEnumerable<T> e, SelectionMode mode, params T[] values)
		{
			return e.ApplySelectionMode(mode, values)
					.RandomOrDefault();
		}

		/// <summary>
		/// Returns a random element from the enumerable, using weights
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="weightSelector">Function that determines the probability of an item to be selected. Open scale
		/// between 0 and <code>Double.MaxValue</code></param>
		/// <returns></returns>
		public static T RandomWeighted<T>(this IEnumerable<T> e, Func<T, double> weightSelector)
			=> RandomWeightedCore(e, e.ToDictionary(k => k, k => weightSelector(k)), false);

		/// <summary>
		/// Returns a random element from the enumerable, using weights
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="weights">The weights constraining the probability of an item to be selected. Open scale
		/// between 0 and <code>Double.MaxValue</code></param>
		/// <returns></returns>
		public static T RandomWeighted<T>(this IEnumerable<T> e, IDictionary<T, double> weights)
			=> RandomWeightedCore(e, weights, false);

		/// <summary>
		/// Returns a random element from the enumerable, using weights or default(T) if the enumerable is empty
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="weightSelector">Function that determines the probability of an item to be selected. Open scale
		/// between 0 and <code>Double.MaxValue</code></param>
		/// <returns></returns>
		public static T RandomWeightedOrDefault<T>(this IEnumerable<T> e, Func<T, double> weightSelector)
			=> RandomWeightedCore(e, e.ToDictionary(k => k, k => weightSelector(k)), true);

		/// <summary>
		/// Returns a random element from the enumerable, using weights or default(T) if the enumerable is empty
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="weights">The weights constraining the probability of an item to be selected. Open scale
		/// between 0 and <code>Double.MaxValue</code></param>
		/// <returns></returns>
		public static T RandomWeightedOrDefault<T>(this IEnumerable<T> e, IDictionary<T, double> weights)
			=> RandomWeightedCore(e, weights, true);

		private static T RandomWeightedCore<T>(this IEnumerable<T> e, IDictionary<T, double> weights, bool fallbackToDefault)
		{
			double accWeight = 0;
			double target = rng.NextDouble() * weights.Values.Sum();
			IEnumerable<T> res = e.SkipWhile(t => (accWeight += weights[t]) < target);
			return fallbackToDefault ? res.FirstOrDefault() : res.First();
		}

		/// <summary>
		/// Executes an action on each element of the enumerable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="action">The action to execute on each element</param>
		public static void ForEach<T>(this IEnumerable<T> e, Action<T> action)
		{
			foreach (T t in e)
			{
				action(t);
			}
		}

		/// <summary>
		/// Transform an enumerable depending on a SelectionMode
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e">The enumerable</param>
		/// <param name="mode">Selection mode, for blackenumerableing or whitenumerableing</param>
		/// <param name="values">Values that will be added into the blackenumerable or whiteenumerable</param>
		/// <returns>The enumerable after applying selection</returns>
		private static IEnumerable<T> ApplySelectionMode<T>(this IEnumerable<T> e, SelectionMode mode, params T[] values)
		{
			switch (mode)
			{
				case SelectionMode.Intersect:
					return e.Intersect(values);
				case SelectionMode.Except:
					return e.Except(values);
				default:
					throw new NotImplementedException();
			}
		}
	}
}
