@echo off
@title �Զ����������������������Ŀ

set basedir="%~dp0"
set basedir
cd /d %basedir%



@echo.
@echo.
@echo.
@echo\&echo  ---------- ������� ----------

dotnet new sln -n GSA2TENET4StarTrek.Voyager





@echo.
@echo.
@echo.
@echo\&echo  ---------- ������gRPC��� �� gPRCTouch 3 ----------

dotnet new web -lang C# -f net6.0 -n gPRC4AspNetCore -o gPRCTouch\gPRC4AspNetCore\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s gPRCTouch gPRCTouch\gPRC4AspNetCore\src

dotnet new wpf -lang C# -f net6.0 -n gPRC4ClientApp1 -o gPRCTouch\gPRC4ClientApp1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s gPRCTouch gPRCTouch\gPRC4ClientApp1\src

dotnet new console -lang C# -f net6.0 -n gPRC4ClientConsole1 -o gPRCTouch\gPRC4ClientConsole1\src
dotnet sln GSA2TENET4StarTrek.Voyager.sln add -s gPRCTouch gPRCTouch\gPRC4ClientConsole1\src





::@echo\&echo ������Ŀ�Զ����������ѽ�����600 ����Զ��˳����Զ���������
::timeout /t 600

@echo.
@echo.
@echo.
@echo.
@echo.
@echo\&echo ������Ŀ�Զ�������ϣ��밴������˳���
pause>nul 
exit