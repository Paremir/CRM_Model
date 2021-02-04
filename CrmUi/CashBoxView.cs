using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public class CashBoxView
    {
        CashDesk cashDesk;
        public Label CashDesk { get; set;   }
        public NumericUpDown Price { get; set; }
        public ProgressBar QueueLenght { get; set; }
        public Label LeaveCustomersCount { get; set; }
        public  CashBoxView(CashDesk cashDesk,int number,int x,int y)
        {
            this.cashDesk = cashDesk;
            CashDesk = new Label();
            Price = new NumericUpDown();
            QueueLenght = new ProgressBar();
            LeaveCustomersCount = new Label();

            CashDesk.AutoSize = true;
            CashDesk.Location = new System.Drawing.Point(x, y);
            CashDesk.Name =  "label" + number;
            CashDesk.Size = new System.Drawing.Size(35, 13);
            CashDesk.TabIndex = number;
            CashDesk.Text = cashDesk.ToString();

            Price.Location = new System.Drawing.Point(x+80, y);
            Price.Name = "NumericUpDown" + number;
            Price.Size = new System.Drawing.Size(120, 120);
            Price.TabIndex = number;
            Price.Maximum = 1000000000000000000;

            QueueLenght.Location = new System.Drawing.Point(x+300, y);
            QueueLenght.Maximum = cashDesk.MaxQueueLenght;
            QueueLenght.Name = "progressBar" + number;
            QueueLenght.Size = new System.Drawing.Size(100, 23);
            QueueLenght.TabIndex = number;
            QueueLenght.Value = 0;

            LeaveCustomersCount.AutoSize = true;
            LeaveCustomersCount.Location = new System.Drawing.Point(x + 400, y);
            LeaveCustomersCount.Name = "label2" + number;
            LeaveCustomersCount.Size = new System.Drawing.Size(35, 13);
            LeaveCustomersCount.TabIndex = number;
            LeaveCustomersCount.Text = "" ;

            cashDesk.CheckClosed += CashDesk_CheckClosed;
        }

        private void CashDesk_CheckClosed(object sender, Check e)
        {

            Price.Invoke((Action)delegate 
            { 

                Price.Value += e.Price;
                QueueLenght.Value = cashDesk.Count;
                LeaveCustomersCount.Text = cashDesk.ExitCustomer.ToString();
            });
        }
    }
}
