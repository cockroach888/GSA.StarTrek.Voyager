@echo off
@title GIT �Զ����͵�Զ�ֿ̲�

set basedir="%~dp0"
set basedir


@echo\&echo ���͵� GitHub Զ�ֿ̲�
git push github --all

::@echo\&echo ���͵� Bitbucket Զ�ֿ̲�
::git push bitbucket --all

::@echo\&echo ���͵� Azure Զ�ֿ̲�
::git push azure --all

@echo\&echo ���͵� Gitee Զ�ֿ̲�
git push gitee --all

::@echo\&echo ���͵� Origin Զ�ֿ̲�
::git push origin --all

@echo\&echo �鿴���زֿ�״̬
git status


@echo.
@echo.
@echo.
@echo.
@echo.
@echo\&echo �������Զ�ֿ̲����ͣ��밴������˳���
pause>nul 
exit