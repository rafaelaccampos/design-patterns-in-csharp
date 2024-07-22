<h1 align="center">Design Patterns in C#</h1>

## 💻 Project
Esse projeto tem como objetivo estudar Design Patterns implementados em C#. Ele contém teoria e a implementação do código.
This project has the goal to study design patterns implemented in C#. It's contain theory and code implementation.

## Creational

### Builder ###
- Builder é um padrão de projeto criacional que permite que você crie objetos complexos em várias etapas.
- Especialmente útil quando se tem muitos parâmetros opcionais ou quando você precisa criar o objeto por partes.

**Exemplos**:
- Formulários muito grandes que são construídos em diversas etapas.
- Na criação de objetos de testes em que você só precisa criar com parâmetros específicos.

**PROS**
- Você consegue construir objetos complexos passo a passo.
- Responsabilidade única: Separar construções complexas de objetos de lógica de negócios.
- Você pode reusar os builders para diferentes criações de produto.
- Evita o code smell telescoping constructor. O code smell telescoping constructor é quando uma classe tem vários construtores com muitos parâmetros e isso dificulta saber a ordem dos parâmetros, bem como qual é o construtor certo a ser utilizado.

**CONS**
- A complexidade do código aumenta porque você precisa criar múltiplas classes.

**Como implementa?**
- Identifique claramente todos os passos de construção para construir todas as representações do produto disponíveis.
- Declare esses passos na interface do builder.
- Crie uma classe construtora concreta para cada uma das representações do produto e implemente suas etapas de construção.
- Implemente um método (nesse caso, Generate) para buscar o resultado da construção.

```csharp
using System.Text;

namespace DesignPatterns.Creational.Builder
{
    public class ItemBuilder
    {
        public ItemBuilder(
            int idItem, 
            string category, 
            string description, 
            decimal price)
        {
            IdItem = idItem;
            Category = category;
            Description = description;
            Price = price;
        }

        public int IdItem { get; private set; }

        public string Category { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public decimal Width { get; private set; }

        public decimal Height { get; private set; }

        public decimal Length { get; private set; }

        public decimal Weight { get; private set; }


        public ItemBuilder WithWidth(decimal width)
        {
            Width = width;
            return this;
        }

        public ItemBuilder WithHeight(decimal height)
        {
            Height = height;
            return this;
        }

        public ItemBuilder WithLenght(decimal lenght)
        {
            Length = lenght;
            return this;
        }

        public ItemBuilder WithWeight(decimal weight)
        {
            Weight = weight;
            return this;
        }

        public Item Generate()
        {
            var item = new Item(this);
            return item;
        }
    }
}

namespace DesignPatterns.Creational.Builder
{
    public class Item
    {
        public Item(ItemBuilder itemBuilder)
        {
            IdItem = itemBuilder.IdItem;
            Category = itemBuilder.Category;
            Description = itemBuilder.Description;
            Width = itemBuilder.Width;
            Height = itemBuilder.Height;
            Length = itemBuilder.Length;
            Weight = itemBuilder.Weight;
        }

        public int IdItem { get; private set; }

        public string Category { get; private set; }
        
        public string Description { get; private set; }
        
        public decimal? Width { get; private set; }
        
        public decimal? Height { get; private set; }
        
        public decimal? Length { get; private set; }
        
        public decimal? Weight { get; private set; }

        public decimal? GetVolume()
        {
            if(Width == null || Height == null || Length == null) return 0;

            return Width / 100 * Height / 100 * Length / 100;
        }

        public decimal? GetDensity()
        {
            if(Width == null || Height == null || Length == null || Height == null || Weight == null) return 0;

            return Weight / GetVolume();
        }
    }
}

using DesignPatterns.Creational.Builder;
using FluentAssertions;

namespace DesignPatterns.Tests.Builder
{
    public class ItemTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnItem()
        {
            var item = new ItemBuilder(1, "Instrumentos Musicais", "Guitarra", 1000)
                .WithWidth(100)
                .WithHeight(50)
                .WithLenght(30)
                .WithWeight(3)
                .Generate();

            var volume = item.GetVolume();

            volume.Should().Be(0.15M);
        }
    }
}
```

