using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using MakuTweakerNew.Properties;
using MicaWPF.Controls;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Helpers;
using MicaWPF.Core.Services;
using ModernWpf.Media.Animation;
using Newtonsoft.Json;

namespace MakuTweakerNew;

public partial class MainWindow : MicaWindow, IComponentConnector
{
	public static class Localization
	{
		public static Dictionary<string, Dictionary<string, string>> LoadLocalization(string language, string category)
		{
			string text = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "loc"), language + ".json");
			if (!File.Exists(text))
			{
				Settings.Default.lang = "en";
				throw new FileNotFoundException("Cannot find a " + text + " localization file.\nPlease reinstall MakuTweaker.\nLanguage has been changed to English.");
			}
			Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> dictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>>(File.ReadAllText(text));
			if (dictionary.ContainsKey("categories"))
			{
				Dictionary<string, Dictionary<string, Dictionary<string, string>>> dictionary2 = dictionary["categories"];
				if (dictionary2.ContainsKey(category))
				{
					return dictionary2[category];
				}
			}
			Settings.Default.lang = "en";
			throw new KeyNotFoundException($"Cannot find a \"{category}\" category in the {text} localization file.\nPlease reinstall MakuTweaker.\nLanguage has been changed to English.");
		}
	}

	private NavigationTransitionInfo _transitionInfo;

	private DispatcherTimer ExpRestart;

	private bool isAnimating;

	public MainWindow()
	{
		InitializeComponent();
		using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MakuTweakerNew.BuildLab.txt"))
		{
			using StreamReader streamReader = new StreamReader(stream);
			streamReader.ReadToEnd();
		}
		if (MicaWPFServiceUtility.ThemeService.CurrentTheme.ToString() == "Light")
		{
			base.Dispatcher.Invoke(delegate
			{
				Separator.Stroke = new SolidColorBrush(Colors.Black);
			});
		}
		else
		{
			base.Dispatcher.Invoke(delegate
			{
				Separator.Stroke = new SolidColorBrush(Colors.White);
			});
		}
		WindowsTheme currentTheme = WindowsThemeHelper.GetCurrentWindowsTheme();
		base.Dispatcher.Invoke(delegate
		{
			if (currentTheme.ToString() == "Light")
			{
				Separator.Stroke = new SolidColorBrush(Colors.Black);
				base.Foreground = new SolidColorBrush(Colors.Black);
				MainFrame.Foreground = base.Foreground;
			}
			else
			{
				Separator.Stroke = new SolidColorBrush(Colors.White);
				base.Foreground = new SolidColorBrush(Colors.White);
				MainFrame.Foreground = base.Foreground;
			}
		});
		MicaWPFServiceUtility.ThemeService.ThemeChanged.Subscribe(delegate(WindowsTheme windowsTheme)
		{
			string themeString = windowsTheme.ToString();
			base.Dispatcher.Invoke(delegate
			{
				if (themeString == "Light")
				{
					Separator.Stroke = new SolidColorBrush(Colors.Black);
					base.Foreground = new SolidColorBrush(Colors.Black);
					MainFrame.Foreground = base.Foreground;
				}
				else
				{
					Separator.Stroke = new SolidColorBrush(Colors.White);
					base.Foreground = new SolidColorBrush(Colors.White);
					MainFrame.Foreground = base.Foreground;
				}
			});
		});
		ExpTimer();
		if (Settings.Default.firRun)
		{
			string name = CultureInfo.CurrentCulture.Name;
			if (name == null)
			{
				goto IL_02c1;
			}
			if (name.StartsWith("uk-"))
			{
				Settings.Default.lang = "ua";
				Settings.Default.langSI = 2;
			}
			else if (name.StartsWith("ru-"))
			{
				Settings.Default.lang = "ru";
				Settings.Default.langSI = 1;
			}
			else if (name.StartsWith("en-"))
			{
				Settings.Default.lang = "en";
				Settings.Default.langSI = 0;
			}
			else if (name.StartsWith("es-"))
			{
				Settings.Default.lang = "es";
				Settings.Default.langSI = 3;
			}
			else if (name.StartsWith("pt-"))
			{
				Settings.Default.lang = "pt";
				Settings.Default.langSI = 4;
			}
			else if (name.StartsWith("de-"))
			{
				Settings.Default.lang = "de";
				Settings.Default.langSI = 5;
			}
			else if (name.StartsWith("kk-"))
			{
				Settings.Default.lang = "kz";
				Settings.Default.langSI = 6;
			}
			else if (name.StartsWith("ja-"))
			{
				Settings.Default.lang = "jp";
				Settings.Default.langSI = 7;
			}
			else if (name.StartsWith("zh-"))
			{
				Settings.Default.lang = "cn";
				Settings.Default.langSI = 8;
			}
			else
			{
				if (!name.StartsWith("hi-"))
				{
					goto IL_02c1;
				}
				Settings.Default.lang = "hi";
				Settings.Default.langSI = 9;
			}
			goto IL_02db;
		}
		goto IL_02f0;
		IL_02c1:
		Settings.Default.lang = "en";
		Settings.Default.langSI = 0;
		goto IL_02db;
		IL_02f0:
		LoadLang(Settings.Default.lang);
		CheckForUpd();
		return;
		IL_02db:
		Settings.Default.firRun = false;
		Settings.Default.Save();
		goto IL_02f0;
	}

	private void ExpTimer()
	{
		ExpRestart = new DispatcherTimer();
		ExpRestart.Interval = TimeSpan.FromMilliseconds(1000.0);
		ExpRestart.Tick += ExpRestart_Tick;
	}

	private void MicaWindow_Closing(object sender, CancelEventArgs e)
	{
		Settings.Default.b1 = false;
		Settings.Default.b2 = false;
		Settings.Default.b3 = false;
		Settings.Default.b4 = false;
		Settings.Default.b5 = false;
		Settings.Default.b6 = false;
		Settings.Default.b7 = false;
		Settings.Default.b8 = false;
		Settings.Default.b9 = false;
		Settings.Default.b10 = false;
		Settings.Default.b11 = false;
		Settings.Default.Save();
	}

	private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		_transitionInfo = new EntranceNavigationTransitionInfo();
		switch (Category.SelectedIndex)
		{
		case 0:
			MainFrame.Navigate(typeof(Explorer), null, _transitionInfo);
			break;
		case 1:
			MainFrame.Navigate(typeof(StartMenuAndTaskbar), null, _transitionInfo);
			break;
		case 2:
			MainFrame.Navigate(typeof(WindowsUpdate), null, _transitionInfo);
			break;
		case 3:
			MainFrame.Navigate(typeof(UWP), null, _transitionInfo);
			break;
		case 4:
			MainFrame.Navigate(typeof(SAT), null, _transitionInfo);
			break;
		case 5:
			MainFrame.Navigate(typeof(QuickSet), null, _transitionInfo);
			break;
		case 6:
			MainFrame.Navigate(typeof(Act), null, _transitionInfo);
			break;
		case 7:
			MainFrame.Navigate(typeof(ContextMenu), null, _transitionInfo);
			break;
		case 8:
			MainFrame.Navigate(typeof(SysAndRec), null, _transitionInfo);
			break;
		case 9:
			try
			{
				MainFrame.Navigate(typeof(Other), null, _transitionInfo);
				break;
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message, "MakuTW Debug", MessageBoxButton.OK, MessageBoxImage.Hand);
				break;
			}
		case 10:
			MainFrame.Navigate(typeof(Personalization), null, _transitionInfo);
			break;
		case 11:
			MainFrame.Navigate(typeof(Telemetry), null, _transitionInfo);
			break;
		}
	}

	private void MicaWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
	{
	}

	private void MicaWindow_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
	{
	}

	private void MicaWindow_Loaded(object sender, RoutedEventArgs e)
	{
		if (Settings.Default.satstart)
		{
			Category.SelectedIndex = 4;
		}
		else
		{
			Category.SelectedIndex = 0;
		}
	}

	public async void ChSt(string st)
	{
		if (isAnimating)
		{
			return;
		}
		try
		{
			isAnimating = true;
			AnimY(status, 300.0, 26.0, 0.0);
			status.Text = st;
			await Task.Delay(5000);
			AnimY(status, 300.0, 0.0, 26.0);
		}
		finally
		{
			isAnimating = false;
		}
	}

	public void LoadLang(string lang)
	{
		try
		{
			Dictionary<string, Dictionary<string, string>> dictionary = Localization.LoadLocalization(Settings.Default.lang ?? "en", "base");
			c1.Content = dictionary["catname"]["expl"];
			c2.Content = dictionary["catname"]["stask"];
			c3.Content = dictionary["catname"]["wu"];
			c4.Content = dictionary["catname"]["uwp"];
			c5.Content = dictionary["catname"]["sat"];
			c6.Content = dictionary["catname"]["quick"];
			c7.Content = dictionary["catname"]["act"];
			c8.Content = dictionary["catname"]["cm"];
			c9.Content = dictionary["catname"]["sr"];
			c10.Content = dictionary["catname"]["oth"];
			c11.Content = dictionary["catname"]["per"];
			c12.Content = dictionary["catname"]["tel"];
			rexplorer.Label = dictionary["lowtabs"]["rexp"];
			settingsButton.Label = dictionary["lowtabs"]["set"];
		}
		catch (Exception ex)
		{
			System.Windows.MessageBox.Show(ex.Message, "MakuTweaker Error", MessageBoxButton.OK, MessageBoxImage.Hand);
			System.Windows.Forms.Application.Restart();
			System.Windows.Application.Current.Shutdown();
		}
	}

	private void AnimY(UIElement element, double durationMilliseconds, double from, double to)
	{
		if (element.RenderTransform != null && element.RenderTransform is TranslateTransform translateTransform)
		{
			_ = translateTransform.Y;
		}
		else if (element.RenderTransform != null && element.RenderTransform is MatrixTransform { Matrix: var matrix })
		{
			_ = matrix.OffsetY;
		}
		DoubleAnimation animation = new DoubleAnimation
		{
			From = from,
			To = to,
			Duration = TimeSpan.FromMilliseconds(durationMilliseconds),
			EasingFunction = new QuinticEase
			{
				EasingMode = EasingMode.EaseOut
			}
		};
		if (element.RenderTransform == null || !(element.RenderTransform is TranslateTransform))
		{
			element.RenderTransform = new TranslateTransform();
		}
		((TranslateTransform)element.RenderTransform).BeginAnimation(TranslateTransform.YProperty, animation);
	}

	public void RebootNotify(int mode)
	{
		string message = string.Empty;
		Dictionary<string, Dictionary<string, string>> dictionary = Localization.LoadLocalization(Settings.Default.lang ?? "en", "base");
		Icon icon = new Icon(GetResourceStream("MakuTweakerNew.MakuT.ico"));
		TaskbarIcon _trayIcon = new TaskbarIcon
		{
			ToolTipText = "MakuTweaker",
			Icon = icon
		};
		switch (mode)
		{
		case 1:
			message = dictionary["def"]["rebnotify"];
			break;
		case 2:
			message = dictionary["def"]["rebnotifyexplorer"];
			break;
		case 3:
			message = dictionary["def"]["rebnotifysfc"];
			break;
		}
		_trayIcon.ShowBalloonTip("MakuTweaker", message, BalloonIcon.Warning);
		Task.Delay(8000).ContinueWith(delegate
		{
			_trayIcon.Dispatcher.Invoke(delegate
			{
				_trayIcon.Dispose();
			});
		});
	}

	private Stream GetResourceStream(string resourceName)
	{
		return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName) ?? throw new FileNotFoundException("Ресурс " + resourceName + " не найден.");
	}

	private void settingsButton_Click(object sender, RoutedEventArgs e)
	{
		Category.SelectedIndex = -1;
		MainFrame.Navigate(typeof(SettingsAbout), null, _transitionInfo);
	}

	private void MainFrame_Navigated(object sender, NavigationEventArgs e)
	{
		if (Category.SelectedIndex == -1)
		{
			settingsButton.IsEnabled = false;
		}
		else
		{
			settingsButton.IsEnabled = true;
		}
	}

	public void expk()
	{
		Process process = new Process();
		process.StartInfo = new ProcessStartInfo
		{
			FileName = "taskkill",
			Arguments = "/F /IM explorer.exe"
		};
		process.Start();
	}

	private void ExpRestart_Tick(object sender, EventArgs e)
	{
		Process.Start("explorer.exe");
		ExpRestart.Stop();
	}

	private void rexplorer_Click(object sender, RoutedEventArgs e)
	{
		expk();
		ExpRestart.Start();
	}

	private async Task CheckForUpd()
	{
		int ThisBuild = int.Parse(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("MakuTweakerNew.BuildNumber.txt")).ReadToEnd().Trim());
		string requestUri = "https://raw.githubusercontent.com/AdderlyMark/MakuTweaker/refs/heads/main/ver.json";
		using HttpClient client = new HttpClient();
		_ = 1;
		try
		{
			HttpResponseMessage obj = await client.GetAsync(requestUri);
			obj.EnsureSuccessStatusCode();
			string value = await obj.Content.ReadAsStringAsync();
			try
			{
				Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
				if (!dictionary.ContainsKey("build") || int.Parse(dictionary["build"]) <= ThisBuild)
				{
					return;
				}
				Icon icon = new Icon(GetResourceStream("MakuTweakerNew.MakuT.ico"));
				TaskbarIcon _trayIcon = new TaskbarIcon
				{
					ToolTipText = "MakuTweaker",
					Icon = icon
				};
				if (Settings.Default.lang == "ru" || Settings.Default.lang == "ua" || Settings.Default.lang == "kz")
				{
					_trayIcon.ShowBalloonTip("MakuTweaker", "Доступно обновление MakuTweaker!\nНажмите на уведомление, чтобы перейти на страницу загрузки.", BalloonIcon.Info);
				}
				else
				{
					_trayIcon.ShowBalloonTip("MakuTweaker", "An update for MakuTweaker is available!\nClick the notification to go to the download page.", BalloonIcon.Info);
				}
				_trayIcon.TrayBalloonTipClicked += delegate
				{
					Process.Start(new ProcessStartInfo("https://adderly.top/soft")
					{
						UseShellExecute = true
					});
				};
				Task.Delay(8000).ContinueWith(delegate
				{
					_trayIcon.Dispatcher.Invoke(delegate
					{
						_trayIcon.Dispose();
					});
				});
			}
			catch (JsonException)
			{
			}
		}
		catch (HttpRequestException)
		{
		}
	}
}
