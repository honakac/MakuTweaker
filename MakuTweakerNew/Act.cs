using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MakuTweakerNew.Properties;
using Microsoft.Win32;
using ModernWpf.Controls;

namespace MakuTweakerNew;

public partial class Act : System.Windows.Controls.Page, IComponentConnector
{
	private MainWindow mw = (MainWindow)Application.Current.MainWindow;

	private string kms;

	private string kmskey;

	public Act()
	{
		InitializeComponent();
		switch (checkWinEd())
		{
		case "Professional":
			wined.SelectedIndex = 0;
			break;
		case "Core":
			wined.SelectedIndex = 1;
			break;
		case "CoreSingleLanguage":
			wined.SelectedIndex = 1;
			break;
		case "Education":
			wined.SelectedIndex = 2;
			break;
		case "Enterprise":
			wined.SelectedIndex = 3;
			break;
		case "EnterpriseS":
			wined.SelectedIndex = 4;
			break;
		default:
			wined.SelectedIndex = 0;
			break;
		}
		LoadLang();
	}

	private string checkWinEd()
	{
		string name = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
		string name2 = "EditionID";
		using RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name);
		return registryKey.GetValue(name2).ToString();
	}

	private async void HWIDAct()
	{
		mw.Category.IsEnabled = false;
		mw.ABCB.IsEnabled = false;
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> act = MainWindow.Localization.LoadLocalization(language, "act");
		string text = await ExecuteCommandAsync(await GetEmbeddedCmdContentAsync("MakuTweakerNew.HWID.cmd"));
		if (text.Contains("Activation is not required"))
		{
			SystemSounds.Asterisk.Play();
			mw.ChSt(act["status"]["alrd"]);
		}
		else if (text.Contains("Evaluation editions cannot be activated"))
		{
			p.ShowError = true;
			mw.ChSt(act["status"]["err"]);
			System.Windows.MessageBox.Show(act["status"]["eval"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
		else if (text.Contains("Not Connected"))
		{
			p.ShowError = true;
			mw.ChSt(act["status"]["err"]);
			System.Windows.MessageBox.Show(act["status"]["nocon"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
		else if (text.Contains("permanently activated"))
		{
			SystemSounds.Asterisk.Play();
			mw.ChSt(act["status"]["success"]);
		}
		else
		{
			SystemSounds.Hand.Play();
			p.ShowError = true;
			mw.ChSt(act["status"]["unk"]);
		}
		PostActAnim();
		mw.Category.IsEnabled = true;
		mw.ABCB.IsEnabled = true;
	}

	private async Task<string> GetEmbeddedCmdContentAsync(string resourceName)
	{
		using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
		using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
		return await reader.ReadToEndAsync();
	}

	private async Task<string> ExecuteCommandAsync(string cmdContent)
	{
		string tempCmdFile = Path.Combine(Path.GetTempPath(), "hwid.cmd");
		await File.WriteAllTextAsync(tempCmdFile, cmdContent);
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			Arguments = "/C \"" + tempCmdFile + "\"",
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};
		using Process process = new Process();
		process.StartInfo = startInfo;
		process.Start();
		string output = await process.StandardOutput.ReadToEndAsync();
		string text = await process.StandardError.ReadToEndAsync();
		if (!string.IsNullOrEmpty(text))
		{
			return "Error: " + text;
		}
		process.WaitForExit();
		return output;
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		HWIDAct();
		ActAnim();
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

	private void ActAnim()
	{
		FadeOut(mact1, 300.0);
		FadeOut(mact2, 300.0);
		FadeOut(mact3, 300.0);
		FadeOut(mact4, 300.0);
		FadeOut(mact5, 300.0);
		FadeOut(mact6, 300.0);
		FadeOut(mact7, 300.0);
		FadeOut(mact9, 300.0);
		FadeOut(wined, 300.0);
		FadeIn(p, 300.0);
		FadeIn(t, 300.0);
		autoact.IsEnabled = false;
		mact1.IsEnabled = false;
		mact2.IsEnabled = false;
		mact3.IsEnabled = false;
		mact4.IsEnabled = false;
		mact5.IsEnabled = false;
		mact6.IsEnabled = false;
		mact7.IsEnabled = false;
		mact9.IsEnabled = false;
		wined.IsEnabled = false;
	}

	private void PostActAnim()
	{
		FadeIn(mact1, 300.0);
		FadeIn(mact2, 300.0);
		FadeIn(mact3, 300.0);
		FadeIn(mact4, 300.0);
		FadeIn(mact5, 300.0);
		FadeIn(mact6, 300.0);
		FadeIn(mact7, 300.0);
		FadeIn(mact9, 300.0);
		FadeIn(wined, 300.0);
		FadeOut(p, 300.0);
		FadeOut(t, 300.0);
		autoact.IsEnabled = true;
		mact1.IsEnabled = true;
		mact2.IsEnabled = true;
		mact3.IsEnabled = true;
		mact4.IsEnabled = true;
		mact5.IsEnabled = true;
		mact6.IsEnabled = true;
		mact7.IsEnabled = true;
		mact9.IsEnabled = true;
		wined.IsEnabled = true;
	}

	private void ManualActAnim()
	{
		FadeIn(p, 300.0);
		FadeIn(t, 300.0);
		autoact.IsEnabled = false;
		mact1.IsEnabled = false;
		mact2.IsEnabled = false;
		mact3.IsEnabled = false;
		mact4.IsEnabled = false;
		mact5.IsEnabled = false;
		mact6.IsEnabled = false;
		mact7.IsEnabled = false;
		mact9.IsEnabled = false;
		wined.IsEnabled = false;
	}

	private void ManualPostActAnim()
	{
		FadeOut(p, 300.0);
		FadeOut(t, 300.0);
		autoact.IsEnabled = true;
		mact1.IsEnabled = true;
		mact2.IsEnabled = true;
		mact3.IsEnabled = true;
		mact4.IsEnabled = true;
		mact5.IsEnabled = true;
		mact6.IsEnabled = true;
		mact7.IsEnabled = true;
		mact9.IsEnabled = true;
		wined.IsEnabled = true;
	}

	private void mact3_Click(object sender, RoutedEventArgs e)
	{
		MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "act");
		switch (mact2.SelectedIndex)
		{
		case 0:
			kms = "kms.digiboy.ir";
			break;
		case 1:
			kms = "kms.ddns.net";
			break;
		case 2:
			kms = "k.zpale.com";
			break;
		case 3:
			kms = "kms789.com";
			break;
		case 4:
			kms = "kms.03k.org:1688";
			break;
		case 5:
			kms = "hq1.chinancce.com";
			break;
		case 6:
			kms = "54.223.212.31";
			break;
		case 7:
			kms = "kms.cnlic.com";
			break;
		case 8:
			kms = "kms.chinancce.com";
			break;
		case 9:
			kms = "franklv.ddns.net";
			break;
		case 10:
			kms = "mvg.zpale.com";
			break;
		case 11:
			kms = "kms.shuax.com";
			break;
		}
		CMDAsync("cscript C:\\Windows\\System32\\slmgr.vbs /skms " + kms, 0);
	}

	private async Task CMDAsync(string command, int act)
	{
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> actv = MainWindow.Localization.LoadLocalization(language, "act");
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			Arguments = "/C " + command,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};
		await Task.Run(delegate
		{
			using Process process = new Process();
			process.StartInfo = startInfo;
			process.Start();
			string text = process.StandardOutput.ReadToEnd();
			string text2 = process.StandardError.ReadToEnd();
			process.WaitForExit();
			string text3 = (string.IsNullOrEmpty(text) ? "" : text);
			if (!string.IsNullOrEmpty(text2))
			{
				text3 = text3 + "\n\nError:\n" + text2;
			}
			if (text3.ToLower().Contains("error"))
			{
				base.Dispatcher.Invoke(delegate
				{
					SystemSounds.Hand.Play();
					if (act == 0)
					{
						mw.ChSt(actv["status"]["kmserr"]);
					}
					else
					{
						mw.ChSt(actv["status"]["keyerr"]);
					}
				});
			}
			else
			{
				base.Dispatcher.Invoke(delegate
				{
					if (act == 0)
					{
						mw.ChSt(actv["status"]["srv"]);
					}
					else
					{
						mw.ChSt(actv["status"]["edit"]);
					}
				});
			}
		});
	}

	private void mact5_Click(object sender, RoutedEventArgs e)
	{
		MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "act");
		switch (wined.SelectedIndex)
		{
		case 0:
			kmskey = "W269N-WFGWX-YVC9B-4J6C9-T83GX";
			break;
		case 1:
			kmskey = "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99";
			break;
		case 2:
			kmskey = "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2";
			break;
		case 3:
			kmskey = "NPPR9-FWDCX-D2C8J-H872K-2YT43";
			break;
		case 4:
			kmskey = "M7XTQ-FN8P6-TTKYV-9D4CC-J462D";
			break;
		}
		CMDAsync("cscript C:\\Windows\\System32\\slmgr.vbs /ipk " + kmskey, 1);
	}

	private void mact7_Click(object sender, RoutedEventArgs e)
	{
		mw.Category.IsEnabled = false;
		mw.ABCB.IsEnabled = false;
		ManualActAnim();
		CMDAsyncFinal("cscript C:\\Windows\\System32\\slmgr.vbs /ato", isfinal: true);
	}

	private async Task CMDAsyncFinal(string command, bool isfinal)
	{
		string language = Settings.Default.lang ?? "en";
		Dictionary<string, Dictionary<string, string>> act = MainWindow.Localization.LoadLocalization(language, "act");
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			Arguments = "/C " + command,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};
		await Task.Run(delegate
		{
			using Process process = new Process();
			process.StartInfo = startInfo;
			process.Start();
			string text = process.StandardOutput.ReadToEnd();
			string text2 = process.StandardError.ReadToEnd();
			process.WaitForExit();
			string text3 = (string.IsNullOrEmpty(text) ? "" : text);
			if (!string.IsNullOrEmpty(text2))
			{
				text3 = text3 + "\n\nError:\n" + text2;
			}
			string text4 = text3.ToLower();
			if (text4.Contains("product activated successfully"))
			{
				base.Dispatcher.Invoke(delegate
				{
					SystemSounds.Asterisk.Play();
					mw.ChSt(act["status"]["success"]);
				});
			}
			else if (text4.Contains("error"))
			{
				base.Dispatcher.Invoke(delegate
				{
					p.ShowError = true;
					mw.ChSt(act["status"]["err"]);
					ModernWpf.Controls.MessageBox.Show(act["status"]["error"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
				});
			}
			base.Dispatcher.Invoke(delegate
			{
				ManualPostActAnim();
				mw.Category.IsEnabled = true;
				mw.ABCB.IsEnabled = true;
			});
		});
	}

	private void LoadLang()
	{
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "act");
		label.Text = dictionary["main"]["label"];
		hwid.Text = dictionary["main"]["hwid"];
		autoact.Content = dictionary["main"]["button20"];
		mact3.Content = dictionary["main"]["button20"];
		mact5.Content = dictionary["main"]["button20"];
		mact7.Content = dictionary["main"]["button20"];
		mact1.Text = dictionary["main"]["step1"];
		mact4.Text = dictionary["main"]["step2"];
		mact6.Text = dictionary["main"]["step3"];
		mact9.Text = dictionary["main"]["kms"];
		t.Text = dictionary["status"]["inprog"];
	}
}
