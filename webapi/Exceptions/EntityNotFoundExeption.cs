namespace webapi.Exceptions
{
    public class EntityNotFoundExeption:Exception
    {
        public EntityNotFoundExeption(int id, string entityName) : base($" {entityName} with id {id} was not found") { }
    }
}
