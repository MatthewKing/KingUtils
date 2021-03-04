$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$project =  "$root/src/KingUtils/KingUtils.csproj"
$output = "$root/artifacts"
dotnet msbuild "/t:Restore;Build;Pack" "/p:Configuration=Release" "/p:PackageOutputPath=$output" $project
dotnet pack $project --configuration Release --output "$output"
