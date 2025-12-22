#:property TargetFramework=net10
#:package Dunet@1.11.3

using Dunet;

Service.Shape shape = Service.GetShape();

var area = shape.Match(
    circle => 3.14 * circle.Radius * circle.Radius,
    rectangle => rectangle.Length * rectangle.Width,
    triangle => triangle.Base * triangle.Height / 2
);

Console.WriteLine($"Area: " + area);

public static class Service
{
    [Union]
    public partial record Shape
    {
        public partial record Circle(double Radius);
        public partial record Rectangle(double Length, double Width);
        public partial record Triangle(double Base, double Height);
    }

    public static Service.Shape GetShape()
    {
        return new Service.Shape.Circle(2);
    }
}

