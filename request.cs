namespace ITServiceApprovalProject
{
    public class request
    {
        public int requestId { get; set; }
        public long employeeId { get; set; }
        public string accessType { get; set; }
        public string accessDuration { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public DateTime requestDate { get; set; }
        public TimeSpan requestTime { get; set; }
    }
}
