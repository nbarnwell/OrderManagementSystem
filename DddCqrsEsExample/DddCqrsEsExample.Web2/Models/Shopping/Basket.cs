using System.Collections.Generic;
using System.Linq;

namespace DddCqrsEsExample.Web2.Models.Shopping
{
    public class Basket
    {
        private readonly IList<AddToBasketViewModel> _items = new List<AddToBasketViewModel>();

        public void AddItem(AddToBasketViewModel item)
        {
            var existingItem = _items.SingleOrDefault(model => model.Id == item.Id);

            if (existingItem == null)
            {
                _items.Add(item);
                return;
            }

            existingItem.Merge(item);
        }

        public IEnumerable<AddToBasketViewModel> GetItems()
        {
            return _items.ToArray();
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}