namespace RestApiNetDemo.BEL.Employee.Admin
{
    public class CreateAdminDTO : EmployeeDTO
    {
        public CreateAdminDTO() : base()
        {
        }

        public string AdminCardNumber { get; set; }
    }
}