<p align="center">
  <h1>Design Patterns in C#</h1>
</p>

## 💻 Project
This project has the goal to study design patterns implemented in C#.

## Structural

**Adapter**
Adapter is a structural design patterns that allow different classes to communicate.
Ex: Different implementations of gateway that have to communicate with your domain that have different interface.

**PROS**:
- SINGLE RESPONSABILITY
Separate the interface data from the business logic.
- OPEN CLOSED PRINCIPLE
You can introduce new adapters without breaking the existing client, as long the contract of interface be preserved.

**CONS**:
- The complexity of code increases because you introduced a lot of new interfaces and classes.


## 🧪 Techs

This project was develop with the following technologies:

- [.NET 7 Console Application](https://docs.microsoft.com/pt-br/dotnet/core/dotnet-7)

## How can I use?

You will need of the Visual Studio 2022 and .NET 7 SDK.
This SKDs and tools can be download in .NET 7 https://dot.net/core.
You can execute in Visual Studio Code too (Windows, Linux or MacOS)

## 🚀 How can I execute?

Clone the projet and access the pasta.

```bash
$ git clone https://github.com/rafaelaccampos/design-patterns-in-csharp
$ cd DesignPatterns
# To install the dependencies
$ dotnet restore
```

To initiate the tests, follow the steps below:
```bash
$ dotnet test
```