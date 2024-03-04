using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApprovalProject.Pages
{
    public class loginModel : PageModel
    {
        private readonly employeeContext _context;

        public loginModel(employeeContext context)
        {
            _context = context;
        }
        [BindProperty]
        public long employeeId { get; set; }

        [BindProperty]
        public string password { get; set; }


        public bool IsInvalidCredentials { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            
            var user = await _context.credentials.FirstOrDefaultAsync(u => u.employeeId == employeeId && u.password == password);

            if (user != null)
            {
                TempData["EmployeeId"] = employeeId.ToString();

                return Redirect("Index");
            }
            else
            {
                IsInvalidCredentials = true; 
                return Page();
            }
        }
    }
}

