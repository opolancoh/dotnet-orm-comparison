using System.Globalization;
using DotNetOrmComparison.Core.Entities;

namespace DotNetOrmComparison.Data.EntityFramework.Seed;

public static class DataSeeder
{
    public static void Seed(AppDbContext dbContext)
    {
        var departmentsCount = 0;
        if (!dbContext.Departments.Any())
        {
            var departments = DepartmentData.GetData().ToList();
            dbContext.Departments.AddRange(departments);

            dbContext.SaveChanges();
            departmentsCount = departments.Count;
        }

        var projectsCount = 0;
        if (!dbContext.Projects.Any())
        {
            var projects = ProjectData.GetData().ToList();
            dbContext.Projects.AddRange(projects);

            dbContext.SaveChanges();
            projectsCount = projects.Count;
        }

        if (dbContext.Employees.Any()) return;

        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seed/employees-20k.csv");
        var employeesCount = SeedEmployeeDataInChunks(filePath, 6000, dbContext);

        Console.WriteLine($"Departments added: {departmentsCount}");
        Console.WriteLine($"Projects added: {projectsCount}");
        Console.WriteLine($"Employees added: {employeesCount}");
    }


    private static int SeedEmployeeDataInChunks(string filePath, int chunkSize, AppDbContext dbContext)
    {
        using var reader = new StreamReader(filePath);
        // Assume the first line is the header and skip it
        var headerLine = reader.ReadLine();
        var employeesCount = 0;
        var employeesChunk = new List<Employee>();
        var employeesChunkCount = 0;
        var departments = DepartmentData.GetData().ToList();
        var projects = ProjectData.GetData().ToList();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line!.Split(',');
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = values[0],
                LastName = values[1],
                Gender = (Gender)Enum.Parse(typeof(Gender), values[2]),
                Email = values[3],
                HireDate = DateTime.ParseExact(values[4], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    .ToUniversalTime(),
                Salary = decimal.Parse(values[5]),
                IsActive = values[6] == "TRUE",
                HourlyRate = int.Parse(values[7]),
                MaritalStatus = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), values[8]),
                Address = new Address
                {
                    Id = Guid.NewGuid(),
                    Street = values[9],
                    City = values[10],
                    State = values[11],
                    ZipCode = values[12]
                },
                DepartmentId = departments[GetRandomNumber(0, departments.Count - 1)].Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            employee.EmployeeProjects = GetRandomProjects(projects, employee.Id);
            employeesChunk.Add(employee);
            employeesChunkCount++;

            if (employeesChunkCount < chunkSize) continue;

            ProcessChunk(employeesChunk, dbContext); // Process the chunk of lines
            employeesChunk.Clear();
            employeesCount += employeesChunkCount;
            employeesChunkCount = 0;
        }
        
        if (!employeesChunk.Any()) return employeesCount;

        // Process the remaining lines if any
        foreach (var item in employeesChunk)
        {
            item.EmployeeProjects = GetRandomProjects(projects, item.Id);
        }
        ProcessChunk(employeesChunk, dbContext);
        employeesCount += employeesChunk.Count;
        
        return employeesCount;
    }

    private static void ProcessChunk(List<Employee> employees, AppDbContext dbContext)
    {
        dbContext.Employees.AddRange(employees);
        dbContext.SaveChanges();
    }

    private static List<EmployeeProject> GetRandomProjects(IReadOnlyCollection<Project> projects, Guid employeeId)
    {
        var employeeProjects = new List<EmployeeProject>();
        var projectsToAddCount = GetRandomNumber(0, 3);
        var projectsToAddIDs = GetRandomList(projects, projectsToAddCount).Select(x => x.Id);
        foreach (var id in projectsToAddIDs)
        {
            employeeProjects.AddRange(new List<EmployeeProject> { new() { EmployeeId = employeeId, ProjectId = id } });
        }

        return employeeProjects;
    }

    private static IReadOnlyCollection<T> GetRandomList<T>(IReadOnlyCollection<T> sourceList, int count)
    {
        if (count >= sourceList.Count)
        {
            // Return the entire list if the count is greater or equal to the list size
            return sourceList;
        }

        var randomList = new List<T>();
        var rand = new Random();

        var tempList = sourceList.ToList(); // Create a copy of the source list

        for (var i = 0; i < count; i++)
        {
            var index = rand.Next(tempList.Count); // Get a random index
            randomList.Add(tempList[index]); // Add the item to the random list
            tempList.RemoveAt(index); // Remove the item from the temporary list to avoid repetition
        }

        return randomList;
    }

    private static int GetRandomNumber(int min, int max)
    {
        var random = new Random();
        return random.Next(min, max + 1); // Generates a random number between min and max
    }
}