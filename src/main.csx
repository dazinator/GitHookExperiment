#!/usr/bin/env dotnet-script

//Create process
System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

//strCommand is path and file name of command to run
pProcess.StartInfo.FileName = "gitversion";

//strCommandParameters are parameters to pass to program
pProcess.StartInfo.Arguments = "/output json";

pProcess.StartInfo.UseShellExecute = false;

//Set output of program to be written to process output stream
pProcess.StartInfo.RedirectStandardOutput = true;   

//Optional
//pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;

//Start the process
pProcess.Start();

//Get program output
string strOutput = pProcess.StandardOutput.ReadToEnd();

//Wait for process to finish
pProcess.WaitForExit();

File.WriteAllText("version.yml", strOutput);

Console.WriteLine("pre-commit hook");
Console.WriteLine(strOutput);