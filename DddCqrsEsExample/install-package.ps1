write-host "*** Installing packages to local repository..."
gci -recurse -filter packages.config | %{ 
    write-host "Installing $($_.Fullname)"
    nuget.exe install $_.fullname -outputdirectory Packages -source "https://go.microsoft.com/fwlink/?LinkID=230477"
}

write-host "*** Re-building repositories.config file..."
$location = get-location
$packageConfigFiles = (gci -recurse -filter packages.config)
$repositoriesConfigFile = "packages\repositories.config"

set-content $repositoriesConfigFile '<?xml version="1.0" encoding="utf-8"?>'
add-content $repositoriesConfigFile '<repositories>'

foreach ($packageConfigFile in $packageConfigfiles) {
    $relativePath = $packageConfigFile.Fullname.Replace($location, "..")
    add-content $repositoriesConfigFile "  <repository path=`"$relativePath`" />"
}

add-content $repositoriesConfigFile '</repositories>'
