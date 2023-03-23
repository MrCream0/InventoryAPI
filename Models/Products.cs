using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace ProductsApi.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int OnHand { get; set; }
    }
}
