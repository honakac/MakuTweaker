using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MakuTweakerNew.Properties;
using ModernWpf.Controls;

namespace MakuTweakerNew;

public partial class UWP : System.Windows.Controls.Page, IComponentConnector
{
	private bool isSkipped;

	private CancellationTokenSource cancellationTokenSource;

	private MainWindow mw = (MainWindow)Application.Current.MainWindow;

	private int mode;

	public UWP()
	{
		InitializeComponent();
		LoadLang();
		CheckInstalledUWPAppsAsync(anim: true);
	}

	private async void CheckInstalledUWPAppsAsync(bool anim)
	{
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> uwp = MainWindow.Localization.LoadLocalization(language, "uwp");
		t.Text = uwp["main"]["chk"];
		string[] appIds = new string[26]
		{
			"Microsoft.ZuneVideo", "Microsoft.ZuneMusic", "Microsoft.MicrosoftStickyNotes", "Microsoft.MixedReality.Portal", "Microsoft.MicrosoftSolitaireCollection", "Microsoft.Messaging", "Microsoft.WindowsFeedbackHub", "microsoft.windowscommunicationsapps", "Microsoft.BingNews", "Microsoft.Microsoft3DViewer",
			"Microsoft.BingWeather", "Microsoft.549981C3F5F10", "Microsoft.XboxApp", "Microsoft.GetHelp", "Microsoft.WindowsCamera", "Microsoft.WindowsMaps", "Microsoft.Office.OneNote", "Microsoft.YourPhone", "Microsoft.Windows.DevHome", "Clipchamp.Clipchamp",
			"Microsoft.PowerAutomateDesktop", "Microsoft.Getstarted", "Microsoft.WindowsSoundRecorder", "Microsoft.WindowsStore", "Microsoft.People", "Microsoft.SkypeApp"
		};
		cancellationTokenSource = new CancellationTokenSource();
		CancellationToken token = cancellationTokenSource.Token;
		bool flag = false;
		while (!isSkipped && !flag)
		{
			string[] array = appIds;
			foreach (string appId in array)
			{
				if (token.IsCancellationRequested)
				{
					t.Text = uwp["main"]["skipped"];
					foreach (ToggleSwitch item in new List<ToggleSwitch>
					{
						u1, u2, u3, u4, u5, u6, u7, u8, u9, u10,
						u11, u12, u13, u14, u15, u16, u17, u18, u19, u20,
						u21, u22, u23, u24, u25, u26
					})
					{
						item.IsEnabled = true;
					}
					foreach (ToggleSwitch item2 in new List<ToggleSwitch>
					{
						u1, u2, u3, u4, u5, u6, u7, u8, u10, u11,
						u12, u13, u14, u21, u25, u26, u20, u18, u19, u22
					})
					{
						item2.IsOn = true;
					}
					if (anim)
					{
						FadeInBloat();
						FadeOutAll1();
					}
					b.IsEnabled = true;
					return;
				}
				bool flag2 = await Task.Run(() => IsAppInstalled(appId), token);
				switch (appId)
				{
				case "Microsoft.MixedReality.Portal":
					u1.IsEnabled = flag2;
					u1.IsOn = flag2;
					break;
				case "Microsoft.MicrosoftSolitaireCollection":
					u2.IsEnabled = flag2;
					u2.IsOn = flag2;
					break;
				case "Microsoft.Messaging":
					u3.IsEnabled = flag2;
					u3.IsOn = flag2;
					break;
				case "Microsoft.549981C3F5F10":
					u4.IsEnabled = flag2;
					u4.IsOn = flag2;
					break;
				case "Microsoft.GetHelp":
					u5.IsEnabled = flag2;
					u5.IsOn = flag2;
					break;
				case "Microsoft.WindowsFeedbackHub":
					u6.IsEnabled = flag2;
					u6.IsOn = flag2;
					break;
				case "Microsoft.Windows.DevHome":
					u7.IsEnabled = flag2;
					u7.IsOn = flag2;
					break;
				case "Microsoft.3DViewer":
					u8.IsEnabled = flag2;
					u8.IsOn = flag2;
					break;
				case "Microsoft.YourPhone":
					u9.IsEnabled = flag2;
					break;
				case "Microsoft.WindowsMaps":
					u10.IsEnabled = flag2;
					u10.IsOn = flag2;
					break;
				case "Microsoft.PowerAutomateDesktop":
					u11.IsEnabled = flag2;
					u11.IsOn = flag2;
					break;
				case "Clipchamp.Clipchamp":
					u12.IsEnabled = flag2;
					u12.IsOn = flag2;
					break;
				case "microsoft.windowscommunicationsapps":
					u13.IsEnabled = flag2;
					u13.IsOn = flag2;
					break;
				case "Microsoft.Office.OneNote":
					u14.IsEnabled = flag2;
					u14.IsOn = flag2;
					break;
				case "Microsoft.ZuneMusic":
					u15.IsEnabled = flag2;
					break;
				case "Microsoft.ZuneVideo":
					u16.IsEnabled = flag2;
					break;
				case "Microsoft.WindowsCamera":
					u17.IsEnabled = flag2;
					break;
				case "Microsoft.BingNews":
					u18.IsEnabled = flag2;
					u18.IsOn = flag2;
					break;
				case "Microsoft.BingWeather":
					u19.IsEnabled = flag2;
					u19.IsOn = flag2;
					break;
				case "Microsoft.MicrosoftStickyNotes":
					u20.IsEnabled = flag2;
					u20.IsOn = flag2;
					break;
				case "Microsoft.Getstarted":
					u21.IsEnabled = flag2;
					u21.IsOn = flag2;
					break;
				case "Microsoft.WindowsSoundRecorder":
					u22.IsEnabled = flag2;
					u22.IsOn = flag2;
					break;
				case "Microsoft.People":
					u25.IsEnabled = flag2;
					u25.IsOn = flag2;
					break;
				case "Microsoft.SkypeApp":
					u26.IsEnabled = flag2;
					u26.IsOn = flag2;
					break;
				}
				t.Text = (flag2 ? (uwp["main"]["chk"] + appId + " " + uwp["main"]["is"]) : (uwp["main"]["chk"] + appId + " " + uwp["main"]["isnt"]));
				p.Value++;
				u23.IsEnabled = true;
				u24.IsEnabled = true;
			}
			flag = true;
			t.Text = uwp["main"]["comp"];
			if (anim)
			{
				FadeInBloat();
				FadeOutAll1();
			}
			b.IsEnabled = true;
		}
	}

