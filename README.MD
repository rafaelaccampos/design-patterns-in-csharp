<h1 align="center">Design Patterns in C#</h1>

## 💻 Project
This project has the goal to study design patterns implemented in C#. It's contain theory and code implementation.

## Structural

### Adapter ###
Adapter is a structural design pattern that allow incompatible classes to communicate. It's serves as well to create an anticorruption layer and to promote uncouple.
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
---

## Behavioral

### Chain of Responsability ###
- Chain of responsability is a behavioral design pattern that allows you to pass requests forward, like a chain. And each request/handle decides what to do next in the chain.
- Especially useful when it has to do steps in sequence.
- Chain of responsability transforms behaviors in objects called Handlers.
- The handlers are linked in a chain and each linked handler has a field for the next reference to the handler.
Examples:
  - When you want to offer a person a credit card, but before you have to check if she's eligible.
  - When you have to check if users are authenticated and authorized for access a system. So, you can use Chain of Responsabily, when the request is created and execute in sequence the authentication and authorization process to acess the system.

**PROS**:
 - Control the order of request handling.
 - Single of Responsability - Decouple classes that invokes operations from class that perform operations.
 - Open/Closed Principle - Introduce new handlers without breaking the exist client code.

**CONS**:
- Risk of infinite chain.
- Debbuging it could be challenging.
- Complexity in the chain configuration.

**How to implement?**
- Declare the handler interface and describe the signature of a method for handling requests. You can convert the request into an object and pass to handler method as an argument.
- To eliminate duplicated code in concrete handlers, you can create an abstract base handler class, derived from the handler interface. This class has to contain a storing reference to the **next handler** in the chain. Consider making this class immutable, but if you need to modify chains at runtime, you need to define a setter for altering the value of the reference field. Besides that, you will need to create a default behavior for handling method, which is to forward the request to the next object unless there's none object. Concrete handlers will be able to use this by calling the parent method.
- Create concrete handler subclasses and implement their handling methods. Each handler should make two decisions when receiving a request. Wheter it'll process the request and Wheter it'll pass the request along the chain.
- You can configure assemble chains on its own, receive pre-built chains from other objects or implement some factory classes to build the chain.
- The request should pass along the chain until some handler refuses to pass it further or until it reaches the end of the chain.
- Clients should be ready to handle the following scenarios: the chain may consit of a single link, some request may not reach the end of the chain and others may reach the end of the chain unhandled.

### Visitor ###
- Visitor is a behavioral design patterns that executes a operation in a list of different objects from the same mother class.
- It's used when you have different implementation for each object inside a list or a tree.

**PROS**:
- Open closed principle because you introduce new behavior only for the specific objects without changing the father class.
- Single Responsability Principle, because you separate behavior with the corresponding class.
- Visitor can accumulate information and be very useful when working with node trees.

**CONS**:
- You need to update all visitor when a class gets modified from the hierarchy.
- Visitors might not have the necessary access to the private fields and methods.

**How to implement?**
- Declare the visitor interface with visiting methods, one per each concrete element class.
- Declare abstract class or interface with the method Accept that will receive receive visitor object as an argument.
- Implement the accept methods in all concrete class. These methods redirect call to the visitor object, that is the class executed in the moment.
- The element classes only work with visitor via visitor interface. Visitors must be aware of all concrete element classes as paremeter types.
- For each behavior on the list or tree, you have to create a concrete visitor class and implement the visiting methods.
- The client must create visitor ojects and pass them into elements via accept methods.

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
