using HelloLibrary;
using System;
using System.Windows.Forms;

namespace HelloWindowsFormsApp
{
    public partial class HelloApp : Form
    {
        public HelloApp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Greetings.GetCurrentTimeGreetings(txtName.Text));
        }
    }
}
