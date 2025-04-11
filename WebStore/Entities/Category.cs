using System.Collections.Generic;

namespace WebStore.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; } = new List<Category>();
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
} 