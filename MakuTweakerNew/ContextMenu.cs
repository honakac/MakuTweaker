using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew;

public partial class ContextMenu : Page, IComponentConnector
{
	private MainWindow mw = (MainWindow)Application.Current.MainWindow;

	private bool isLoaded;

	public ContextMenu()
	{
		InitializeComponent();
		checkReg();
		LoadLang();
		isLoaded = true;
	}

	private void t1_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.o8 = t1.IsOn;
			if (t1.IsOn)
			{
				Registry.CurrentUser.CreateSubKey("Control Panel\\Desktop").SetValue("MenuShowDelay", "50");
			}
			else
			{
				Registry.CurrentUser.CreateSubKey("Control Panel\\Desktop").SetValue("MenuShowDelay", "400");
			}
			mw.RebootNotify(2);
		}
	}

	private void t2_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm2 = t2.IsOn;
		if (t2.IsOn)
		{
			try
			{
				Registry.ClassesRoot.DeleteSubKey("CLSID\\{09A47860-11B0-4DA5-AFA5-26D86198A780}\\InprocServer32");
				Registry.ClassesRoot.DeleteSubKey("CLSID\\{09A47860-11B0-4DA5-AFA5-26D86198A780}\\Version");
				return;
			}
			catch
			{
				return;
			}
		}
		Registry.ClassesRoot.CreateSubKey("CLSID\\{09A47860-11B0-4DA5-AFA5-26D86198A780}\\InprocServer32").SetValue("", "C:\\Program Files\\Windows Defender\\shellext.dll");
		Registry.ClassesRoot.CreateSubKey("CLSID\\{09A47860-11B0-4DA5-AFA5-26D86198A780}\\InprocServer32").SetValue("ThreadingModel", "Apartment");
		Registry.ClassesRoot.CreateSubKey("CLSID\\{09A47860-11B0-4DA5-AFA5-26D86198A780}\\Version").SetValue("", "10.0.22621.1");
	}

	private void t3_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm3 = t3.IsOn;
		if (t3.IsOn)
		{
			try
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked").SetValue("{9F156763-7844-4DC4-B2B1-901F640F5155}", "");
			}
			catch
			{
			}
		}
		else
		{
			try
			{
				Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked");
			}
			catch
			{
			}
		}
		mw.RebootNotify(2);
	}

	private void t4_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm4 = t4.IsOn;
		if (t4.IsOn)
		{
			try
			{
				Registry.ClassesRoot.CreateSubKey("*\\shell\\pintohomefile").SetValue("ProgrammaticAccessOnly", "");
				Registry.ClassesRoot.CreateSubKey("Drive\\shell\\pintohomefile").SetValue("ProgrammaticAccessOnly", "");
				Registry.ClassesRoot.CreateSubKey("Folder\\shell\\pintohomefile").SetValue("ProgrammaticAccessOnly", "");
				Registry.ClassesRoot.CreateSubKey("Network\\shell\\pintohomefile").SetValue("ProgrammaticAccessOnly", "");
				Registry.ClassesRoot.DeleteSubKeyTree("Drive\\shell\\pintohome");
				Registry.ClassesRoot.DeleteSubKeyTree("Folder\\shell\\pintohome");
				Registry.ClassesRoot.DeleteSubKeyTree("Network\\shell\\pintohome");
			}
			catch
			{
			}
		}
		else
		{
			try
			{
				Registry.ClassesRoot.CreateSubKey("AllFilesystemObjects\\shell\\pintohome").SetValue("CommandStateHandler", "{b455f46e-e4af-4035-b0a4-cf18d2f6f28e}");
				Registry.ClassesRoot.CreateSubKey("AllFilesystemObjects\\shell\\pintohome").SetValue("CommandStateSync", "");
				Registry.ClassesRoot.CreateSubKey("AllFilesystemObjects\\shell\\pintohome").SetValue("MUIVerb", "@shell32.dll,-51377");
				Registry.ClassesRoot.CreateSubKey("AllFilesystemObjects\\shell\\pintohome\\command").SetValue("DelegateExecute", "{b455f46e-e4af-4035-b0a4-cf18d2f6f28e}");
				Registry.ClassesRoot.CreateSubKey("Drive\\shell\\pintohome").SetValue("CommandStateHandler", "{b455f46e-e4af-4035-b0a4-cf18d2f6f28e}");
				Registry.ClassesRoot.CreateSubKey("Drive\\shell\\pintohome").SetValue("CommandStateSync", "");
				Registry.ClassesRoot.CreateSubKey("Drive\\shell\\pintohome").SetValue("MUIVerb", "@shell32.dll,-51377");
				Registry.ClassesRoot.CreateSubKey("Drive\\shell\\pintohome").SetValue("NeverDefault", "");
				Registry.ClassesRoot.CreateSubKey("Drive\\shell\\pintohome\\command").SetValue("DelegateExecute", "{b455f46e-e4af-4035-b0a4-cf18d2f6f28e}");
				Registry.ClassesRoot.CreateSubKey("Folder\\shell\\pintohome").SetValue("AppliesTo", "System.ParsingName:<>\"::{679f85cb-0220-4080-b29b-5540cc05aab6}\" AND System.ParsingName:<>\"::{645FF040-5081-101B-9F08-00AA002F954E}\" AND System.IsFolder:=System.StructuredQueryType.Boolean#True");
				Registry.ClassesRoot.CreateSubKey("Folder\\shell\\pintohome").SetValue("MUIVerb", "@shell32.dll,-51377");
				Registry.ClassesRoot.CreateSubKey("Folder\\shell\\pintohome\\command").SetValue("DelegateExecute", "{b455f46e-e4af-4035-b0a4-cf18d2f6f28e}");
				Registry.ClassesRoot.CreateSubKey("Network\\shell\\pintohome").SetValue("CommandStateHandler", "{b455f46e-e4af-4035-b0a4-cf18d2f6f28e}");
				Registry.ClassesRoot.CreateSubKey("Network\\shell\\pintohome").SetValue("CommandStateSync", "");
				Registry.ClassesRoot.CreateSubKey("Network\\shell\\pintohome").SetValue("MUIVerb", "@shell32.dll,-51377");
				Registry.ClassesRoot.CreateSubKey("Network\\shell\\pintohome").SetValue("NeverDefault", "");
				Registry.ClassesRoot.CreateSubKey("Network\\shell\\pintohome\\command").SetValue("DelegateExecute", "{b455f46e-e4af-4035-b0a4-cf18d2f6f28e}");
			}
			catch
			{
			}
		}
		mw.RebootNotify(2);
	}

	private void t5_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm5 = t5.IsOn;
		if (t5.IsOn)
		{
			try
			{
				Registry.ClassesRoot.DeleteSubKey("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\ModernSharing");
				return;
			}
			catch
			{
				return;
			}
		}
		Registry.ClassesRoot.CreateSubKey("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\ModernSharing").SetValue("", "{e2bf9676-5f8f-435c-97eb-11607a5bedf7}");
	}

	private void t6_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm6 = t6.IsOn;
		if (t6.IsOn)
		{
			try
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked").SetValue("{596AB062-B4D2-4215-9F74-E9109B0A8153}", "");
			}
			catch
			{
			}
		}
		else
		{
			try
			{
				Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked");
			}
			catch
			{
			}
		}
		mw.RebootNotify(2);
	}

	private void t7_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm7 = t7.IsOn;
		if (t7.IsOn)
		{
			try
			{
				Registry.ClassesRoot.DeleteSubKey("Folder\\ShellEx\\ContextMenuHandlers\\Library Location");
				return;
			}
			catch
			{
				return;
			}
		}
		Registry.ClassesRoot.CreateSubKey("Folder\\ShellEx\\ContextMenuHandlers\\Library Location").SetValue("", "{3dad6c5d-2167-4cae-9914-f99e41c12cfa}");
	}

	private void t8_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm8 = t8.IsOn;
		if (t8.IsOn)
		{
			try
			{
				Registry.ClassesRoot.CreateSubKey("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\SendTo").SetValue("", "");
			}
			catch
			{
			}
		}
		else
		{
			Registry.ClassesRoot.CreateSubKey("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\SendTo").SetValue("", "{7BA4C740-9E81-11CF-99D3-00AA004AE837}");
		}
		mw.RebootNotify(2);
	}

	private void t9_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm9 = t9.IsOn;
		if (t9.IsOn)
		{
			try
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked").SetValue("{f81e9010-6ea4-11ce-a7ff-00aa003ca9f6}", "");
			}
			catch
			{
			}
		}
		else
		{
			try
			{
				Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked");
			}
			catch
			{
			}
		}
		mw.RebootNotify(2);
	}

	private void t10_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm10 = t10.IsOn;
		if (t10.IsOn)
		{
			try
			{
				Registry.ClassesRoot.DeleteSubKey("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\CopyAsPathMenu");
				return;
			}
			catch
			{
				return;
			}
		}
		Registry.ClassesRoot.CreateSubKey("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\CopyAsPathMenu").SetValue("", "{f3d06e7c-1e45-4a26-847e-f9fcdee59be0}");
	}

	private void t11_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm11 = t11.IsOn;
		if (t11.IsOn)
		{
			try
			{
				Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Classes\\Folder\\shellex\\ContextMenuHandlers\\PintoStartScreen");
				Registry.ClassesRoot.CreateSubKey("exefile\\shellex\\ContextMenuHandlers\\PintoStartScreen").SetValue("", "");
				return;
			}
			catch
			{
				return;
			}
		}
		Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\Folder\\shellex\\ContextMenuHandlers\\PintoStartScreen").SetValue("", "{470C0EBD-5D73-4d58-9CED-E91E22E23282}");
		Registry.ClassesRoot.CreateSubKey("exefile\\shellex\\ContextMenuHandlers\\PintoStartScreen").SetValue("", "{470C0EBD-5D73-4d58-9CED-E91E22E23282}");
	}

	private void t12_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm12 = t12.IsOn;
		if (t12.IsOn)
		{
			try
			{
				Registry.ClassesRoot.DeleteSubKey("*\\shellex\\ContextMenuHandlers\\{90AA3A4E-1CBA-4233-B8BB-535773D48449}");
				return;
			}
			catch
			{
				return;
			}
		}
		Registry.ClassesRoot.CreateSubKey("*\\shellex\\ContextMenuHandlers\\{90AA3A4E-1CBA-4233-B8BB-535773D48449}").SetValue("", "Taskband Pin");
	}

	private void t13_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm13 = t13.IsOn;
		if (t13.IsOn)
		{
			try
			{
				Registry.ClassesRoot.DeleteSubKeyTree("Folder\\shell\\opennewtab");
				return;
			}
			catch
			{
				return;
			}
		}
		Registry.ClassesRoot.CreateSubKey("Folder\\shell\\opennewtab").SetValue("CommandStateHandler", "{11dbb47c-a525-400b-9e80-a54615a090c0}");
		Registry.ClassesRoot.CreateSubKey("Folder\\shell\\opennewtab").SetValue("CommandStateSync", "");
		Registry.ClassesRoot.CreateSubKey("Folder\\shell\\opennewtab").SetValue("LaunchExplorerFlags", 32);
		Registry.ClassesRoot.CreateSubKey("Folder\\shell\\opennewtab").SetValue("MUIVerb", "@windows.storage.dll,-8519");
		Registry.ClassesRoot.CreateSubKey("Folder\\shell\\opennewtab").SetValue("MultiSelectModel", "Document");
		Registry.ClassesRoot.CreateSubKey("Folder\\shell\\opennewtab").SetValue("OnlyInBrowserWindow", "");
		Registry.ClassesRoot.CreateSubKey("Folder\\shell\\opennewtab\\command").SetValue("DelegateExecute", "{11dbb47c-a525-400b-9e80-a54615a090c0}");
	}

	private void t14_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.cm14 = t14.IsOn;
		if (t14.IsOn)
		{
			try
			{
				Registry.CurrentUser.CreateSubKey("Software\\NVIDIA Corporation\\Global\\NvCplApi\\Policies").SetValue("ContextUIPolicy", 0);
				return;
			}
			catch
			{
				return;
			}
		}
		try
		{
			Registry.CurrentUser.CreateSubKey("Software\\NVIDIA Corporation\\Global\\NvCplApi\\Policies").SetValue("ContextUIPolicy", 2);
		}
		catch
		{
		}
	}

	private void t15_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.cm15 = t15.IsOn;
			if (t15.IsOn)
			{
				Process.Start("cmd.exe", "/c \"reg.exe add \"HKCU\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\\InprocServer32\" /f /ve\"");
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0);
			}
			else
			{
				Process.Start("cmd.exe", "/c \"reg delete \"HKCU\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\\InprocServer32\" /f\"");
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0);
			}
			mw.RebootNotify(2);
		}
	}

	private void LoadLang()
	{
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(language, "cm");
		Dictionary<string, Dictionary<string, string>> dictionary2 = MainWindow.Localization.LoadLocalization(language, "base");
		label.Text = dictionary["main"]["label"];
		t15.Header = dictionary["main"]["t1"];
		t1.Header = dictionary["main"]["o8"];
		t2.Header = dictionary["main"]["t2"];
		t3.Header = dictionary["main"]["t3"];
		t4.Header = dictionary["main"]["t4"];
		t5.Header = dictionary["main"]["t5"];
		t6.Header = dictionary["main"]["t6"];
		t7.Header = dictionary["main"]["t7"];
		t8.Header = dictionary["main"]["t8"];
		t9.Header = dictionary["main"]["t9"];
		t10.Header = dictionary["main"]["t10"];
		t11.Header = dictionary["main"]["t11"];
		t12.Header = dictionary["main"]["t12"];
		t13.Header = dictionary["main"]["t13"];
		t14.Header = dictionary["main"]["t14"];
		t1.OffContent = dictionary2["def"]["off"];
		t2.OffContent = dictionary2["def"]["off"];
		t3.OffContent = dictionary2["def"]["off"];
		t4.OffContent = dictionary2["def"]["off"];
		t5.OffContent = dictionary2["def"]["off"];
		t6.OffContent = dictionary2["def"]["off"];
		t7.OffContent = dictionary2["def"]["off"];
		t8.OffContent = dictionary2["def"]["off"];
		t9.OffContent = dictionary2["def"]["off"];
		t10.OffContent = dictionary2["def"]["off"];
		t11.OffContent = dictionary2["def"]["off"];
		t12.OffContent = dictionary2["def"]["off"];
		t13.OffContent = dictionary2["def"]["off"];
		t14.OffContent = dictionary2["def"]["off"];
		t15.OffContent = dictionary2["def"]["off"];
		t1.OnContent = dictionary2["def"]["on"];
		t2.OnContent = dictionary2["def"]["on"];
		t3.OnContent = dictionary2["def"]["on"];
		t4.OnContent = dictionary2["def"]["on"];
		t5.OnContent = dictionary2["def"]["on"];
		t6.OnContent = dictionary2["def"]["on"];
		t7.OnContent = dictionary2["def"]["on"];
		t8.OnContent = dictionary2["def"]["on"];
		t9.OnContent = dictionary2["def"]["on"];
		t10.OnContent = dictionary2["def"]["on"];
		t11.OnContent = dictionary2["def"]["on"];
		t12.OnContent = dictionary2["def"]["on"];
		t13.OnContent = dictionary2["def"]["on"];
		t14.OnContent = dictionary2["def"]["on"];
		t15.OnContent = dictionary2["def"]["on"];
	}

	private void checkReg()
	{
		t1.IsOn = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop")?.GetValue("MenuShowDelay")?.Equals("50") == true;
		t3.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked")?.GetValue("{9F156763-7844-4DC4-B2B1-901F640F5155}")?.Equals("") == true;
		t5.IsOn = Registry.ClassesRoot.OpenSubKey("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\ModernSharing") == null;
		t6.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked")?.GetValue("{596AB062-B4D2-4215-9F74-E9109B0A8153}")?.Equals("") == true;
		t7.IsOn = Registry.ClassesRoot.OpenSubKey("Folder\\ShellEx\\ContextMenuHandlers\\Library Location") == null;
		t8.IsOn = Registry.ClassesRoot.OpenSubKey("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\SendTo")?.GetValue("")?.Equals("") == true;
		t9.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked")?.GetValue("{f81e9010-6ea4-11ce-a7ff-00aa003ca9f6}")?.Equals("") == true;
		t10.IsOn = Registry.ClassesRoot.OpenSubKey("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\CopyAsPathMenu") == null;
		t11.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Classes\\Folder\\shellex\\ContextMenuHandlers\\PintoStartScreen") == null || Registry.ClassesRoot.CreateSubKey("exefile\\shellex\\ContextMenuHandlers\\PintoStartScreen")?.GetValue("")?.Equals("") == true;
		t12.IsOn = Registry.ClassesRoot.OpenSubKey("*\\shellex\\ContextMenuHandlers\\{90AA3A4E-1CBA-4233-B8BB-535773D48449}") == null;
		t13.IsOn = Registry.ClassesRoot.OpenSubKey("Folder\\shell\\opennewtab") == null;
		t14.IsOn = Registry.CurrentUser.OpenSubKey("Software\\NVIDIA Corporation\\Global\\NvCplApi\\Policies")?.GetValue("ContextUIPolicy")?.Equals(0) == true;
		t15.IsOn = Registry.CurrentUser.OpenSubKey("Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\\InprocServer32")?.GetValue("")?.Equals("") == true;
		t2.IsOn = Registry.ClassesRoot.OpenSubKey("CLSID\\{09A47860-11B0-4DA5-AFA5-26D86198A780}\\InprocServer32") == null || Registry.ClassesRoot.OpenSubKey("CLSID\\{09A47860-11B0-4DA5-AFA5-26D86198A780}\\Version") == null;
		t4.IsOn = Registry.ClassesRoot.OpenSubKey("Network\\shell\\pintohome") == null || Registry.ClassesRoot.OpenSubKey("Drive\\shell\\pintohome") == null || Registry.ClassesRoot.OpenSubKey("Folder\\shell\\pintohome") == null || Registry.ClassesRoot.OpenSubKey("Drive\\shell\\pintohomefile")?.GetValue("ProgrammaticAccessOnly")?.Equals("") == true || Registry.ClassesRoot.OpenSubKey("Folder\\shell\\pintohomefile")?.GetValue("ProgrammaticAccessOnly")?.Equals("") == true || Registry.ClassesRoot.OpenSubKey("Network\\shell\\pintohomefile")?.GetValue("ProgrammaticAccessOnly")?.Equals("") == true || Registry.ClassesRoot.OpenSubKey("*\\shell\\pintohomefile")?.GetValue("ProgrammaticAccessOnly")?.Equals("") == true;
	}
}
