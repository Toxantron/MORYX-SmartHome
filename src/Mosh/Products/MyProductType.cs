using Moryx.AbstractionLayer.Products;

namespace Mosh.Products
{
    public class MyProductType : ProductType
    {
        protected override ProductInstance Instantiate()
        {
            return new MyProductInstance();
        }
    }
}
