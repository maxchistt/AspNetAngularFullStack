using Backend.Models.Goods;
using Backend.Services.DAL.Interfaces;
using System.Linq.Expressions;

namespace Backend.Services.DAL
{
    public class ProductOrderingExpressionParcer : IProductOrderingExpressionParcer
    {
        public Expression<Func<Product, object>>? GetExpression(string OrderingProperty)
        {
            GoodsOrderingTypes type = GoodsOrderingTypes.None;
            bool parced = Enum.TryParse(OrderingProperty, true, out type);
            if (!parced || type == GoodsOrderingTypes.None) return null;

            return type switch
            {
                GoodsOrderingTypes.Id => p => p.Id,
                GoodsOrderingTypes.Name => p => p.Name,
                GoodsOrderingTypes.Price => p => p.Price,
                _ => null,
            };
        }

        public enum GoodsOrderingTypes
        {
            None = 0,
            Id,
            Name,
            Price
        }
    }
}