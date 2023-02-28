<h1  align="center">API Client Sample</h1>

<h3  align="center">API Restful Client com .NET6, C#, DDD, CQRS, xUnit, MongoDB, authentication JWT.</h3>

<p  align="center">

![Badge](https://img.shields.io/badge/license-MIT-brightgreen?style=flat) ![Badge](https://img.shields.io/badge/build-sucess-brightgreen?style=flat) ![Badge](https://img.shields.io/badge/test-sucess-brightgreen?style=flat)
</p>

Sumário
===========
<!--ts-->
* [Executar o Projeto](#executar-o-projeto)
	* [Criar imagem e Container MongoDB](#criar-imagem-e-container-mongodb)
	* [Comandos MongoDB](#comandos-mongodb)
	* [Executar API](#executar-api)
	* [Executar Teste](#executar-teste)
	* [Importar Postman](#importar-postman)
* [Publicar no Docker](#publicar-no-docker)
* [ Ferramentas](#ferramentas)
* [Autor(a)](#autora)
<!--te-->

---
<h4 align="center"> 🚀API Client Sample 🚀 Concluído✅ </h4>

---

## Executar o Projeto

### Criar imagem e Container MongoDB

👉 Abra o CMD e digite os comandos abaixo
```bash
# Criar network para comunicação do banco e aplicação
docker network create --driver bridge apiCrud-bridge

# Verifique se foi criada
docker network ls

# Criar container do mongo
docker run -d --network apiCrud-bridge --name mongoDB -p 27017:27017 mongo:6.0.4

# Verifique se o container foi criado
docker container ls
```
### Comandos MongoDB
👉 Ainda no cmd digite

```bash
# Abrir o executável do mongodb no docker
docker exec -it mongoDB bash

# iniciar o mongo
mongosh

# Criar a collection
use ManageClient

# Criar o document
db.createCollection("User")

# Inserir o registro do usuário admin
db.User.insertOne({"_id": "69f2cd42-b6a4-418f-a4c2-ece3d719ed6f", "Name":"Admin", "Password": "admin", "IsActive": true, "Role"  :  "Admin"})
```

### Executar API
⚠️ Pré-requesitos:
		
		✅ Visual Studio 2022
		✅ .NET SDK 6.0
		✅ Docker
		✅ MongoDBCompass, Robo 3T or Other

👉 Abra o projeto no Visual Studio 2022

⚠️ Abra o arquivo appsettings.Development.json dentro do projeto AMD_CrudClientDDDSample.Services :
<p>❗Comente o trecho "Connection": "mongodb://mongoDB:27017" </p>
<p>❗Descomente o trecho "Connection": "mongodb://localhost:27017"</p>

➡️Abra o Package Manager Console : View -> Other windows -> Package Manager Console

```bash
# Restore as dependências
dotnet restore

# Build o projeto
dotnet build

# Insira o caminho que está o projeto Services
cd .\AMD_CrudClientDDDSample.Services

# Execute o Projeto com o comando abaixo
dotnet run --project AMD_CrudClientDDDSample.Services.csproj
```
👉 No browser acesse a url: https://localhost:7113/swagger/index.html

🌟 Ou execute de forma mais simples, clique com o botão direito no projeto AMD_CrudClientDDDSample.Services e escolha a opção "Set as Startup Project"

➡️ Aperte a tecla F5

### Executar Teste
👉 No Package Manager Console digite o comando
```bash
dotnet test
```
🌟 Ou abra a aba Test Explorer : View -> Test Explorer
👉 Pressione o botão▶️ (Ctrl+R, T)

### Importar Postman
👉 No Postman clique em import -> Depois upload files -> Localize o arquivo  API_Client_DDD_Sample.postman_collection.json nas pasta "doc" no projeto.

👉 No Postman clique em import -> Depois upload files -> Localize o arquivo  API_Client_DDD_Sample Docker.postman_collection.json nas pasta "doc" no projeto.

👉 Clique na aba Evironments -> Import -> upload files -> Localize o arquivo local.postman_environment.json na pasta "doc" no projeto.

---
## Publicar no Docker

⚠️Abra o arquivo appsettings.Development.json dentro do projeto AMD_CrudClientDDDSample.Services:
<p>❗ Comente o trecho "Connection": "mongodb://localhost:27017" </p>
<p>❗ Descomente o trecho "Connection": "mongodb://mongoDB:27017" </p>

👉 No cmd 
```bash
# Digite caminho do projeto onde está o dockerfile, por exemplo no meu 
cd D:\\Workspaces\AMD_CrudClientDDDSample\src\

# Após entrar na pasta digite o comando para criar a imagem
docker build -f AMD_CrudClientDDDSample/Dockerfile -t  amd/crud-sample:1.0 .

# Criando o container
docker run -d --network apiCrud-bridge -p 7113:80 --name APICrudSample amd/crud-sample:1.0

```
👉 No browser acesse a url: http://localhost:7113/swagger/index.html

---

###  Ferramentas:
<p  align="left">

<a  href=""  target="_blank"  rel="noreferrer"><img  src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg"  alt="csharp"  width="40"  height="40"/></a>  <a  href="https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.406-windows-x64-installer"  target="_blank"  rel="noreferrer"><img  src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original-wordmark.svg"  alt="dotnet"  width="40"  height="40"/></a>  <a  href="https://docs.docker.com/desktop/install/windows-install/"  target="_blank"  rel="noreferrer"><img  src="https://raw.githubusercontent.com/devicons/devicon/master/icons/docker/docker-original-wordmark.svg"  alt="docker"  width="40"  height="40"/></a>  <a  href="https://www.mongodb.com/"  target="_blank"  rel="noreferrer"><img  src="https://raw.githubusercontent.com/devicons/devicon/master/icons/mongodb/mongodb-original-wordmark.svg"  alt="mongodb"  width="40"  height="40"/></a>  <a  href="https://git-scm.com/"  target="_blank"  rel="noreferrer"><img  src="https://www.vectorlogo.zone/logos/git-scm/git-scm-icon.svg"  alt="git"  width="40"  height="40"/></a>  <a  href="https://postman.com"  target="_blank"  rel="noreferrer"><img  src="https://www.vectorlogo.zone/logos/getpostman/getpostman-icon.svg"  alt="postman"  width="40"  height="40"/></a>
</p>

---
## Autor(a)

Angelica Dupim