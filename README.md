<div id="top"></div>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

## Lesson-DotNet-ConfigServer

<!-- ABOUT THE PROJECT -->
## About The Project

The main idea of this project is to create a instance of **Spring Cloud Config** loading the secrets from a **key vault** and show how to use it on .NET 6.0 applications.

Here's why:
* Spring Cloud Config is a well know tool for externalized configuration in distributed systems.
* A Key Vault is excellent to save sensitive information, once we are going to save the applications settings on the repositories, anyone with access to that repository could read the secrest, so to protect these informations we need to store it somewhere else, and a Key Vault fits like a glove in this scenario, we will be using a Key Vault from HashiCorp

* .NET 6.0 is the most advanced and fast framework to develop applications using C#
* It's fun! 🚀🎉

### Built With

This section should list any major frameworks/libraries used to bootstrap your project. Leave any add-ons/plugins for the acknowledgements section. Here are a few examples.

* [Spring Cloud Config](https://cloud.spring.io/spring-cloud-config/reference/html/)
* [Maven](https://maven.apache.org/)
* [Vault](https://learn.hashicorp.com/vault?track=getting-started#getting-started/)
* [net6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/)
* [Steeltoe](https://docs.steeltoe.io/api/v3/configuration/)

<!-- GETTING STARTED -->
## Getting Started

#### Running it on docker: 

Use the file docker-compose.yaml which has the fallowing content 

```yaml
version: "3.9"
services:  
  key-vault:
    container_name: key-vault
    image: vault
    environment:
      VAULT_DEV_ROOT_TOKEN_ID: myroot
      VAULT_DEV_LISTEN_ADDRESS: 0.0.0.0:8200
    ports:
      - "8200:8200"
  config-server:
    depends_on: 
      - key-vault
    container_name: config-server
    image: canaldopubdev/config-server
    links:
      - "key-vault:vault"
    environment:      
      VAULT_TOKEN: myroot
      VAULT_HOST: key-vault
      VAULT_PORT: 8200
      VAULT_SCHEME: http
      VAULT_URI: http://key-vault:8200
      GIT_REPOSITORY_URI: https://github.com/Pub-Dev/Lesson-DotNet-ConfigServer-Configs
      GIT_DEFAULT_BRANCH: main   
    ports:
      - "8888:8888"
```

<!-- USAGE EXAMPLES -->
## Usage

Keep the Config Server running, using docker-compose for example, the api needs this to be up in order to get the configurations 🤓

There is an WebApi called Sample inside the folder /apps

#### Running the Solutions

##### Product API

```sh
dotnet run src/product-api/product-api.csproj
```

##### User  API

```sh
dotnet run src/user-api/user-api.csproj
```

When you access the */* endpoint on the applications you will see something like this

![image](https://user-images.githubusercontent.com/3129978/150921808-2570b7ae-cc6b-4bf7-9db6-56ca7499dfa2.png)

on the file application.
```json
"Spring": {
  "Application": {
    "Name": "api"
  },
  "Cloud": {
    "Config": {
      "Uri": "http://localhost:8888",
      "FailFast": true 
    }
  }
}  
```

* *Spring.Application.Name* = application's name needs to be the same as stored on the repository where the config is stored, ill get there in a minute don't worry
* *Spring.Cloud.Config.Uri* = this is the spring cloud url
* *Spring.Cloud.Config.FailFast* = if set to true the application will not start up if the config server is not found

### Storing the configuration and Settings

As you might have realised the configurations and settings are inside another repository [Config-Repository](https://github.com/Pub-Dev/Lesson-DotNet-ConfigServer-Configs/)

Within this config you will find the following structure

![image](https://user-images.githubusercontent.com/3129978/150923013-a1133d9a-ee45-4a21-9aab-0bba5fb4e0c1.png)

the spring cloud config works using layers, and this is the hierarchy:
* first it will loads the configuration on the file *application.yml*
* then [Spring.Application.Name].yml if exists
* then [Spring.Application.Name]-[profile].yml 
 * profile in this case is set as "Development", you can change it on the file /tests/Properties/launchSettings.json changing the `ASPNETCORE_ENVIRONMENT`

if we change the `ASPNETCORE_ENVIRONMENT` to *Production* and run the api the new response on the *https://localhost:7180* endpoint will be 

![image](https://user-images.githubusercontent.com/3129978/150924486-01916077-7a64-4b31-b6b6-e58fd8d108f6.png)

### Using Vault

If you need help you can access this [link](https://cloud.spring.io/spring-cloud-config/multi/multi__spring_cloud_config_server.html#vault-backend)

<!-- CONTACT -->
## Contact

**Store**
- 👕https://loja.pubdev.com.br/

**Social Networks**
- 📸 https://www.instagram.com/pub_dev/
- 🚀 https://discord.gg/EvD6Um5Jw2
- 🏢 https://www.linkedin.com/company/pubdev/

**Humberto Rodrigues**
- 🏢 https://www.linkedin.com/in/humbberto
- 📸 [@1bberto](https://instagram.com/1bberto)
- 📧 humberto_henrique1@live.com

**Rafael Nagai**
- 🏢 https://www.linkedin.com/in/rafakenji
- 📸 [@rafakenji23](https://instagram.com/rafakenji23)
- 📧 rafakenji23@gmail.com

Project Link: [https://github.com/Pub-Dev/Lesson-DotNet-ConfigServer](https://github.com/Pub-Dev/Lesson-DotNet-ConfigServer)

<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Spring Cloud Config](https://github.com/spring-cloud-samples/configserver)

<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/Pub-Dev/Lesson-DotNet-ConfigServer.svg?style=for-the-badge
[contributors-url]: https://github.com/Pub-Dev/Lesson-DotNet-ConfigServer/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Pub-Dev/Lesson-DotNet-ConfigServer.svg?style=for-the-badge
[forks-url]: https://github.com/Pub-Dev/Lesson-DotNet-ConfigServer/network/members
[stars-shield]: https://img.shields.io/github/stars/Pub-Dev/Lesson-DotNet-ConfigServer.svg?style=for-the-badge
[stars-url]: https://github.com/Pub-Dev/Lesson-DotNet-ConfigServer/stargazers
[issues-shield]: https://img.shields.io/github/issues/Pub-Dev/Lesson-DotNet-ConfigServer.svg?style=for-the-badge
[issues-url]: https://github.com/Pub-Dev/Lesson-DotNet-ConfigServer/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/company/pubdev
