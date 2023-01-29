using System.Collections.Generic;

namespace Exam.Categorization
{
    using System;
    using System.Linq;

    public class Categorizator : ICategorizator
    {
        private Dictionary<string, Category> categoriesById;

        public Categorizator()
        {
            this.categoriesById = new Dictionary<string, Category>();
        }

        public void AddCategory(Category category)
        {
            if (this.categoriesById.ContainsKey(category.Id))
            {
                throw new ArgumentException();
            }

            this.categoriesById.Add(category.Id, category);
        }

        public void AssignParent(string childCategoryId, string parentCategoryId)
        {
            if (!this.categoriesById.ContainsKey(parentCategoryId))
            {
                throw new ArgumentException();
            }

            if (!this.categoriesById.ContainsKey(childCategoryId))
            {
                throw new ArgumentException();
            }

            Category parent = this.categoriesById[parentCategoryId];
            Category child = this.categoriesById[childCategoryId];

            if (child.Parent != null)
            {
                if (child.Parent.Equals(parent))
                {
                    throw new ArgumentException();
                }
            }

            child.Parent = parent;
            if (parent.Children.Count == 0)
            {
                parent.Depth++;
                Category c = parent.Parent;
                while (c != null)
                {
                    c.Depth++;
                    c = c.Parent;
                }
            }
            parent.Children.Add(child);
        }

        public void RemoveCategory(string categoryId)
        {
            if (!this.categoriesById.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }

            Category category = this.categoriesById[categoryId];

            this.categoriesById.Remove(categoryId);
            this.RemoveChildren(category);
        }

        private void RemoveChildren(Category category)
        {
            foreach (var child in category.Children)
            {
                this.categoriesById.Remove(child.Id);
                this.RemoveChildren(child);
            }
        }

        public bool Contains(Category category)
        {
            return this.categoriesById.ContainsKey(category.Id);
        }

        public int Size()
        {
            return this.categoriesById.Count;
        }

        public IEnumerable<Category> GetChildren(string categoryId)
        {
            if (!this.categoriesById.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }

            Category category = this.categoriesById[categoryId];

            return this.GetChildren(category);
        }

        private IEnumerable<Category> GetChildren(Category category)
        {
            var list = new List<Category>();

            foreach (var child in category.Children)
            {
                list.Add(child);

                if (child.Children.Count > 0)
                {
                    list.AddRange(this.GetChildren(child));
                }
            }

            return list;
        }

        public IEnumerable<Category> GetHierarchy(string categoryId)
        {
            if (!this.categoriesById.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }

            Category category = this.categoriesById[categoryId];

            Stack<Category> result = new Stack<Category>();
            result.Push(category);
            while (category.Parent != null)
            {
                result.Push(category.Parent);
                category = category.Parent;
            }

            return result;
        }

        public IEnumerable<Category> GetTop3CategoriesOrderedByDepthOfChildrenThenByName()
        {
            var list = this.categoriesById.Values
                .OrderByDescending(c => c.Depth)
                .Take(3);
            return list.OrderBy(c => c.Name);
        }
    }
}
