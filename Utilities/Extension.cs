using System;
using System.Collections.Generic;

namespace Utilities {
	public static partial class Extension {
		
		/// <summary>
		/// Performs the specified action on each element of the System.Collections.Generic.IEnumerable<T>. 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumeration">System.Collections.Generic.IEnumerable<T></param>
		/// <param name="action">The System.Action<T> delegate to perform on each element of the System.Collections.Generic.IEnumerable<T>.</param>
		/// <returns>void</returns>
		public static void ForEach<T>( this IEnumerable<T> enumeration, Action<T> action ) {
			foreach ( T item in enumeration ) {
				  action( item );
			}
		}
	}
}