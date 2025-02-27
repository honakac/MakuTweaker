using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using MakuTweakerNew.Properties;
using ModernWpf.Controls;

namespace MakuTweakerNew;

public partial class HidePart : ContentDialog, IComponentConnector
{
	public TaskCompletionSource<decimal> TaskCompletionSource { get; private set; }

	public HidePart()
	{
		InitializeComponent();
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "expl");
		Run item = new Run
		{
			Text = dictionary["status"]["hdInfo1"],
			FontSize = 18.0,
			FontFamily = new FontFamily("Segoe UI Semilight")
		};
		Run item2 = new Run
		{
			Text = dictionary["status"]["hdInfo2"],
			FontSize = 18.0,
			FontFamily = new FontFamily("Segoe UI Semibold")
		};
		base.CloseButtonText = dictionary["status"]["hide"];
		base.PrimaryButtonText = dictionary["status"]["cc"];
		textBlock.Inlines.Add(item);
		textBlock.Inlines.Add(new LineBreak());
		textBlock.Inlines.Add(item2);
		textBlock.TextAlignment = TextAlignment.Left;
		textBlock.MaxWidth = 500.0;
		TaskCompletionSource = new TaskCompletionSource<decimal>();
		if (string.IsNullOrEmpty(Settings.Default.hiddenDrives))
		{
			return;
		}
		foreach (object child in checkboxpanel.Children)
		{
			if (child is CheckBox checkBox)
			{
				if (Settings.Default.hiddenDrives.Contains(checkBox.Content.ToString()))
				{
					checkBox.IsChecked = true;
				}
				else
				{
					checkBox.IsChecked = false;
				}
			}
		}
	}

	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
	}

	private void CloseDialog(decimal result)
	{
		TaskCompletionSource.SetResult(result);
		Hide();
	}

	private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
	{
		CloseDialog(-1m);
	}

	private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
	{
		decimal result = default(decimal);
		if (a.IsChecked == true)
		{
			result += 1m;
		}
		if (d.IsChecked == true)
		{
			result += 8m;
		}
		if (e.IsChecked == true)
		{
			result += 16m;
		}
		if (f.IsChecked == true)
		{
			result += 32m;
		}
		if (g.IsChecked == true)
		{
			result += 64m;
		}
		if (h.IsChecked == true)
		{
			result += 128m;
		}
		if (i.IsChecked == true)
		{
			result += 256m;
		}
		if (j.IsChecked == true)
		{
			result += 512m;
		}
		if (k.IsChecked == true)
		{
			result += 1024m;
		}
		if (l.IsChecked == true)
		{
			result += 2048m;
		}
		if (m.IsChecked == true)
		{
			result += 4096m;
		}
		if (n.IsChecked == true)
		{
			result += 8192m;
		}
		if (o.IsChecked == true)
		{
			result += 16384m;
		}
		if (p.IsChecked == true)
		{
			result += 32768m;
		}
		if (q.IsChecked == true)
		{
			result += 65536m;
		}
		if (r.IsChecked == true)
		{
			result += 131072m;
		}
		if (s.IsChecked == true)
		{
			result += 262144m;
		}
		if (t.IsChecked == true)
		{
			result += 524288m;
		}
		if (u.IsChecked == true)
		{
			result += 1048576m;
		}
		if (v.IsChecked == true)
		{
			result += 2097152m;
		}
		if (w.IsChecked == true)
		{
			result += 4194304m;
		}
		if (x.IsChecked == true)
		{
			result += 8388608m;
		}
		if (y.IsChecked == true)
		{
			result += 16777216m;
		}
		if (z.IsChecked == true)
		{
			result += 33554432m;
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (object child in checkboxpanel.Children)
		{
			if (child is CheckBox { IsChecked: var isChecked } checkBox && isChecked == true)
			{
				stringBuilder.Append(checkBox.Content.ToString());
			}
		}
		Settings.Default.hiddenDrives = stringBuilder.ToString();
		CloseDialog(result);
	}
}