### Factory Method ###
- Factory Method é um padrão de projeto criacional que provê uma interface comum para criação de um objeto, e permite que as subclasses alterem o tipo do objeto que vai ser executado, ou seja, delega a responsabilidade de instanciar objetos para as classes concretas.
- Especialmente útil quando há algum processamento genérico em uma classe, mas as subclasses são decididas dinamicamente no período de execução.
- Geralmente usado quando existe comportamento polimórfico.

**Exemplos**:
- Uma aplicação que precisa gerenciar diferents tipos de documento (Word, PDF).
- Uma aplicação que precisa gerenciar diferentes tipos de pagamento (Boleto, Cartão de Crédito, PayPal).

**PROS**
- Você evita um acoplamento forte entre a criação dos objetos e as classes concretas.
- Princípio da responsabilidade única: Você consegue a criação do objeto em um único lugar.
- Princípio do aberto fechado: Você consegue introduzir novos objetos sem criar os já existentes.

**CONS**
- O código pode ser mais complexo já que você precisa criar novas classes para cada tipo de objeto a ser instanciado para implementar o padrão de projeto.

**Como implementa?**
- Defina a interface comum para o serviço que será implementada pelos outros serviços.

```csharp
namespace DesignPatterns.Creational.Factory_Method
{
    public interface IPaymentService
    {
        object Process(OrderItem orderItem);
    }
}
```

- Implemente os serviços concretos que herdaram da interface.

```csharp
namespace DesignPatterns.Creational.Factory_Method
{
    public class CreditCardService : IPaymentService
    {
        public object Process(OrderItem orderItem)
        {
            return "Transação aprovada!";
        }
    }

    public class PaymentSlipService : IPaymentService
    {
        public object Process(OrderItem orderItem)
        {
            return "Dados do boleto";
        }
    }
}
```

- Defina a interface da factory.

```csharp
namespace DesignPatterns.Creational.Factory_Method
{
    public interface IPaymentServiceFactory
    {
        IPaymentService GetService(PaymentMethod paymentMethod);
    }
}
```

- Implemente a fábrica que cria instâncias dos serviços.

```csharp
namespace DesignPatterns.Creational.Factory_Method
{
    public class PaymentServiceFactory : IPaymentServiceFactory
    {
        private static readonly IDictionary<PaymentMethod, IPaymentService> paymentServices = new Dictionary<PaymentMethod, IPaymentService>()
        {
            { PaymentMethod.CreditCard, new CreditCardService() },
            { PaymentMethod.PaymentSlip, new PaymentSlipService() }
        };

        public IPaymentService GetService(PaymentMethod paymentMethod)
        {
            if (!paymentServices.TryGetValue(paymentMethod, out var service))
            {
                throw new InvalidOperationException("Payment Method not found!");
            }

            return service;
        }
    }
}
```

- Define a classe que vai orquestrar a factory method.

```csharp
namespace DesignPatterns.Creational.Factory_Method
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}

namespace DesignPatterns.Creational.Factory_Method
{
    public enum PaymentMethod
    {
        CreditCard = 1,
        PaymentSlip = 2,
    }
}

namespace DesignPatterns.Creational.Factory_Method
{
    public class Order
    {
        private readonly IPaymentServiceFactory _paymentServiceFactory;

        public Order(IPaymentServiceFactory paymentServiceFactory)
        {
            _paymentServiceFactory = paymentServiceFactory;
        }

        public void Create(OrderItem orderItem)
        {
            var paymentService = _paymentServiceFactory.GetService(orderItem.PaymentMethod);
            paymentService.Process(orderItem);
        }
    }
}
```

- Defina a classe cliente que vai utilizar a factory.

