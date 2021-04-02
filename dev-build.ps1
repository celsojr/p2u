dotnet tool uninstall -g p2u

dotnet build './src/p2u.csproj'

dotnet pack './src/p2u.csproj' --output './src/bin/out'

dotnet tool install -g p2u --add-source './src/bin/out'
