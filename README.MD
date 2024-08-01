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

            using (new AssertionScope())
            {
                view.Label.Color.Should().Be("white");
                view.Button.Color.Should().Be("white");
                view.Button.BackgroundColor.Should().Be("black");
            }
        }
    }
}
```

### Prototype ###
- Prototype é um padrão de projeto criacional que permite copiar objetos sem depender das suas classes.
- Permite que você clone objetos privados sem a dependência da classe concreta, ao contrário da abordagem direta passando campo a campo para um novo objeto.
- Delega o processo de clonagem para o próprio objeto.
- Utilize o padrão quando seu código não puder depender da classe concreta que você deseja copiar.
- Utilize o padrão quando você tem muitas subclasses que só diferem na forma que inicializam seus objetos.
- O padrão pode ser útil para quando você precisa salvar cópias de comandos (Command - padrão de projeto) no histórico.
- Algumas vezes pode ser uma alternativa ao padrão de projeto Memento.

**Exemplos**:
- Editor de gráficos ou imagens, pois pode ser feita a cópia de círculos, quadrados, linhas e etc.
- Algumas configurações da aplicação pesadas podem se benefeciar do uso do Prototype para um carregamento mais rápido.
- Jogos, pois você pode copiar as características de um personagem.
- Sistemas de gerenciamento de documentos, pois muitas vezes esses documentos tem pequenas variações.

**PROS**
- Clonar objetos sem dependência da classe concreta.
- Evitar códigos de inicialização repetidos.
- Facilidade para construção de objetos complexos.

**CONS**
- Clonar objetos com referências circulares pode ser difícil.

**Como implementa?**
- Crie a interface do Prototype com um método clone.

```csharp
namespace DesignPatterns.Creational.Prototype
{
    public interface IVehiclePrototype
    {
        IVehiclePrototype Clone();
    }
}
```

- Uma classe Prototype deve definir o construtor alternativo que aceita os objetos da classe como argumento. O construtor deve copiar os valores de todos os campos definidos na classe do objeto passado para a nova instância.
- O método de clonagem consiste em uma linha executando um operador new com a versão do protótipo do construtor ou no nosso caso, usando o método MemberWiseClone para clonar o objeto.

```csharp
namespace DesignPatterns.Creational.Prototype
{
    public class Car : IVehiclePrototype
    {
        public string Model { get; private set; }

        public string Color { get; private set; }

        public int Seats { get; private set; }

        public Car(string model, string color, int seats)
        {
            Model = model;
            Color = color;
            Seats = seats;
        }

        public IVehiclePrototype Clone()
        {
            return (IVehiclePrototype)MemberwiseClone();
        }

        public void SetColor(string color)
        {
            Color = color;
        }

        public override string ToString()
        {
            return $"Car: {Model}, Color: {Color}, Seats: {Seats}";
        }
    }
}

