﻿namespace MsieJavaScriptEngine.ActiveScript
{
	using System;

	[Flags]
	internal enum ScriptTypeLibFlags : uint
	{
		/// <summary>
		/// No flags
		/// </summary>
		None = 0,

		/// <summary>
		/// The type library describes an ActiveX control used by the host
		/// </summary>
		IsControl = 0x00000010,

		/// <summary>
		/// Not documented
		/// </summary>
		IsPersistent = 0x00000040,
	}
}