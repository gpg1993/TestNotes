using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    public abstract class BaseProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double OriginalPrice { get; set; }
        public abstract double ProductPrice();
    }

    public class LaTiaoProduct : BaseProduct
    {
        public LaTiaoProduct(string id, string name, double price)
        {
            this.Id = id;
            this.Name = name;
            this.OriginalPrice = price;
        }
        public override double ProductPrice()
        {
            return this.OriginalPrice;
        }
    }

    public class LaoGanMaProduct : BaseProduct
    {
        public LaoGanMaProduct(string id, string name, double price)
        {
            this.Id = id;
            this.Name = name;
            this.OriginalPrice = price;
        }
        public override double ProductPrice()
        {
            return this.OriginalPrice;
        }
    }
    
    public class ProductMuti : List<BaseProduct>
    {
        public virtual double ProductsPrice()
        {
            double Allprice = 0;
            base.ForEach(c => 
            {
                Allprice += c.ProductPrice();
            });
            return Allprice;
        }
    }
    internal abstract class BaseActivityMuti : ProductMuti
    {

    }

    class DisCountActivityMuti : BaseActivityMuti
    {
        ProductMuti products = null;
        public double Discount { get; set; }
        public DisCountActivityMuti(double discountM, ProductMuti _products)
        {
            Discount = discountM;
            products = _products;
        }

        public override double ProductsPrice()
        {
            var productPrice = products.ProductsPrice();
            return productPrice * Discount;
        }
    }
    class FullRductionActivityMuti : BaseActivityMuti
    {
        ProductMuti products = null;
        public List<(double, double)> ReductionList;
        public FullRductionActivityMuti(List<(double, double)> reductionList, ProductMuti _products)
        {
            ReductionList = reductionList;
            products = _products;
        }

        public override double ProductsPrice()
        {
            var priceProduct = this.products.ProductsPrice();
            double reductionM = 0;//匹配最高优惠
            ReductionList.ForEach(c =>
            {
                if (priceProduct > c.Item1)
                {
                    reductionM = reductionM > c.Item2 ? reductionM : c.Item2;
                }
            });
            return priceProduct - reductionM;
        }
    }

    public abstract class BaseActivity : BaseProduct
    {
        public BaseActivity()
        { }
    }

    public class DisCountActivity : BaseActivity
    {
        public BaseProduct Product = null;
        public DisCountActivity(double discountM,BaseProduct product)
        {
            this.DisCountM = discountM;
            this.Product = product;
        }
        public double DisCountM { get; private set; }
        public override double ProductPrice()
        {
            return this.Product.ProductPrice() * DisCountM;
        }
    }
    public class FullRductionActivity : BaseActivity
    {
        public BaseProduct Product = null;
        public List<(double, double)> ReductionList;
        public FullRductionActivity(List<(double, double)> reductionList, BaseProduct product)
        {
            this.ReductionList = reductionList;
            this.Product = product;
        }
        public override double ProductPrice()
        {
            var priceProduct = this.Product.ProductPrice();
            double reductionM = 0;//匹配最高优惠
            ReductionList.ForEach(c =>
            {
                if (priceProduct > c.Item1)
                {
                    reductionM = reductionM > c.Item2 ? reductionM : c.Item2;
                }
            });
            return priceProduct - reductionM;
        }
    }
}
