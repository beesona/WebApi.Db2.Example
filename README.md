# DB2.WebApi.Template

aspnet:3.1 web api with DB2 Data Access to an IBMi/Z system. Probably other things too, my use is simply DB2 to Iseries.

## Notes:
- You'll need a valid 11.1 license for hosting this in a linux docker container. Put it in the solution root.
  - A license can be obtained from IBM's Passport Advantage portal (https://www.ibm.com/support/pages/node/282227). If you are running this as a windows app only, the license that comes with the nuget package for windows will work without the need of any extra licensing.
- You'll need to set a valid DB2 IBMi Connection string in the appsettings.json file.
- This project has a dependency on an image from my DockerHub repository: beesona/aspnet3.1-db2:latest. Check it out on Dockerhub if you are interested in the contents (https://hub.docker.com/repository/docker/beesona/aspnet3.1-db2) but really it's just a simplification of the configuration needed for DB2 in a linux container.
- If you have an existing IBM DATA DRIVER install on your machine, you will probably need to uninstall it as version conflicts may arise when running locally. (Running in a docker container shouldn't pose a problem).
