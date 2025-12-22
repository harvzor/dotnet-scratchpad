#:property TargetFramework=net10.0

// string[] arguments = Environment
//     .GetCommandLineArgs()
//     // First always seems to be the dll.
//     .Skip(1)
//     .ToArray();

Console.WriteLine("Your inputs: " + String.Join(',', args));

