#:property TargetFramework=net10.0

Foo foo = GetFoo();

// Pattern matching is preferred because != null could be overridden (by a crazy person).
if (foo is { })
{
    Console.WriteLine("Foo is not null");
}
else
{
    Console.WriteLine("Foo is null");
}

// You can also create a new variable which isn't nullable.
if (foo is { } notNullFoo)
{
    Console.WriteLine(notNullFoo);
}

// You can also check for a property value.
if (foo is { Bar: "Baz" } notNullFooWithBarEqualToBaz)
{
    Console.WriteLine(foo.Bar);
}

Foo? GetFoo()
{
    Random random = new();

    int coinFlip = random.Next(0, 2);

    if (coinFlip == 0)
        return null;

    return new Foo()
    {
        Bar = "Baz"
    };
}

record Foo
{
    public string Bar { get; set; }
}
