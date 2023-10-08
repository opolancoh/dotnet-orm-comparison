using DotNetOrmComparison.Core.Entities;

namespace DotNetOrmComparison.Data.EntityFramework.Seed;

public static class ProjectData
{
    public static IEnumerable<Project> GetData()
    {
        return new List<Project>
        {
            new() { Id = Guid.Parse("0d0a0350-ed18-417e-8db3-23b5d48532aa"), Name = "CodeCraft Pro", Status = ProjectStatus.NotStarted, Progress = 0 },
            new() { Id = Guid.Parse("10b2c41f-d575-4e0e-b1cb-b65bb032f98a"), Name = "DataSync Connect", Status = ProjectStatus.InProgress, Progress = 33 },
            new() { Id = Guid.Parse("16aea6e9-f0ec-45ea-ac11-0f85fbf4f71b"), Name = "QuantumLeap AI", Status = ProjectStatus.Completed, Progress = 100 },
            new() { Id = Guid.Parse("226e88fb-ba4e-4220-851a-33d9d86f01fd"), Name = "CyberShield X", Status = ProjectStatus.Canceled, Progress = 4 },
            new() { Id = Guid.Parse("337f032a-8825-4194-866b-196b45dcbee2"), Name = "CloudSprint", Status = ProjectStatus.InProgress, Progress = 5 },
            new() { Id = Guid.Parse("381d671b-508b-4326-a2fb-ce8b25197fbb"), Name = "RoboVision", Status = ProjectStatus.Canceled, Progress = 54 },
            new() { Id = Guid.Parse("47030828-e502-4832-8194-6155dea273bf"), Name = "NanoNet Innovate", Status = ProjectStatus.Canceled, Progress = 23 },
            new() { Id = Guid.Parse("494d0bae-6b54-43c8-a104-6dfc7b286a08"), Name = "CryptoGuardian", Status = ProjectStatus.Completed, Progress = 100 },
            new() { Id = Guid.Parse("590afec8-19ff-4be6-bca4-86de6dc8de6f"), Name = "HyperFusion Labs", Status = ProjectStatus.InProgress, Progress = 76 },
            new() { Id = Guid.Parse("5fc808b0-6de7-439d-89cf-36f8dda5c434"), Name = "VirtualMind AI", Status = ProjectStatus.InProgress, Progress = 87 },
            new() { Id = Guid.Parse("67c6725a-fb02-468a-975b-873963bd0a12"), Name = "TechTrakr Pro", Status = ProjectStatus.NotStarted, Progress = 0 },
            new() { Id = Guid.Parse("69ca1176-d08f-4862-9c14-ab529f1e97ce"), Name = "BioTech Nexus", Status = ProjectStatus.Completed, Progress = 100 },
            new() { Id = Guid.Parse("6bbfc976-6f97-4297-a2a4-4502408f9f80"), Name = "SpaceXplorer", Status = ProjectStatus.InProgress, Progress = 94 },
            new() { Id = Guid.Parse("6d4dbabb-c816-477d-a8e0-5428b725ad48"), Name = "BlockChain Forge", Status = ProjectStatus.InProgress, Progress = 79 },
            new() { Id = Guid.Parse("7decc518-5a0b-431e-be38-d29b5bb7f204"), Name = "AeroDrone Technologies", Status = ProjectStatus.NotStarted, Progress = 0 },
            new() { Id = Guid.Parse("90d81037-aa95-4591-b9b9-2424e8526626"), Name = "HealthHub Plus", Status = ProjectStatus.Completed, Progress = 100 },
            new() { Id = Guid.Parse("c6c7cc44-b814-4299-a270-1d5acce9a662"), Name = "EcoSolutions X", Status = ProjectStatus.NotStarted, Progress = 0 },
            new() { Id = Guid.Parse("d7468008-91f3-459d-85d6-6ff286bdba84"), Name = "SmartGrid Pro", Status = ProjectStatus.Completed, Progress = 100 },
            new() { Id = Guid.Parse("de4b64f0-4c2d-4ac1-8eaa-c12e2c0a228f"), Name = "Augmented Xperience", Status = ProjectStatus.InProgress, Progress = 73 },
            new() { Id = Guid.Parse("f6629231-12a4-4da1-b6ef-c037937ac266"), Name = "SecureChat Matrix", Status = ProjectStatus.InProgress, Progress = 27 },
        };
    }
}