using System.Collections.Generic;
using prep.collections;

namespace prep.utility
{
    public interface IMatchFactory<ItemToFind, PropertyType>
    {
        IMatchAn<ItemToFind> equal_to(PropertyType value);
        IMatchAn<ItemToFind> equal_to_any(params PropertyType[] values);
        IMatchAn<ItemToFind> not_equal_to(PropertyType value);
    }

    public class MatchFactory<ItemToFind, PropertyType> : IMatchFactory<ItemToFind, PropertyType>
  {
    PropertyAccessor<ItemToFind, PropertyType> accessor;

    public MatchFactory(PropertyAccessor<ItemToFind, PropertyType> accessor)
    {
      this.accessor = accessor;
    }

    public IMatchAn<ItemToFind> equal_to(PropertyType value)
    {
      return equal_to_any(value);
    }

    public IMatchAn<ItemToFind> equal_to_any(params PropertyType[] values)
    {
      return new LambdaMatcher<ItemToFind>(x => new List<PropertyType>(values).Contains(accessor(x)));
    }

    public IMatchAn<ItemToFind> not_equal_to(PropertyType value)
    {
      return equal_to(value).not();
    }
  }
}