using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueuingProgram
{
    public partial class CashierWindowsQueueForm : Form
    {
        public CashierWindowsQueueForm()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = (1 * 100);
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
        }
        Boolean openForm = false;
        ClientForm customerView = new ClientForm();
        FormCollection allForms = Application.OpenForms;
        Form OpenedForm = new Form();

        private void timer1_Tick(object sender, EventArgs e)
        {
            DisplayCashierQueue(CashierClass.CashierQueue);
        }

        private void CashierWindowsQueueForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayCashierQueue(CashierClass.CashierQueue);
        }

        public void DisplayCashierQueue(IEnumerable CashierList)
        {
            listCashierQueue.Items.Clear();
            foreach (object obj in CashierList)
            {
                listCashierQueue.Items.Add(obj.ToString());
            }
        }

        public delegate void PassControl(object sender);
        public PassControl passControl;

        public void OpenCashier()
        {
            if (openForm == false)
            {
                CashierWindowsQueueForm cashierWindow = new CashierWindowsQueueForm();
                cashierWindow.Visible = true;
                openForm = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NextServing();
        }

        public void NextServing()
        {
            foreach (Form form in allForms)
            {
                if (form.Name == "CustomerView")
                {
                    OpenedForm = form;
                    openForm = true;
                }
            }
            if (openForm == true)
            {
                if (passControl != null)
                {
                    customerView.lblNowServing.Text = CashierClass.CashierQueue.Peek();
                    CashierClass.CashierQueue.Dequeue();
                    passControl(customerView.lblNowServing);
                }
            }
            else
            {
                customerView.ShowDialog();
                customerView.lblNowServing.Text = CashierClass.CashierQueue.Peek().ToString();
                CashierClass.CashierQueue.Dequeue();
            }
        }
    }
}
