using System;
using System.Collections.Generic;

namespace ErtugrulGaziSari_BEChallenge
{
    public class Customer
    {
        public List<string> id = new List<string>();
        public List<string> name = new List<string>();
        public List<string> email = new List<string>();
        public List<string> orderId = new List<string>();
        public List<DateTime> createdAt = new List<DateTime>();
        public List<DateTime> updatedAt = new List<DateTime>();

        public void Create(string cId, string cName, string cEmail, string cOrderId, DateTime cCreatedAt, DateTime cUpdatedAt)
        {
            id.Add(cId);
            email.Add(cEmail);
            name.Add(cName);
            createdAt.Add(cCreatedAt);
            updatedAt.Add(cUpdatedAt);
            orderId.Add(cOrderId);
        }
        
        public void Get(int locator)
        {
            Console.WriteLine("\nCustomer Id    : " + id[locator] +
                              "\nCustomer Name  : " + name[locator] +
                              "\nCustomer Email : " + email[locator] +
                              "\nCreated at     : " + createdAt[locator] +
                              "\nUpdated at     : " + updatedAt[locator]);
        }

        public void ShowCustomers()
        {
            Console.WriteLine("\nCustomer ID ---- Customer Name ---- Order ID");
            
            for (int i = 0; i < id.Count; i++)
            {
                Console.WriteLine((i+1) + ")   " + id[i] + " ---- " + name[i] + " ---- " + orderId[i]);
            }
        }
        
        public void Update(int locator, string cName, string cEmail)
        {
            name[locator] = cName;
            email[locator] = cEmail;
            updatedAt[locator] = DateTime.Now;
            Console.WriteLine("Customer {0} updated", name[locator]);
        }
        
        public void Delete(int locator)
        {
            id.RemoveAt(locator);
            name.RemoveAt(locator);
            email.RemoveAt(locator);
            orderId.RemoveAt(locator);
            createdAt.RemoveAt(locator);
            updatedAt.RemoveAt(locator);
        }
    }
}
