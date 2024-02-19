using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace ITServiceApprovalProject.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly employeeContext _context;

        public RegisterModel(employeeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public employee employee { get; set; }

        [BindProperty]
        public string password { get; set; }


        public void OnGet()
        {
        }

        

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _context.employee.Add(employee);
                _context.SaveChanges();

                var credentials = new credentials
                {
                    employeeId = employee.employeeId,
                    password = password
                };
                _context.credentials.Add(credentials);
                _context.SaveChanges();
            }

            catch
            {
                return Redirect("Error");
            }

            return Redirect("login");

            
        }




    }

    public class employeeModel
    {
        
    }
}
