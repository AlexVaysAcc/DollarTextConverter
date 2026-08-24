# Currency Number-to-Words Converter

A modular, client-server C# application built on .NET 8.0 that converts currency amounts into their written word representations in both English and German. --- ##


Project Structure & Architecture

The solution (`DollarTextConverter`) is decoupled into three projects to enforce clean separation of concerns:

### Application Layers 
* **`Converter.Client`**: A graphical user interface (GUI) application. It handles user inputs (currency amount and target language), runs client-side validation, sends HTTP requests to the server, and renders the converted text result.

* **`Converter.Server`**: An ASP.NET Core Minimal/Web API that exposes HTTP endpoints. It acts as the orchestration layer, receiving payloads from the client and invoking the core logic. 

* **`Converter.Core`**: A lightweight class library containing the domain logic.
It implements an `IConverter` interface to achieve dependency inversion, utilizing an abstract `ConverterFactory` to dynamically route requests to either `EnglishConverter` or `DeutschConverter` classes.

### Verification Layers
* **`Converter.Core.Tests`**: A unit testing assembly dedicated to verifying exact string transformation outputs, boundary values, and language-switching algorithms. 
* **`Converter.Server.Tests`**: An API testing layer to validate route handlers, payload models, factory injection, and server error scenarios. 

### Process Flow
Converter.Client(WinForms GUI) -> Viladates (First validation) and sends HTTP request to Converter.Server (ASP.NET Core API) -> Validates 
and invokes Converter.Core (domain logic) -> Returns converted text back to Converter.Client for display.

## Build, Run, and Test Guide

### Prerequisites
*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
*   Windows OS (Required to compile and run the `Converter.Client` WinForms desktop wrapper).
*   An IDE like Visual Studio 2022 or higher.

### 1. Installation & Compilation
Clone the repository and restore dependencies across the entire solution:

```bash
# Clone the repository
git clone https://github.com
cd converter-app

# Restore NuGet packages and compile all projects
dotnet restore Converter.sln
dotnet build Converter.sln --configuration Release
```

### 2. Running the Test Suites
Run all automated tests across the Core algorithm library and Server controller layer to ensure stability:

```bash
# Execute all unit tests simultaneously
dotnet test Converter.sln --logger "console;verbosity=detailed"
```

### 3. Running the Application
To run the full client-server ecosystem locally, you must run both the backend se