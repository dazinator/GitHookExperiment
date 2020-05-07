#!/usr/bin/env dotnet-script
//Create process

Console.WriteLine("gitversion pre-push hook running");

// TODO if this is non-notes branch being pushed
// then check status of notes branch - if there are unpushed commits then prevent this push.

foreach(var arg in Args)
{
    Console.WriteLine(arg);
   //git log origin/master..master 
}

var args = string.Join(", ", Args);

throw new Exception($"You can't push - I WON'T ALLOW IT! + {args}");