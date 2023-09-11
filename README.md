# Hot it works

You have 2 options to run this project, docker or with visual studio 2022.

# Docker

You need to have docker installed and running it.

Clone a repository and open a CMD (Command Prompt) in the folder where you cloned the repository (you should be on same path of docker-compose.yml and DockerFile) and write:

docker-compose up --build

With that is created a image and execute a container with the name PowerPlant-project.

After this you can open the browser and put: http://localhost:8888/swagger and use swagger to do the request, or can use a postman to the url: http://localhost:8888/productionplan

# Visual Studio 2022

Clone a repository, go to the folder PowerPlantAPI and open the PowerPlantAPI.sln, rebuild solution and right click on project PowerPlantAPI and select
the option "Set as Startup project" and below of help menu you have a "play" icone click on a right small icon and pick http or IIS express.
Then press the "play" with http description on front of the image and with this you have the project running.

After this you can open the browser and put: http://localhost:8888/swagger and use swagger to do the request, or can use a postman to the url: http://localhost:8888/productionplan

Notes: I didn't use System.Linq or interfaces (like IComparable) because I didn't know if I could use them or not, I order the list by cost and efficiency the way I did because of this.
The solution to order the list could be OrderBy and ThenByDEcending, the other solution could be to implement IComparable.
