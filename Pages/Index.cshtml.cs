using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace ITServiceApprovalProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly employeeContext _context;

        private readonly ILogger<IndexModel> _logger;

 

        [BindProperty]
        public long EmployeeId {  get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Department { get; set; }

        [BindProperty]
        public string RequestType { get; set; }
        [BindProperty]
        public string? RequestDuration { get; set; }
        [BindProperty]
        public string? Remarks { get; set; }









        public IndexModel(ILogger<IndexModel> logger, employeeContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            if (TempData.ContainsKey("EmployeeId"))
            {
                long employeeId = long.Parse(TempData["EmployeeId"].ToString());

                // Query the database to find the employee by ID
                var employee = _context.employee.FirstOrDefault(e => e.employeeId == employeeId);

                if (employee != null)
                {
                    // If the employee is found, assign additional details
                    EmployeeId = employeeId;
                    Name = employee.name;
                    Department = employee.department;
                    Email = employee.email;
                }
                else
                {
                    // Handle case where employee with the provided ID is not found
                    // You may want to set default values or handle the error differently
                }


            }





        }


        public void OnPost()
        {
            

            // Create a new request object with the form data
            // Create a new request object with the form data
            var newRequest = new request
            {
                employeeId = EmployeeId,
                accessType = RequestType,
                accessDuration = string.IsNullOrWhiteSpace(RequestDuration) ? "" : RequestDuration,
                remarks = string.IsNullOrWhiteSpace(Remarks) ? "" : Remarks,
                requestDate = DateTime.Now.Date,  // Current date
                requestTime = DateTime.Now.TimeOfDay,  // Current time
                status = "Pending"  // Default status
            };


            // Add the new request to the database
            _context.request.Add(newRequest);
            _context.SaveChanges();

            



        }
    }
}
   