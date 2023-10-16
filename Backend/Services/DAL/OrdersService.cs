using Backend.EF.Context;

namespace Backend.Services.DAL
{
    public class OrdersService
    {
        private DataContext Context { get; }

        public OrdersService(DataContext context)
        { Context = context; }
    }
}