```csharp
namespace DesignPatterns.Tests
{
    public class OrderTests
    {
        [Fact]
        public void ShouldProcessOrderWithCreditCardService()
        {
            var factory = new PaymentServiceFactory();
            var order = new Order(factory);
            var orderItem = new OrderItem
            {
                ProductId = Guid.NewGuid(),
                Quantity = 1,
                PaymentMethod = PaymentMethod.CreditCard
            };

            order.Create(orderItem);

            var service = factory.GetService(orderItem.PaymentMethod);
            var creditCard = service.Process(orderItem);
            Assert.Equal("Transação aprovada!", result);
        }

        [Fact]
        public void Create_ShouldProcessOrderWithPaymentSlipService()
        {
            var factory = new PaymentServiceFactory();
            var order = new Order(factory);
            var orderItem = new OrderItem
            {
                ProductId = Guid.NewGuid(),
                Quantity = 1,
                PaymentMethod = PaymentMethod.PaymentSlip
            };

            order.Create(orderItem);

            var service = factory.GetService(orderItem.PaymentMethod);
            var paymentSlip = service.Process(orderItem);
            Assert.Equal("Dados do boleto", result);
        }
    }
}
```

### Abstract Factory ###
- O Abstract Factory é um padrão de projeto criacional que permite que você tenha famílias de projetos relacionados sem criar classes concretas.
- Use Abstract Factory quando seu código precisar trabalhar com diversas famílias de produtos relacionados.
- Quando uma classe lida com múltiplos tipos de produto pode valer a pena implementar o Abstract Factory.

**Exemplos**
- Escolher tema light ou dark, pois você precisa de um conjunto de elementos compatível com o tema escolhido.
- Escolha de móveis modernos ou vitorianos, pois você precisa que seja criado elementos de acordo com o tipo de móvel escolhido.
- Para criação de múltiplos repositórios (que são passados pelo construtor).

**PROS**
- Os produtos que você obtém de um abstract factory são compatíveis entre si.
- Evita vínculo forte entre implementações concretas e o código do cliente.
- Princípio da responsabilidade única: É possível extrair o código de criação de produtos para um único lugar.
- Princípio do aberto e fechado: É possível introduzir novos produtos sem quebrar o código cliente já existente.

**CONS**
- O código se torna mais complexo porque são adicionadas muitas interfaces e classes juntos com o padrão.

**Como implementar?**
- Mapeie os produtos distintos e as variantes desses produtos.
- Declare interface de produto abstratas para todos os tipos de produto. Então, faça com que as classes concretas de produtos implementem essas interfaces.

```csharp
namespace DesignPatterns.Structural.Abstract_Factory
{
    public interface IButton
    {
        string Color { get; }
        string BackgroundColor { get; }
    }
}
namespace DesignPatterns.Structural.Abstract_Factory
{
    public interface ILabel
    {
        string Color { get; }
    }
}

namespace DesignPatterns.Structural.Abstract_Factory
{
    public class DarkButton : IButton
    {
        public DarkButton()
        {
            Color = "white";
            BackgroundColor = "black";
        }

        public string Color { get; private set; }

        public string BackgroundColor { get; private set; }
    }
}
namespace DesignPatterns.Structural.Abstract_Factory
{
    public class LightButton : IButton
    {
        public LightButton()
        {
            Color = "white";
            BackgroundColor = "blue";
        }

        public string Color { get; private set; }

        public string BackgroundColor { get; private set; }
    }
}
namespace DesignPatterns.Structural.Abstract_Factory
{
    public class DarkLabel : ILabel
    {
        public DarkLabel()
        {
            Color = "white";
        }

        public string Color { get; private set; }
    }
}
namespace DesignPatterns.Structural.Abstract_Factory
{
    public class LightLabel : ILabel
    {
        public LightLabel()
        {
            Color = "black";
        }

        public string Color { get; private set; }
    }
}
```

- Implemente um conjunto de classes fábricas concretas, uma para cada variante de produto.

```csharp
namespace DesignPatterns.Structural.Abstract_Factory
{
    public interface IAbstractWidgetFactory
    {
        ILabel CreateLabel();
        IButton CreateButton();
    }
}
namespace DesignPatterns.Structural.Abstract_Factory
{
    public class DarkThemeFactory : IAbstractWidgetFactory
    {
        public ILabel CreateLabel()
        {
            return new DarkLabel();
        }

        public IButton CreateButton()
        {
            return new DarkButton();
        }
    }
}
namespace DesignPatterns.Structural.Abstract_Factory
{
    public class LightThemeFactory : IAbstractWidgetFactory
    {
        public ILabel CreateLabel()
        {
            return new LightLabel();
        }

        public IButton CreateButton()
        {
            return new LightButton();
        }
    }
}
```

