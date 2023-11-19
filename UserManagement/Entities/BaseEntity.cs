namespace UserManagement.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }

        // We can add here propeties for IsDeleted, DeltedOn, CreateOn, CreatedBy but we don't need for this simple task
    }
}
