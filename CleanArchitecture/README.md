# CleanArchitecture

Command to run the Client app in specific port (here using 44447 you can choose your port):
```
docker run -d -p 44447:44447 --name node --network backend-network node:latest npm start -- --host
```

Command to run the PostgreSQL Database in specific port (here using 5432 you can choose your port with some environment variable like server name, db name, username and password): 
```
docker run -d -p 5432:5432 --name clean.db --network backend-network -e POSTGRES_DB=cleanarchitecturedb -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres postgres:latest
```

Command to run the Backend application in specific port (here using 5001 for https & 5000 for http you can choose your port with some environment variable like Run Migrations - value true means migrations will takeplace and for https need to copy certificates as well for that you need to download the certificate first and here is the link): 
```
dotnet dev-certs https -ep $HOME/.aspnet/https/aspnetapp.pfx -p Yourconvenientpassword
```

```
dotnet dev-certs https --trust
```

```
docker run -d -p 5000:5000 -p 5001:5001 --name web --network backend-network -e ASPNETCORE_HTTP_PORTS=5000 -e ASPNETCORE_HTTPS_PORTS=5001 -e RUN_MIGRATIONS=true -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Password="Your password" -e ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx" -v "C:/httpcert:/https:ro" web:latest
```