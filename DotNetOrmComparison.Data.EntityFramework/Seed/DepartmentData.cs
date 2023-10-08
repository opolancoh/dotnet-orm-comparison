using DotNetOrmComparison.Core.Entities;

namespace DotNetOrmComparison.Data.EntityFramework.Seed;

public static class DepartmentData
{
    public static IEnumerable<Department> GetData()
    {
        return new List<Department>
        {
            new() { Id = Guid.Parse("3365a40d-8c53-411e-a7c7-b332ad3af5a8"), Name = "Research and Development (R&D)", Manager = "Ethan Taylor" },
            new() { Id = Guid.Parse("5b86887b-2d26-4033-9c78-54c265b87de0"), Name = "Software Engineering", Manager = "Olivia Davis" },
            new() { Id = Guid.Parse("7eb1a49d-bc53-4f3d-b92d-8a9c8a9325e4"), Name = "Hardware Engineering", Manager = "Sophia Miller" },
            new() { Id = Guid.Parse("87aba6d8-81ec-4e3d-ab5a-17762be8fa64"), Name = "Data Science and Analytics", Manager = "Oliver Smith" },
            new() { Id = Guid.Parse("8964465b-f330-41d4-8868-16b1348dc8f6"), Name = "Product Management", Manager = "Benjamin Jones" },
            new() { Id = Guid.Parse("9d195535-5265-44fd-919a-5667cc8d4fb2"), Name = "Quality Assurance (QA)", Manager = "Henry Brown" },
            new() { Id = Guid.Parse("cac524d6-77df-42f7-bdcf-26db61992a2d"), Name = "Information Security", Manager = "Emily White" },
            new() { Id = Guid.Parse("d8807590-f3fc-4603-aaaa-e655ee4d29a1"), Name = "Customer Support and Success", Manager = "Ava Wilson" },
            new() { Id = Guid.Parse("e12b2b15-6840-45f9-b501-aa2c6a8aa4d5"), Name = "Sales and Marketing", Manager = "William Johnson" },
            new() { Id = Guid.Parse("e8f9e70b-fb08-49ac-a1e7-d2e84cfbc7e8"), Name = "Operations and Infrastructure", Manager = "Isabella Anderson" },
        };
    }
}