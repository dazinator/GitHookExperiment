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
// Are there unpushed notes?

//WIP. I noticed that
// git fetch --refmap='' origin +refs/notes/*:refs/notes/* --dry-run
// will yield no output when the remote and the local refs/notes/commits match.
// and will yield output like this when they don't match:
//   + 6d724c0...a035bca refs/notes/commits -> refs/notes/commits  (forced update)

// Also using this refspec: +refs/notes/*:refs/notes/* seems to cause local notes branch to overwrite server branch and vice versa,
// -- think it would be safer if there was a way to fetch refs/notes/* from the server into a remote branch like  refs/notes/origin/* and then we can merge our local refs/notes/* with that branch.
// otherwise I don't see how it could work in a team scenario as everyone would constantly be overwriting eachother notes.

// This one might also come in handy once merging of notes is set up.
// git merge-base refs/notes/commits origin/refs/notes/commits

//throw new Exception($"You can't push this branch {branchName} because the notes branch has unpushed commits. Please push those first. + {args} + {statusOutput}");