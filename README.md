# How it works

You have 2 options to run this project, docker or with visual studio 2022.

# How to get this Repository to your local

You can clone this repository openning a CMD (Command Prompt) and write git clone https://github.com/ruiNovais9/powerplant.git , when is finish cd powerplant to go to the folder.

Note: To use with this command you need have installed git.

This will clone this repository to your local or you can just download zip on green Code button on repository page.

# Docker

You need to have docker installed and running it.

Open a CMD (Command Prompt) in the folder where you cloned the repository (you should be on same path of docker-compose.yml and DockerFile) and write:

docker-compose up --build

With that is created a image and execute a container with the name PowerPlant-project.

After this you can open the browser and put: http://localhost:8888/swagger and use swagger to do the request, or can use a postman to the url: http://localhost:8888/productionplan

# Visual Studio 2022

Go to the folder PowerPlantAPI and open the PowerPlantAPI.sln, rebuild solution and right click on project PowerPlantAPI and select
the option "Set as Startup project" and below of help menu you have a "play" icone click on a right small icon and pick http or IIS express.
Then press the "play" with http description on front of the image and with this you have the project running.

After this you can open the browser and put: http://localhost:8888/swagger and use swagger to do the request, or can use a postman to the url: http://localhost:8888/productionplan

# How to test using swagger or postman

To use swagger (http://localhost:8888/swagger) open the green menu /productionplan and click on try it out and past the request you want below and click on execute, below you will have the result.

On postman create a new tab and pick a option (default is get) POST and write the URL: http://localhost:8888/productionplan , then on option body select raw, it will appear the option TEXT, change that value to JSON, then past the request you want below.

# Note

I didn't use System.Linq or interfaces (like IComparable) because I didn't know if I could use them or not, I order the list by cost and efficiency the way I did because of this.
The solution to order the list could be OrderBy and ThenByDEcending, the other solution could be to implement IComparable.


