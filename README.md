# .NET Core Templates

![GitHub stars](https://img.shields.io/github/stars/ealenn/templates-dotnet?style=for-the-badge)
![Nuget](https://img.shields.io/nuget/dt/Ealen.Dotnet.Templates?style=for-the-badge)
![Nuget](https://img.shields.io/nuget/v/Ealen.Dotnet.Templates?style=for-the-badge)

## Install

### Install Templates
```bash
~$ dotnet new -i Ealen.Dotnet.Templates
```

```bash
~$ dotnet new --list

Templates                          Short Name          Language          Tags
-------------------------------------------------------------------------------------------------------------- 
Ealen .NET Core API                ealen-api           [C#]              Ealen/Common/Api
...
```

## Use

## Clone your repository

```bash
~$ git clone https://.../My.New.Repository
Cloning into My.New.Repository... 
warning: You appear to have cloned an empty repository. 
~$ cd My.New.Repository
```

## Generate template
``` bash
~$ dotnet new ealen-api --namespace My.New.Repository --repository ealen/example

The template "Ealen .NET Core API" was created successfully. 

Processing post-creation actions...
Restore succeeded. 
```

``` bash
/My.New.Repository
│   README.md
│   Api.My.New.Repository.sln
|   .gitignore
│   
└───src
│   │   
│   └───Api.My.New.Repository
│       │   Api.My.New.Repository.csproj
│       │   Startup.cs
│       │   ...
│   
└───tests
│   │   
│   |───Api.My.New.Repository.IntegrationTests
│   │   │   ...
│   │
│   └───Api.My.New.Repository.UnitTests
│       │   ...
│   
└───.github
│   │   
│   └───ISSUE_TEMPLATE
│   │   │   ...
│   │   
│   └───workflows
│   │   │   ...
|
```