	private bool IsAppInstalled(string appId)
	{
		try
		{
			return GetInstalledUWPApps().Contains(appId);
		}
		catch (Exception ex)
		{
			System.Windows.MessageBox.Show("Error checking app " + appId + ": " + ex.Message, "App Check Error", MessageBoxButton.OK, MessageBoxImage.Hand);
			return false;
		}
	}

	private List<string> GetInstalledUWPApps()
	{
		List<string> result = new List<string>();
		try
		{
			string text = "\r\n                    Get-AppxPackage | Select-Object -ExpandProperty Name\r\n                ";
			using Process process = Process.Start(new ProcessStartInfo
			{
				FileName = "powershell.exe",
				Arguments = "-Command \"" + text + "\"",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			});
			using StreamReader streamReader = process.StandardOutput;
			string text2 = streamReader.ReadToEnd();
			if (string.IsNullOrEmpty(text2))
			{
				System.Windows.MessageBox.Show("No output received from PowerShell.");
			}
			else
			{
				result = text2.Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			}
		}
		catch (Exception ex)
		{
			System.Windows.MessageBox.Show("Error executing PowerShell: " + ex.Message, "PowerShell Error", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
		return result;
	}

	private void FadeIn(UIElement element, double durationSeconds)
	{
		DoubleAnimation animation = new DoubleAnimation
		{
			From = 0.0,
			To = 1.0,
			Duration = TimeSpan.FromMilliseconds(durationSeconds),
			EasingFunction = new QuadraticEase
			{
				EasingMode = EasingMode.EaseIn
			}
		};
		element.BeginAnimation(UIElement.OpacityProperty, animation);
	}

	private void FadeOut(UIElement element, double durationSeconds)
	{
		DoubleAnimation animation = new DoubleAnimation
		{
			From = 1.0,
			To = 0.0,
			Duration = TimeSpan.FromMilliseconds(durationSeconds),
			EasingFunction = new QuadraticEase
			{
				EasingMode = EasingMode.EaseOut
			}
		};
		element.BeginAnimation(UIElement.OpacityProperty, animation);
	}

	private void FadeInAll1()
	{
		FadeIn(u1, 300.0);
		FadeIn(u2, 300.0);
		FadeIn(u3, 300.0);
		FadeIn(u4, 300.0);
		FadeIn(u5, 300.0);
		FadeIn(u6, 300.0);
		FadeIn(u7, 300.0);
		FadeIn(u8, 300.0);
		FadeIn(u9, 300.0);
		FadeIn(u10, 300.0);
		FadeIn(u11, 300.0);
		FadeIn(u12, 300.0);
		FadeIn(u13, 300.0);
		FadeIn(u14, 300.0);
		FadeIn(u15, 300.0);
		FadeIn(u16, 300.0);
		FadeIn(u17, 300.0);
		FadeIn(u18, 300.0);
		FadeIn(u19, 300.0);
		FadeIn(u20, 300.0);
		FadeIn(u21, 300.0);
		FadeIn(u22, 300.0);
		FadeIn(u23, 300.0);
		FadeIn(u24, 300.0);
		FadeIn(u25, 300.0);
		FadeIn(u26, 300.0);
		FadeIn(b, 300.0);
		FadeIn(view, 300.0);
	}

	private async void FadeOutAll1()
	{
		FadeOut(p, 300.0);
		FadeOut(t, 300.0);
		FadeOut(skip, 300.0);
		skip.IsEnabled = false;
		await Task.Delay(300);
		skip.Visibility = Visibility.Collapsed;
	}

	private void FadeInAll2()
	{
		FadeIn(p, 300.0);
		FadeIn(t, 300.0);
	}

	private void FadeOutAll2()
	{
		FadeOut(u1, 300.0);
		FadeOut(u2, 300.0);
		FadeOut(u3, 300.0);
		FadeOut(u4, 300.0);
		FadeOut(u5, 300.0);
		FadeOut(u6, 300.0);
		FadeOut(u7, 300.0);
		FadeOut(u8, 300.0);
		FadeOut(u9, 300.0);
		FadeOut(u10, 300.0);
		FadeOut(u11, 300.0);
		FadeOut(u12, 300.0);
		FadeOut(u13, 300.0);
		FadeOut(u14, 300.0);
		FadeOut(u15, 300.0);
		FadeOut(u16, 300.0);
		FadeOut(u17, 300.0);
		FadeOut(u18, 300.0);
		FadeOut(u19, 300.0);
		FadeOut(u20, 300.0);
		FadeOut(u21, 300.0);
		FadeOut(u22, 300.0);
		FadeOut(u23, 300.0);
		FadeOut(u24, 300.0);
		FadeOut(u25, 300.0);
		FadeOut(u26, 300.0);
		FadeOut(b, 300.0);
		FadeOut(view, 300.0);
		u1.IsEnabled = false;
		u2.IsEnabled = false;
		u3.IsEnabled = false;
		u4.IsEnabled = false;
		u5.IsEnabled = false;
		u6.IsEnabled = false;
		u7.IsEnabled = false;
		u8.IsEnabled = false;
		u9.IsEnabled = false;
		u10.IsEnabled = false;
		u11.IsEnabled = false;
		u12.IsEnabled = false;
		u13.IsEnabled = false;
		u14.IsEnabled = false;
		u15.IsEnabled = false;
		u16.IsEnabled = false;
		u17.IsEnabled = false;
		u18.IsEnabled = false;
		u19.IsEnabled = false;
		u20.IsEnabled = false;
		u21.IsEnabled = false;
		u22.IsEnabled = false;
		u23.IsEnabled = false;
		u24.IsEnabled = false;
		u25.IsEnabled = false;
		u26.IsEnabled = false;
		b.IsEnabled = false;
		view.IsEnabled = false;
	}

	private async void b_Click(object sender, RoutedEventArgs e)
	{
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> uwp = MainWindow.Localization.LoadLocalization(language, "uwp");
		int count = 0;
		ToggleSwitch[] array = new ToggleSwitch[26]
		{
			u1, u2, u3, u4, u5, u6, u7, u8, u9, u10,
			u11, u12, u13, u14, u15, u16, u17, u18, u19, u20,
			u21, u22, u23, u24, u25, u26
		};
		foreach (ToggleSwitch toggleSwitch in array)
		{
			if (!toggleSwitch.IsOn)
			{
				continue;
			}
			if (toggleSwitch == u8)
			{
				count += 3;
			}
			else if (toggleSwitch == u23)
			{
				ILOVEMAKUTWEAKERDialog dialog = new ILOVEMAKUTWEAKERDialog("Microsoft Store");
				await dialog.ShowAsync();
				if (await dialog.TaskCompletionSource.Task == 0)
				{
					return;
				}
				count++;
			}
			else if (toggleSwitch == u24)
			{
				ILOVEMAKUTWEAKERDialog dialog = new ILOVEMAKUTWEAKERDialog("Xbox");
				await dialog.ShowAsync();
				if (await dialog.TaskCompletionSource.Task == 0)
				{
					return;
				}
				count += 5;
			}
			else
			{
				count = ((toggleSwitch != u25) ? (count + 1) : (count + 2));
			}
		}
		if (count > 0)
		{
			t.Text = $"{uwp["status"]["started"]} 0/{count}";
			mw.Category.IsEnabled = false;
			mw.ABCB.IsEnabled = false;
			p.Maximum = count;
			p.Value = 0.0;
			FadeInAll2();
			switch (mode)
			{
			case 0:
				FadeOutBloat();
				break;
			case 1:
				FadeOutBloat();
				FadeOutPopular();
				break;
			case 2:
				FadeInAll1();
				FadeOutAll2();
				break;
			}
			await Task.Delay(300);
			b.Visibility = Visibility.Collapsed;
			(ToggleSwitch, string)[] array2 = new(ToggleSwitch, string)[33]
			{
				(u1, "Microsoft.MixedReality.Portal"),
				(u2, "Microsoft.MicrosoftSolitaireCollection"),
				(u3, "Microsoft.Messaging"),
				(u4, "Microsoft.549981C3F5F10"),
				(u5, "Microsoft.GetHelp"),
				(u6, "Microsoft.WindowsFeedbackHub"),
				(u7, "Microsoft.Windows.DevHome"),
				(u8, "Microsoft.MSPaint"),
				(u8, "Microsoft.3DBuilder"),
				(u8, "Microsoft.Microsoft3DViewer"),
				(u9, "Microsoft.YourPhone"),
				(u10, "Microsoft.WindowsMaps"),
				(u11, "Microsoft.PowerAutomateDesktop"),
				(u12, "Clipchamp.Clipchamp"),
				(u13, "microsoft.windowscommunicationsapps"),
				(u14, "Microsoft.Office.OneNote"),
				(u15, "Microsoft.ZuneMusic"),
				(u16, "Microsoft.ZuneVideo"),
				(u17, "Microsoft.WindowsCamera"),
				(u18, "Microsoft.BingNews"),
				(u19, "Microsoft.BingWeather"),
				(u20, "Microsoft.MicrosoftStickyNotes"),
				(u21, "Microsoft.Getstarted"),
				(u22, "Microsoft.WindowsSoundRecorder"),
				(u23, "Microsoft.WindowsStore"),
				(u24, "Microsoft.XboxApp"),
				(u24, "Microsoft.GamingApp"),
				(u24, "Microsoft.Xbox.TCUI"),
				(u24, "Microsoft.XboxSpeechToTextOverlay"),
				(u24, "Microsoft.XboxGameCallableUI"),
				(u25, "Microsoft.People"),
				(u25, "Microsoft.Windows.PeopleExperienceHost"),
				(u26, "Microsoft.SkypeApp")
			};
			(ToggleSwitch toggle, string packageName)[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				var (toggleSwitch2, packageName) = array3[i];
				if (toggleSwitch2.IsOn)
				{
					await RemovePackageAsync(packageName);
					p.Value++;
					t.Text = $"{uwp["status"]["started"]} {p.Value}/{count}";
				}
			}
			p.Value = 0.0;
			p.Maximum = 26.0;
			isSkipped = false;
			view.SelectedIndex = 0;
			FadeInBloat();
			FadeOutAll1();
			SystemSounds.Asterisk.Play();
			mw.ChSt(uwp["status"]["complete"]);
			mw.Category.IsEnabled = true;
			mw.ABCB.IsEnabled = true;
			u1.IsOn = false;
			u2.IsOn = false;
			u3.IsOn = false;
			u4.IsOn = false;
			u5.IsOn = false;
			u6.IsOn = false;
			u7.IsOn = false;
			u8.IsOn = false;
			u9.IsOn = false;
			u10.IsOn = false;
			u11.IsOn = false;
			u12.IsOn = false;
			u13.IsOn = false;
			u14.IsOn = false;
			u15.IsOn = false;
			u16.IsOn = false;
			u17.IsOn = false;
			u18.IsOn = false;
			u19.IsOn = false;
			u20.IsOn = false;
			u21.IsOn = false;
			u22.IsOn = false;
			u23.IsOn = false;
			u24.IsOn = false;
			u25.IsOn = false;
			u26.IsOn = false;
			u1.IsEnabled = false;
			u2.IsEnabled = false;
			u3.IsEnabled = false;
			u4.IsEnabled = false;
			u5.IsEnabled = false;
			u6.IsEnabled = false;
			u7.IsEnabled = false;
			u8.IsEnabled = false;
			u9.IsEnabled = false;
			u10.IsEnabled = false;
			u11.IsEnabled = false;
			u12.IsEnabled = false;
			u13.IsEnabled = false;
			u14.IsEnabled = false;
			u15.IsEnabled = false;
			u16.IsEnabled = false;
			u17.IsEnabled = false;
			u18.IsEnabled = false;
			u19.IsEnabled = false;
			u20.IsEnabled = false;
			u21.IsEnabled = false;
			u22.IsEnabled = false;
			u23.IsEnabled = false;
			u24.IsEnabled = false;
			u25.IsEnabled = false;
			u26.IsEnabled = false;
			b.IsEnabled = false;
			b.Visibility = Visibility.Visible;
			CheckInstalledUWPAppsAsync(anim: false);
		}
		else
		{
			mw.ChSt(uwp["status"]["noapps"]);
		}
	}

	private async Task RemovePackageAsync(string packageName)
	{
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = "powershell.exe",
			Arguments = "-Command \"& {Get-AppxPackage -name \"" + packageName + "\" | Remove-AppxPackage}\"",
			WindowStyle = ProcessWindowStyle.Hidden,
			CreateNoWindow = true,
			UseShellExecute = false
		};
		Process process = new Process();
		process.StartInfo = startInfo;
		await Task.Run(delegate
		{
			process.Start();
			process.WaitForExit();
		});
	}

	private void button1_Click(object sender, EventArgs e)
	{
	}

	private void skip_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		isSkipped = true;
		cancellationTokenSource?.Cancel();
	}

