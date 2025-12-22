#:property TargetFramework=net10.0

#:package Shouldly@4.3.0

using Shouldly;

// Useful reading https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/choosing-between-class-and-struct

PersonClass personClass = new()
{
    FirstName = "John",
    LastName = "Doe"
};

PersonClass personClassReference = personClass;

// Difficult to clone as there's no clone method by default.
PersonClass personClass2 = new()
{
    FirstName = "John",
    LastName = "Doe"
};

PersonRecord personRecord = new()
{
    FirstName = "John",
    LastName = "Doe"
};

PersonRecord personRecordReference = personRecord;

// Cloning a record is easy with the `with` expression.
PersonRecord personRecord2 = personRecord with { };

PersonStruct personStruct = new()
{
    FirstName = "John",
    LastName = "Doe"
};

// Cloning a struct is easy with the `with` expression.
PersonStruct personStruct2 = personStruct with { };

dynamic personStructBoxed = (object)personStruct; // This copies the struct into a new object.
dynamic personStructBoxedReference = personStructBoxed;

// ## ToString()

personClass.ToString().ShouldBe("PersonClass");
personRecord.ToString().ShouldBe("PersonRecord { FirstName = John, LastName = Doe }");
personStruct.ToString().ShouldBe("PersonStruct");

// ## GetHashCode()

personClass.GetHashCode().ShouldBe(54267293);
// personRecord.GetHashCode() // Changes every time.
// personStruct.GetHashCode() // Changes every time.

// ## Equality

// Classes are compared by reference, not value.
personClass.ShouldBe(personClassReference); // Same references.
personClass.ShouldNotBe(personClass2); // Same values, but different references.

// Records are compared by value, not reference.
personRecord.ShouldBe(personRecordReference); // Same values.
personRecord.ShouldBe(personRecord2); // Same values.

personStruct.ShouldBe(personStruct2); // Same values.
// personStruct.ShouldBe(personStructBoxed); // Would be true, if it worked.

public class PersonClass
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
};

public record PersonRecord
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
};

public struct PersonStruct
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
