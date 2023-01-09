namespace RestApiNetCoreDemo.DAL.DTOs
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
