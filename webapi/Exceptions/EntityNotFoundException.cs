namespace webapi.Exceptions
{
    public class EntityNotFoundException:Exception
    {
        public EntityNotFoundException(int id, string entityName) : base($" {entityName} with id {id} was not found") { }
    }
}
