using Controllers;
using Models;
using Repositories;
using System.Configuration;
using System.Xml.Linq;

internal class Program
{
    
    private static readonly int connectionType = 0; // 0 - ADO.NET  1 - Dapper
    // appsettings.json e App.config devem estar com o mesmo banco de dados AndreVeiculosAPI
    // executar database-update para gerar as tabelas antes
    private static void Main(string[] args)
    {
        /*
        int opt = -1;
        do
        {
            Console.WriteLine("André Veículos");
            Console.WriteLine("1 - Gerar JSON");
            Console.WriteLine("2 - Inserir JSON no SQL");
            Console.WriteLine("3 - Criar/Associar Serviço");
            Console.WriteLine("4 - Extrair XMLs");
            Console.WriteLine("5 - Testar Insert de registros");
            opt = int.Parse(Console.ReadLine());
            switch (opt)
            {

                case 0:
                    Console.WriteLine("Adeus");
                    break;
                case 1:
                    Geracao();
                    break;
                case 2:
                    Ingestao();
                    break;
                case 3:
                    Associacao();
                    break;
                case 4:
                    Extracao();
                    break;
                case 5:
                    TesteInsert();
                    break;

            }
        } while (opt != 0);


        // Boleto DateOnly -> DateTime, erro no Dapper na conversão
        // Pagamento -> receber cartao = nro cartao, boleto = numero do boleto e pix = chavepix (ao invés do Ids de cada um)


    }

    static void TesteInsert()
    {

        // Pagamentos

        // Tipo de Pix
        PixType pixType = new PixType
        {
            Name = "CPF"
        };
        PixTypeController pixTypeController = new PixTypeController();
        var pixTypeId = pixTypeController.Insert(pixType, connectionType);
        pixType.Id = pixTypeId;

        // Pix
        Pix pix = new Pix
        {
            PixKey = "Teste",
            PixType = pixType
        };
        PixController pixController = new PixController();
        var pixId = pixController.Insert(pix, connectionType);
        pix.Id = pixId;

        // Boleto
        Boleto boleto = new Boleto
        {
            ExpirationDate = new DateTime(2024, 10, 10),
            Number = 105
        };
        BoletoController boletoController = new BoletoController();
        var boletoId = boletoController.Insert(boleto, connectionType);
        boleto.Id = boletoId;

        // Cartao de Credito
        CreditCard creditCard = new CreditCard
        {
            CardNumber = "1234 5678 9101 2345",
            SecurityCode = "099",
            ExpirationDate = "06/2025",
            CardName = "GUILHERME F S"
        };
        CreditCardController creditCardController = new CreditCardController();
        var cardId = creditCardController.Insert(creditCard, connectionType);
        creditCard.Id = cardId;

        // Pagamento
        Payment payment = new Payment
        {
            Boleto = boleto,
            CreditCard = creditCard,
            Pix = pix,
            PaymentDate = new DateTime(2024, 06, 05)
        };
        PaymentController paymentController = new PaymentController();
        var paymentId = paymentController.Insert(payment, connectionType);


        // Cargo
        Role role = new Role { Description = "Vendedor" };
        RoleController roleController = new RoleController();
        var idRole = roleController.Insert(role, connectionType);
        role.Id = idRole;



        // Endereço
        Address address = new Address
        {
            CEP = "14.802-000",
            City = "Araraquara",
            Complement = "",
            Neighborhood = "Centro",
            Number = 50,
            Street = "Avenida Brasil",
            StreetType = "Avenida",
            Uf = "SP"
        };
        AddressController addressController = new AddressController();
        var addressId = addressController.Insert(address, connectionType);
        address.Id = addressId;

        // Objeto genérico temporário para criar a pessoa
        // depois eu escolho se uso ele no Cliente ou no Funcionario
        var auxPerson = new
        {
            Address = address,
            DateOfBirth = new DateTime(1993, 10, 06),
            Document = "999.444.300-05",
            Email = "guilherme@gmail.com",
            Name = "Guilherme FS",
            Phone = "16982331450",
        };

        // Cliente
        Customer customer = new Customer
        {
            Address = auxPerson.Address,
            DateOfBirth = auxPerson.DateOfBirth,
            Document = auxPerson.Document,
            Email = auxPerson.Email,
            Name = auxPerson.Name,
            Phone = auxPerson.Phone,
            Income = 3500.20m,
            PDFDocument = "arquivo.pdf"
        };
        customer.Address.Id = addressId;

        CustomerController customerController = new CustomerController();
        customerController.Insert(customer, connectionType);

        // Funcionario
        Employee employee = new Employee
        {
            Comission = 10,
            ComissionValue = 100,
            Address = auxPerson.Address,
            Document = auxPerson.Document,
            Email = auxPerson.Email,
            Name = auxPerson.Name,
            Phone = auxPerson.Phone,
            DateOfBirth = auxPerson.DateOfBirth
        };
        employee.Address.Id = addressId;
        employee.Role = role;
        EmployeeController employeeController = new EmployeeController();
        employeeController.Insert(employee, connectionType);



        // Carro
        Car car = new Car
        {
            Color = "Vermelho",
            FabricationYear = 2020,
            LicensePlate = "AKF1X46",
            ModelYear = 2019,
            Name = "Carro Novo",
            Sold = false
        };

        //Compra
        Acquisition acquisition = new Acquisition
        {
            AcquisitionDate = new DateTime(2024, 06, 06),
            Car = car,
            Price = 8500.30m
        };
        AcquisitionController acquisitionController = new AcquisitionController();
        acquisitionController.Insert(acquisition, connectionType);

        // Venda
        Sale sale = new Sale
        {
            Customer = customer,
            Employee = employee,
            Payment = payment,
            SaleDate = new DateTime(2024, 06, 05),
            SaleValue = 10000,
            Car = car
        };
        sale.Payment.Id = paymentId;
        SaleController saleController = new SaleController();
        saleController.Insert(sale, connectionType);

    }

    static void Geracao()
    {
        int opt = -1;
        Console.WriteLine("Geração Massa de Dados");

        do
        {
            Console.WriteLine("Digite 0 para uma lista de carros nacionais e 1 para carros especiais.");
            opt = int.Parse(Console.ReadLine());
        } while (opt != 0 && opt != 1);
        CarCreator.GenerateCarJSONFile(opt);
    }

    static void Ingestao()
    {
        CarController carController = new CarController();
        Console.WriteLine("Ingestão de Dados");

        if (carController.SaveCarDataFromAPI(ConfigurationManager.ConnectionStrings["JSONFileOutput"].ConnectionString, connectionType))
        {
            Console.WriteLine("Inserção no SQL realizada com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro na inserção!");
        }
    }

    static void Associacao()
    {
        int menu = -1;
        OperationController serviceController = new();
        CarController carController = new();
        CarOperationController carOperationController = new();
        Console.WriteLine("Associação de Dados");

        do
        {
            Console.WriteLine("1 - Criar Serviço");
            Console.WriteLine("2 - Associar Serviço a um Carro");
            Console.WriteLine("3 - Alterar situação de um Carro x Serviço");
            Console.WriteLine("0 - Sair");
            menu = int.Parse(Console.ReadLine());
            switch (menu)
            {
                case 1:
                    CreateService();
                    break;
                case 2:
                    AssociateService();
                    break;
                case 3:
                    RetrieveCarService();
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
            Console.ReadKey();
        } while (menu != 0);


        void CreateService()
        {
            Console.WriteLine("Informe a descrição do Serviço:");
            string dc = Console.ReadLine();
            Operation service = new Operation { Description = dc };
            var result = serviceController.Insert(service, connectionType);
            if (result == 0)
            {
                Console.WriteLine("Erro ao inserir serviço.");
            }
            else
            {
                service.Id = result;
                Console.WriteLine($"Serviço criado: {result} - {dc}");
            }
        }

        Car RetrieveCar()
        {
            CarList carList = new();
            Console.WriteLine("Lista de Carros cadastrados:");
            carList = carController.Retrieve(connectionType);
            int index = 1;
            int opt = 0;

            Console.WriteLine(
            $"[Id]".PadRight(10) +
            $"[Placa]".PadRight(10) +
            $"[Nome]".PadRight(20) +
            $"[Ano Modelo]".PadRight(12) +
            $"[Ano Fabricação]".PadRight(13) +
            $"[Cor]".PadRight(20));
            foreach (var item in carList.Car)
            {
                Console.WriteLine(
                 $"{index}".PadRight(10) +
                 $"({item.LicensePlate})".PadRight(10) +
                 $"{item.Name}".PadRight(20) +
                 $"{item.ModelYear}".PadRight(12) +
                 $"{item.FabricationYear}".PadRight(16) +
                 $"{item.Color}".PadRight(20));
                index++;
            }

            do
            {
                Console.WriteLine("Escolha o Id do carro desejado:");
                opt = int.Parse(Console.ReadLine());
            } while (opt <= 0 || opt > carList.Car.Count());
            Car cr = carList.Car[opt - 1];
            return cr;
        }

        Operation RetrieveService()
        {
            int opt = 0;
            int index = 1;
            OperationList serviceList = new OperationList();

            Console.WriteLine("Lista de Serviços disponíveis:");
            serviceList = serviceController.Retrieve();

            Console.WriteLine(
            $"[Id]".PadRight(10) +
            $"[Serviço]".PadRight(10));
            foreach (var item in serviceList.Operation)
            {
                Console.WriteLine(
                 $"{index}".PadRight(10) +
                 $"{item.Description}".PadRight(10));
                index++;
            }
            do
            {
                Console.WriteLine("Informe o Id do serviço desejado:");
                opt = int.Parse(Console.ReadLine());
            } while (opt <= 0 || opt > serviceList.Operation.Count());
            Operation sv = serviceList.Operation[opt - 1];
            return sv;
        }

        void AssociateService()
        {
            Car cr = RetrieveCar();
            Operation sv = RetrieveService();
            CarOperation cs = new CarOperation { Car = cr, Operation = sv, Status = true };

            Console.WriteLine(cr + " " + sv);
            carOperationController.Insert(cs, connectionType);
        }


        CarOperation RetrieveCarService()
        {
            int opt = 0;
            int index = 1;
            CarOperationList csList = new();
            csList = carOperationController.Retrieve(connectionType);

            Console.WriteLine("Lista de Carros x Serviços atuais:");
            Console.WriteLine(
            $"[Id]".PadRight(10) +
            $"[Placa]".PadRight(10) +
            $"[Serviço]".PadRight(10) +
            $"[Situacao]".PadRight(10));
            foreach (var item in csList.CarOperation)
            {
                Console.WriteLine(
                 $"{index}".PadRight(10) +
                 $"{item.Car.LicensePlate}".PadRight(10) +
                 $"{item.Operation.Description}".PadRight(10) +
                 $"{item.Status}".PadRight(10));
                index++;
            }
            do
            {
                Console.WriteLine("Informe o Id que deseja alterar a situação:");
                opt = int.Parse(Console.ReadLine());
            } while (opt <= 0 || opt > csList.CarOperation.Count());
            CarOperation csv = csList.CarOperation[opt - 1];
            csv.Status = !csv.Status;
            carOperationController.ChangeStatusCarServiceTable(csv, connectionType);
            return csv;
        }
    }

    static void Extracao()
    {
        Console.WriteLine("Extração de Dados");

        CarController carcontroller = new();
        CarOperationController carServiceTableController = new();
        var carList = carcontroller.Retrieve(connectionType);
        var carStatus = carServiceTableController.RetrieveCarServiceTableStatus(true, connectionType);

        // Carros por Status
        using (var writer = new StreamWriter(ConfigurationManager.ConnectionStrings["XMLFileOutputStatus"].ConnectionString))
        {
            var xml = new XElement("Root");
            foreach (var item in carStatus.CarOperation)
            {
                xml.Add(item.Car.GetXMLDocument());
            }
            writer.WriteLine(xml);
            writer.Close();
        }
        // Carros por Cor
        using (var writer = new StreamWriter(ConfigurationManager.ConnectionStrings["XMLFileOutputColor"].ConnectionString))
        {
            var xml = new XElement("Root");
            foreach (var item in carList.Car.FindAll(s => s.Color == "Vermelho"))
            {
                xml.Add(item.GetXMLDocument());
            }
            writer.WriteLine(xml);
            writer.Close();
        }
        // Carros por Ano
        using (var writer = new StreamWriter(ConfigurationManager.ConnectionStrings["XMLFileOutputYear"].ConnectionString))
        {
            var xml = new XElement("Root");
            foreach (var item in carList.Car.FindAll(s => s.FabricationYear >= 2010 && s.FabricationYear <= 2011))
            {
                xml.Add(item.GetXMLDocument());
            }
            writer.WriteLine(xml);
            writer.Close();
        }
        */
    }
}