	private void FadeInPopular()
	{
		u15.Visibility = Visibility.Visible;
		u16.Visibility = Visibility.Visible;
		u17.Visibility = Visibility.Visible;
		u9.Visibility = Visibility.Visible;
		FadeIn(u16, 300.0);
		FadeIn(u15, 300.0);
		FadeIn(u17, 300.0);
		FadeIn(u9, 300.0);
	}

	private void FadeInNecessary()
	{
		u23.Visibility = Visibility.Visible;
		u24.Visibility = Visibility.Visible;
		FadeIn(u23, 300.0);
		FadeIn(u24, 300.0);
	}

	private async void FadeOutPopular()
	{
		FadeOut(u16, 300.0);
		FadeOut(u15, 300.0);
		FadeOut(u17, 300.0);
		FadeOut(u9, 300.0);
		await Task.Delay(300);
		u15.Visibility = Visibility.Collapsed;
		u16.Visibility = Visibility.Collapsed;
		u17.Visibility = Visibility.Collapsed;
		u9.Visibility = Visibility.Collapsed;
	}

	private async void FadeOutNecessary()
	{
		FadeOut(u23, 300.0);
		FadeOut(u24, 300.0);
		await Task.Delay(300);
		u23.Visibility = Visibility.Collapsed;
		u24.Visibility = Visibility.Collapsed;
	}

