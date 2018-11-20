@echo off

del outputresult.txt

call echo Excute On

setlocal EnableDelayedExpansion

set fbspath=%cd%\fbs

set jsonpath=%cd%\json

call echo !fbspath!
call echo !jsonpath!

flatc.exe -n -o %cd%\cs\ !fbspath! !jsonpath! >> outputresult.txt
flatc.exe -b -o %cd%\bin\Table !fbspath! !jsonpath! >> outputresult.txt


for /f "delims=," %%i in (outputresult.txt)do (
	call echo %%i
	pause
) 

call echo Excute Off