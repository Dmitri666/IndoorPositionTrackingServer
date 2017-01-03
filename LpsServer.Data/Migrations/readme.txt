1.
Enable-Migrations -ProjectName LpsServer.Data -StartUpProjectName LpsServer -Force

2.
Add-Migration "NewScript1" -ProjectName LpsServer.Data -StartUpProjectName LpsServer

3.
Update-Database -ProjectName LpsServer.Data -StartUpProjectName LpsServer


-----------------

Install-Package EntityFramework
Install-Package AutoMapper
