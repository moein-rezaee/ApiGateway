
@echo off

REM Define paths
set SOURCE_DIR=C:\Projects\Local\ApiGateway
set DEST_DIR=C:\Projects\SecurityWave\infrastructure_backend_apigateway

REM Navigate to source directory
cd /d %SOURCE_DIR%

REM Push changes to GitHub
git push origin main

REM Sync changes to destination directory using robocopy
robocopy %SOURCE_DIR% %DEST_DIR% /MIR /XD .git

REM Navigate to destination directory
cd /d %DEST_DIR%

REM Add, commit and push changes to GitLab
git add .
git commit -m "Sync from ApiGateway"
git push origin main
