# .NET Core Templates

![GitHub stars](https://img.shields.io/github/stars/ealenn/templates-dotnet?style=for-the-badge)
![Nuget](https://img.shields.io/nuget/dt/Ealen.Dotnet.Templates?style=for-the-badge)
![Nuget](https://img.shields.io/nuget/v/Ealen.Dotnet.Templates?style=for-the-badge)

<!-- vscode-markdown-toc -->
* [Available Templates](#AvailableTemplates)
* [Generate Template](#GenerateTemplate)
	* [.NET CLI](#NETCLI)
	* [Docker](#Docker)
* [Example](#Example)

<!-- vscode-markdown-toc-config
	numbering=false
	autoSave=true
	/vscode-markdown-toc-config -->
<!-- /vscode-markdown-toc -->

## <a name='AvailableTemplates'></a>Available Templates

``` bash
# With .NET CLI
~$ dotnet new --list
# With Docker
~$ docker run --rm ealen/dotnet-templates
```

``` md
Templates                          Short Name          Language          Tags
-----------------------------------------------------------------------------------------------
Ealen .NET Core API                ealen-api           [C#]              Ealen/Common/Api
...
```

| Parameters | Type                      | Example                     |
| ---------- | ------------------------- | --------------------------- |
| namespace  | C# Namespace              | `My.Repository`             |
| repository | GitHub User/Repository    | `ealen/dotnet-templates`    |

## <a name='GenerateTemplate'></a>Generate Template

### <a name='NETCLI'></a>.NET CLI

Install [templates nuget](https://www.nuget.org/packages/Ealen.Dotnet.Templates/) with 

```bash
~$ dotnet new --install Ealen.Dotnet.Templates
```

``` bash
~$ dotnet new ealen-api --namespace My.New.Repository --repository ealen/example

The template "Ealen .NET Core API" was created successfully. 

Processing post-creation actions...
Restore succeeded. 
```

### <a name='Docker'></a>Docker

``` bash
~$ docker run --rm -v $(pwd):/app ealen/dotnet-templates ealen-api --namespace My.New.Repository --repository ealen/example

The template "Ealen .NET Core API" was created successfully. 

Processing post-creation actions...
Restore succeeded. 
```

## <a name='Example'></a>Example

``` bash
# With .NET CLI
~$ dotnet new ealen-api --namespace My.New.Repository --repository ealen/example
# With Docker
~$ docker run --rm -v $(pwd):/app ealen/dotnet-templates ealen-api --namespace My.New.Repository --repository ealen/example
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
