namespace Domain.Interfaces
{
    public interface IIdentifiable<T>
    {
        public T Id { get; set; }
    }
}
