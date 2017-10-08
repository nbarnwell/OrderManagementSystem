using System;

namespace DddCqrsEsExample.Web2.Models.Shopping
{
    public class AddToBasketViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }

        public void Merge(AddToBasketViewModel item)
        {
            if (item.Id != Id) throw new InvalidOperationException();
            if (item.Description != Description) throw new InvalidOperationException();

            Quantity += item.Quantity;
        }
    }
}