- Crie um código de inicialização da fábrica. Ele deve instanciar uma das classes fábricas concretas, dependendo da configuração da aplicação ou do ambiente atual. Passe esse objeto fábrica para todas as classes que constroem produtos.
- Substitua todas as chamadas diretas aos construtores do produto e chame o método de criação apropriado no objeto fábrica.

```csharp
namespace DesignPatterns.Structural.Abstract_Factory
{
    public class View
    {
        public View(IAbstractWidgetFactory abstractWidgetFactory)
        {
            Label = abstractWidgetFactory.CreateLabel();
            Button = abstractWidgetFactory.CreateButton();
        }

        public ILabel Label { get; private set; }
        public IButton Button { get; private set; }
    }
}
using DesignPatterns.Structural.Abstract_Factory;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DesignPatterns.Tests.AbstractFactory
{
    public class ViewTests
    {
        [Test]
        public void ShouldBeAbleToCreateAViewWithLightTheme()
        {
            var view = new View(new LightThemeFactory());

            using(new AssertionScope())
            {
                view.Label.Color.Should().Be("black");
                view.Button.Color.Should().Be("white");
                view.Button.BackgroundColor.Should().Be("blue");
            }
        }

        [Test]
        public void ShouldBeAbleToCreateAViewWithDarkTheme()
        {
            var view = new View(new DarkThemeFactory());

            using(new AssertionScope())
            {
                view.Label.Color.Should().Be("white");
                view.Button.Color.Should().Be("white");
                view.Button.BackgroundColor.Should().Be("black");
            }
        }
    }
}
```

## Structural

### Adapter ###
- Adapter é um padrão de projeto estrutural que permite que classes incompatíveis se comuniquem. Ele também serve como uma camada anticorrupção que permite desacoplamento.

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
Exemplos:
- Quando você quer ofertar um cartão de crédito, mas precisa executar diversas validações antes para saber se a pessoa é elegível.
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
- Visitor permite que você separe o comportamento/lógica do objeto em que ele opera.

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

### Strategy ###
- O Strategy é um padrão de projeto comportamental que permite selecionar um comportamento ou algoritmo em tempo de execução. O Strategy permite que o comportamento varie independentemente dos clientes que o utilizam, ou seja, torna-o intercambiável.
- Use o padrão quando você quer usar usar diferentes variantes de um algoritmo dentro de um objeto e quer trocar de um algoritmo para o outro em tempo de execução.
- Quando tem classes parecidas que só diferem na forma que eles executam o comportamento.
- Use o padrão quando você tem um operador condicional muito grande que troca diferentes algoritmos dentro da mesma classe.

**Exemplos**
- Cálculo de impostos com base numa faixa salarial.
- Um estacionamento em que o preço varia conforme o local (Praia, Shopping e Aeroporto, por exemplo).

**PROS**
- Princípio do aberto fechado: porque você pode introduzir novos comportamentos e algoritmos sem mudar o contexto.
- É possível trocar o algoritmo/comportamento usados dentro de um objeto durante a execução.
- Substitui herança por composição.
- É possível desacoplar os detalhes de implementação do algoritimo do código cliente que usa ele.

**CONS**
- Se não há muita variabilidade de algoritmos e eles raramente mudam, então não há motivo para usar o padrão, pois só vai tornar o programa mais complexo.
- Os clientes devem estar cientes das diferenças entre as estratégias para selecionar a adequada.

**Como implementar?**
- Encontre o algoritmo que varia bastante ou a condicional na classe contexto. A classe contexto mantém uma referência para uma das estratégias concretas e se comunica com esse objeto atráves da interface.
- O contexto chama o método de execução no objeto estratégia. Ele não sabe qual tipo de estratégia está rodando ou como o algoritmo está sendo executado.

