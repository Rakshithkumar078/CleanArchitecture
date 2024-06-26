## Docker
Docker commands to run the application:
 
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


## Commands to run in all the environment 

Client CI 
```
docker run -d --name clientci --network backend-network -p 44447:44447 -e REACT_APP_BASE_URL=https://localhost:5001/api node:latest
```

Client QA
```
docker run -d --name clientqa --network backend-network -p 44448:44447 -e REACT_APP_BASE_URL=https://localhost:6001/api node:latest
```

Client Prod
```
docker run -d --name clientprod --network backend-network -p 44449:44447 -e REACT_APP_BASE_URL=https://localhost:7001/api node:latest
```

Web CI
```
docker run -d -p 5000:5000 -p 5001:5001 --name webci --network backend-network -e ASPNETCORE_HTTP_PORTS=5000 -e ASPNETCORE_HTTPS_PORTS=5001 -e ASPNETCORE_ENVIRONMENT=CI -e ASPNETCORE_Kestrel__Certificates__Default__Password="PearlArc@12345" -e ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx" -v "C:/httpcert:/https:ro" web:latest
```

Web QA
```
docker run -d -p 6000:6000 -p 6001:6001 --name webqa --network backend-network -e ASPNETCORE_HTTP_PORTS=6000 -e ASPNETCORE_HTTPS_PORTS=6001 -e ASPNETCORE_ENVIRONMENT=QA -e ASPNETCORE_Kestrel__Certificates__Default__Password="PearlArc@12345" -e ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx" -v "C:/httpcert:/https:ro" web:latest
```

Web prod
```
docker run -d -p 7000:7000 -p 7001:7001 --name webprod --network backend-network -e ASPNETCORE_HTTP_PORTS=7000 -e ASPNETCORE_HTTPS_PORTS=7001 -e ASPNETCORE_ENVIRONMENT=Production -e ASPNETCORE_Kestrel__Certificates__Default__Password="PearlArc@12345" -e ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx" -v "C:/httpcert:/https:ro" web:latest
```

Save the Docker Image as a Tarball - client 
```
docker save -o client.tar node:latest
```

Save the Docker Image as a Tarball - web
```
docker save -o web.tar web:latest
```

Save the Docker Image as a Tarball - MSSQL 
```
docker save -o sql.tar mcr.microsoft.com/mssql/server:latest
```