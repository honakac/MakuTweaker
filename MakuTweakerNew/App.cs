using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MakuTweakerNew;

public partial class App : Application
{
	private readonly string logFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MarkAdderly", "MakuTweaker", "CrashLogs");

	public App()
	{
		base.DispatcherUnhandledException += OnDispatcherUnhandledException;
		AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
		TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
	}

	private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
	{
		HandleCrash("Unhandled UI Exception", e.Exception, 2);
		e.Handled = true;
	}

	private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		HandleCrash("Unhandled Critical Exception", e.ExceptionObject as Exception, 1);
	}

	private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
	{
		HandleCrash("Unhandled Task Exception", e.Exception, 3);
		e.SetObserved();
	}

	private void HandleCrash(string errorType, Exception? ex, int exitCode)
	{
		string text = ex?.Message ?? "Unknown error";
		string value = ex?.StackTrace ?? "No stack trace available";
		string contents = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {errorType}\nMessage: {text}\nStackTrace:\n{value}\n\n";
		try
		{
			Directory.CreateDirectory(logFolder);
			string text2 = Path.Combine(logFolder, $"crash_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
			File.WriteAllText(text2, contents);
			MessageBox.Show("MakuTweaker Has Crashed!\n\nError: " + text + "\n\nCrash Log Saved on Path:\n" + text2, "Critical Error", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
		catch
		{
			MessageBox.Show("MakuTweaker has crashed.\n\nError: " + text, "Critical Error", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
		Application.Current.Shutdown(exitCode);
	}
}
