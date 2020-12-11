using System;
using System.Collections.Generic;

namespace ErtugrulGaziSari_BEChallenge
{
    public class Order
    {
        public List<string> id = new List<string>();
        public List<string> customerId = new List<string>();
        public List<double> price = new List<double>();
        public List<int> quantity = new List<int>();
        public List<string> status = new List<string>();
        public List<DateTime> createdAt = new List<DateTime>();
        public List<DateTime> updatedAt = new List<DateTime>();
        
        public void Create(string oId, string oCustomerId, int oQuantity, double oPrice, string oStatus, DateTime oCreatedAt, DateTime oUpdatedAt)
        {
            id.Add(oId);
            customerId.Add(oCustomerId);
            quantity.Add(oQuantity);
            createdAt.Add(oCreatedAt);
            updatedAt.Add(oUpdatedAt);
            status.Add(oStatus);
            price.Add(oPrice);
        }

        public void Get(int locator)
        {
            Console.WriteLine("\nOrder ID       : " + id[locator] +
                              "\nCustomer ID    : " + customerId[locator] +
                              "\nOrder Quantity : " + quantity[locator] +
                              "\nUnit Price     : " + price[locator] +
                              "\nOrder Price    : " + (price[locator] * quantity[locator]) +
                              "\nOrder Status   : " + status[locator] +
                              "\nCreated at     : " + createdAt[locator] +
                              "\nUpdated at     : " + updatedAt[locator]);
        }
        

        public void Update(int locator, double oPrice, int oQuantity)
        {
            price[locator] = oPrice;
            quantity[locator] = oQuantity;
            status[locator] = "Updated";
            updatedAt[locator] = DateTime.Now;
            Console.WriteLine("Order {0} updated", id[locator]);
        }

        public void Delete(int locator)
        {
            id.RemoveAt(locator);
            customerId.RemoveAt(locator);
            price.RemoveAt(locator);
            quantity.RemoveAt(locator);
            status.RemoveAt(locator);
            createdAt.RemoveAt(locator);
            updatedAt.RemoveAt(locator);
        }
    }
}