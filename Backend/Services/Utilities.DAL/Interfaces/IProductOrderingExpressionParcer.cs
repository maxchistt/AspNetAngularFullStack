using Backend.Models.Goods;
using System.Linq.Expressions;

namespace Backend.Services.Utilities.DAL.Interfaces
{
    public interface IProductOrderingExpressionParcer
    {
        Expression<Func<Product, object>>? GetExpression(string OrderingProperty);
    }
}