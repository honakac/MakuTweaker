using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System;

namespace WinRT.MakuTweakerGenericHelpers;

internal static class GlobalVtableLookup
{
	[ModuleInitializer]
	internal static void InitializeGlobalVtableLookup()
	{
		ComWrappersSupport.RegisterTypeComInterfaceEntriesLookup(LookupVtableEntries);
		ComWrappersSupport.RegisterTypeRuntimeClassNameLookup(LookupRuntimeClassName);
	}

	private static ComWrappers.ComInterfaceEntry[] LookupVtableEntries(System.Type type)
	{
		if (type.ToString() == "System.Windows.Controls.TextBlock")
		{
			return new ComWrappers.ComInterfaceEntry[1]
			{
				new ComWrappers.ComInterfaceEntry
				{
					IID = IServiceProviderMethods.IID,
					Vtable = IServiceProviderMethods.AbiToProjectionVftablePtr
				}
			};
		}
		return null;
	}

	private static string LookupRuntimeClassName(System.Type type)
	{
		if (type.ToString() == "System.Windows.Controls.TextBlock")
		{
			return "Microsoft.UI.Xaml.IXamlServiceProvider";
		}
		return null;
	}
}
