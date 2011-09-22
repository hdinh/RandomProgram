pushd src
for /f "tokens=*" %%G in ('DIR /B /AD /S bin') do rmdir /S /Q "%%G"
for /f "tokens=*" %%G in ('DIR /B /AD /S obj') do rmdir /S /Q "%%G"
del /s /q /ah *.Cache *.suo *.user
del /s /q /a *.Cache *.suo *.user
popd

pushd tests
for /f "tokens=*" %%G in ('DIR /B /AD /S bin') do rmdir /S /Q "%%G"
for /f "tokens=*" %%G in ('DIR /B /AD /S obj') do rmdir /S /Q "%%G"
del /s /q /ah *.Cache *.suo *.user
del /s /q /a *.Cache *.suo *.user
popd
