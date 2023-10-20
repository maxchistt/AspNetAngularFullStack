using Backend.Models.Goods;
using System.Linq.Expressions;

namespace Backend.Services.DAL.Utilities.Interfaces
{
    public interface IProductOrderingExpressionParcer
    {
        Expression<Func<Product, object>>? GetExpression(string OrderingProperty);
    }
}