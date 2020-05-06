Commands used to create this repo:

```
mkdir git-hooks-example
cd git-hooks-example
git init
dotnet new gitignore
dotnet new tool-manifest
dotnet tool install -g dotnet-script
mkdir .githooks
git add *
git commit -m "initial commit"
```
## Prerequisites

1. VS Code for debugging purposes.
1. You must have installed `dotnet-script` on your environment with `dotnet tool install -g dotnet-script` for the git commit hook to execute.

## Debugging

To debug, open the "src" folder in VS Code (I have included launchSettings and workspace settings in the repo) set a breakpoint in the `main.csx` script and start debugging using the ".NET Script Debug" profile. The script is just an experiment and it calls GitVersion.exe and then converts the json variables to yml (I had a hardtime committing json as a note), then adds it as a git note to the current repo for the current commit.

## Run as a git commit hook

Debugging the script in VS Code is all well and good, but you want to see it execute as a proper git post commit hook right?

1. Install the [filewatcher](https://marketplace.visualstudio.com/items?itemName=appulate.filewatcher) extension for VS Code.
2. Save a change to the `main.csx` file - it will automatically be copied to `.git/hooks/post-commit` thanks to filewatcher, and the vscode settings.json.
3. Commit your change! This will now trigger the post-commit hook! You will see the output in VS Code output window. This is now running the script as a proper git post commit hook.
 
```
## Example skeleton git hook


Here is an example of a git "pre-commit" hook that uses C# script. You must have installed `dotnet-script` on your environment with `dotnet tool install -g dotnet-script` first.

```
#!/usr/bin/env dotnet-script

Console.WriteLine("pre-commit hook");
```
