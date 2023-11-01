namespace WebApplication7.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WorkflowInstance> Requests { get; set; }
        public bool IsActive { get; set; }
    }

}
