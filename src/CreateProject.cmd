@echo off
@title 自动创建解决方案及各隶属项目

set basedir="%~dp0"
set basedir
cd /d %basedir%



@echo.
@echo.
@echo.
@echo\&echo  ---------- 解决方案 ----------

dotnet new sln -n GSA2TENET4StarTrek.Voyager





@echo.
@echo.
@echo.
@echo\&echo  ---------- 高性能gRPC框架 ﹫ gPRCTouch 3 ----------

dotnet new web -lang C# -f net6.0 -n gPRC4AspNetCore -o gPRCTouch\gPRC4AspNetCore\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s gPRCTouch gPRCTouch\gPRC4AspNetCore\src

dotnet new wpf -lang C# -f net6.0 -n gPRC4ClientApp1 -o gPRCTouch\gPRC4ClientApp1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s gPRCTouch gPRCTouch\gPRC4ClientApp1\src

dotnet new console -lang C# -f net6.0 -n gPRC4ClientConsole1 -o gPRCTouch\gPRC4ClientConsole1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s gPRCTouch gPRCTouch\gPRC4ClientConsole1\src





::@echo\&echo 所有项目自动创建工作已结束，600 秒后将自动退出本自动创建程序。
::timeout /t 600

@echo.
@echo.
@echo.
@echo.
@echo.
@echo\&echo 所有项目自动创建完毕，请按任意键退出。
pause>nul 
exit