```csharp
namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class ParkingLot
    {
        private readonly IList<Ticket> _tickets;
        private readonly ITicketCalculator _ticketCalculator;
        private readonly int _totalLots;

        public ParkingLot(string location, int totalLots)
        {
            _tickets = new List<Ticket>();
            _ticketCalculator = TicketCalculatorFactory.Create(location);
            _totalLots = totalLots;
        }

        public void CheckIn(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public void CheckOut(string plate, DateTime checkoutDate)
        {
            var ticket = GetTicket(plate);
            var period = new Period(ticket.CheckInDate, checkoutDate);
            ticket.Price = _ticketCalculator.Calculate(period);
        }

        public Ticket GetTicket(string plate)
        {
            var ticket = _tickets.FirstOrDefault(ticket => ticket.Plate == plate);

            if (ticket == null)
            {
                throw new Exception("Ticket not found!");
            }
            return ticket;
        }

        public int GetSlots()
        {
            return _totalLots - _tickets.Count;
        }
    }
}
```
- Declare a interface de estratégia comum a todos os algoritmos.

```csharp
namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public interface ITicketCalculator
    {
        long Calculate(Period period);
    }
}
```
- Extraia todos os algoritmos para suas próprias classes. Elas devem implementar a classe estratégia.

```csharp
namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class AiportCalculator : ITicketCalculator
    {
        private const int DAILY_RATE = 50;

        public long Calculate(Period period)
        {
            return DAILY_RATE * period.GetDiffInDays();
        }
    }
}
namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class BeachCalculator : ITicketCalculator
    {
        private const int HOURLY_RATE = 5;

        public long Calculate(Period period)
        {
            return HOURLY_RATE * period.GetDiffInHours();
        }
    }
}
namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class ShoppingCalculator : ITicketCalculator
    {
        private const long BASE_RATE = 10;
        private const int BASE_PERIOD = 3;
        private const int HOURLY_RATE = 3;

        public long Calculate(Period period)
        {
            var price = BASE_RATE;
            var remainingHours = period.GetDiffInHours() - BASE_PERIOD;

            if(remainingHours > 0)
            {
                price += remainingHours * HOURLY_RATE;
            }

            return price;
        }
    }
}
namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class Ticket
    {
        public string Plate { get; set; } = null!;
        public DateTime CheckInDate { get; set; }
        public decimal? Price { get; set; }
    }
}
namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class TicketCalculatorFactory
    {
        private static IDictionary<string, ITicketCalculator> calculators = new Dictionary<string, ITicketCalculator>()
        {
            {
                "beach", new BeachCalculator()
            },
            {
                "shopping", new ShoppingCalculator()
            },
            {
                "airport", new AiportCalculator()
            }
        };

        public static ITicketCalculator Create(string location)
        {
            if(!calculators.TryGetValue(location, out var findCalculator))
            {
                throw new Exception("Ticket calculator not found!");
            }

            return findCalculator;
        }
    }
}
```
- O cliente identifica qual estratégia ele quer chamar e chama pelo contexto.

