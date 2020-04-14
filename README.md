Commands used to create this repo:

```
mkdir git-hooks-example
cd git-hooks-example
git init
dotnet new gitignore
dotnet new tool-manifest
dotnet tool install -g dotnet-script
mkdir .githooks
```

Here is an example of a git "pre-commit" hook that uses C# script:

```
#!/usr/bin/env dotnet-script

Console.WriteLine("pre-commit hook");
```