	private void FadeInBloat()
	{
		FadeIn(u1, 300.0);
		FadeIn(u2, 300.0);
		FadeIn(u3, 300.0);
		FadeIn(u4, 300.0);
		FadeIn(u5, 300.0);
		FadeIn(u6, 300.0);
		FadeIn(u7, 300.0);
		FadeIn(u8, 300.0);
		FadeIn(u10, 300.0);
		FadeIn(u11, 300.0);
		FadeIn(u12, 300.0);
		FadeIn(u13, 300.0);
		FadeIn(u14, 300.0);
		FadeIn(u18, 300.0);
		FadeIn(u19, 300.0);
		FadeIn(u20, 300.0);
		FadeIn(u21, 300.0);
		FadeIn(u22, 300.0);
		FadeIn(u25, 300.0);
		FadeIn(u26, 300.0);
		FadeIn(b, 300.0);
		FadeIn(view, 300.0);
	}

	private void FadeOutBloat()
	{
		FadeOut(u1, 300.0);
		FadeOut(u2, 300.0);
		FadeOut(u3, 300.0);
		FadeOut(u4, 300.0);
		FadeOut(u5, 300.0);
		FadeOut(u6, 300.0);
		FadeOut(u7, 300.0);
		FadeOut(u8, 300.0);
		FadeOut(u10, 300.0);
		FadeOut(u11, 300.0);
		FadeOut(u12, 300.0);
		FadeOut(u13, 300.0);
		FadeOut(u14, 300.0);
		FadeOut(u18, 300.0);
		FadeOut(u19, 300.0);
		FadeOut(u20, 300.0);
		FadeOut(u21, 300.0);
		FadeOut(u22, 300.0);
		FadeOut(u25, 300.0);
		FadeOut(u26, 300.0);
		FadeOut(b, 300.0);
		FadeOut(view, 300.0);
	}

