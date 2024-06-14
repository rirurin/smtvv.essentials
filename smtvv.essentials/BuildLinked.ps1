# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/smtvv.essentials/*" -Force -Recurse
dotnet publish "./smtvv.essentials.csproj" -c Release -o "$env:RELOADEDIIMODS/smtvv.essentials" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location