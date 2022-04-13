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

dotnet new wpf -lang C# -f net6.0 -n MSWebView2WPF4Appx1 -o MSWebView2\MSWebView2WPF4Appx1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MSWebView2 MSWebView2\MSWebView2WPF4Appx1\src





@echo.
@echo.
@echo.
@echo\&echo  ---------- MahApps.Metro ﹫ MahAppsMetroUI 1 ----------

dotnet new wpf -lang C# -f net6.0 -n MahAppsMetroAppx -o MahAppsMetroUI\MahAppsMetroAppx\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MahAppsMetroUI MahAppsMetroUI\MahAppsMetroAppx\src





@echo.
@echo.
@echo.
@echo\&echo  ---------- MaterialDesignThemes ﹫ MaterialDesignUI 2 ----------

dotnet new wpf -lang C# -f net6.0 -n MaterialDesignAppx -o MaterialDesignUI\MaterialDesignAppx\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MaterialDesignUI MaterialDesignUI\MaterialDesignAppx\src

dotnet new wpf -lang C# -f net6.0 -n MaterialDesign4MahApps -o MaterialDesignUI\MaterialDesign4MahApps\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MaterialDesignUI MaterialDesignUI\MaterialDesign4MahApps\src





@echo.
@echo.
@echo.
@echo\&echo  ---------- NTreePackModule ﹫ NTreePackSDK 3 ----------

dotnet new console -lang C# -f net6.0 -n NTreePackSDK4ServerCon1 -o NTreePackSDK\NTreePackSDK4ServerCon1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s NTreePackSDK NTreePackSDK\NTreePackSDK4ServerCon1\src

dotnet new console -lang C# -f net6.0 -n NTreePackSDK4ClientCon1 -o NTreePackSDK\NTreePackSDK4ClientCon1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s NTreePackSDK NTreePackSDK\NTreePackSDK4ClientCon1\src

dotnet new console -lang C# -f net6.0 -n NTreePackSDK4DecodeCon1 -o NTreePackSDK\NTreePackSDK4DecodeCon1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s NTreePackSDK NTreePackSDK\NTreePackSDK4DecodeCon1\src





@echo.
@echo.
@echo.
@echo\&echo  ---------- MQTT Hands-on Applications ﹫ MQTT666Touch 5 ----------

dotnet new wpf -lang C# -f net6.0 -n MQTT2Appx4WPF1 -o MQTT666Touch\MQTT2Appx4WPF1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MQTT666Touch MQTT666Touch\MQTT2Appx4WPF1\src

dotnet new console -lang C# -f net6.0 -n MQTT2Appx4Con1 -o MQTT666Touch\MQTT2Appx4Con1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MQTT666Touch MQTT666Touch\MQTT2Appx4Con1\src

dotnet new blazorserver -lang C# -f net6.0 -n MQTT2Blazor4ServerApp1 -o MQTT666Touch\MQTT2Blazor4ServerApp1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MQTT666Touch MQTT666Touch\MQTT2Blazor4ServerApp1\src

dotnet new blazorwasm -lang C# -f net6.0 -n MQTT2Blazor4WasmApp1 -o MQTT666Touch\MQTT2Blazor4WasmApp1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MQTT666Touch MQTT666Touch\MQTT2Blazor4WasmApp1\src

dotnet new mvc -lang C# -f net6.0 -n MQTT2MVC4WebApp1 -o MQTT666Touch\MQTT2MVC4WebApp1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s MQTT666Touch MQTT666Touch\MQTT2MVC4WebApp1\src





@echo.
@echo.
@echo.
@echo\&echo  ---------- TDengine 时间序列数据库 ﹫ TDengineTouch 1 ----------

dotnet new wpf -lang C# -f net6.0 -n TDengine4Appx1 -o TDengineTouch\TDengine4Appx1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s TDengineTouch TDengineTouch\TDengine4Appx1\src

dotnet new console -lang C# -f net6.0 -n TDengine4Con1 -o TDengineTouch\TDengine4Con1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s TDengineTouch TDengineTouch\TDengine4Con1\src





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
