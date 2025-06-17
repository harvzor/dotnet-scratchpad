#:property TargetFramework net10.0

#:package Shouldly@4.3.0

using Shouldly;

// This code demonstrates the use of the C# 8.0 index operator to access elements from the end of an array.

// Arrange
string[] array = ["beginning", "middle", "end"];

// Act
string result = array[^1];

// Assert
result.ShouldBe("end");