	private async void view_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		switch (view.SelectedIndex)
		{
		case 0:
			if (mode == 1)
			{
				FadeOutPopular();
			}
			if (mode == 2)
			{
				FadeOutNecessary();
				FadeOutPopular();
			}
			mode = 0;
			view.IsEnabled = false;
			await Task.Delay(300);
			view.IsEnabled = true;
			break;
		case 1:
			if (mode == 0)
			{
				FadeInPopular();
			}
			else if (mode == 2)
			{
				FadeOutNecessary();
			}
			mode = 1;
			view.IsEnabled = false;
			await Task.Delay(300);
			view.IsEnabled = true;
			break;
		case 2:
			if (mode == 0)
			{
				FadeInNecessary();
				FadeInPopular();
			}
			else if (mode == 1)
			{
				FadeInNecessary();
			}
			mode = 2;
			view.IsEnabled = false;
			await Task.Delay(300);
			view.IsEnabled = true;
			break;
		}
	}

	private void LoadLang()
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "uwp");
		label.Text = dictionary["main"]["label"];
		info1.Text = dictionary["main"]["info1"];
		info2.Text = dictionary["main"]["info2"];
		u3.OffContent = dictionary["main"]["u3"];
		u5.OffContent = dictionary["main"]["u5"];
		u6.OffContent = dictionary["main"]["u6"];
		u9.OffContent = dictionary["main"]["u9"];
		u10.OffContent = dictionary["main"]["u10"];
		u13.OffContent = dictionary["main"]["u13"];
		u15.OffContent = dictionary["main"]["u15"];
		u16.OffContent = dictionary["main"]["u16"];
		u17.OffContent = dictionary["main"]["u17"];
		u18.OffContent = dictionary["main"]["u18"];
		u19.OffContent = dictionary["main"]["u19"];
		u20.OffContent = dictionary["main"]["u20"];
		u22.OffContent = dictionary["main"]["u22"];
		u3.OnContent = dictionary["main"]["u3"];
		u5.OnContent = dictionary["main"]["u5"];
		u6.OnContent = dictionary["main"]["u6"];
		u9.OnContent = dictionary["main"]["u9"];
		u10.OnContent = dictionary["main"]["u10"];
		u13.OnContent = dictionary["main"]["u13"];
		u15.OnContent = dictionary["main"]["u15"];
		u16.OnContent = dictionary["main"]["u16"];
		u17.OnContent = dictionary["main"]["u17"];
		u18.OnContent = dictionary["main"]["u18"];
		u19.OnContent = dictionary["main"]["u19"];
		u20.OnContent = dictionary["main"]["u20"];
		u22.OnContent = dictionary["main"]["u22"];
		mode1.Content = dictionary["main"]["mode1"];
		mode2.Content = dictionary["main"]["mode2"];
		mode3.Content = dictionary["main"]["mode3"];
		b.Content = dictionary["main"]["b"];
		skip.Text = dictionary["main"]["skip"];
		t.Text = dictionary["main"]["chk"];
	}
}
