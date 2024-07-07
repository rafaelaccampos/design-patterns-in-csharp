<h1 align="center">Design Patterns in C#</h1>

## 💻 Project
Esse projeto tem como objetivo estudar Design Patterns implementados em C#. Ele contém teoria e a implementação do código.
This project has the goal to study design patterns implemented in C#. It's contain theory and code implementation.

## Structural

### Adapter ###
Adapter é um padrão de projeto estrutural que permite que classes incompatíveis se comuniquem. Ele também serve como uma camada anticorrupção que permite desacoplamento.
**Exemplos**:
- Diferentes implementações de gateway que tem que se comunicar com seu domínio que tem diferentes interfaces.
- Quando você tem código legado e precisa adaptar para classes modernas.

**PROS**
- Responsabilidade única: Separar diferentes dados das regras de negócio.
- Princípio do aberto fechado: Você consegue introduzir novos adaptadores sem quebrar os clientes existentes, contanto que o contrato da interface seja preservada.

**CONS**
- A complexidade do código aumenta porque é introduzido muitas novas interfaces e classes.

**Como implementa?**
- Cria uma interface que descreve o contrato que outras classes devem seguir.

```csharp
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

```csharp
    public class PaypalTransaction
    {
        public PaypalTransaction(int id, int amount, string status)
        {
            Id = id;
            Amount = amount;
            Status = status;
        }

        public int Id { get; private set; }
        public decimal Amount { get; private set; }
        public string Status { get; private set; }
    }
```
```csharp
    public class StripeTransaction
    {
        public StripeTransaction(string code, decimal grossAmount, int situation)
        {
            Code = code;
            GrossAmount = grossAmount;
            Situation = situation;
        }

        public string Code { get; private set; }
        public decimal GrossAmount { get; private set; }
        public int Situation { get; private set; }
    }
```
- Criar o adaptador para as classes desejadas que vão implementar a interface.

```csharp
    public class PayPalTransactionAdapter : ITransaction
    {
        public PayPalTransactionAdapter(PaypalTransaction payPalTransaction)
        {
            TrackNumber = payPalTransaction.Id.ToString();
            Amount = payPalTransaction.Amount;
            Status = ConvertStatus(payPalTransaction.Status);
        }

        public string TrackNumber { get; private set; }

        public decimal Amount { get; private set; }

        public string Status { get; private set; }

        public string ConvertStatus(string status)
        {
            switch (status)
            {
                case "P":
                    return "waiting_payment";
                case "S":
                    return "paid";
                case "F":
                    return "refunded";
                default:
                    return string.Empty;
            }
        }
    }
```
```csharp
        public StripeTransactionAdapter(StripeTransaction stripeTransaction)
        {
            TrackNumber = stripeTransaction.Code;
            Amount = stripeTransaction.GrossAmount;
            Status = ConvertStatus(stripeTransaction.Situation);
        }

        public string TrackNumber { get; private set; }

        public decimal Amount { get; private set; }

        public string Status { get; private set; }

        public string ConvertStatus(int status)
        {
            switch (status)
            {
                case 1:
                    return "waiting_payment";
                case 2:
                    return "paid";
                case 3:
                    return "cancelled";
                default:
                    return string.Empty;
            }
        }
```
- Adicionar a referência da classe que você precisar adaptar na sua classe adaptadora. É comum usar o construtor, mas você pode chamar quando chamar o método das suas classes.

```csharp
        [Test]
        public void ShouldBeAbleToCreateTransactionFromStripe()
        {
            var stripeTransaction = new StripeTransaction("AHN765NHD89", 1000, 2);
            var transaction = new StripeTransactionAdapter(stripeTransaction);

            using (new AssertionScope())
            {
                transaction.TrackNumber.Should().Be("AHN765NHD89");
                transaction.Amount.Should().Be(1000);
                transaction.Status.Should().Be("paid");
            }
        }

        [Test]
        public void ShouldBeAbleToCreateTransactionFromPaypal()
        {
            var payPalTransaction = new PaypalTransaction(7897897, 1000, "S");
            var transaction = new PayPalTransactionAdapter(payPalTransaction);

            using (new AssertionScope())
            {
                transaction.TrackNumber.Should().Be("7897897");
                transaction.Amount.Should().Be(1000);
                transaction.Status.Should().Be("paid");
            }
        }
