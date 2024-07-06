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
- Responsabilidade única: Separar diferentes dados das regras de negócio.
- Princípio do aberto fechado: Você consegue introduzir novos adaptadores sem quebrar os clientes existentes, contanto que o contrato da interface seja preservada.

**CONS**
- A complexidade do código aumenta porque é introduzido muitas novas interfaces e classes.

**Como implementa?**
- Cria uma interface que descreve o contrato que outras classes devem seguir.

```
namespace DesignPatterns.Structural.Adapter.Adapter_Transaction
{
    public interface ITransaction
    {
        public string TrackNumber { get; }
        public decimal Amount { get; }
        public string Status { get; }
    }
}
```

- Criar as classes desejadas.
- Criar o adaptador para as classes desejadas que vão implementar a interface.
- Adicionar a referência da classe que você precisar adaptar na sua classe adaptadora. É comum usar o construtor, mas você pode chamar quando chamar o método das suas classes.
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


