using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using MakuTweakerNew.Properties;
using ModernWpf.Controls;

namespace MakuTweakerNew;

public partial class ILOVEMAKUTWEAKERDialog : ContentDialog, IComponentConnector
{
	public TaskCompletionSource<int> TaskCompletionSource { get; private set; }

	public ILOVEMAKUTWEAKERDialog(string app)
	{
		InitializeComponent();
		Dictionary<string, Dictionary<string, string>> dictionary = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "uwp");
		Run item = new Run
		{
			Text = $"{dictionary["main"]["suredialogT1"]} {app} {dictionary["main"]["suredialogT2"]}\n",
			FontSize = 14.0,
			FontFamily = new FontFamily("Segoe UI")
		};
		Run item2 = new Run
		{
			Text = dictionary["main"]["suredialogT3"] + "\n",
			FontSize = 14.0,
			FontFamily = new FontFamily("Segoe UI")
		};
		Run item3 = new Run
		{
			Text = (dictionary["main"]["suredialogT4"] ?? ""),
			FontSize = 18.0,
			FontFamily = new FontFamily("Segoe UI Semibold")
		};
		base.CloseButtonText = dictionary["main"]["suredialogNS"];
		textBlock.Inlines.Add(item);
		textBlock.Inlines.Add(new LineBreak());
		textBlock.Inlines.Add(item2);
		textBlock.Inlines.Add(new LineBreak());
		textBlock.Inlines.Add(item3);
		textBlock.TextAlignment = TextAlignment.Left;
		TaskCompletionSource = new TaskCompletionSource<int>();
	}

	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (ILOVEMAKUTWEAKER.Text == "ILOVEMAKUTWEAKER")
		{
			base.PrimaryButtonText = "OK";
		}
		else
		{
			base.PrimaryButtonText = string.Empty;
		}
	}

	private void CloseDialog(int result)
	{
		TaskCompletionSource.SetResult(result);
		Hide();
	}

	private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
	{
		CloseDialog(1);
	}

	private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
	{
		CloseDialog(0);
	}
}