```csharp
using DesignPatterns.Behavioral.Strategy_Parking;
using FluentAssertions;

namespace DesignPatterns.Tests.Strategy
{
    public class ParkingLotTests
    {
        [Test]
        public void ShouldBeAbleToCreateAParkingLot()
        {
            var parkingLot = new ParkingLot("airport", 500);
            parkingLot.GetSlots().Should().Be(500);
        }

        [Test]
        public void ShouldBeAbleToParkTheCarOnTheBeachForTwoHoursAndWhenLeavingTheValueShouldBeTen_FiveByHour()
        {
            var parkingLot = new ParkingLot("beach", 500);
            var ticketDto = new Ticket
            {
                CheckInDate = new DateTime(2021, 10, 01, 10, 00, 00),
                Plate = "AAA-1111",
            };
            
            parkingLot.CheckIn(ticketDto);
            parkingLot.CheckOut("AAA-1111", new DateTime(2021, 10, 01, 12, 00, 00));

            var ticket = parkingLot.GetTicket("AAA-1111");
            ticket.Price.Should().Be(10);
        }

        [Test]
        public void ShouldBeAbleToParkTheCarInTheShoppingForSevenHoursAndWhenLeavingTheValueShouldBeTwentyTwoTenTheFirstThreeHoursAndAfterThreeForHour()
        {
            var parkingLot = new ParkingLot("shopping", 500);
            var ticketDto = new Ticket
            {
                CheckInDate = new DateTime(2021, 10, 01, 10, 00, 00),
                Plate = "AAA-1111",
            };

            parkingLot.CheckIn(ticketDto);
            parkingLot.CheckOut("AAA-1111", new DateTime(2021, 10, 01, 17, 00, 00));

            var ticket = parkingLot.GetTicket("AAA-1111");
            ticket.Price.Should().Be(22);        
        }

        [Test]
        public void ShouldBeAbleToParkInTheAirportForThreeDaysAndWhenLeavingTheValueShouldBeOneHundredAndFiftyForDay()
        {
            var parkingLot = new ParkingLot("airport", 500);
            var ticketDto = new Ticket
            {
                CheckInDate = new DateTime(2021, 10, 01, 10, 00, 00),
                Plate = "AAA-1111",
            };

            parkingLot.CheckIn(ticketDto);
            parkingLot.CheckOut("AAA-1111", new DateTime(2021, 10, 04, 10, 00, 00));

            var ticket = parkingLot.GetTicket("AAA-1111");
            ticket.Price.Should().Be(150);
        }
    }
}
```

### Template Method ###
- É um padrão de projeto comportamental que permite que subclasses redefinam passos específicos de um algoritmo sem mudar a ordem e estrutura em que esses passos são executados.
- Geralmente usado quando as subclasses tem códigos muito semelhantes (quando existe um padrão).
- Utilize template method quando você quiser que o cliente estenda apenas etapas específicas de uma classe.
- Utilize quando você tem classes quase idênticas com pequenas diferenças.

**Exemplos**
- Preparação de uma bebida em que você tem uma ordem de preparação e ingredientes comuns e específicos.
- Um item que tem cálculos de impostos e tem as subclasses Whisky, Beer e Water, mas Water não tem imposto.

**PROS**
- O cliente pode sobrescrever apenas partes de um algoritmo grande.
- Pode reduzir código duplicado para uma superclasse.

**CONS**
- Pode violar o Princípio de substituição de Liskov ao suprimir uma etapa padrão de implementação atráves de subclasse.
- Implementações do Template Method tendem a ser mais difíceis de se manter quanto mais etapas eles tiverem.

**Como implementar?**
- Defina a classe abstrata e declare o método que servirá como templateMethod.

```csharp
public abstract class Item
{
    public Item(string category, string description, decimal price)
    {
        Category = category;
        Description = description;
        Price = price;
    }

    public string Category { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
}

public abstract class TaxItem : Item
{
    protected TaxItem(string category, string description, decimal price) 
        : base(category, description, price)
    {
    }

    public decimal CalculateTax()
    {
        return (Price * GetTaxRate()) / 100;
    }

    public abstract decimal GetTaxRate();
}
```

- Defina as subclasses concretas.

```csharp
public class Beer : TaxItem
{
    public Beer(string description, decimal price) 
        : base("Beer", description, price)
    {
    }

    public override decimal GetTaxRate()
    {
        return 10;
    }
}

public class Whisky : TaxItem
{
    public Whisky(string description, decimal price) 
        : base("Whisky", description, price)
    {
    }

    public override decimal GetTaxRate()
    {
        return 20;
    }
}

public class Water : Item
{
    public Water(string description, decimal price) 
        : base("Water", description, price)
    {
    }
}
```

- Defina as classes que irá chamar as classes abstratas e o cliente que irá utilizar

