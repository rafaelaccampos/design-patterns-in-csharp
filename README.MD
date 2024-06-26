<h1 align="center">Design Patterns in C#</h1>

## 💻 Project
Esse projeto tem como objetivo estudar Design Patterns implementados em C#. Ele contém teoria e a implementação do código.
This project has the goal to study design patterns implemented in C#. It's contain theory and code implementation.

## Structural

### Adapter ###
Adapter é um padrão de projeto estrutural que permite que classes incompatíveis se comuniquem. Ele também serve como uma camada anticorrupção que permite desacoplamento.
Exemplos:
- Diferentes implementações de gateway que tem que se comunicar com seu domínio que tem diferentes interfaces.
- Quando você tem código legado e precisa adaptar para classes modernas.

**PROS**
- Responsabilidade Única: Separar diferentes dados das regras de negócio.
- Princípio do aberto fechado: Você consegue introduzir novos adaptadores sem quebrar os clientes existentes, contanto que o contrato da interface seja preservada.

**CONS**
- A complexidade do código aumenta porque é introduzido muitas novas interfaces e classes.

**Como implementa?**
- Cria uma interface que descreve o contrato que outras classes devem seguir.
- Criar as classes desejadas.
- Criar o adaptador para as classes desejadas que vão implementar a interface.
- Adicionar a referência da classe que você precisar adaptar na sua classe adaptadora. É comum usar o construtor, mas você pode chamar quando chamar o método das suas classes.

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
- Cadeia de responsabilidade é um design pattern comportamental que permite que passem as requisições adiante, como uma cadeia. E cada requisição/handle decide o que vai ser feito na cadeia.
- Especialmente útil quando tem que executar passos em sequência.
- Os handlers são conectados a uma cadeia e cada conexão do handler tem um campo que guarda a referência para o próximo handler.
Examples:
- Quando você quer oferecer um cartão de crédio, mas precisa executar diversas validações antes para saber se a pessoa é elegível.
- Quando você precisa verificar se os usuários estão autenticados e autorizados para acessar o sistema. Então, você pode usar cadeia de responsabilidade, quando o request é criado e executado em sequência o processo de autenticação.

**PROS**
- Controlar a ordem de execução do request.
- Responsabilidade Única: Desacople classes que invocam operação de classes que performam operações.
- Princípio do aberto fechado: Introduza novos handlers sem quebrar o código cliente já existente.

**CONS**
- Risco de criar uma cadeia infinita.
- Depuração pode ser mais difícil.
- A configuração da cadeia pode ser complexa.

**Como implementar?**
- Declare a interface do handler e descreva a assinatura do seu método para lidar com as requisições do handler. Você pode converter as requisições em um objeto e passar o método do handler como um argumento.
- Para eliminar código duplicado em handlers concretos, você pode criar uma classe abstrata base, derivada da interface do handler. Essa classe contém uma referência ao **próximo handler** na cadeia. Considere fazer essa classe imutável, mas se você precisa modificar cadeias em tempo de execução, você precisa definir um setter para alterar o campo de referência. Além disso, você vai precisar criar um comportamento padrão para o método do handler, que é passar o request para o próximo objeto a menos que não haja objeto. Handlers concretos poderão usar isso chamando o método pai.
- Você pode criar suas próprias cadeias, receber cadeias pré construídas de outros objetos ou implementar algumas classes factory para construir a cadeia.
- Clientes devem estar preparados para os seguintes cenários: a cadeia consiste de um único link, aguns request não devem seguir ao fim da cadeia e outras podem chegar ao fim da cadeia sem tratamento.

- Chain of responsability is a behavioral design pattern that allows you to pass requests forward, like a chain. And each request/handle decides what to do next in the chain.
- Especially useful when it has to do steps in sequence.
- Chain of responsability transforms behaviors in objects called Handlers.
- The handlers are linked in a chain and each linked handler has a field for the next reference to the handler.
Examples:
  - When you want to offer a person a credit card, but before you have to check if it's eligible.
  - When you have to check if users are authenticated and authorized for access a system. So, you can use Chain of Responsability, when the request is created and execute in sequence the authentication and authorization process to acess the system.

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
- Visitor é uma padrão de projeto comportamental que executa uma operação em uma lista de objetos diferentes da mesma classe mãe.
- É usado quando você tem diferentes objetos dentro de uma lista ou árvore.

**PROS**
- Princípio do aberto fechado: porque você pode introduzir novos comportamentos para objetos específicos sem mudar a classe pai.
- Princípio da responsabilidade única: porque você pode separar comportamento com sua classe respectiva.
- Visitor pode acumular informações e ser muito útil quando se trabalha com nós de árvore.

**CONS**
- Você precisa atualizar todos os visitors quando uma classe é modificada na hierarquia.
- Visitors podem não ter os acessos necessários para campos privados ou métodos.

**Como implementar?**
- Declare uma interface visitor com métodos visits, uma para elemento concreto da classe.
- Declare uma classe abstrata ou uma interface com os métodos Accept que irão receber objetos visitors como argumento.
- Implemente os métodos Accept em todas as classes concretas. Esses métodos irão redirecionar cada chamada do objeto visitor como um argumento.
- As classes de elementos somente trabalham com o visitor via interface do visitor. Visitors devem saber de todas os elementos concretos da classe como tipos de parâmetro.
- Para cada comportamento na lista ou árvore, você tem que criar uma classe concreta do visitor e implementar os métodos visits.
- O cliente deve criar objetos visitors e passá-los para elementos por meio de métodos de aceitação.

- Visitor is a behavioral design patterns that executes a operation in a list of different objects from the same mother class.
- It's used when you have different implementation for each object inside a list or a tree.

**PROS**:
- Open closed principle: because you introduce new behavior only for the specific objects without changing the father class.
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


