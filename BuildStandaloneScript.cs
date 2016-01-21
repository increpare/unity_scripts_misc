using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Diagnostics;

public class BuildStandaloneScript : MonoBehaviour {
	[MenuItem ("File/Build All")]
	static void DoSomething () {
		var pname = PlayerSettings.productName;

		BuildDirectories ();

		var icons = PlayerSettings.GetIconsForTargetGroup (BuildTargetGroup.Standalone);
		string[] levels = EditorBuildSettings.scenes.Select (s => s.path).ToArray ();
		print ("building osx");
		BuildPipeline.BuildPlayer (levels, "../Build/"+PlayerSettings.productName+"/OSX/"+PlayerSettings.productName, BuildTarget.StandaloneOSXUniversal,BuildOptions.None);
		print ("building win32");
		BuildPipeline.BuildPlayer (levels, "../Build/" + PlayerSettings.productName + "/Win/Win_32/" + PlayerSettings.productName + "/" + PlayerSettings.productName + ".exe", BuildTarget.StandaloneWindows, BuildOptions.None);
		print ("building win64");
		BuildPipeline.BuildPlayer (levels, "../Build/" + PlayerSettings.productName + "/Win/Win_64/" + PlayerSettings.productName + "/" + PlayerSettings.productName + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
		print ("building linux");
		BuildPipeline.BuildPlayer (levels, "../Build/" + PlayerSettings.productName + "/Linux/" + PlayerSettings.productName + "/" + PlayerSettings.productName, BuildTarget.StandaloneLinuxUniversal, BuildOptions.None);


		try
		{
			File.Delete("../Build/"+pname+"_linux.zip");
		}
        catch(System.Exception e){
        }
		
		try
		{
			File.Delete("../Build/"+pname+"_osx.zip");
		}
        catch(System.Exception e){
		}
		
		try
		{
			File.Delete("../Build/"+pname+"_win32.zip");
		}
		catch(System.Exception e){
        }
		
		
		try
		{
			File.Delete("../Build/"+pname+"_win64.zip");
		}
        catch(System.Exception e){
		}
		
		try
		{
			File.Delete("../Build/"+pname+"_src.zip");
		}
		catch(System.Exception e){
        }
		//var escapedpname = pname.Replace(" ","\\ ");

        ProcessStartInfo startInfo;
        
        print ("compressing linux");
        startInfo = new ProcessStartInfo("/usr/bin/zip");
		startInfo.WorkingDirectory = "../Build/"+pname+"/Linux";
		startInfo.Arguments = "-r \"../" + pname + "_linux.zip\" \"" + pname+"\"";
		Process.Start(startInfo).WaitForExit();        

		print ("compressing osx");
		startInfo = new ProcessStartInfo("/usr/bin/zip");
		startInfo.WorkingDirectory = "../Build/"+pname+"/OSX";
		startInfo.Arguments = "-r \"../" + pname + "_osx.zip\" \"" + pname + ".app\"";
		Process.Start(startInfo).WaitForExit();
		
		print ("compressing windows");
		startInfo = new ProcessStartInfo("/usr/bin/zip");
		startInfo.WorkingDirectory = "../Build/"+pname+"/Win/Win_32";
		startInfo.Arguments = "-r \"../../" + pname + "_win32.zip\" \"" + pname +"\"";
		Process.Start(startInfo).WaitForExit();
		
		print ("compressing win");
		startInfo = new ProcessStartInfo("/usr/bin/zip");
		startInfo.WorkingDirectory = "../Build/"+pname+"/Win/Win_64";
		startInfo.Arguments = "-r \"../../" + pname + "_win64.zip\" \"" + pname +"\"";
		Process.Start(startInfo).WaitForExit();

		/*
		print ("compressing src");
		var dir = new DirectoryInfo(Application.dataPath+"/../");
		var dirName = dir.Name;
		startInfo = new ProcessStartInfo("/usr/bin/zip");
		startInfo.WorkingDirectory = Application.dataPath+"/../../";
		startInfo.Arguments = "-r \"Build/" +pname+"/"+ pname + "_src.zip\" " + dirName;
		Process.Start(startInfo).WaitForExit();
		print ("done");*/
	}

	static void BuildDirectories(){
		
				var pname = PlayerSettings.productName;
		
				Directory.CreateDirectory ("../Build");
				Directory.CreateDirectory ("../Build/" + PlayerSettings.productName);
				Directory.CreateDirectory ("../Build/" + PlayerSettings.productName + "/OSX");
				Directory.CreateDirectory ("../Build/" + PlayerSettings.productName + "/Win");
				Directory.CreateDirectory ("../Build/" + PlayerSettings.productName + "/Win/Win_32");
				Directory.CreateDirectory ("../Build/" + PlayerSettings.productName + "/Win/Win_64");
				Directory.CreateDirectory ("../Build/" + PlayerSettings.productName + "/Win/Win_32/" + PlayerSettings.productName);
				Directory.CreateDirectory ("../Build/" + PlayerSettings.productName + "/Win/Win_64/" + PlayerSettings.productName);
				Directory.CreateDirectory ("../Build/" + PlayerSettings.productName + "/Linux/" + PlayerSettings.productName);
				//	System.Diagnostics.Process.Start(
		

		}
}
