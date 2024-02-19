using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ITServiceApprovalProject.Pages;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ITServiceApprovalProject.Pages
{
    public class hodPageModel : PageModel
    {
        public List<dataModel> list = new List<dataModel>();
        public void OnGet()
        {
            Console.WriteLine("Onget");
            string connectionString = "Data Source=localhost;Initial Catalog=ITServiceDB;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM employee";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        dataModel model = new dataModel();

                        model.employeeId = reader.GetInt64("employeeId");
                        model.name = reader.GetString("name");
                        model.department = reader.GetString("department");
                        model.email = reader.GetString("email");

                        list.Add(model);
                    }
                }
            }
        }
    }
}
