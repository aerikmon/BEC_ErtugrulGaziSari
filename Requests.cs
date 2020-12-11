using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace ErtugrulGaziSari_BEChallenge
{
    public class Requests
    {
        Order Orders = new Order();
        Customer Customers = new Customer();

        public void ReadData()
        {
            
            var pathLocator = ".";
            var path = @Path.GetFullPath(pathLocator)+"\\database.csv";
            
            Console.WriteLine("\nReading from database\nPath of the data file: " + Path.GetFullPath(path) + "\n");

            TextFieldParser csvReader = new TextFieldParser(path);
            
            csvReader.SetDelimiters(new string[] {","});
            csvReader.ReadLine();

            while (!csvReader.EndOfData)
            {
                string[] fields = csvReader.ReadFields();
                
                var cId = fields[0];
                var oId = fields[1];
                var cName = fields[2];
                var cEmail = fields[3];
                var oPrice = fields[4];
                var oQuantity = fields[5];
                var oStatus = fields[6];
                var oCreatedAt = DateTime.Parse(fields[7]);
                var oUpdatedAt = DateTime.Parse(fields[8]);

                Customers.Create(cId, cName, cEmail, oId, oCreatedAt, oUpdatedAt);
                Orders.Create(oId, cId, int.Parse(oQuantity), Double.Parse(oPrice), oStatus, oCreatedAt, oUpdatedAt);
            }
        }

        public void WriteData()
        {
            var pathLocator = ".";
            var path = @Path.GetFullPath(pathLocator)+"\\database.csv";
            Console.WriteLine("\nSaving operations to database\nPath of the data file: " + Path.GetFullPath(path) + "\n");
            
            var csvWriter = new StreamWriter(path);

            csvWriter.WriteLine("customerID,orderID,customerName,customerEmail,orderPrice,orderQuantity,orderStatus,createdAt,updatedAt");
            csvWriter.Flush();
            
            for (int i = 0; i < Customers.id.Count; i++)
            {
                var cId = Customers.id[i];
                var oId = Orders.id[i];
                var cName = Customers.name[i];
                var cEmail = Customers.email[i];
                var oPrice = Orders.price[i];
                var oQuantity = Orders.quantity[i];
                var oCreatedAt = Orders.createdAt[i];
                var oUpdatedAt = Orders.updatedAt[i];
                var oStatus = Orders.status[i];

                var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", cId, oId, cName, cEmail, oPrice, oQuantity,
                    oStatus, oCreatedAt, oUpdatedAt);

                csvWriter.WriteLine(line);
                csvWriter.Flush();
            }
        }
        
        public void TakeRequest()
        {
            Console.WriteLine("\nol   : Show order list" + 
                              "\nci   : Show customer info" +
                              "\noi   : Show order info" +
                              "\ncr   : Create order" + 
                              "\nup   : Update order" +
                              "\ndl   : Delete order" + 
                              "\ncp   : Close program");
            
            var request = (Console.ReadLine());
            
            switch (request)
            {
                case "ol":
                    ShowCustomerList();
                    TakeRequest();
                    break;
                case "cr":
                    CreateOrder();
                    TakeRequest();
                    break;
                case "up":
                    UpdateOrder();
                    TakeRequest();
                    break;
                case "dl":
                    DeleteOrder();
                    TakeRequest();
                    break;
                case "ci":
                    ShowCustomerInfo();
                    TakeRequest();
                    break;
                case "oi":
                    ShowOrderInfo();
                    TakeRequest();
                    break;
                case "cp":
                    break;
                default:
                    Console.WriteLine("\nPlease enter a valid command: ");
                    TakeRequest();
                    break;
            }
        }
        
        public void CreateOrder()
        {
            var rnd = new Random();
            
            string cId;
            string cEmail;
            string cName;
            string oId;
            string oStatus = "Processed";
            int oQuantity;
            double oPrice;
            DateTime createdAt = DateTime.Now;
            DateTime updatedAt = createdAt;
            
            cId = DateTime.Now.ToString("MMddHHmmss");
            oId = (DateTime.Now.ToString("HHmm"))+(rnd.Next(10000).ToString());
            
            Console.WriteLine("\nCustomer name: ");
            cName = Console.ReadLine();
                
            Console.WriteLine("\nCustomer email: ");
            cEmail = Console.ReadLine();
            
            Console.WriteLine("\nOrder Quantity: ");
            oQuantity = int.Parse(Console.ReadLine());
            
            Console.WriteLine("\nUnit Price: ");
            oPrice = Double.Parse(Console.ReadLine());
            
            Customers.Create(cId,cName,cEmail, oId, createdAt, updatedAt);
            Orders.Create(oId,cId, oQuantity, oPrice, oStatus, createdAt, updatedAt);
        }

        public void ShowCustomerList()
        {
            Customers.ShowCustomers();
        }
        
        public void ShowCustomerInfo()
        {
            ShowCustomerList();
            Console.WriteLine("\nChoose customer:");
            var idRequest = Int32.Parse(Console.ReadLine());
            idRequest = idRequest - 1;
            Customers.Get(idRequest);
        }

        public void ShowOrderInfo()
        {
            ShowCustomerList();
            Console.WriteLine("\nChoose order:");
            var idRequest = Int32.Parse(Console.ReadLine());
            idRequest = idRequest - 1;
            
            Orders.Get(idRequest);
        }
        
        public void UpdateOrder()
        {
            ShowCustomerList();
            Console.WriteLine("\nChoose order to update or enter 'cn' to cancel:");
            var idRequest = Console.ReadLine();
            
            if (idRequest != "cn")
            {
                int idUpdate = Int32.Parse(idRequest) - 1;
                string updateRequest;
                string uName = Customers.name[idUpdate];
                string uEmail = Customers.email[idUpdate];
                double uPrice = Orders.price[idUpdate];
                int uQuantity = Orders.quantity[idUpdate];
                var endRequest = true;
                
                Console.WriteLine("\nWhat do you want to update?");
                while (endRequest)
                {
                    Console.WriteLine("\nn : Customer name" + 
                                      "\ne : Customer email" + 
                                      "\nq : Order quantity" + 
                                      "\np : Order unit price" + 
                                      "\na : Update all");
                    updateRequest = Console.ReadLine();
                    switch (updateRequest)
                    {
                        case "n":
                            Console.Write("\nEnter updated name: ");
                            uName = Console.ReadLine();
                            break;
                        case "e":
                            Console.Write("\nEnter updated email: ");
                            uEmail = Console.ReadLine();
                            break;
                        case "q":
                            Console.Write("\nEnter updated quantity: ");
                            try
                            {
                                uQuantity = Int32.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\nInvalid quantity! Please enter a valid quantity");
                                uQuantity = Int32.Parse(Console.ReadLine());
                            }
                            break;
                        case "p":
                            Console.Write("\nEnter updated unit price: ");
                            try
                            {
                                uPrice = double.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\nInvalid unit price! Please enter a valid Price");
                                uPrice = double.Parse(Console.ReadLine());
                            }
                            break;
                        case "a":
                            Console.Write("\nEnter updated name: ");
                            uName = Console.ReadLine();
                            
                            Console.Write("\nEnter updated email: ");
                            uEmail = Console.ReadLine();
                            
                            Console.Write("\nEnter updated quantity: ");
                            try
                            {
                                uQuantity = Int32.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\nInvalid quantity! Please enter a valid quantity");
                                uQuantity = Int32.Parse(Console.ReadLine());
                            }
                            
                            Console.Write("\nEnter updated unit price: ");
                            try
                            {
                                uPrice = double.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\nInvalid unit price! Please enter a valid Price");
                                uPrice = double.Parse(Console.ReadLine());
                            }
                            break;
                        default:
                            Console.WriteLine("\nPlease enter a valid command: ");
                            break;
                    }
                    Console.WriteLine("\nAnything else: (y or n)");
                    if (Console.ReadLine() == "n")
                    {
                        endRequest = false;
                    }
                }
                Customers.Update(idUpdate, uName, uEmail);
                Orders.Update(idUpdate, uPrice, uQuantity);
            }
        }

        public void DeleteOrder()
        {
            ShowCustomerList();
            Console.WriteLine("\nChoose order to delete or enter 'cn' to cancel:");
            var idRequest = Console.ReadLine();
            if (idRequest != "cn")
            {
                var idUpdate = Int32.Parse(idRequest) - 1;
                Customers.Delete(idUpdate);
                Orders.Delete(idUpdate);
            }
        }
    }
}