```
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
- Responsabilidade única: Desacople classes que invocam operação de classes que performam operações.
- Princípio do aberto fechado: Introduza novos handlers sem quebrar o código cliente já existente.

**CONS**
- Risco de criar uma cadeia infinita.
- Depuração pode ser mais difícil.
- A configuração da cadeia pode ser complexa.

**Como implementar?**
- Declare a interface do handler e descreva a assinatura do seu método para lidar com as requisições do handler. Você pode converter as requisições em um objeto e passar no método do handler como um argumento.

```csharp
namespace DesignPatterns.Behavioral.Chain_of_Responsability_Adm
{
    public interface IHandler
    {
        void Handle(IList<Bill> bills, int amount);
    }
}
```

- Para eliminar código duplicado em handlers concretos, você pode criar uma classe abstrata base, derivada da interface do handler. Essa classe contém uma referência ao **próximo handler** na cadeia. Considere fazer essa classe imutável, mas se você precisa modificar cadeias em tempo de execução, você precisa definir um setter para alterar o campo de referência. Além disso, você vai precisar criar um comportamento padrão para o método do handler, que é passar o request para o próximo objeto a menos que não haja objeto. Handlers concretos poderão usar isso chamando o método pai.

```csharp
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DesignPatterns.Behavioral.Chain_of_Responsability_Adm
{
    public class BillHandler : IHandler
    {
        private readonly IHandler? _nextHandler;
        private readonly int _type;
       
        public BillHandler(IHandler? nextHandler, int type)
        {
            _nextHandler = nextHandler;
            _type = type;
        }

        public void Handle(IList<Bill> bills, int amount)
        {
            decimal notes = amount / _type;
            var count = (int)Math.Floor(notes);
            var bill = new Bill
            {
                Count = count,
                Type = _type,
            };

            bills.Add(bill);
            var remaining = amount % _type;

            if (_nextHandler != null)
            {
                _nextHandler.Handle(bills, remaining);
                return;
            }
            if (remaining > 0) throw new Exception("Without available notes!");
        }
    }
}
```

- Você pode criar suas próprias cadeias, receber cadeias pré construídas de outros objetos ou implementar algumas classes factory para construir a cadeia.

```csharp
namespace DesignPatterns.Behavioral.Chain_of_Responsability_Adm
{
    public class Bill
    {
        public int Type { get; set; }
        public int Count { get; set; }
    }
}

namespace DesignPatterns.Behavioral.Chain_of_Responsability_Adm
{
    public class Atm
    {
        private readonly IHandler _handler;

        public Atm(IHandler handler)
        {
            _handler = handler;
        }

        public IList<Bill> Withdraw(int amount)
        {
            var bills = new List<Bill>();
            _handler.Handle(bills, amount);
            return bills;
        }
    }
}
```
- Clientes devem estar preparados para os seguintes cenários: a cadeia consiste de um único link, aguns request não devem seguir ao fim da cadeia e outras podem chegar ao fim da cadeia sem tratamento.

```csharp
using DesignPatterns.Behavioral.Chain_of_Responsability_Adm;
using FluentAssertions;

namespace DesignPatterns.Tests.Chain_of_Responsability
{
    public class AtmTests
    {
        [Test]
        public void ShouldBeAbleToRetrieveMoneyWithAllNotes()
        {
            var handler1 = new BillHandler(null, 1);
            var handler2 = new BillHandler(handler1, 2);
            var handler5 = new BillHandler(handler2, 5);
            var handler10 = new BillHandler(handler5, 10);
            var handler20 = new BillHandler(handler10, 20);
            var handler50 = new BillHandler(handler20, 50);
            var handler100 = new BillHandler(handler50, 100);
            var atm = new Atm(handler100);

            var bills = atm.Withdraw(978);

            var expectedBills = new List<Bill>
            {
                { new Bill { Count = 9, Type = 100 } },
                { new Bill { Count = 1, Type = 50 } },
                { new Bill { Count = 1, Type = 20 } },
                { new Bill { Count = 0, Type = 10 } },
                { new Bill { Count = 1, Type = 5 } },
                { new Bill { Count = 1, Type = 2 } },
                { new Bill { Count = 1, Type = 1 } },
            };

            bills.Should().BeEquivalentTo(expectedBills);
        }

        [Test]
        public void ShouldBeAbleToRetrieveMoneyWithOnlyOneNotes()
        {
            var handler1 = new BillHandler(null, 1);
            var atm = new Atm(handler1);

            var bills = atm.Withdraw(978);

            var expectedBills = new List<Bill>
            {
                { new Bill { Count = 978, Type = 1 } },
            };

            bills.Should().BeEquivalentTo(expectedBills);
        }

