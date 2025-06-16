// Run with `dotnet run -e INPUT=test .\read-env-var.cs`
#:property TargetFramework net10.0

var input = Environment.GetEnvironmentVariable("INPUT");

Console.WriteLine(input);
