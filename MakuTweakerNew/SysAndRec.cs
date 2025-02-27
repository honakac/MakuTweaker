using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew;

public partial class SysAndRec : Page, IComponentConnector
{
	private bool isLoaded;

	private MainWindow mw = (MainWindow)Application.Current.MainWindow;

	public SysAndRec()
	{
		InitializeComponent();
		sr1.IsOn = Settings.Default.sr1;
		sr2.IsOn = Settings.Default.sr2;
		sr3.IsOn = Settings.Default.sr3;
		sr4.IsOn = Settings.Default.sr4;
		sr5.IsOn = Settings.Default.sr5;
		dp.IsEnabled = !Settings.Default.b1;
		dnet.IsEnabled = !Settings.Default.b2;
		sfc.IsEnabled = !Settings.Default.b3;
		dism.IsEnabled = !Settings.Default.b4;
		sxs.IsEnabled = !Settings.Default.b5;
		temp.IsEnabled = !Settings.Default.b6;
		wupd.IsEnabled = !Settings.Default.b7;
		lgp.IsEnabled = !Settings.Default.b8;
		checkReg();
		if (checkWinEd() == "Core" || checkWinEd() == "CoreSingleLanguage" || checkWinEd() == "CoreCountrySpecific" || checkWinEd() == "CoreN")
		{
			sr8L.Visibility = Visibility.Visible;
			lgp.Visibility = Visibility.Visible;
		}
		LoadLang();
		isLoaded = true;
	}

