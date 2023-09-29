using Microsoft.Data.SqlClient;
using System.Data;

namespace SQLInjectionTest
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=DESKTOP-OL3HP30\\SQLEXPRESS;Initial Catalog=SQLInjectionTest;Integrated Security=True;TrustServerCertificate=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connectionObject = new(connectionString))
            {
                connectionObject.Open();
                InsecureSQLMethod(connectionObject);
                // SecureSQLMethod(connectionObject);
            }
                
        }

        private void InsecureSQLMethod(SqlConnection connectionObject)
        {
            string queryCheckLogin = $"SELECT COUNT(*) FROM loginTB WHERE username = '{usernameTB.Text}' AND password = '{passwordTB.Text}';";
            if ((int)new SqlCommand(queryCheckLogin, connectionObject).ExecuteScalar() == 1)
            {
                MessageBox.Show("Login Successful");
            }
            else
            {
                MessageBox.Show("Wrong credentials");
            }
        }
        private void SecureSQLMethod(SqlConnection connectionObject)
        {
            string queryCheckLogin = $@"SELECT COUNT(*) 
                                        FROM loginTB 
                                        WHERE username = @usernameText 
                                        AND password = @passwordText;";

            SqlCommand command = new SqlCommand(queryCheckLogin, connectionObject);

            command.Parameters.Add(new SqlParameter("@usernameText", usernameTB.Text));
            command.Parameters.Add(new SqlParameter("@passwordText", passwordTB.Text));


            if ((int)command.ExecuteScalar() == 1)
            {
                MessageBox.Show("Login Successful");
            }
            else
            {
                MessageBox.Show("Wrong credentials");
            }
        }
    }
}