using DesignPatterns.Creational.Prototype;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DesignPatterns.Tests.Prototype
{
    public class CarTests
    {
        [Test]
        public void ShouldBeAbleToCloneAnExactlyCopyOfTheCar()
        {
            var car = new Car("Toyota Camry", "Red", 5);

            var clonedCar = car.Clone() as Car;

            using (new AssertionScope())
            {
                car.Model.Should().Be(clonedCar!.Model);
                car.Color.Should().Be(clonedCar.Color);
                car.Seats.Should().Be(clonedCar.Seats);
            }
        }

        [Test]
        public void ShouldBeAbleToCloneAndModifyWithoutAffectingTheOriginalCar()
        {
            var car = new Car("Toyota Camry", "Red", 5);

            var clonedCar = car.Clone() as Car;
            clonedCar!.SetColor("Blue");

            using (new AssertionScope())
            {
                car.Color.Should().Be("Red");
                clonedCar.Color.Should().Be("Blue");
            }
        }
    }
}
```
### Singleton ###
- O Singleton é um padrão de projeto criacional que permite que a classe tenha apenas uma instância, enquanto provê um ponto de acesso global para cada instância.
- Utilize o padrão Singleton quando uma classe deve ter apenas uma instância disponível.
- Utilize o padrão quando você quiser um controle mais estrito sobre as variáveis globais.

**Exemplos**
- Configurações do sistema.
- Conexões com o banco de dados.
- Escrita de logs.

**PROS**
- Garantia de que a classe só vai ter uma instância.
- Acesso global a instância.
- O objeto é inicializado somente quando é chamado pela primeira vez.

**CONS**
- Viola o princípio da responsabilidade única, pois resolve dois problemas de uma vez só: garantir que uma classe só tenha uma instância e fornece um ponto de acesso global a instância.
- Pode fazer com que as classes do programa conheçam muito sobre cada uma.
- Requer cuidado quando está se trabalhando com multithread, pois pode criar o objeto Singleton múltiplas vezes.
- Pode ser difícil de testar, uma vez que oculta o construtor privado e muitos frameworks utilizam herança para produzirem objetos simulados.

**Como implementar?**
- Adicione um campo privado estático para o armazenamento da instância Singleton.
- Declare um método de criação público estático para obter a instância do Singleton.
- Faça o construtor "vazio" ser privado.

```csharp
namespace DesignPatterns.Creational.Singleton
{
    public class BusinessHours
    {
        private static BusinessHours _instance = null!;
        
        private BusinessHours(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }


        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public static BusinessHours GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BusinessHours(
                    new DateTime(1, 1, 1, new Random().Next(0, 10), 0, 0),
                    new DateTime(1, 1, 1, new Random().Next(18, 24), 0, 0)
                    );
            }

            return _instance;
        }
    }
}
```

- Mude todas as chamadas diretas para o construtor do Singleton chamando o método de criação estático.

```csharp
using DesignPatterns.Creational.Singleton;
using FluentAssertions;

namespace DesignPatterns.Tests.Singleton
{
    public class BusinessHoursTests
    {
        [Test]
        public void ShouldBeAbleToGetAnInstance()
        {
            var instance = BusinessHours.GetInstance();
            
            instance.Should().NotBeNull();
        }

