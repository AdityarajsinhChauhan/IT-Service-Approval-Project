using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace ITServiceApprovalProject.Pages
{
    public class IndexModel : PageModel
    {
        public List<dataModel> list = new List<dataModel>();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

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

                        model.employeeId = reader.GetInt32("employeeId");
                        model.name = reader.GetString("name");
                        model.department = reader.GetString("department");
                        model.email = reader.GetString("email");
                        model.accessType = reader.GetString("accessType");
                        model.accessDuration = reader.GetString("accessDuration");
                        model.remarks = reader.GetString("remarks");

                        list.Add(model);
                    }
                }
            }


        }
        public dataModel mo = new dataModel();

        public void OnPost()
        {
            mo.name = Request.Form["Name"];
            mo.department = Request.Form["Department"];
            mo.email = Request.Form["Email"];
            mo.accessType = Request.Form["AccessType"];
            mo.accessDuration = Request.Form["AccessDuration"];
            mo.remarks = Request.Form["Remarks"];

            try
            {
                string connectionstring = "Data Source=localhost;Initial Catalog=ITServiceDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "INSERT INTO employee" +
                        "(name, department, email, accessType, accessDuration, remarks)" +
                        "VALUES" +
                        "(@Name, @Department, @Email, @AccessType, @AccessDuration, @Remarks);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", mo.name);
                        command.Parameters.AddWithValue("@Department", mo.department);
                        command.Parameters.AddWithValue("@Email", mo.email);
                        command.Parameters.AddWithValue("@AccessType", mo.accessType);
                        command.Parameters.AddWithValue("@AccessDuration", mo.accessDuration);

                        if (mo.remarks != null)
                            command.Parameters.AddWithValue("@Remarks", mo.remarks);
                        else
                            command.Parameters.AddWithValue("@Remarks", DBNull.Value);

                        command.ExecuteNonQuery();
                    }
                    string requestSql = "INSERT INTO ServicePermissionRequest (employeeId, AccessType, AccessDuration)" +
                    "VALUES (@EmployeeId, @AccessType, @AccessDuration);";

                    using (SqlCommand requestCommand = new SqlCommand(requestSql, connection))
                    {
                        requestCommand.Parameters.AddWithValue("@EmployeeId", mo.employeeId);
                        requestCommand.Parameters.AddWithValue("@AccessType", mo.accessType);
                        requestCommand.Parameters.AddWithValue("@AccessDuration", mo.accessDuration);



                        requestCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., logging or displaying an error message.
            }

            // Clear form fields after submitting
            mo.employeeId = 0;
            mo.name = "";
            mo.department = "";
            mo.email = "";
            mo.accessType = "";
            mo.accessDuration = "";
            mo.remarks = "";

            // Redirect to the same page after inserting data
            Response.Redirect("Index");
        }
    }
    public class dataModel
    {
        public int employeeId { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string email { get; set; }
        public string accessType { get; set; }
        public string? accessDuration { get; set; }

        public string? remarks { get; set; }
    }
}
