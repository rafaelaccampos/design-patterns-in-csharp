<h1 align="center">Design Patterns in C#</h1>

## 💻 Project
This project has the goal to study design patterns implemented in C#. It's contain theory and code implementation.

## Structural

### Adapter ###
Adapter is a structural design patterns that allow incompatible classes to communicate. It's serves as well to create an anticorruption layer and to promote uncouple.
Examples: 
  - Different implementations of gateway that has to communicate with your domain that has different interface.
  - When you have legacy code and need to adapt to modern classes.

**PROS**:
- Single responsability: Separate the interface data from the business logic.
- Open closed principle: You can introduce new adapters without breaking the existing client, as long the contract of interface be preserved.

**CONS**:
- The complexity of code increases because you introduced a lot of new interfaces and classes.

**How to implement?**
- Create an interface that describes the contract the others classes should follow.
- Create the desired classes.
- Create the adapter for the desired classes that implements the interface.
- Add a reference to the class you need to adapt in the adapter class. It's common to use the constructor, but you can use when calling its methods.
- Client should use the adapter class to comunicate with internal classes.


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