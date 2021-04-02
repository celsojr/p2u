dotnet nuget push './src/bin/out/p2u.1.0.3.nupkg' `
  --api-key $Env:NUGET_APY_KEY `
  --source 'https://api.nuget.org/v3/index.json'
