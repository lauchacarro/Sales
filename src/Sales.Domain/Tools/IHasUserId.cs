namespace Sales.Domain.Tools
{
    public interface IHasUserId<TKey>
    {
        public TKey UserId { get; set; }
    }
}
