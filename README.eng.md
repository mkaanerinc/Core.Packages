# Core.Packages

**Core.Packages** is a library project that contains reusable code snippets and helper tools designed for use across multiple projects. This project was created to reduce code duplication, ensure consistency, and accelerate the development process. I’ve consolidated the core functionalities I frequently need in my main projects here, allowing seamless integration into other projects as needed.

## Project Purpose

This library was designed to achieve the following goals:
- **Reusability**: Centralize common code snippets to eliminate the need to rewrite them for each project.
- **Ease of Maintenance**: Simplify updates and bug fixes by keeping code in a single location.
- **Standardization**: Ensure a consistent code structure and approach across all projects.

## Content

Currently, Core.Packages includes the following types of code (content can be expanded based on project needs):
- **Security**: Helper classes for authentication, authorization, and data security (e.g., token management, encryption).
- **Repositories**: A generic repository pattern implementation to abstract and standardize database operations.
- **Pagination**: Helper methods and classes to manage list data with pagination.
- **Logging**: Consistent logging mechanisms across the application (e.g., file, console, or external service integration).
- **Error Handling**: Standardized error catching, custom exception classes, and user-friendly error messages.
- **Pipelines**: Middleware-like structures to simplify authorization, caching, logging, validation, and transaction management.
- **Utilities**: General-purpose helper methods (e.g., string manipulation, date operations).
- **Extension Methods**: Extensions that add functionality to existing .NET types.

## Project Structure

The project is organized in a modular and readable manner:

```bash
Core.Packages/
|── src/
|   |── Core.Application/       
|   |── Core.Infrastructure/    
|   |── Core.Shared/
|   |── Core.CrossCuttingConcerns/
|   |── Core.Security/
|
|── Core.Packages.sln                    # Solution file
```

## Usage

To use Core.Packages in another project, follow these steps:

1. **Adding as a NuGet Package** (Planned):
   - Package the project as a NuGet package and add it from a local NuGet source (to be implemented in the future).
   <br><br>
   ```bash
   dotnet add package Core.Packages --version 1.0.0
   ```
   
2. **Adding as a Project Reference**:
   - Add the Core.Packages.csproj file to your solution::
   <br><br>
   ```bash
   dotnet add reference ../Core.Packages/Core.Packages.csproj
   ```

3. **Using in Code**:
   - Include the relevant namespaces in your project and start using the tools:
   <br><br>
   ```bash
   using Core.Shared.Extensions;

   ReportStatus status = ReportStatus.Completed;
   string displayName = status.GetDisplayName(); 
   Console.WriteLine(displayName); // Output: "Tamamlandı"
   ```
   
## Installation

1. **Clone the Repository**:
   <br><br>
   ```bash
   git clone https://github.com/kullaniciadiniz/Core.Packages.git
   cd Core.Packages

3. **Install Dependencies**:
   <br><br>
   ```bash
   dotnet restore Core.Packages.csproj

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).
