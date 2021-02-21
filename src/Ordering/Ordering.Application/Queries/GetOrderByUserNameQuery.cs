namespace Ordering.Application.Queries
{
    public class GetOrderByUserNameQuery
    {
        public string UserName { get; set; }

        public GetOrderByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
