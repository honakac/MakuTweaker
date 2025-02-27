using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew;

public partial class Other : Page, IComponentConnector
{
	private bool isLoaded;

	private MainWindow mw = (MainWindow)Application.Current.MainWindow;

	public Other()
	{
		InitializeComponent();
		checkReg();
		LoadLang();
		pv.IsEnabled = !Settings.Default.b10;
		isLoaded = true;
	}

	private void t1_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.o1 = t1.IsOn;
			if (t1.IsOn)
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("EnableSmartScreen", 0);
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer").SetValue("SmartScreenEnabled", "Off", RegistryValueKind.String);
			}
			else
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("EnableSmartScreen", 1);
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer").SetValue("SmartScreenEnabled", "Warn", RegistryValueKind.String);
			}
		}
	}

	private void t2_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.o2 = t2.IsOn;
			if (t2.IsOn)
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("EnableLUA", 0);
			}
			else
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("EnableLUA", 1);
			}
		}
	}

	private void t3_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.o3 = t3.IsOn;
			if (t3.IsOn)
			{
				Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\StickyKeys").SetValue("Flags", "506");
				Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\Keyboard Response").SetValue("Flags", "122");
				Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\ToggleKeys").SetValue("Flags", "58");
			}
			else
			{
				Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\StickyKeys").SetValue("Flags", "510");
				Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\Keyboard Response").SetValue("Flags", "126");
				Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\ToggleKeys").SetValue("Flags", "62");
			}
			mw.RebootNotify(1);
		}
	}

	private void t4_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.o4 = t4.IsOn;
			if (t4.IsOn)
			{
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Clipboard").SetValue("EnableClipboardHistory", 1);
			}
			else
			{
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Clipboard").SetValue("EnableClipboardHistory", 0);
			}
		}
	}

	private void t5_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.o5 = t5.IsOn;
			if (t5.IsOn)
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("verbosestatus", 1);
			}
			else
			{
				Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("verbosestatus", 0);
			}
		}
	}

	private void t6_Toggled(object sender, RoutedEventArgs e)
	{
	}

	private void t7_Toggled(object sender, RoutedEventArgs e)
	{
	}

	private void t8_Toggled(object sender, RoutedEventArgs e)
	{
	}

	private void t9_Toggled(object sender, RoutedEventArgs e)
	{
	}

	private void report_Click(object sender, RoutedEventArgs e)
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "oth");
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "HTML (*.html)|*.html";
		saveFileDialog.Title = "Microsoft Battery Report";
		saveFileDialog.FileName = "battery-report.html";
		if (saveFileDialog.ShowDialog() == true)
		{
			string fileName = saveFileDialog.FileName;
			try
			{
				Process.Start("cmd.exe", "/c powercfg /batteryreport /output \"" + fileName + "\"");
				mw.ChSt(dictionary["status"]["o1b"]);
			}
			catch
			{
			}
		}
	}

	private void pv_Click(object sender, RoutedEventArgs e)
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "oth");
		try
		{
			using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey("Applications\\photoviewer.dll\\shell\\open"))
			{
				registryKey.SetValue("MuiVerb", "@photoviewer.dll,-3043");
			}
			using (RegistryKey registryKey2 = Registry.ClassesRoot.CreateSubKey("Applications\\photoviewer.dll\\shell\\open\\command"))
			{
				registryKey2.SetValue("", "%SystemRoot%\\System32\\rundll32.exe \"%ProgramFiles%\\Windows Photo Viewer\\PhotoViewer.dll\", ImageViewer_Fullscreen %1", RegistryValueKind.String);
			}
			using (RegistryKey registryKey3 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows Photo Viewer\\Capabilities\\FileAssociations"))
			{
				registryKey3.SetValue(".bmp", "PhotoViewer.FileAssoc.Tiff");
				registryKey3.SetValue(".gif", "PhotoViewer.FileAssoc.Tiff");
				registryKey3.SetValue(".jpeg", "PhotoViewer.FileAssoc.Tiff");
				registryKey3.SetValue(".jpg", "PhotoViewer.FileAssoc.Tiff");
				registryKey3.SetValue(".png", "PhotoViewer.FileAssoc.Tiff");
			}
			pv.IsEnabled = false;
			Settings.Default.b10 = true;
			mw.ChSt(dictionary["status"]["o2b"]);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ошибка: " + ex.Message);
		}
	}

	private void LoadLang()
	{
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(language, "oth");
		Dictionary<string, Dictionary<string, string>> dictionary2 = MainWindow.Localization.LoadLocalization(language, "base");
		label.Text = dictionary["main"]["label"];
		t1.Header = dictionary["main"]["o1"];
		t2.Header = dictionary["main"]["o2"];
		t3.Header = dictionary["main"]["o3"];
		t4.Header = dictionary["main"]["o4"];
		t5.Header = dictionary["main"]["o5"];
		l1.Text = dictionary["main"]["o10"];
		l2.Text = dictionary["main"]["o11"];
		pv.Content = dictionary["main"]["o11b"];
		report.Content = dictionary["main"]["o10b"];
		t1.OffContent = dictionary2["def"]["off"];
		t2.OffContent = dictionary2["def"]["off"];
		t3.OffContent = dictionary2["def"]["off"];
		t4.OffContent = dictionary2["def"]["off"];
		t5.OffContent = dictionary2["def"]["off"];
		t1.OnContent = dictionary2["def"]["on"];
		t2.OnContent = dictionary2["def"]["on"];
		t3.OnContent = dictionary2["def"]["on"];
		t4.OnContent = dictionary2["def"]["on"];
		t5.OnContent = dictionary2["def"]["on"];
	}

	private void checkReg()
	{
		try
		{
			t1.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", writable: true)?.GetValue("EnableSmartScreen")?.Equals(0) == true || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer", writable: true)?.GetValue("SmartScreenEnabled")?.Equals("Off") == true;
			t2.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", writable: true)?.GetValue("EnableLUA")?.Equals(0) == true;
			t3.IsOn = Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\StickyKeys", writable: true)?.GetValue("Flags")?.Equals("506") == true || Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\ToggleKeys", writable: true)?.GetValue("Flags")?.Equals("58") == true || Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\Keyboard Response", writable: true)?.GetValue("Flags")?.Equals("122") == true;
			t4.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Clipboard", writable: true)?.GetValue("EnableClipboardHistory")?.Equals(1) == true;
			t5.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", writable: true)?.GetValue("verbosestatus")?.Equals(1) == true;
		}
		catch (Exception)
		{
		}
	}
}
