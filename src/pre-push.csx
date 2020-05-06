#!/usr/bin/env dotnet-script
//Create process
Console.WriteLine("gitversion pre-push hook running");

// TODO if this is non-notes branch being pushed
// then check status of notes branch - if there are unpushed commits then prevent this push.