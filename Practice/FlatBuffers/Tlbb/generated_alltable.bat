
@echo off

del outputresult.txt

call echo Excute On

cd Generated/GeneratedAllTable/Fbs
setlocal EnableDelayedExpansion
for /R %%s in (*.fbs) do (
	set fbspath=%%s
	set tr=!fbspath:\Fbs\=\Json\%!
	set jsonpath=!tr:~0,-4!%.json
	call echo !fbspath!
	call echo !jsonpath!
	..\..\..\flatc.exe -n -o ..\..\GeneratedAllCSharp\ !fbspath! !jsonpath! >> ..\..\..\outputresult.txt
	set tr2=%%~dps
	set tablepath=!tr2:%cd%=!
    ..\..\..\flatc.exe -b -o ..\..\GeneratedAllTable\Table!tablepath! !fbspath! !jsonpath! >> ..\..\..\outputresult.txt
)

cd ../../../

cd Generated/GeneratedAllTable/OriginalFbs
setlocal EnableDelayedExpansion
for /R %%s in (*.fbs) do (
	set originalfbspath=%%s
	set tr=!originalfbspath:\OriginalFbs\=\OriginalJson\%!
	set originaljsonpath=!tr:~0,-4!%.json
	call echo !originalfbspath!
	call echo !originaljsonpath!
	..\..\..\flatc.exe -n -o ..\..\GeneratedAllCSharp\ !originalfbspath! !originaljsonpath! >> ..\..\..\outputresult.txt
	set tr2=%%~dps
	set originaltablepath=!tr2:%cd%=!
    ..\..\..\flatc.exe -b -o ..\..\GeneratedAllTable\OriginalTable!originaltablepath! !originalfbspath! !originaljsonpath! >> ..\..\..\outputresult.txt
)

cd ../../../

for /f "delims=," %%i in (outputresult.txt)do (
	call echo %%i
	pause
) 

call echo Excute Off