```csharp
public class Invoice
{
    private readonly IList<Item> _items;

    public Invoice()
    {
        _items = new List<Item>();
    }

    public void Add(Item item)
    {
        _items.Add(item);
    }

    public decimal GetTaxes()
    {
        return _items.Where(item => item is TaxItem)
                     .Cast<TaxItem>()
                     .Sum(taxItem => taxItem.CalculateTax());
    }
}

using DesignPatterns.Behavioral.TemplateMethod_Beverage;
using FluentAssertions;

namespace DesignPatterns.Tests.Template_Method
{
    public class TempleateMethodBeverageTests
    {
        [Test]
        public void ShouldBeAbleToCalculateTaxesOfInvoice()
        {
            var invoice = new Invoice();
            invoice.Add(new Beer("Heineken", 20));
            invoice.Add(new Whisky("Jack Daniels", 100));
            invoice.Add(new Water("Crystal", 5));

            var taxes = invoice.GetTaxes();

            taxes.Should().Be(22);
        }
    }
}
```

### Mediator ###
- O Mediator é um padrão de projeto comportamental que restringe comunicações diretas entre objetos e os força se comunicar por meio de um objeto mediador.
- Define um objeto que encapsula a forma como um conjunto de objetos interage.
- É como se fosse um motor de controle. É um tipo de padrão de desacoplamento.
- Utilize o padrão Mediator quando é difícil mudar algumas classes porque elas estão acopladas a outras.
- Utilize o padrão quando não puder reutilizar um componente porque ele é dependente de outros componentes.
- Utilize o padrão quando você criar muitas subclasses para componentes apenas para reusar algum comportamento básico em vários contextos.
- Relação com eventos, mensageria.

**Exemplos**
- Um chat é um bom exemplo de Mediator, pois facilita a comunicação entre vários usuários, grupos de usuários e funcionalidades adicionais.
- Um formulário de inscrição que você escolhe uma opção em um menu que pode habilitar ou desabilitar campos com base nas interações do usuário, o mediador pode centralizar essa lógica de habilitar ou desabilitar campos.
- Um painel de controle, onde diferentes widgets precisam interagir entre si. Por exemplo, ao atualizar um gráfico, a atualização pode refletir em um painel de resumo de estatística.

**PROS**
- Promove o desacoplamento ao evitar que os objetos se refiram uns aos outros explicitamente, permitindo variar suas interações independentemente.
- Princípio da responsabilidade única, pois você pode extrair as comunicações entre vários componentes para um único lugar.
- Princípio aberto/fechado você pode introduzir novos mediadores sem ter que modificar o componente.
- Você pode reutilizar componentes individuais mais facilmente.

**CONS**
- O objeto mediador pode se tornar um objeto Deus, ou seja, um objeto que sabe demais ou que faz demais.

**Como implementar?**
- Identifique classes que estão acopladas e que poderiam ser beneficiadas se estiverem mais independentes.
- Declare a interface do mediador e o método que vai receber as notificações.
- Implemente a classe concreta do mediador. A classe fica responsável por armazenar todas as referências que ela gerencia.
- Componentes devem armazenar uma referência ao objeto do mediador, geralmente no construtor passando o mediador por argumento.

```csharp
namespace DesignPatterns.Behavioral.Mediator_Channel
{
    public class Channel
    {
        private readonly IList<Participant> _participants;

        public Channel()
        {
            _participants = new List<Participant>();
        }

        public void Register(Participant participant)
        {
            _participants.Add(participant);
        }

        public void SendAll(Participant from, string message)
        {
            foreach(var to in _participants)
            {
                if (from != to)
                {
                    to.Receive(from, message);
                }
            }
        }
    }
}
```
- Mude as classes para chamar o objeto mediador ao invés dos outros componentes diretamente.

```csharp
using DesignPatterns.Behavioral.Mediator_Channel;

namespace DesignPatterns.Tests.Mediator
{
    public class ChatTests
    {
        [Test]
        public void ShouldBeAbleToExchangeMessagesBetweenTheChannel()
        {
            var firstParticipant = new Participant("FirstParticipant");
            var secondParticipant = new Participant("SecondParticipant");
            var thirdParcipant = new Participant("ThirdParticipant");

            var channel = new Channel();
            channel.Register(firstParticipant);
            channel.Register(secondParticipant);
            channel.Register(thirdParcipant);

            channel.SendAll(firstParticipant, "Hello!");
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

$ dotnet restore

```

To initiate the tests, follow the steps below:
```bash
$ dotnet test
```


