using System.ComponentModel.DataAnnotations.Schema;

namespace ITServiceApprovalProject
{
    public class credentials
    {
        public int Id {  get; set; }

        public long employeeId {  get; set; }

        public string password {  get; set; }

        [ForeignKey("employeeId")]
        public employee employee { get; set; }
    }
}