	private string checkWinEd()
	{
		string name = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
		string name2 = "EditionID";
		using RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name);
		return registryKey.GetValue(name2).ToString();
	}

	private void sr1_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.sr1 = sr1.IsOn;
			if (sr1.IsOn)
			{
				Process.Start("cmd.exe", "/c \"bcdedit /set \"{current}\" bootmenupolicy legacy\"");
			}
			else
			{
				Process.Start("cmd.exe", "/c \"bcdedit /set \"{current}\" bootmenupolicy standard\"");
			}
		}
	}

	private void sr2_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.sr2 = sr2.IsOn;
			if (sr2.IsOn)
			{
				Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" custom:16000067 true\"");
			}
			else
			{
				Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" custom:16000067 false\"");
			}
		}
	}

	private void sr3_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.sr3 = sr3.IsOn;
			if (sr3.IsOn)
			{
				Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" custom:16000069 true\"");
			}
			else
			{
				Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" custom:16000069 false\"");
			}
		}
	}

	private void sr4_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.sr4 = sr4.IsOn;
			if (sr4.IsOn)
			{
				Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" advancedoptions true\"");
			}
			else
			{
				Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" advancedoptions false\"");
			}
		}
	}

	private void sr5_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.sr5 = sr5.IsOn;
			if (sr5.IsOn)
			{
				Process.Start("cmd.exe", "/k compact /compactos:always");
			}
			else
			{
				Process.Start("cmd.exe", "/k compact /compactos:never");
			}
			sr5.IsEnabled = false;
		}
	}

	private void sr6_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.sr6 = sr6.IsOn;
			if (sr6.IsOn)
			{
				Process.Start("cmd.exe", "/c REG ADD HKLM\\SYSTEM\\CurrentControlSet\\Control\\BitLocker /v PreventDeviceEncryption /t REG_DWORD /d 1 /f");
			}
			else
			{
				Process.Start("cmd.exe", "/c REG ADD HKLM\\SYSTEM\\CurrentControlSet\\Control\\BitLocker /v PreventDeviceEncryption /t REG_DWORD /d 0 /f");
			}
		}
	}

	private void dp_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("cmd.exe", "/C dism /online /Enable-Feature /FeatureName:DirectPlay /All");
		dp.IsEnabled = false;
		Settings.Default.b1 = !dp.IsEnabled;
	}

	private void dnet_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("powershell.exe", "/C Add-WindowsCapability -Online -Name NetFx3~~~~\"");
		dnet.IsEnabled = false;
		Settings.Default.b2 = !dnet.IsEnabled;
	}

	private void sfc_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("cmd.exe", "/k sfc /scannow");
		mw.RebootNotify(3);
		sfc.IsEnabled = false;
		Settings.Default.b3 = !sfc.IsEnabled;
	}

	private void dism_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("cmd.exe", "/C DISM /Online /Cleanup-Image /RestoreHealth");
		mw.RebootNotify(3);
		dism.IsEnabled = false;
		Settings.Default.b4 = !dism.IsEnabled;
	}

	private void sxs_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("cmd.exe", "/C dism /Online /Cleanup-Image /StartComponentCleanup /ResetBase");
		mw.RebootNotify(3);
		sxs.IsEnabled = false;
		Settings.Default.b5 = !sxs.IsEnabled;
	}

	private void temp_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("cmd.exe", "/k del /q /f %temp%");
		temp.IsEnabled = false;
		Settings.Default.b6 = !temp.IsEnabled;
	}

	private void wupd_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("cmd.exe", "/k del /f /s /q %windir%\\SoftwareDistribution\\Download\\*");
		wupd.IsEnabled = false;
		Settings.Default.b7 = !wupd.IsEnabled;
	}

	private void lgp_Click(object sender, RoutedEventArgs e)
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "sr");
		string contents = "\r\n            pushd \"%~dp0\"\r\n\r\n            dir /b %SystemRoot%\\servicing\\Packages\\Microsoft-Windows-GroupPolicy-ClientExtensions-Package~3*.mum >List.txt \r\n            dir /b %SystemRoot%\\servicing\\Packages\\Microsoft-Windows-GroupPolicy-ClientTools-Package~3*.mum >>List.txt \r\n\r\n            for /f %%i in ('findstr /i . List.txt 2^>nul') do dism /online /norestart /add-package:\"%SystemRoot%\\servicing\\Packages\\%%i\"";
		string text = Path.Combine(Path.GetTempPath(), "script.bat");
		File.WriteAllText(text, contents);
		Process process = new Process();
		process.StartInfo.FileName = "cmd.exe";
		process.StartInfo.Arguments = "/c \"" + text + "\"";
		process.StartInfo.UseShellExecute = true;
		process.StartInfo.CreateNoWindow = false;
		mw.ChSt(dictionary["status"]["sr8"]);
		try
		{
			process.Start();
		}
		catch
		{
		}
		lgp.IsEnabled = false;
		Settings.Default.b8 = !lgp.IsEnabled;
	}

	private void t9_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.o9 = t9.IsOn;
			if (t9.IsOn)
			{
				Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager").SetValue("AutoChkTimeout", 60);
			}
			else
			{
				Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager").SetValue("AutoChkTimeout", 8);
			}
		}
	}

	private void LoadLang()
	{
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(language, "sr");
		Dictionary<string, Dictionary<string, string>> dictionary2 = MainWindow.Localization.LoadLocalization(language, "base");
		Dictionary<string, Dictionary<string, string>> dictionary3 = MainWindow.Localization.LoadLocalization(language, "oth");
		label.Text = dictionary["main"]["label"];
		l1.Text = dictionary["main"]["sr1l"];
		l2.Text = dictionary["main"]["sr2l"];
		l3.Text = dictionary["main"]["sr3l"];
		l4.Text = dictionary["main"]["sr4l"];
		l5.Text = dictionary["main"]["sr5l"];
		l6.Text = dictionary["main"]["sr6l"];
		l7.Text = dictionary["main"]["sr7l"];
		sr8L.Text = dictionary["main"]["sr8l"];
		dp.Content = dictionary["main"]["b1"];
		dnet.Content = dictionary["main"]["b1"];
		sfc.Content = dictionary["main"]["b2"];
		dism.Content = dictionary["main"]["b2"];
		sxs.Content = dictionary["main"]["b3"];
		temp.Content = dictionary["main"]["b4"];
		wupd.Content = dictionary["main"]["b4"];
		sr1.Header = dictionary["main"]["sr1"];
		sr2.Header = dictionary["main"]["sr2"];
		sr3.Header = dictionary["main"]["sr3"];
		sr4.Header = dictionary["main"]["sr4"];
		sr5.Header = dictionary["main"]["sr5"];
		sr6.Header = dictionary["main"]["sr6"];
		t9.Header = dictionary3["main"]["o9"];
		t10.Header = dictionary["main"]["sr7"];
		t11.Header = dictionary["main"]["sr8"];
		sr1.OffContent = dictionary2["def"]["off"];
		sr2.OffContent = dictionary2["def"]["off"];
		sr3.OffContent = dictionary2["def"]["off"];
		sr4.OffContent = dictionary2["def"]["off"];
		sr5.OffContent = dictionary2["def"]["off"];
		sr6.OffContent = dictionary2["def"]["off"];
		t9.OffContent = dictionary2["def"]["off"];
		t10.OffContent = dictionary2["def"]["off"];
		t11.OffContent = dictionary2["def"]["off"];
		sr1.OnContent = dictionary2["def"]["on"];
		sr2.OnContent = dictionary2["def"]["on"];
		sr3.OnContent = dictionary2["def"]["on"];
		sr4.OnContent = dictionary2["def"]["on"];
		sr5.OnContent = dictionary2["def"]["on"];
		sr6.OnContent = dictionary2["def"]["on"];
		t9.OnContent = dictionary2["def"]["on"];
		t10.OnContent = dictionary2["def"]["on"];
		t11.OnContent = dictionary2["def"]["on"];
	}

	private void checkReg()
	{
		sr6.IsOn = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\BitLocker")?.GetValue("PreventDeviceEncryption")?.Equals(1) == true;
		t9.IsOn = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager")?.GetValue("AutoChkTimeout")?.Equals(60) == true;
		t10.IsOn = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard\\Scenarios")?.GetValue("HypervisorEnforcedCodeIntegrity")?.Equals(0) == true;
		t11.IsOn = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Power")?.GetValue("HibernateEnabled")?.Equals(0) == true;
	}

	private void t10_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			if (t10.IsOn)
			{
				Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard\\Scenarios").SetValue("HypervisorEnforcedCodeIntegrity", 0);
			}
			else
			{
				Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard\\Scenarios").SetValue("HypervisorEnforcedCodeIntegrity", 1);
			}
			mw.RebootNotify(1);
		}
	}

	private void t11_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Process.Start("cmd.exe", "/C powercfg /h off");
			mw.RebootNotify(1);
		}
	}
}
