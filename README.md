# Clean Architecture Template

Clean Architecture with dot net and react

## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (latest version)
- [Node.js](https://nodejs.org/) (latest LTS, only required if you are using Angular or React)

The easiest way to get started is to install the [.NET template](https://www.nuget.org/packages/Clean.Architecture.Solution.Template):

```
dotnet new install Clean.Architecture.Solution.Template::8.0.5
```

Once installed, create a new solution using the template. You can choose to use Angular, React, or create a Web API-only solution. Specify the client framework using the `-cf` or `--client-framework` option, and provide the output directory where your project will be created. Here are some examples:

To create a SPA with React and ASP.NET Core:

```bash
dotnet new ca-sln -cf React -o YourProjectName
```

If you already have an existing React app and want to add TypeScript manually, install the necessary dependencies:

```bash
npm install --save typescript @types/react @types/react-dom
```
