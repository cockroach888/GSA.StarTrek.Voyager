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





@echo.
@echo.
@echo.
@echo\&echo  ---------- 高性能网络通信框架 ﹫ HP-Socket 2 ----------

dotnet new console -lang C# -f net6.0 -n HPSocket4ServerConsole1 -o HPSocket\HPSocket4ServerConsole1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s HPSocket HPSocket\HPSocket4ServerConsole1\src

dotnet new console -lang C# -f net6.0 -n HPSocket4ClientConsole1 -o HPSocket\HPSocket4ClientConsole1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s HPSocket HPSocket\HPSocket4ClientConsole1\src





@echo.
@echo.
@echo.
@echo\&echo  ---------- 轻量级跨平台服务器程序框架 ﹫ SuperSocketFrame 2 ----------

dotnet new console -lang C# -f net6.0 -n SuperSocket4ServerConsole1 -o SuperSocketFrame\SuperSocket4ServerConsole1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s SuperSocketFrame SuperSocketFrame\SuperSocket4ServerConsole1\src

dotnet new console -lang C# -f net6.0 -n SuperSocket4ClientConsole1 -o SuperSocketFrame\SuperSocket4ClientConsole1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s SuperSocketFrame SuperSocketFrame\SuperSocket4ClientConsole1\src





@echo.
@echo.
@echo.
@echo\&echo  ---------- Microsoft Edge WebView2 ﹫ MSWebView2 2 ----------

dotnet new winforms -lang C# -f net6.0 -n MSWebView4WinForm -o MSWebView2\MSWebView4WinForm\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MSWebView2 MSWebView2\MSWebView4WinForm\src

dotnet new wpf -lang C# -f net6.0 -n MSWebView4WPF -o MSWebView2\MSWebView4WPF\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MSWebView2 MSWebView2\MSWebView4WPF\src





@echo.
@echo.
@echo.
@echo\&echo  ---------- MahApps.Metro ﹫ MahAppsMetroUI 1 ----------

dotnet new wpf -lang C# -f net6.0 -n MahAppsMetroAppx -o MahAppsMetroUI\MahAppsMetroAppx\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MahAppsMetroUI MahAppsMetroUI\MahAppsMetroAppx\src





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
