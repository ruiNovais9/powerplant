# Hot it works

You have 2 options to run this project, docker or with visual studio.

# Docker:

You need to have docker installed and running it.

Clone a repository and open a CMD in the folder where you cloned the repository and write:

docker-compose up --build

With that is created a image and execute a container with the name PowerPlant-project.

After this you can open the browser and put: http://localhost:8888/swagger , or can use a postman to the url: http://localhost:8888/productionplan

# Visual Studio:

Clone a repository, go to the folder PowerPlantAPI and open the PowerPlantAPI.sln, rebuild solution and right click on project PowerPlantAPI and select
the option "Set as Startup project" and below of help menu you have a "play" icone click on a right small icon and pick http or IIS express.
Then press the "play" with http description on front of the image and with this you have the project running.

After this you can open the browser and put: http://localhost:8888/swagger , or can use a postman to the url: http://localhost:8888/productionplan

