using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBL.Model
{
   public class ShopComputerModel
    {
        Generator Generator = new Generator();
        Random rnd = new Random();

        bool IsWorking = false;
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sells { get; set; } = new List<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();

        public int CustomerSpeed { get; set; } = 100;
        public int CashDeskSpeed { get; set; } = 100;

        public ShopComputerModel()
        {
            var sellers = Generator.GetNewSeller(20);

            Generator.GetNewProduct(1000);
            Generator.GetNewCustomers(100);


            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }
            
            for (int i = 0; i < 3; i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue(), null));
            } 
        }

        public void Start()
        {
            IsWorking = true;
            Task.Run(()=>CreateCarts(10));

            var CashDeskTasks = CashDesks.Select(c => new Task(() => CashDeskWork(c)));
            foreach (var task in CashDeskTasks)
            {
                task.Start();
            }
        }

        public void Stop()
        {
            IsWorking = false;
        }

        private void CashDeskWork(CashDesk cashDesk)
        {
            while (IsWorking)
            {
                if (cashDesk.Count > 0)
                {
                    cashDesk.Dequeue();
                    Thread.Sleep(CashDeskSpeed);
                }
            }
           
        }

        private void CreateCarts(int customerCounts)
        {
            while (IsWorking)
            {
                var customers = Generator.GetNewCustomers(customerCounts);

                

                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);
                    foreach (var product in Generator.GetRandomProduct(10,30))
                    {
                        cart.Add(product);
                    }
                    var cash = CashDesks[rnd.Next(CashDesks.Count)];
                    cash.Enqueue(cart);
                }

                Thread.Sleep(customerCounts);
            }
        }
    }
}
