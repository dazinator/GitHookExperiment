#!/usr/bin/env dotnet-script

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

Console.WriteLine(versionJson);

if(!string.IsNullOrWhiteSpace(versionJson))
{
    using(var gitNotesProcess = new System.Diagnostics.Process())
    {
        gitNotesProcess.StartInfo.FileName = "git";
        gitNotesProcess.StartInfo.Arguments = $@"notes add -m ""{versionJson}""";
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

