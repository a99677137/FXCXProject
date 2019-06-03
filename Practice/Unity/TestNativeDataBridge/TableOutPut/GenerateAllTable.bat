@echo off

cd TableOutPut

del %cd%\outputresult.txt

call echo Excute On

setlocal EnableDelayedExpansion

set fbspath=%cd%\Generate\Fbs

set jsonpath=%cd%\Generate\Json

call echo !fbspath!
call echo !jsonpath!

flatc.exe -n -o %cd%\Generate\cs\ !fbspath! !jsonpath! >> outputresult.txt
flatc.exe -b -o %cd%\Generate\bin\Table\ !fbspath! !jsonpath! >> outputresult.txt


for /f "delims=," %%i in (outputresult.txt)do (
	call echo %%i
	pause
) 

call echo Excute Off