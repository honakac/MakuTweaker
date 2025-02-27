using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MakuTweakerNew.Properties;
using Microsoft.Win32;
using ModernWpf.Controls;

namespace MakuTweakerNew;

public partial class Personalization : System.Windows.Controls.Page, IComponentConnector
{
	private bool isLoaded;

	private MainWindow mw = (MainWindow)Application.Current.MainWindow;

	public Personalization()
	{
		InitializeComponent();
		color.SelectedIndex = Settings.Default.p3;
		checkReg();
		LoadLang();
		isLoaded = true;
	}

	private void apN_Click(object sender, RoutedEventArgs e)
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "per");
		Settings.Default.p2 = newname.Text;
		string text = newname.Text;
		string text2 = "reg add HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates /v RenameNameTemplate /t REG_SZ /d \"" + text + "\" /f";
		Process.Start("cmd.exe", "/c " + text2);
		mw.ChSt(dictionary["status"]["apN"]);
	}

	private void stN_Click(object sender, RoutedEventArgs e)
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "per");
		Settings.Default.p2 = string.Empty;
		newname.Text = string.Empty;
		string text = "reg delete HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates /v RenameNameTemplate /f";
		Process.Start("cmd.exe", "/c " + text);
		mw.ChSt(dictionary["status"]["stN"]);
	}

	private void p1_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.p1 = p1.IsOn;
			if (p1.IsOn)
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\PolicyManager\\current\\device\\Education").SetValue("EnableEduThemes", 1);
			}
			else
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\PolicyManager\\current\\device\\Education").SetValue("EnableEduThemes", 0);
			}
			mw.RebootNotify(1);
		}
	}

	private void apC_Click(object sender, RoutedEventArgs e)
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "per");
		Settings.Default.p3 = color.SelectedIndex;
		string name = "Control Panel\\Colors";
		string text = "";
		string text2 = "";
		switch (color.SelectedIndex)
		{
		case 0:
			text = "51 153 255";
			text2 = "0 102 204";
			break;
		case 1:
			text = "0 100 100";
			text2 = "0 100 100";
			break;
		case 2:
			text = "180 0 180";
			text2 = "110 0 110";
			break;
		case 3:
			text = "0 90 30";
			text2 = "0 90 30";
			break;
		case 4:
			text = "100 40 0";
			text2 = "100 40 0";
			break;
		case 5:
			text = "135 0 0";
			text2 = "135 0 0";
			break;
		case 6:
			text = "15, 0, 120";
			text2 = "15, 0, 120";
			break;
		case 7:
			text = "40 40 40";
			text2 = "40 40 40";
			break;
		default:
			text = "51 153 255";
			text2 = "0 102 204";
			return;
		}
		RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, writable: true);
		if (registryKey != null)
		{
			registryKey.SetValue("HightLight", text, RegistryValueKind.String);
			registryKey.SetValue("Hilight", text, RegistryValueKind.String);
			registryKey.SetValue("HotTrackingColor", text2, RegistryValueKind.String);
		}
		mw.ChSt(dictionary["status"]["apC"]);
		mw.RebootNotify(1);
	}

	private void p2_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.p4 = p2.IsOn;
			if (p2.IsOn)
			{
				Process.Start("cmd.exe", "/c \"reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics\" /v CaptionHeight /t REG_SZ /d -270 /f\"");
				Process.Start("cmd.exe", "/c \"reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics\" /v CaptionWidth /t REG_SZ /d -270 /f\"");
			}
			else
			{
				Process.Start("cmd.exe", "/c \"reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics\" /v CaptionHeight /t REG_SZ /d -330 /f\"");
				Process.Start("cmd.exe", "/c \"reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics\" /v CaptionWidth /t REG_SZ /d -330 /f\"");
			}
			mw.RebootNotify(1);
		}
	}

	private void p3_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.o6 = p3.IsOn;
			if (p3.IsOn)
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System").SetValue("DisableAcrylicBackgroundOnLogon", 1);
			}
			else
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System").SetValue("DisableAcrylicBackgroundOnLogon", 0);
			}
		}
	}

	private void p4_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.o7 = p4.IsOn;
			if (p4.IsOn)
			{
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer").SetValue("AltTabSettings", 1);
			}
			else
			{
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer").SetValue("AltTabSettings", 0);
			}
		}
		mw.RebootNotify(2);
	}

	private void LoadLang()
	{
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(language, "per");
		Dictionary<string, Dictionary<string, string>> dictionary2 = MainWindow.Localization.LoadLocalization(language, "base");
		label.Text = dictionary["main"]["label"];
		l1.Text = dictionary["main"]["p1l"];
		newname.Watermark = dictionary["main"]["newname"];
		apN.Content = dictionary["main"]["b1"];
		apC.Content = dictionary["main"]["b1"];
		stN.Content = dictionary["main"]["b2"];
		l2.Text = dictionary["main"]["p2l"];
		c1.Content = dictionary["main"]["c1"];
		c2.Content = dictionary["main"]["c2"];
		c3.Content = dictionary["main"]["c3"];
		c4.Content = dictionary["main"]["c4"];
		c5.Content = dictionary["main"]["c5"];
		c6.Content = dictionary["main"]["c6"];
		c7.Content = dictionary["main"]["c7"];
		c8.Content = dictionary["main"]["c8"];
		p1.Header = dictionary["main"]["p3"];
		p2.Header = dictionary["main"]["p4"];
		p3.Header = dictionary["main"]["p5"];
		p4.Header = dictionary["main"]["p6"];
		p1.OffContent = dictionary2["def"]["off"];
		p2.OffContent = dictionary2["def"]["off"];
		p3.OffContent = dictionary2["def"]["off"];
		p4.OffContent = dictionary2["def"]["off"];
		p1.OnContent = dictionary2["def"]["on"];
		p2.OnContent = dictionary2["def"]["on"];
		p3.OnContent = dictionary2["def"]["on"];
		p4.OnContent = dictionary2["def"]["on"];
	}

	private void checkReg()
	{
		newname.Text = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates")?.GetValue("RenameNameTemplate")?.ToString();
		ToggleSwitch toggleSwitch = p1;
		RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\PolicyManager\\current\\device\\Education");
		toggleSwitch.IsOn = registryKey != null && registryKey.GetValue("EnableEduThemes")?.Equals(1) == true;
		ToggleSwitch toggleSwitch2 = p2;
		RegistryKey? registryKey2 = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop\\WindowMetrics");
		int isOn;
		if (registryKey2 == null || registryKey2.GetValue("CaptionHeight")?.Equals(-270) != true)
		{
			RegistryKey? registryKey3 = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop\\WindowMetrics");
			isOn = ((registryKey3 != null && registryKey3.GetValue("CaptionWidth")?.Equals(-270) == true) ? 1 : 0);
		}
		else
		{
			isOn = 1;
		}
		toggleSwitch2.IsOn = (byte)isOn != 0;
		ToggleSwitch toggleSwitch3 = p3;
		RegistryKey? registryKey4 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System");
		toggleSwitch3.IsOn = registryKey4 != null && registryKey4.GetValue("DisableAcrylicBackgroundOnLogon")?.Equals(1) == true;
		ToggleSwitch toggleSwitch4 = p4;
		RegistryKey? registryKey5 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer");
		toggleSwitch4.IsOn = registryKey5 != null && registryKey5.GetValue("AltTabSettings")?.Equals(1) == true;
	}
}
