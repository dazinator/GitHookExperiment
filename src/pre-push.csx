#!/usr/bin/env dotnet-script
//Create process

Console.WriteLine("gitversion pre-push hook running");

//1. if this the notes branch being pushed, then just return without doing any checks.
//2. if this is any other branch being pushed, check the status of notes branch - if there are unpushed commits then prevent this push.

var args = string.Join(", ", Args);
string statusOutput = "";

//1.
using (var gitBranchNameProcess = new System.Diagnostics.Process())
{
    gitBranchNameProcess.StartInfo.FileName = "git";
    gitBranchNameProcess.StartInfo.Arguments = $@"symbolic-ref HEAD";
    gitBranchNameProcess.StartInfo.UseShellExecute = false;
    gitBranchNameProcess.StartInfo.RedirectStandardOutput = true;
    //Optional
    //pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;
    gitBranchNameProcess.Start();
    //Get program output
    statusOutput = gitBranchNameProcess.StandardOutput.ReadToEnd();

    //Wait for process to finish
    gitBranchNameProcess.WaitForExit();
}

var lines =  statusOutput.Split(Environment.NewLine);
var branchName = lines[0];
if(branchName=="refs/notes/commits")
{
    return;
}


// 2.
// push notes?

// using (var gitPushNotesProcess = new System.Diagnostics.Process())
// {
//     gitPushNotesProcess.StartInfo.FileName = "git";
//     gitPushNotesProcess.StartInfo.Arguments = $@"push origin refs/notes/*";
//     gitPushNotesProcess.StartInfo.UseShellExecute = false;
//     gitPushNotesProcess.StartInfo.RedirectStandardOutput = true;
//     //Optional
//     //pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;
//     gitPushNotesProcess.Start();
//     //Get program output
//     statusOutput = gitPushNotesProcess.StandardOutput.ReadToEnd();

//     //Wait for process to finish
//     gitPushNotesProcess.WaitForExit();
// }

// using (var gitBranchStatus = new System.Diagnostics.Process())
// {
//     gitBranchStatus.StartInfo.FileName = "git";
//     gitBranchStatus.StartInfo.Arguments = $@"status";
//     gitBranchStatus.StartInfo.UseShellExecute = false;
//     gitBranchStatus.StartInfo.RedirectStandardOutput = true;
//     //Optional
//     //pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;
//     gitBranchStatus.Start();
//     //Get program output
//     statusOutput = gitBranchStatus.StandardOutput.ReadToEnd();
 
//     //Wait for process to finish
//     gitBranchStatus.WaitForExit();
// }

// git merge-base refs/notes/commits origin/refs/notes/commits

//throw new Exception($"You can't push this branch {branchName} because the notes branch has unpushed commits. Please push those first. + {args} + {statusOutput}");