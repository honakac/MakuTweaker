using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew;

public partial class StartMenuAndTaskbar : Page, IComponentConnector
{
	private bool isLoaded;

	private MainWindow mw = (MainWindow)Application.Current.MainWindow;

	public StartMenuAndTaskbar()
	{
		InitializeComponent();
		checkReg();
		LoadLang(Settings.Default.lang);
		isLoaded = true;
	}

	private void st1_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.st1 = st1.IsOn;
		if (st1.IsOn)
		{
			try
			{
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("ShowTaskViewButton", 0);
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarDa", 0);
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarMn", 0);
				return;
			}
			catch
			{
				return;
			}
		}
		try
		{
			Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("ShowTaskViewButton", 1);
			Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarDa", 1);
			Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarMn", 1);
		}
		catch
		{
		}
	}

	private void st2_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.st2 = st2.IsOn;
		if (st2.IsOn)
		{
			try
			{
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Taskband").SetValue("MinThumbSizePX", 500);
			}
			catch
			{
			}
		}
		else
		{
			try
			{
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Taskband").SetValue("MinThumbSizePX", 170);
			}
			catch
			{
			}
		}
		mw.RebootNotify(2);
	}

	private void st3_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.st3 = st3.IsOn;
		if (st3.IsOn)
		{
			try
			{
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0);
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("ExtendedUIHoverTime", 10);
			}
			catch
			{
			}
		}
		else
		{
			try
			{
				Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("ExtendedUIHoverTime", 750);
			}
			catch
			{
			}
		}
		mw.RebootNotify(2);
	}

	private void st4_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.st4 = st4.IsOn;
		if (st4.IsOn)
		{
			try
			{
				Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\SearchSettings").SetValue("IsDynamicSearchBoxEnabled", 0);
				return;
			}
			catch
			{
				return;
			}
		}
		try
		{
			Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\SearchSettings").SetValue("IsDynamicSearchBoxEnabled", 1);
		}
		catch
		{
		}
	}

	private void st5_Toggled(object sender, RoutedEventArgs e)
	{
		if (!isLoaded)
		{
			return;
		}
		Settings.Default.st5 = st5.IsOn;
		if (st5.IsOn)
		{
			try
			{
				Registry.CurrentUser.CreateSubKey("Software\\Policies\\Microsoft\\Windows\\Explorer").SetValue("DisableSearchBoxSuggestions", 1);
				return;
			}
			catch
			{
				return;
			}
		}
		try
		{
			Registry.CurrentUser.CreateSubKey("Software\\Policies\\Microsoft\\Windows\\Explorer").SetValue("DisableSearchBoxSuggestions", 0);
		}
		catch
		{
		}
	}

	private void LoadLang(string lang)
	{
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(language, "base");
		Dictionary<string, Dictionary<string, string>> dictionary2 = MainWindow.Localization.LoadLocalization(language, "stask");
		label.Text = dictionary2["main"]["label"];
		st1.Header = dictionary2["main"]["st1"];
		st2.Header = dictionary2["main"]["st2"];
		st3.Header = dictionary2["main"]["st3"];
		st4.Header = dictionary2["main"]["st4"];
		st5.Header = dictionary2["main"]["st5"];
		st1.OnContent = dictionary["def"]["on"];
		st2.OnContent = dictionary["def"]["on"];
		st3.OnContent = dictionary["def"]["on"];
		st4.OnContent = dictionary["def"]["on"];
		st5.OnContent = dictionary["def"]["on"];
		st1.OffContent = dictionary["def"]["off"];
		st2.OffContent = dictionary["def"]["off"];
		st3.OffContent = dictionary["def"]["off"];
		st4.OffContent = dictionary["def"]["off"];
		st5.OffContent = dictionary["def"]["off"];
	}

	private void checkReg()
	{
		st1.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", writable: true)?.GetValue("ShowTaskViewButton")?.Equals(0) == true;
		st2.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Taskband", writable: true)?.GetValue("MinThumbSizePX")?.Equals(500) == true;
		st3.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel", writable: true)?.GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}")?.Equals(0) == true;
		st4.IsOn = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\SearchSettings", writable: true)?.GetValue("IsDynamicSearchBoxEnabled")?.Equals(0) == true;
		st5.IsOn = Registry.CurrentUser.OpenSubKey("Software\\Policies\\Microsoft\\Windows\\Explorer", writable: true)?.GetValue("DisableSearchBoxSuggestions")?.Equals(1) == true;
	}
}
