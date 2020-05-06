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

To debug:
- Open the "src" folder in VS Code.
- Set a breakpoint in the `post-commit.csx` script, and start debugging using the ".NET Script Debug" profile.

The `post-commit.csx` script calls GitVersion.exe and then converts the json variables to yml (I had a hardtime committing json as a note), then adds it as a git note to the current repo for the current commit.

## Run as a git commit hook

Debugging the script in VS Code is all well and good, but you want to see it execute as a proper git post commit hook right?

1. Install the [filewatcher](https://marketplace.visualstudio.com/items?itemName=appulate.filewatcher) extension for VS Code.
2. Save a change to the `post-commit.csx` file - it will automatically be copied to the local `.git/hooks/post-commit` thanks to filewatcher, and the vscode settings.json.
3. Commit your change the repo! This will now trigger the post-commit hook! You will see the output in VS Code output window (select git from the dropdown). This is now running the script as a proper git post commit hook.

Also this speeds up development:

1. Make a change to the `post-commit.csx` and save it.
2. Commit it to the repo - you will see it run!
 
## How do I see the note?

Multiple ways:

```
git notes show
```

![git notes show](/images/gitnotes.PNG)

Or you can see it in git log:

```
git --no-pager show -1

``` 

and it can be included in pretty printed logs using the `%N` parameter:

```
git --no-pager log -1 --pretty=%N
```

Or

```
git --no-pager show -1
```

## Auto pushing notes

Notes are a seperate branch that must be pushed to the server seperately.
For convenience you can set up a `git alias` so that you can always push notes when pushing the current branch:

Run this command:

```
git config --global alias.push-notes "!git push origin refs/notes/* && git push"

```

You can now execute:

```
git push-notes
```

## Auto fetching notes

This is probably going to very useful for the build server.
If this idea / proof of concept pans out, GitVersion will probably be amended in the future so that its branch normalization logic will automatically update the refspecs so when fetching, notes will automatically be included. To do this yourself, update your refspec in the /.git/config file to also fetch notes whenever fetching from the remote:

```
[remote "origin"]
	fetch = +refs/notes/*:refs/notes/*
	fetch = +refs/heads/*:refs/remotes/origin/*
        # omitted for brevity
```


