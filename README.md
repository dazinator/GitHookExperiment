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

To debug, open in VS Code (I have included launchSettings in the repo) set a breakpoint in the main.csx script and start debugging. The script is just an experiment and it calls GitVersion and writes to a local version.yml file.


## Run as a git commit hook

Copy the contents of main.csx to .git/hooks/pre-commit (or relevent hook you want to run this on). Then perform a git commit in the command line and watch the output:

```
PS D:\git-hooks-example> git commit -m "added files"
pre-commit hook
{
  "Major":0,
  "Minor":1,
  "Patch":0,
  "PreReleaseTag":"",
  "PreReleaseTagWithDash":"",
  "PreReleaseLabel":"",
  "PreReleaseNumber":"",
  "BuildMetaData":0,
  "BuildMetaDataPadded":"0000",
  "FullBuildMetaData":"0.Branch.master.Sha.db8fb0d4ac2b9c898d3356838a372789ca3fffa9",
  "MajorMinorPatch":"0.1.0",
  "SemVer":"0.1.0",
  "LegacySemVer":"0.1.0",
  "LegacySemVerPadded":"0.1.0",
  "AssemblySemVer":"0.1.0.0",
  "FullSemVer":"0.1.0+0",
  "InformationalVersion":"0.1.0+0.Branch.master.Sha.db8fb0d4ac2b9c898d3356838a372789ca3fffa9",
  "BranchName":"master",
  "Sha":"db8fb0d4ac2b9c898d3356838a372789ca3fffa9",
  "NuGetVersionV2":"0.1.0",
  "NuGetVersion":"0.1.0",
  "CommitsSinceVersionSource":0,
  "CommitsSinceVersionSourcePadded":"0000",
  "CommitDate":"2020-04-14"
}

[master eacc94a] added files
 4 files changed, 82 insertions(+)
 create mode 100644 src/.vscode/launch.json
 create mode 100644 src/main.csx
 create mode 100644 src/omnisharp.json
 create mode 100644 src/version.yml
```
## Example skeleton git hook


Here is an example of a git "pre-commit" hook that uses C# script. You must have installed `dotnet-script` on your environment with `dotnet tool install -g dotnet-script` first.

```
#!/usr/bin/env dotnet-script

Console.WriteLine("pre-commit hook");
```
