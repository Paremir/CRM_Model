using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBL.Model.Tests
{
    [TestClass()]
    public class CartTests
    {
        [TestMethod()]
        public void CartTest()
        {
            //arrange
            var customer = new Customer()
            {
                CustomerId = 1,
                Name = "testuser"
            };
            var product1 = new Product()
            {
                ProductId = 1,
                Name = "pr1",
                Price = 100,
                Count = 10
            };
            var product2 = new Product()
            {
                ProductId = 2,
                Name = "pr3",
                Price = 200,
                Count = 20
            };

            var cart = new Cart(customer);
            cart.Add(product1);
            cart.Add(product1);
            cart.Add(product2);

            var expectResult = new List<Product>()
            {
                product1,product1,product2
            };
            //assert

            var cartResult = cart.GetAll(); 
            Assert.AreEqual(expectResult.Count, cart.GetAll().Count);
            for (int i = 0; i < expectResult.Count(); i++)
            {
                Assert.AreEqual(expectResult[i],cartResult[i]);
            }
        }

    }
}