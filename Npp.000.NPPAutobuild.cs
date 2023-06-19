//css_ref C:\Program Files\Notepad++\plugins\NppScripts\NppScripts.dll
//css_ref C:\Program Files\Notepad++\plugins\NppScripts\NppScripts\NppScripts.asm.dll
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NppScripts;

public class Script : NppScript
{
	public override void Run()
	{
		FileInfo fi = new FileInfo(Npp.Editor.GetCurrentFilePath());
		string path = fi.DirectoryName + "/.npp_autobuild.bat";
		
		Process process = new Process();
		process.StartInfo.FileName               = path;
		process.StartInfo.UseShellExecute        = false;
		process.StartInfo.CreateNoWindow         = true;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.WorkingDirectory       = fi.DirectoryName;
		process.Start();
		
		Task.Factory.StartNew(() => TypeText(process.StandardOutput.ReadToEnd()));
	}
	void TypeText(string s) {
		Npp.Editor.SendMenuCommand(NppMenuCmd.IDM_FILE_NEW);
		Npp.Document.AppendText(s.Length, s);
	}
}