        [Test]
        public void ShouldBeAbleToCheckIfInstancesAreTheSame()
        {
            var firstIntance = BusinessHours.GetInstance();
            var secondIntance = BusinessHours.GetInstance();

            firstIntance.Should().Be(secondIntance);
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

**Como implementar?**
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

### Decorator ou Wrapper ###
- O Decorator é um padrão de projeto estrutural que permite que você adicione novos comportamentos a um objeto ao envolvê-lo num wrapper que contém os comportamentos.
- Dinamicamente agrega funcionalidades adicionais ao objeto. Fornece uma alternativa flexível ao uso de subclasses.
- O Decorator é estruturado com base na **agregação** e **composição**. Essa abordagem permite que um objeto possa usar o comportamento de várias classes, ter referência a múltiplos objetos e delegue qualquer trabalho a eles.
- Utilize o padrão quando você precisa adicionar comportamentos para objetos em tempo de execução sem quebrar o código que usa esses objetos.
- Utilize o padrão quando é complicado ou impossível usar herança para estender um comportamento.

**Exemplos**:
- Biblioteca de notificação que precisa enviar a mesma notificação por múltiplos canais (SMS, E-mail, Instagram, Facebook). Nesse caso, tem uma classe Notificador com método enviar e as demais classes SMS, E-mail, Instagram e Facebook são decorators.
- Para conseguir customizar logs da aplicação. 

**PROS**
- Pode estender o comportamento de um objeto sem fazer uma nova subclasse.
- Adicionar ou remover comportamentos de um objeto no momento da execução.
- Combinar comportamentos ao envolver o projeto com múltiplos decorators.
- Princípio da responsabilidade única: Pode dividir uma classe que implementa muitos comportamentos em classes menores. 

**CONS**
- É difícil remover um wrapper de uma pilha de wrappers.
- É difícil implementar um decorator que seu comportamento não dependa da ordem da pilha de decorators. 

**Como implementar?**
- Descubra quais métodos são comuns para o componente e para as camadas opcionais. Crie uma interface componente e declare os métodos.
- Crie uma classe componente concreta e defina o comportamento base nela.

```csharp
namespace DesignPatterns.Structural.Decorator
{
    public class Input
    {
        public string Cpf { get; set; }
        public IList<Item> Items { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}

namespace DesignPatterns.Structural.Decorator
{
    public interface IUseCase
    {
        public void Execute(Input input);
    }
}
namespace DesignPatterns.Structural.Decorator
{
    public class PlaceOrder : IUseCase
    {
        public void Execute(Input input)
        {
            Console.WriteLine($"Executando o place order {input}");
        }
    }
}
namespace DesignPatterns.Structural.Decorator
{
    public class SimulateFreight : IUseCase
    {
        public void Execute(Input input)
        {
            Console.WriteLine($"Executando o simulate freight: {input}");
        }
    }
}
namespace DesignPatterns.Structural.Decorator
{
    public class ValidateCoupon : IUseCase
    {
        public void Execute(Input input)
        {
            Console.WriteLine($"Executando o validate coupon {input}"); 
        }
    }
}
```

- Crie uma classe decorator base. Ela deve ter um campo para armazenar uma referência ao objeto envolvido. O campo deve ser declarado com o tipo da interface componente para permitir uma ligação entre os componentes concretos e decoradores. O decorador base deve delegar todo o trabalho para ao objeto envolvido.

```csharp
namespace DesignPatterns.Structural.Decorator
{
    public class LoggerUseCaseDecorator : IUseCase
    {
        private readonly IUseCase _useCase;

        public LoggerUseCaseDecorator(IUseCase useCase)
        {
            _useCase = useCase;    
        }

        public void Execute(Input input)
        {
            Console.WriteLine("Executando decorator de Log");
            _useCase.Execute(input);
        }
    }
}
```

- Crie decoradores concretos estendendo-os a partir do decorador base. Um decorador concreto deve executar seu comportamento antes ou depois da chamada para o método pai, que delega o comportamento para o objeto envolvido.

```csharp
using System.Diagnostics;

namespace DesignPatterns.Structural.Decorator
{
    public class PerformanceUseCaseDecorator : IUseCase
    {
        private readonly IUseCase _useCase;

        public PerformanceUseCaseDecorator(IUseCase useCase)
        {
            _useCase = useCase;
        }

        public void Execute(Input input)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            _useCase.Execute(input);
            stopWatch.Stop();

            Console.WriteLine($"Performance: {stopWatch.Elapsed}");
        }
    }
}
```
- O cliente deve ser responsável por criar decoradores e compô-los da maneira desejada.

```csharp
using DesignPatterns.Structural.Decorator;

namespace DesignPatterns.Tests.Decorator
{
    public class DecoratorTests
    {
        [Test]
        public void ShouldBeAbleToLogOperationsInAUseCase()
        {
            var input = new Input
            {
                Cpf = "111111111",
                Items = new List<Item>
                {
                    new Item { Id = 1, Count = 1 },
                    new Item { Id = 2, Count = 1 },
                    new Item { Id = 3, Count = 3 },
                }
            };

            var placeOrder = new LoggerUseCaseDecorator(new PerformanceUseCaseDecorator(new PlaceOrder()));
            placeOrder.Execute(input);

            var simulateFreight = new LoggerUseCaseDecorator(new PerformanceUseCaseDecorator(new SimulateFreight()));
            simulateFreight.Execute(input);
        }
    }
}
```

### Proxy ###
- O Proxy é um padrão de projeto estrutural que permite que você forneça um substituto e controla o acesso ao objeto original, permitindo que você faça algo antes ou depois do pedido chegar no objeto original.
- Ao contrário do Facade, o Proxy tem o mesma interface que seu objeto de serviço.
- Ao contrário do Decorator, que o ciclo de vida é gerenciado pelo cliente, o Proxy geralmente gerencia o ciclo de vida de seu objeto serviço.
- Utilize o padrão quando você precisa de uma inicialização preguiçosa, quando o objeto é muito pesado e gasta muitos recursos do sistema por estar sempre rodando.
- Controle de acesso, o proxy pode passar o request somente se as credenciais coincidirem com os critérios.
- Execução de um serviço remoto (proxy remoto), nesse caso o proxy lida com toda a parte de redes.
- Execução de cache de resultado de pedidos.
- Referência inteligente para analisar quando deve dispensar um objeto que utiliza recursos muito pesados, mas não está sendo utilizado.

**Exemplos**:
- Um conversor que traduz json para xml e vice-versa antes de enviar para os objetos reais.
- Proxy de cache que armazena em cache resultados de consulta para evitar consumir a API real desnecessariamente.
- Proxy de segurança que controla o acesso com base nas permissões antes de permitir consumir uma API. 

**PROS**
- Pode controlar o objeto do serviço sem os clientes ficarem sabendo.
- Pode gerenciar o ciclo de vida de um objeto do serviço, quando os clientes não precisam mais dele.
- Pode trabalhar quando o objeto do serviço não está pronto ou indisponível.
- Princípio aberto e fechado: Pode introduzir novos proxies sem mudar o serviço ou clientes.

**CONS**
- O código pode ficar mais complexo, já que você precisa introduzir novas classes.
- Pode ter delay na resposta de um serviço.

**Como implementar?**
- Crie uma interface o objeto proxy e o serviço serem intercomunicáveis. Quando não for possível faça proxy ser uma subclasse do serviço.
- Crie a classe proxy que contém um campo de referência para o serviço. Proxies costumam gerenciar todo o ciclo de vida, sendo em raras ocasiões, um serviço passado ao proxy atráves do construtor do cliente.
- Implemente os métodos proxy e após a realização do trabalho, delegue o trabalho para o objeto serviço.

```csharp
namespace DesignPatterns.Structural.Proxy
{
    public class Customer
    {
        public Customer(string fullName, DateTime birthDate)
        {
            FullName = fullName;
            BirthDate = birthDate;
        }

        public string FullName { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
using Microsoft.Extensions.Caching.Memory;

namespace DesignPatterns.Structural.Proxy
{
    public interface IMemoryCacheWrapper
    {
        object GetOrCreate(string key, Func<ICacheEntry, object> cacheFactory);
    }
}
using Microsoft.Extensions.Caching.Memory;

namespace DesignPatterns.Structural.Proxy
{
    public class MemoryCacheWrapper : IMemoryCacheWrapper
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheWrapper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public object GetOrCreate(string key, Func<ICacheEntry, object> cacheFactory)
        {
            return _memoryCache.GetOrCreate(key, cacheFactory)!;
        }
    }
}
namespace DesignPatterns.Structural.Proxy
{
    public class CustomerRepository
    {
        public virtual IList<Customer> GetBlockedUsers()
        {
            return new List<Customer>
            {
                new Customer("Fulano", DateTime.Now.AddYears(-20)),
                new Customer("Fulano", DateTime.Now.AddYears(-30)),
                new Customer("Fulano", DateTime.Now.AddYears(-40))
            };
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace DesignPatterns.Structural.Proxy
{
    public class CustomerRepositoryProxy
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IMemoryCacheWrapper _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerRepositoryProxy(
            CustomerRepository customerRepository,
            IMemoryCacheWrapper cache,
            IHttpContextAccessor httpContextAccessor)
        {
            _customerRepository = customerRepository;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Customer?> GetBlockedUsers()
        {
            var httpContext = _httpContextAccessor?.HttpContext;

            if(httpContext == null)
            {
                return null!;
            }

            if(httpContext.Request.Headers["x-role"] != "admin")
            {
                return null!;
            }

            var blockedCustomers = (IList<Customer?>)_cache.GetOrCreate("blocked-customers", c =>
            {
                return _customerRepository.GetBlockedUsers();
            });

            return blockedCustomers!;
        }
    }
}
```


### Flyweight ###
- Flyweight é um padrão de projeto estrutural que permite otimizar o uso de objetos alocados na memória RAM, compartilhando estados comuns entre os objetos, ao invés de criar um para cada objeto.
- Utilize o padrão Flyweight apenas quando seu programa deve suportar um grande números de objetos que não cabe na memória RAM.
- Lembre-se de sempre separar os estados mutáveis dos imutáveis, o padrão deve ser aplicado para estados imutáveis.

**Exemplos**:
- Renderização de gráficos e textos para reduzir o consumo de memória, reaproveitando a forma ou a fonte em outros objetos.
- Gestão de estados de jogos para armazenar as partes imutáveis dos objetos, como modelo 3D, textura em uma instância compartilhada.

**PROS**
- Pode economizar muita RAM, desde que o sistema tenha muitos objetos similares.

**CONS**
- Pode estar trocando RAM por ciclos de CPU quando parte dos dados de contexto precisa ser recalculado cada vez que é chamado um método flyweight.
- O código fica mais complexo, nem sempre é fácil perceber porque existe uma separação dos estados.

**Como implementar?**
- Divida os campos de uma classe que vai se tornar um flyweight em duas partes:
    - estado intrínseco: os campos que contém dados imutáveis e duplicados em muitos objetos.
    - estado extrínseco: os campos que contém dados únicos para cada objeto.
- Deixe os campos imutáveis dentro da classe, certifique-se de que eles não são mutáveis. Eles só podem obter valor inicial por meio do construtor.
- Para os métodos que usam campos para obter os dados mutáveis introduza um novo parâmetro e use-o ao invés do campo.
- Opcionalmente pode-se criar uma factory para gerenciar o conjunto de flyweights e checar-se se já existe antes de criar um novo. Os clientes devem pedir flyweights a partir da fábrica.
- O cliente deve armazenar ou calcular valores para o estado extrínseco (contexto) para ser capaz de chamar métodos do objetos flyweight.


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
- Muitos para muitos.

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

### Command ###
- O Command é um padrão de projeto comportamental que encapsula uma solicitação como um objeto.
- Desacopla quem chama de quem é chamado.
- Vem com o objetivo de separar leitura da escrita. 
- Permite que cliente diferentes parametrizem comandos de forma diferente.
- Enfileira ou faz registros de solicitações.
- Suporta operações que podem ser desfeitas.
- O retorno de um Command é sempre void, pelo motivo de que você desacopla quem chama de quem é chamado (não tem quem espere a resposta).
- Utilize o padrão quando você quer colocar operações em fila, agendar sua execução ou executá-las após um tempo.
- Utilize o padrão Command quando você quer implementar operações reversíveis.

**Exemplos**
- Os débitos e créditos de uma conta, pois é importante ter todo o histórico e conseguir desfazer ou refazer uma operação.
- Editor de texto.
- Cenários em que você deseja separar leitura de escrita.

**PROS**
- Princípio da responsabilidade única: É possível desacoplar classes que invocam operações de classes que fazem operações.
- Princípio do aberto e fechado: É possível introduzir novos comandos sem quebrar o código já existente.
- Você pode implementar desfazer/refazer.
- Você pode implementar a execução adiada de operações.
- Você pode montar um conjunto de comandos simples em um complexo. 

**CONS**
- O código pode ficar mais complicado já que você está introduzindo uma nova camada entre remetentes e destinatários.

**Como implementar?**
- Declare a interface de command com um único método de execução.

```csharp
namespace DesignPatterns.Behavioral.Command_Transaction
{
    public interface ICommand
    {
        void Execute();
    }
}
```

- Extraia os pedidos para dentro das classes concretas que implementam a classe command. Cada classe deve ter um conjunto de campos para armazenar os argumentos dos pedidos junto com a referência ao objeto destinatário real. Os valores devem ser inicializados no construtor do command.

```csharp
namespace DesignPatterns.Behavioral.Command_Transaction
{
    public class CreditCommand : ICommand
    {
        private readonly Account _account;      
        private readonly decimal _amount;

        public CreditCommand(Account account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        public void Execute()
        {
            _account.Credit(_amount);
        }
    }
}
namespace DesignPatterns.Behavioral.Command_Transaction
{
    public class DebitCommand : ICommand
    {
        private readonly Account _account;
        private readonly decimal _amount;

        public DebitCommand(Account account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        public void Execute()
        {
            _account.Debit(_amount);
        }
    }
}
```

- Identifique as classes que vão ser remetentes. Adicione os campos para armazenar commands nessa classe. Remetentes devem sempre se comunicar com seus comandos atráves da interface command.
- Mude os remetentes para executar o command ao invés de enviar o pedido para o destinatário diretamente.

```csharp
namespace DesignPatterns.Behavioral.Command_Transaction
{
    public class Transaction
    {
        public Transaction(string type, decimal amount) 
        {
            Type = type;
            Amount = amount;
        }

        public string Type { get; private set; }

        public decimal Amount { get; private set; }
    }
}

namespace DesignPatterns.Behavioral.Command_Transaction
{
    public class Account
    {
        private readonly IList<Transaction> _transactions;

        public Account()
        {
            _transactions = new List<Transaction>();
        }

        public void Credit(decimal amount)
        {
            var transaction = new Transaction("credit", amount);
            _transactions.Add(transaction);
        }

        public void Debit(decimal amount)
        {
            var transaction = new Transaction("debit", amount);
            _transactions.Add(transaction);
        }

        public decimal GetBalance()
        {
            decimal balance = 0;
            foreach (var transaction in _transactions)
            {
                if(transaction.Type == "credit")
                {
                    balance += transaction.Amount;
                }

                if(transaction.Type == "debit")
                {
                    balance -= transaction.Amount;
                }
            }

            return balance;
        }
    }
}
```
- O cliente deve inicializar objetos criando os destinatários, criando os commands e os associando com os destinatários se necessário, ou criando os remetentes e associando os commands específicos.

```csharp
using DesignPatterns.Behavioral.Command_Transaction;
using FluentAssertions;

namespace DesignPatterns.Tests.Command
{
    public class AccountTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnAccount()
        {
            var account = new Account();
            var balance = account.GetBalance();

            balance.Should().Be(0);
        }

        [Test]
        public void ShouldBeAbleToCreditFromAnAccountUsingCommand()
        {
            var account = new Account();
            var creditCommand = new CreditCommand(account, 100);
            
            creditCommand.Execute();

            var balance = account.GetBalance();
            balance.Should().Be(100);
        }

        [Test]
        public void ShouldBeAbleToDebitFromAnAccountUsingCommand()
        {
            var account = new Account();
            var creditCommand = new CreditCommand(account, 100);
            var debitCommand = new DebitCommand(account, 50);

            creditCommand.Execute();
            debitCommand.Execute();

            var balance = account.GetBalance();
            balance.Should().Be(50);
        }
    }
}
```

### Observer (Publish-Subscriber) ###
- O observer é um padrão de projeto comportamental que permite que você defina uma forma de assinatura para que vários objetos tenham ciência de quando um evento acontecer sobre o objeto que estão observando.
- Quando o objeto muda de estado, os seus dependentes são notificados e atualizados.
- Utilize o padrão quando observer quando mudanças no estado de um objeto podem precisar mudar outros objetos e o conjunto de objetos é desconhecido ou muda dinamicamente.

**Exemplos**
- Para assinatura de um produto específico (Ex: Um cliente que quer receber notificação sobre quando uma TV específica estiver a venda ou em promoção).
- Quando você quer executar alguma ação em um componente após um evento em outro componente acontecer (Ex: Mudar a label do País para Brasil quando o input receber esse valor). 

**PROS**
- Princípio aberto fechado: Você pode introduzir novas classes assinantes sem ter que modificar o código da publicadora e vice-versa.
- Você pode estabelecer relações entre objetos durante a execução.

**CONS**
- Assinantes são notificados em ordem aleatória.

**Como implementar?**
- Identifique a funcionalidade principal e defina-a como publicadora. As outras classes serão as assinantes.
- Declare a interface do assinante com um método para notificar os assinantes.

```csharp
namespace DesignPatterns.Behavioral.Observer_UI
{
    public interface IObserver
    {
        void Notify(string name, string value);
    }
}
```

- Declare a interface da publicadora e descreva métodos para adicionar e remover um assinante da lista. As classes publicadoras só devem se comunicar com os assinantes por meio do assinante.
- Decida onde colocar a lista atual dos assinantes e a implementação do métodos para adição e remoção de assinantes, o melhor lugar é numa classe abstrata derivada da classe publicadora.

```csharp
namespace DesignPatterns.Behavioral.Observer_UI
{
    public abstract class Observable
    {
        public IList<IObserver> _observables;

        public Observable()
        {
            _observables = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            _observables.Add(observer);
        }

        public void NotifyAll(string name, string value)
        {
            foreach(var observer in _observables)
            {
                observer.Notify(name, value);
            }
        }
    }
}
```

- Crie as classes publicadoras concretas e a cada vez que algo acontece, ela deve notificar seus assinantes.

```csharp
namespace DesignPatterns.Behavioral.Observer_UI
{
    public class InputText : Observable
    {
        public InputText(string name)
            : base()
        {
            Value = "";
            Name = name;
        }

        public string Value { get; private set; }
        public string Name { get; private set; }

        public void SetValue(string value)
        {
            Value = value;
            NotifyAll(Name, Value);
        }
    }
}
```
- Crie as classes concretas dos assinantes.

```csharp
namespace DesignPatterns.Behavioral.Observer_UI
{
    public class Label : IObserver
    {
        private readonly string _expression;

        public Label(string expression)
        {
            _expression = expression;
            Value = "";
        }

        public string Value { get; private set; }

        public void Notify(string name, string value)
        {
            Value = _expression.Replace($"{name}", value);
        }
    }
}
```

- O cliente deve criar todos os assinantes necessários e registrá-los com suas publicadoras.

```csharp
using DesignPatterns.Behavioral.Observer_UI;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DesignPatterns.Tests.Observer
{
    public class ComponentTests
    {
        [Test]
        public void ShouldBeAbleToWriteInTheInputAndShowUpdatedDataInTheLabel()
        {
            var inputText = new InputText("country"); //observable
            var firstCountryLabel = new Label($"País: {inputText.Name}"); //observer
            var secondCountryLabel = new Label($"Country: {inputText.Name}"); //observer

            inputText.Subscribe(firstCountryLabel);
            inputText.Subscribe(secondCountryLabel);
            inputText.SetValue("Brasil");

            using (new AssertionScope())
            {
                firstCountryLabel.Value.Should().Be("País: Brasil");
                secondCountryLabel.Value.Should().Be("Country: Brasil");
            }

            inputText.SetValue("França");

            using (new AssertionScope())
            {
                firstCountryLabel.Value.Should().Be("País: França");
                secondCountryLabel.Value.Should().Be("Country: França");
            }
        }
    }
}
```

### State ###
- O State é um padrão de projeto comportamental que permite que um objeto altere seu comportamento com base num estado interno.
- Semelhante ao padrão Strategy, mas o State pode estar ciente de outro estado e transitar de um para o outro, enquanto o Strategy raramente sabe sobre as outras estratégias.
- Utilize o padrão quando você tem um objeto que se comporta de maneira diferente dependendo do seu estado atual, quando o número de estados é enorme e quando o código específico muda com frequência.
- Utilize o padrão quando você tem muitas condicionais grandes que alteram como a classe se comporta de acordo com os valores atuais.
- Utilize o padrão quando você tem muito código duplicado em muitos estados parecidos e transições baseadas em condições.

**Exemplos**
-  Um documento que tenha três estados: rascunho, moderação e publicado e cada um desses status é responsável por fazer algo diferenre no sistema.
- Um pedido que tem os status pendente, confirmado e cancelado e a partir de cada estado tem que ser feito uma ação diferente.
- Um editor de texto que a partir de um seleção, como por exemplo, negrito, muda a cor do texto para negrito.

**PROS**
- Princípio da responsabilidade única: Separa os estados em classes separadas.
- Princípio aberto e fechado: Introduz novos estados sem quebrar código já existente.
- Evita o uso de condicionais muito grandes e complexas.

**CONS**
- A aplicação pode ser over-engineer se a classe não tiver muito estados ou se raramente os status mudam.

**Como implementar?**
- Decida qual classe vai agir como contexto. Pode ser uma classe existente que já tenha código dependente do estado.
- Na classe contexto, adicione um campo de referência do tipo interface do estado e um setter público que permite sobrescrever o valor daquele campo.
- Para trocar o estado do contexto, crie uma instância de uma das classes de estado e passe para o contexto. Pode ser feito dentro do próprio contexto, ou em vários estados, ou no cliente. Lembre-se que a classe se torna dependente da classe concretaa de estado que ela instanciou.

```csharp
namespace DesignPatterns.Behavioral.State
{
    public class Order
    {
        public Order()
        {
            Status = new PendingStatus(this);
        }

        public OrderStatus Status { get; set; }

        public void Confirm()
        {
            Status.Confirm();
        }

        public void Cancel()
        {
            Status.Cancel();
        }
    }
}
```
- Declare a interface do estado, colocando métodos que contenham comportamentos específico ao estado.

```csharp
namespace DesignPatterns.Behavioral.State
{
    public abstract class OrderStatus
    {
        private readonly Order _order;

        public OrderStatus(Order order)
        {
            _order = order;
        }

        public abstract string Value { get; set; }

        public abstract void Confirm();

        public abstract void Cancel();
    }
}
```

- Para cada estado, crie uma classe concreta que deriva da interface do estado. Então, extraia todo o conteúdo do contexto e coloque nessa classe.

```csharp
namespace DesignPatterns.Behavioral.State
{
    public class PendingStatus : OrderStatus
    {
        private readonly Order _order;

        public PendingStatus(Order order)
            : base(order)
        {
            _order = order;
            Value = "Pending";
        }

        public override string Value { get; set; } = null!;

        public override void Confirm()
        {
            _order.Status = new ConfirmedStatus(_order);
        }

        public override void Cancel()
        {
            _order.Status = new CancelledStatus(_order);
        }
    }
}

namespace DesignPatterns.Behavioral.State
{
    public class ConfirmedStatus : OrderStatus
    {
        private readonly Order _order;

        public ConfirmedStatus(Order order)
            : base(order)
        {
            Value = "Confirmed";
            _order = order;
        }

        public override string Value { get; set; } = null!;

        public override void Confirm()
        {
            throw new Exception("The order is already confirmed!");
        }
        
        public override void Cancel()
        {
            _order.Status = new CancelledStatus(_order);
        }
    }
}

namespace DesignPatterns.Behavioral.State
{
    public class CancelledStatus : OrderStatus
    {
        private readonly Order _order;

        public CancelledStatus(Order order)
            : base(order)
        {
            _order = order;
            Value = "Cancelled";
        }

        public override string Value { get; set; }

        public override void Confirm()
        {
            throw new Exception("The order is already cancelled!");
        }

        public override void Cancel()
        {
            throw new Exception("The order is already cancelled!");
        }
    }
}

using DesignPatterns.Behavioral.State;
using FluentAssertions;

namespace DesignPatterns.Tests.State
{
    public class OrderStatusTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnPedingStatus()
        {
            var order = new Order();
            order.Status.Value.Should().Be("Pending");
        }

        [Test]
        public void ShouldBeAbleToChangeOrderStatusToConfirmed()
        {
            var order = new Order();
            order.Confirm();

            order.Status.Value.Should().Be("Confirmed");
        }

        [Test]
        public void ShouldBeAbleToChangeOrderStatusToCancelled()
        {
            var order = new Order();
            order.Cancel();

            order.Status.Value.Should().Be("Cancelled");
        }

        [Test]
        public void ShouldNotBeAbleToChangeOrderStatusToConfirmedIfTheOrderHasAlreadyBeenCancelled()
        {
            var order = new Order();
            order.Cancel();

            order.Invoking(o => o.Confirm())
                .Should().Throw<Exception>()
                .WithMessage("The order is already cancelled!");
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


