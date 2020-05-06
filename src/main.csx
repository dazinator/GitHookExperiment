#!/usr/bin/env dotnet-script
#r "nuget: Newtonsoft.Json, 12.0.3"
#r "nuget: YamlDotNet, 8.1.1"

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using YamlDotNet;

//Create process
Console.WriteLine("gitversion hook running");
string versionJson = string.Empty;
  
using(var gitVersionProcess = new System.Diagnostics.Process())
{
    gitVersionProcess.StartInfo.FileName = "gitversion";
    gitVersionProcess.StartInfo.Arguments = "/output json";
    gitVersionProcess.StartInfo.UseShellExecute = false;
    gitVersionProcess.StartInfo.RedirectStandardOutput = true;   
    //Optional
    //pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;
    gitVersionProcess.Start();
    //Get program output
    versionJson = gitVersionProcess.StandardOutput.ReadToEnd();

    //Wait for process to finish
    gitVersionProcess.WaitForExit();

}

//dynamic variablesObject = JsonConvert.DeserializeObject(versionJson);
var expConverter = new ExpandoObjectConverter();
dynamic deserializedObject = JsonConvert.DeserializeObject<ExpandoObject>(versionJson, expConverter);

var serializer = new YamlDotNet.Serialization.Serializer();
string yaml = serializer.Serialize(deserializedObject);
//var yamlSerialiser = new YamlDotNet.Serialization.Serializer();

Console.WriteLine(versionJson);
Console.WriteLine(yaml);
if(!string.IsNullOrWhiteSpace(yaml))
{
    using(var gitNotesProcess = new System.Diagnostics.Process())
    {
        gitNotesProcess.StartInfo.FileName = "git";
        gitNotesProcess.StartInfo.Arguments = $@"notes add -m ""{yaml}""";
        gitNotesProcess.StartInfo.UseShellExecute = false;
        gitNotesProcess.StartInfo.RedirectStandardOutput = true;   
        //Optional
        //pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;
        gitNotesProcess.Start();
        //Get program output
        versionJson = gitNotesProcess.StandardOutput.ReadToEnd();

        //Wait for process to finish
        gitNotesProcess.WaitForExit();
    }
}




// Add version as post commit

// File.WriteAllText("version.yml", strOutput);

