using System.Data;

namespace UMan.DataAccess.Repositories.Exceptions
{
    public class EntityNotFoundException<T> : DataException
    {
        public EntityNotFoundException()
        {

        }

        public EntityNotFoundException(T entitySearchValue)
        {
            Data.Add("keyValue", entitySearchValue);
        }

        public override string Message => $"Entity with {Data["keyValue"]} not found";
    }
}
