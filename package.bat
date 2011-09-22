call clean.bat
echo %time% >> version.txt
tools\7-Zip\7z.exe a Dinh.Genetic.7z *
del version.txt
