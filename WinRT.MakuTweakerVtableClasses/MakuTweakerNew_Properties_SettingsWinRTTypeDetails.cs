using System.Runtime.InteropServices;
using ABI.System.ComponentModel;

namespace WinRT.MakuTweakerVtableClasses;

internal sealed class MakuTweakerNew_Properties_SettingsWinRTTypeDetails : IWinRTExposedTypeDetails
{
	public ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
	{
		return new ComWrappers.ComInterfaceEntry[1]
		{
			new ComWrappers.ComInterfaceEntry
			{
				IID = INotifyPropertyChangedMethods.IID,
				Vtable = INotifyPropertyChangedMethods.AbiToProjectionVftablePtr
			}
		};
	}
}
