namespace WebApplication7.Entities
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public WfType WorkflowType { get; set; }
        public WfStep CurrentWorkflowStep { get; set; }
        public Guid RequestId { get; set; }
        public WfAuthor AssignTo { get; set; }
        public bool IsClosed { get; set; }
        public WfDesicion Desicion { get; set; }
        public WfDesicion AllowedDesicions { get; set; }
        public string? Comment { get; set; }

        public void TakeDesicion(WfDesicion desicion, string comment = "")
        {
            ValidateAllowedDesicion(desicion);

            ValidateAuthority(0);

            Desicion = desicion;
            Comment = comment;

            Close();
        }

        private void Close()
        {
            IsClosed = true;
        }

        public void ValidateAllowedDesicion(WfDesicion desicion)
        {
            if (!AllowedDesicions.HasFlag(desicion))
                throw new UnauthorizedAccessException("this desicion is not allowed!!");
        }

        public void ValidateAuthority(WfAuthor authors)
        {
            if (IsClosed)
                throw new UnauthorizedAccessException("you can't used a closed task!!");

            //if (!authors.HasFlag(AssignTo))
            //    throw new UnauthorizedAccessException("this user is not in the need role!!");
        }
    }
}
