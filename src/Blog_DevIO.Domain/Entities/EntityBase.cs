namespace Blog_DevIO.Domain.Entities
{
    public class EntityBase
    {
        public EntityBase(DateTime creation)
        {
            Creation = creation;
        }

        public Guid Id { get; private set; }
        public DateTime Creation { get; private set; }
    }
}
