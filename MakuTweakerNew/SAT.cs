using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using MakuTweakerNew.Properties;

namespace MakuTweakerNew;

public partial class SAT : Page, IComponentConnector
{
	private bool isLoaded;

	public SAT()
	{
		InitializeComponent();
		LoadLang();
		satstart.IsOn = Settings.Default.satstart;
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "sat");
		int.TryParse(mins.Text, out var result);
		hours.Text = dictionary["main"]["minho"] + Math.Round((double)result / 60.0, 2);
		isLoaded = true;
	}

	private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		e.Handled = !e.Text.All(char.IsDigit);
	}

	private void time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
	{
		mins.Text = Math.Round(time.Value * 5.0).ToString();
	}

	private void mins_TextChanged(object sender, TextChangedEventArgs e)
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "sat");
		int.TryParse(mins.Text, out var result);
		hours.Text = dictionary["main"]["minho"] + Math.Round((double)result / 60.0, 2);
	}

	private void mins_GotFocus(object sender, RoutedEventArgs e)
	{
		mins.Dispatcher.InvokeAsync(delegate
		{
			mins.SelectAll();
		}, DispatcherPriority.Input);
	}

	private void satstart_Toggled(object sender, RoutedEventArgs e)
	{
		if (isLoaded)
		{
			Settings.Default.satstart = satstart.IsOn;
		}
	}

	private void tenM_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 600");
	}

	private void threeM_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 1800");
	}

	private void oneH_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 3600");
	}

	private void twoH_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 7200");
	}

	private void fourH_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 14400");
	}

	private void sixH_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 21600");
	}

	private void shut_Click(object sender, RoutedEventArgs e)
	{
		double num = Convert.ToDouble(mins.Text);
		double num2 = Convert.ToDouble(60);
		Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t " + Convert.ToString(num * num2));
	}

	private void cancel_Click(object sender, RoutedEventArgs e)
	{
		Process.Start("C:\\Windows\\System32\\shutdown.exe", " -a");
	}

	private void LoadLang()
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "sat");
		label.Text = dictionary["main"]["label"];
		sat.Text = dictionary["main"]["info"];
		hours.Text = dictionary["main"]["minho"];
		os.Text = dictionary["main"]["os"];
		oned.Text = dictionary["main"]["oned"];
		tenM.Content = dictionary["main"]["tenM"];
		threeM.Content = dictionary["main"]["threeM"];
		oneH.Content = dictionary["main"]["oneH"];
		twoH.Content = dictionary["main"]["twoH"];
		fourH.Content = dictionary["main"]["fourH"];
		sixH.Content = dictionary["main"]["sixH"];
		shut.Content = dictionary["main"]["b1"];
		cancel.Content = dictionary["main"]["b2"];
		satstart.OnContent = dictionary["main"]["set"];
		satstart.OffContent = dictionary["main"]["set"];
	}
}
