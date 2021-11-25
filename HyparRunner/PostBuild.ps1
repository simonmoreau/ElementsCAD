param ($Configuration, $TargetName, $ProjectDir, $TargetPath, $TargetDir)
write-host $Configuration
write-host $TargetName
write-host $ProjectDir
write-host $TargetPath
write-host $TargetDir

# sign the exe
$thumbPrint = "e729567d4e9be8ffca04179e3375b7669bccf272"
$cert=Get-ChildItem -Path Cert:\CurrentUser\My -CodeSigningCert | Where { $_.Thumbprint -eq $thumbPrint}

Set-AuthenticodeSignature -FilePath $TargetPath -Certificate $cert -IncludeChain All -TimestampServer "http://timestamp.comodoca.com/authenticode"


# Copy the exe file
# $ElementCADFolder = ((get-item $ProjectDir).parent.parent.FullName + "\ElementsCADWindow\bin\Debug")
$ElementCADFolder = "C:\Users\Simon\Github\AutoCAD\ElementsCAD\ElementsCADWindow\bin\Debug"

if (Test-Path $ElementCADFolder) {
    try {
        # Remove previous versions
        if (Test-Path ($ElementCADFolder  + "\" + $TargetName)) { Remove-Item -R ($ElementCADFolder  + "\" + $TargetName) }

        # Create a folder for the runner
        
        # create the runner folder
        New-Item -ItemType Directory -Path ($ElementCADFolder  + "\" + $TargetName)

        # Copy the exe file
        xcopy /Y ($TargetDir) ($ElementCADFolder  + "\" + $TargetName)
    }
    catch {
        Write-Host "Something went wrong"
    }
}