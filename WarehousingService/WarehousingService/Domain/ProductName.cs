using System;

namespace WarehousingService.Domain
{
    public class ProductName
    {
        public string Value { get; }

        public ProductName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ApplicationException();
            }
            Value = value;
        }
    }
}