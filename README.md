# Dollar currency to word converter

A modular, client-server C# application built on .NET 8.0 that converts dollars currency amounts into their written word representations in both English and German. --- ##


# Project Structure & Architecture

The solution (`DollarTextConverter`) is decoupled into three projects to enforce clean separation of concerns:

### Application Layers 
* **`Converter.Client`**: A graphical user interface (WinForms GUI) application. It handles user inputs (currency amount and target language), runs client-side validation, sends HTTP requests to the server, and renders the converted text result.

* **`Converter.Server`**: An ASP.NET Web API that exposes HTTP endpoints. It acts as the orchestration layer, receiving payloads from the client and invoking the core logic. 

* **`Converter.Core`**: A project containing the domain logic.
It implements an `IConverter` interface to achieve dependency inversion, utilizing an abstract `ConverterFactory` to dynamically route requests to either `EnglishConverter` or `DeutschConverter` classes.

### Verification Layers
* **`Converter.Core.Tests`**: A unit testing assembly dedicated to verifying exact string transformation outputs, boundary values, and language-switching algorithms. 
* **`Converter.Server.Tests`**: An API testing layer to validate route handlers, payload models, factory injection, and server error scenarios. 

### Process Flow
Converter.Client(WinForms GUI) -> Viladates (First validation) and sends HTTP request to Converter.Server (ASP.NET Core API) -> Validates 
and invokes Converter.Core (domain logic) -> Returns converted numbers to text back to Converter.Client and display it on GUI.

## Build, Run, and Test Guide

### Prerequisites
*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
*   Windows OS (Required to compile and run the `DollarTextConverter.Client` WinForms desktop wrapper).
*   An IDE like Visual Studio 2022 or higher.

### 1. Installation & Compilation
Clone the repository and restore dependencies across the entire solution:

```
1.Clone the repository to your local machine:
git clone https://github.com/AlexVaysAcc/DollarTextConverter.git

2. To run the full client-server solution locally, you must run both the server API and the WinForms desktop client.
  Open the solution in Visual Studio 2022 or higher and set the startup project to `DollarTextConverter.Server` to run the backend API, 
  and `DollarTextConverter.Client` to run the WinForms desktop client in the solution properties.
  Solution -> Properties -> Startup Project -> Multiple startup projects -> Set both projects to "Start".

3. Build the projects to ensure all dependencies are resolved and compiled successfully.

4. Run the solution by starting both the server and client projects. The server will listen for incoming requests, 
   while the client will provide a user interface for input and display of results.

5. Switch to the GUI client and enter a dollar amount and select the desired language (English or German) to see the converted text output.

```

------------------------------------------------------------------------------------------------------------------------------

## Design Decisions & Assumptions

### Key Architectural Decisions
*   **Dependency Inversion (`IConverter`)**: The server does not couple itself directly to concrete language algorithms. By relying on the `IConverter` abstraction inside `Converter.Core`, scaling up to support new languages involves adding a new class without refactoring endpoints..
*   **Two-Tier Validation**: 
    1.  *Client-Side*: Basic user input layout checks (e.g., prevention of text characters in number boxes, negative figures, or empty boxes) are caught inside the WinForms thread immediately to prevent unnecessary network overhead.
    2.  *Server-Side*: The API validates raw payloads to protect domain logic against corrupted network payloads or malicious direct API calls.
*   **Comprehensive Logging**: Integrated via the standard Microsoft `ILogger` abstraction across all modules. 
    *   `Core` logs structural algorithm paths and factory routing events.
    *   `Server` logs incoming HTTP requests, route timing, and caught exceptions.
    *   `Client` logs application lifecycle states, client-side validation slips, and network timeout warnings.
-------------------------------------------------------------------------------------------------------------------------------

### Assumptions
*   **Host OS Culture Formatting**: The app assumes standard universal decimal formatting matching the system's local desktop configuration for parsing strings to `decimal` values.
*   **Synchronous Processing**: The application uses a standard request-response lifecycle where the WinForms event loop briefly locks or transitions to a "loading" cursor state until the HTTP client thread resolves.

------------------------------------------------------------------------------------------------------------------------------

##  Limitations & Known Issues

*   **Platform Restriction**: Due to the choice of WinForms for the graphical client user interface, the `Converter.Client` application can only be built and executed on a Windows operating system.
*   **Maximum Converion Limits**: The dollars to words algorithm is bounded by the max of 99999999 dollars. Submitting strings that overflow this numerical constraint triggers a validation error.
*   **No Automated Connection Retries**: If the local server falls offline during a conversion cycle, the WinForms client logs the socket exception and drops straight to an error notification prompt instead of silently retrying the request queue.
