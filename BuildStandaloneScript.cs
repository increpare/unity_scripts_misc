using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Diagnostics;

public class BuildStandaloneScript : MonoBehaviour {
	[MenuItem ("Edit/Build All")]
	static void DoSomething () {
		PlayerSettings.showUnitySplashScreen = false;

		var pname = PlayerSettings.productName;

		BuildDirectories ();
		
		var icons = PlayerSettings.GetIconsForTargetGroup (BuildTargetGroup.Standalone);

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");
		string[] levels = EditorBuildSettings.scenes.Select (s => s.path).ToArray ();
        print ("building win32");
		BuildPipeline.BuildPlayer (levels, "../Build/" + PlayerSettings.productName + "/Win/Win_32/" + PlayerSettings.productName + "/" + PlayerSettings.productName + ".exe", BuildTarget.StandaloneWindows, BuildOptions.None);
		print ("building win64");
		BuildPipeline.BuildPlayer (levels, "../Build/" + PlayerSettings.productName + "/Win/Win_64/" + PlayerSettings.productName + "/" + PlayerSettings.productName + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
		print ("building linux");
		BuildPipeline.BuildPlayer (levels, "../Build/" + PlayerSettings.productName + "/Linux/" + PlayerSettings.productName + "/" + PlayerSettings.productName, BuildTarget.StandaloneLinuxUniversal, BuildOptions.None);
        print ("building osx");
        BuildPipeline.BuildPlayer (levels, "../Build/"+PlayerSettings.productName+"/OSX/"+PlayerSettings.productName, BuildTarget.StandaloneOSX,BuildOptions.None);
 		print ("building html");
        BuildPipeline.BuildPlayer (levels, "../Build/"+PlayerSettings.productName+"/HTML/"+PlayerSettings.productName, BuildTarget.WebGL,BuildOptions.None);

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "STEAM");


        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");

		try
		{
			File.Delete("../Build/"+pname+"_linux.tar.gz");
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


		var dir = new DirectoryInfo("../Build/"+pname+"/Win/Win_64/"+pname);
		
		foreach (var file in dir.GetFiles("*.pdb")) {
			print (file.Name);
			file.Delete();
		}
		
		
		dir = new DirectoryInfo("../Build/"+pname+"/Win/Win_32/"+pname);
		
		foreach (var file in dir.GetFiles("*.pdb")) {
			print (file.Name);
			file.Delete();
		}


		print ("compressing linux");
		startInfo = new ProcessStartInfo("/usr/bin/tar");
		startInfo.WorkingDirectory = "../Build/"+pname+"/Linux";
		startInfo.Arguments = "-czvf \"../" + pname + "_linux.tar.gz\" \"" + pname+"\"";
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
		
            
		print ("compressing src");
		dir = new DirectoryInfo(Application.dataPath+"/../");
		var dirName = dir.Name;
		startInfo = new ProcessStartInfo("/usr/bin/zip");
		startInfo.WorkingDirectory = Application.dataPath+"/../../";
		startInfo.Arguments = "-r \"Build/" +pname+"/"+ pname + "_src.zip\" " + dirName;
		Process.Start(startInfo).WaitForExit();
		print ("done");
	}
	
	static void BuildDirectories(){
		
		var pname = PlayerSettings.productName;
		
		Directory.CreateDirectory ("../Build");
		Directory.CreateDirectory ("../Build/" + PlayerSettings.productName);
		Directory.CreateDirectory ("../Build/" + PlayerSettings.productName + "/HTML");
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
