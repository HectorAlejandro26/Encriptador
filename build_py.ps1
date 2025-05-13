if (-not (Test-Path "./bin_py")) {
    New-Item -Type Directory ./bin_py
}

if (-not $env:VIRTUAL_ENV) {
    & ./.venv/Scripts/Activate.ps1;
}

pyinstaller --onefile --console --name "HillCipher" --distpath .\bin_py --workpath "pyinstaller_temp" main.py;