        [Test]
        public void ShouldBeAbleToRetrieveMoneyWithOnlyFiveAndTenNotes()
        {
            var handler5 = new BillHandler(null, 5);
            var handler10 = new BillHandler(handler5, 10);

            var atm = new Atm(handler10);
            var bills = atm.Withdraw(500);

            var expectedBills = new List<Bill>
            {
                { new Bill { Count = 50, Type = 10 } },
                { new Bill { Count = 0, Type = 5 } },
            };

            bills.Should().BeEquivalentTo(expectedBills);
        }
    }
}
```

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

```csharp
namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public interface INotificationVisitor
    {
        string Visit(SmsMessage message);

        string Visit(EmailMessage message);
    }
}
```

- Declare uma classe abstrata ou uma interface com os métodos Accept que irão receber objetos visitors como argumento.

```csharp
namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public interface IMarketingMessage
    {
        string From { get; }

        string To { get; }

        string Content { get; }

        string Accept(INotificationVisitor visitor);
    }
}
```

- Implemente os métodos Accept em todas as classes concretas. Esses métodos irão redirecionar cada chamada do objeto visitor como um argumento.

```csharp
namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public class EmailMessage : IMarketingMessage
    {
        public EmailMessage(string from, string to, string subject, string content)
        {
            From = from;
            To = to;
            Subject = subject;
            Content = content;
        }

        public string From { get; private set; }

        public string To { get; private set; }

        public string Subject { get; private set; }

        public string Content { get; private set; }

        public string Accept(INotificationVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}

namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public class SmsMessage : IMarketingMessage
    {
        public SmsMessage(string from, string to, string content)
        {
            From = from;
            To = to;
            Content = content;
        }

        public string From { get; private set; }

        public string To { get; private set; }

        public string Content { get; private set; }

        public string Accept(INotificationVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
```

- As classes de elementos somente trabalham com o visitor via interface do visitor. Visitors devem saber de todas os elementos concretos da classe como tipos de parâmetro.
- Para cada comportamento na lista ou árvore, você tem que criar uma classe concreta do visitor e implementar os métodos visits.

```csharp
namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public class NotificationVisitor : INotificationVisitor
    {
        public string Visit(SmsMessage message)
        {
            return $"SMS message: From: {message.From}, To: {message.To}, Content: {message.Content}";
        }

        public string Visit(EmailMessage message)
        {
            return $"Email message: From: {message.From}, Subject: {message.Subject}, To: {message.To}, Content: {message.Content}";
        }
    }
}
namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public class NotificationService
    {
        private readonly INotificationVisitor _notificationVisitor;

        public NotificationService(INotificationVisitor notificationVisitor)
        {
            _notificationVisitor = notificationVisitor;
        }

        public IEnumerable<string> Notify(IList<IMarketingMessage> messages)
        {
            var visitor = _notificationVisitor;

            foreach (var message in messages)
            {
                yield return message.Accept(visitor);
            }
        }
    }
}
```
- O cliente deve criar objetos visitors e passá-los para elementos por meio de métodos de aceitação.

```csharp
using DesignPatterns.Behavioral.Visitor_Marketing;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DesignPatterns.Tests.Visitor
{
    public class NotificationServiceTests
    {
        [Test]
        public void ShouldBeAbleToCreateEmailMessageNotification()
        {
            var emailMessages = new List<IMarketingMessage>
            {
                new EmailMessage("Rafa", "T", "BFF", "We need to go out sometime!"),
            };

            var notificationVisitor = new NotificationVisitor();
            var notificationService = new NotificationService(notificationVisitor);
            var notifications = notificationService.Notify(emailMessages).ToList();

            var expectedEmailMessage = "Email message: From: Rafa, Subject: BFF, To: T, Content: We need to go out sometime!";
            using (new AssertionScope())
            {
                notifications.Should().BeEquivalentTo(expectedEmailMessage);
            }
        }

        [Test]
        public void ShouldBeAbleToCreateSMSMessageNotification()
        {
            var smsMessages = new List<IMarketingMessage>
            {
                new SmsMessage("T", "Rafa", "Definitely, we need to schedule!"),
            };

            var notificationVisitor = new NotificationVisitor();
            var notificationService = new NotificationService(notificationVisitor);
            var notifications = notificationService.Notify(smsMessages).ToList();

            var expectedSmsMessage = "SMS message: From: T, To: Rafa, Content: Definitely, we need to schedule!";
            using (new AssertionScope())
            {
                notifications.Should().BeEquivalentTo(expectedSmsMessage);
            }
        }

        [Test]
        public void ShouldBeAbleToCreateEmailAndSmsMessagesNotifications()
        {
            var emailMessage = new EmailMessage("Rafa", "T", "BFF", "We need to go out sometime!");
            var smsMessage = new SmsMessage("T", "Rafa", "Definitely, we need to schedule!");

            var messages = new List<IMarketingMessage>
            {
                emailMessage,
                smsMessage
            };

            var notificationVisitor = new NotificationVisitor();
            var notificationService = new NotificationService(notificationVisitor);
            var notifications = notificationService.Notify(messages).ToList();

            var expectedMessages = new List<string>
            {
                { "Email message: From: Rafa, Subject: BFF, To: T, Content: We need to go out sometime!" },
                { "SMS message: From: T, To: Rafa, Content: Definitely, we need to schedule!" },
            };

            using (new AssertionScope())
            {
                notifications.Should().Contain(expectedMessages);
            }
        }
    }
}
```

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


