﻿namespace MsieJavaScriptEngine.JsRt.Edge
{
	using System;
	using System.Runtime.InteropServices;

	/// <summary>
	/// “Edge” property identifier
	/// </summary>
	/// <remarks>
	/// Property identifiers are used to refer to properties of JavaScript objects instead of using
	/// strings.
	/// </remarks>
	internal struct EdgeJsPropertyId : IEquatable<EdgeJsPropertyId>
	{
		/// <summary>
		/// The id
		/// </summary>
		private readonly IntPtr _id;

		/// <summary>
		/// Gets a invalid ID
		/// </summary>
		public static EdgeJsPropertyId Invalid
		{
			get { return new EdgeJsPropertyId(IntPtr.Zero); }
		}

		/// <summary>
		/// Gets a name associated with the property ID
		/// </summary>
		/// <remarks>
		/// <para>
		/// Requires an active script context.
		/// </para>
		/// </remarks>
		public string Name
		{
			get
			{
				IntPtr buffer;
				EdgeJsErrorHelpers.ThrowIfError(EdgeNativeMethods.JsGetPropertyNameFromId(this, out buffer));

				return Marshal.PtrToStringAuto(buffer);
			}
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="EdgeJsPropertyId"/> struct
		/// </summary>
		/// <param name="id">The ID</param>
		internal EdgeJsPropertyId(IntPtr id)
		{
			_id = id;
		}


		/// <summary>
		/// Gets a property ID associated with the name
		/// </summary>
		/// <remarks>
		/// <para>
		/// Property IDs are specific to a context and cannot be used across contexts.
		/// </para>
		/// <para>
		/// Requires an active script context.
		/// </para>
		/// </remarks>
		/// <param name="name">The name of the property ID to get or create.
		/// The name may consist of only digits.</param>
		/// <returns>The property ID in this runtime for the given name</returns>
		public static EdgeJsPropertyId FromString(string name)
		{
			EdgeJsPropertyId id;
			EdgeJsErrorHelpers.ThrowIfError(EdgeNativeMethods.JsGetPropertyIdFromName(name, out id));

			return id;
		}

		/// <summary>
		/// The equality operator for property IDs
		/// </summary>
		/// <param name="left">The first property ID to compare</param>
		/// <param name="right">The second property ID to compare</param>
		/// <returns>Whether the two property IDs are the same</returns>
		public static bool operator ==(EdgeJsPropertyId left, EdgeJsPropertyId right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// The inequality operator for property IDs
		/// </summary>
		/// <param name="left">The first property ID to compare</param>
		/// <param name="right">The second property ID to compare</param>
		/// <returns>Whether the two property IDs are not the same</returns>
		public static bool operator !=(EdgeJsPropertyId left, EdgeJsPropertyId right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Checks for equality between property IDs
		/// </summary>
		/// <param name="obj">The other property ID to compare</param>
		/// <returns>Whether the two property IDs are the same</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			return obj is EdgeJsPropertyId && Equals((EdgeJsPropertyId)obj);
		}

		/// <summary>
		/// The hash code
		/// </summary>
		/// <returns>The hash code of the property ID</returns>
		public override int GetHashCode()
		{
			return _id.ToInt32();
		}

		/// <summary>
		/// Converts a property ID to a string
		/// </summary>
		/// <returns>The name of the property ID</returns>
		public override string ToString()
		{
			return Name;
		}

		#region IEquatable<T> implementation

		/// <summary>
		/// Checks for equality between property IDs
		/// </summary>
		/// <param name="other">The other property ID to compare</param>
		/// <returns>Whether the two property IDs are the same</returns>
		public bool Equals(EdgeJsPropertyId other)
		{
			return _id == other._id;
		}

		#endregion
	}
}