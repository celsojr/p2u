## p2u (Paste to Unix) [![NuGet Version](https://img.shields.io/nuget/v/p2u.svg)](https://www.nuget.org/packages/p2u)

Paste text content with Unix-like line endings into Windows Terminal without extra line wrapping or spaces. That includes Cmd and Vim.

### Installation

It requires the .NET Core 3.0+ runtime that you can download from here [dot.net](https://dot.net)

*.NET Core is a cross-platform version of .NET for building websites, services, and console apps.*

```
$ dotnet tool install --global p2u
```

After installation, copy any bash multi-line commands like this from the internet

```
helm install --name redis-cluster \
  --set cluster.slaveCount=3 \
  --set password=password \
  --set securityContext.enabled=true \
  --set securityContext.fsGroup=2000 \
  --set securityContext.runAsUser=1000 \
  --set volumePermissions.enabled=true \
  --set master.persistence.enabled=true \
  --set master.persistence.path=/data \
  --set master.persistence.size=8Gi \
  --set master.persistence.storageClass=manual \
  --set slave.persistence.enabled=true \
  --set slave.persistence.path=/data \
  --set slave.persistence.size=8Gi \
  --set slave.persistence.storageClass=manual \
stable/redis
```

### Example 

Convert the line endings by doing

```
$ p2u --wsl

Line endings converted!
Try to hit the right mouse button now (or CTRL+SHIFT+V)...
```

And then paste into Windows Terminal like you do in other Unix system terminals

### How to use
```
USAGE:
Copy content from anywhere and run it before pasting into Wsl, which is the default:
  p2u --wsl
And before pasting into Cmd without any feedback messages:
  p2u --cmd --silent

  -s, --silent        Run without print any feedback message
  -c, --cmd           Paste text content into cmd/DOS with Windows Terminal
  -v, --vim           Paste text content into Vim with Windows Terminal
  -p, --powershell    Paste text content into Powershell with Windows Terminal
  -b, --git-bash      Paste text content into Git Bash with Windows Terminal
  -w, --wsl           (Default: true) Paste text content into WSL (Windows Subsystem for Linux) with Windows Terminal
  --help              Display this help screen.
```

### How to uninstall

You can uninstall the tool using the following command
```
$ dotnet tool uninstall --global p2u
```

### How to build and install from source
Run the **Powershell** script from the root folder
```
PS> .\dev-build.ps1
```