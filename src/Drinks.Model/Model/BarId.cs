/* MIT License
 * 
 * Copyright (c) 2019, Olaf Kober
 * https://github.com/Amarok79/Bar
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

using System;
using System.Diagnostics;
using System.Globalization;


namespace Drinks.Model
{
	/// <summary>
	/// This value type represents the Id of a Bar.
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public struct BarId :
		IEquatable<BarId>
	{
		// data
		private readonly Guid mGuid;


		#region ++ Public Interface ++

		/// <summary>
		/// Gets the Guid that is being wrapped.
		/// </summary>
		public Guid Guid => mGuid;


		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// 
		/// <param name="guid">
		/// The Guid that should be wrapped.</param>
		public BarId(Guid guid)
		{
			mGuid = guid;
		}


		/// <summary>
		/// Returns a string that represents the current instance. This method returns a string in upper case 
		/// to make returned strings equatable.
		/// </summary>
		/// 
		/// <returns>
		/// A string that represents the current instance.
		/// </returns>
		public override String ToString()
		{
			return mGuid.ToString("B", CultureInfo.InvariantCulture).ToUpperInvariant();
		}

		#endregion

		#region ++ Public Interface (Equality) ++

		/// <summary>
		/// Returns the hash code for the current instance. 
		/// </summary>
		/// 
		/// <returns>
		/// A 32-bit signed integer hash code.</returns>
		public override Int32 GetHashCode()
		{
			return mGuid.GetHashCode();
		}


		/// <summary>
		/// Determines whether the specified instance is equal to the current instance.
		/// </summary>
		/// 
		/// <param name="obj">
		/// The instance to compare with the current instance.</param>
		/// 
		/// <returns>
		/// True, if the specified instance is equal to the current instance; otherwise, False.</returns>
		public override Boolean Equals(Object obj)
		{
			return obj is BarId && Equals((BarId)obj);
		}

		/// <summary>
		/// Determines whether the specified instance is equal to the current instance.
		/// </summary>
		/// 
		/// <param name="other">
		/// The instance to compare with the current instance.</param>
		/// 
		/// <returns>
		/// True, if the specified instance is equal to the current instance; otherwise, False.</returns>
		public Boolean Equals(BarId other)
		{
			// determine equality from fields
			return mGuid.Equals(other.mGuid);
		}


		/// <summary>
		/// Determines whether the specified instances are equal.
		/// </summary>
		/// 
		/// <param name="a">
		/// The first instance to compare.</param>
		/// <param name="b">
		/// The second instance to compare.</param>
		/// <returns>
		/// True, if the specified instances are equal; otherwise, False.</returns>
		public static Boolean operator ==(BarId a, BarId b)
		{
			return a.Equals(b);
		}

		/// <summary>
		/// Determines whether the specified instances are unequal.
		/// </summary>
		/// 
		/// <param name="a">
		/// The first instance to compare.</param>
		/// <param name="b">
		/// The second instance to compare.</param>
		/// <returns>
		/// True, if the specified instances are unequal; otherwise, False.</returns>
		public static Boolean operator !=(BarId a, BarId b)
		{
			return !a.Equals(b);
		}

		#endregion

		#region ~~ Internal Interface ~~

		/// <summary>
		/// This member serves the infrastructure and is not intended to be used directly.
		/// </summary>
		internal String DebuggerDisplay => this.ToString();

		#endregion
	}
}
