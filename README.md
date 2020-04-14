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

## Debugging

To debug, open in VS Code (I have included launchSettings in the repo) set a breakpoint in the main.csx script and start debugging.


## Run as a git commit hook

Copy the contents of main.csx to .git/hooks/pre-commit (or relevent hook you want to run this on). Then perform a git commit in the command line and watch the output:

## Example skeleton git hook


Here is an example of a git "pre-commit" hook that uses C# script:

```
#!/usr/bin/env dotnet-script

Console.WriteLine("pre-commit hook");
```
