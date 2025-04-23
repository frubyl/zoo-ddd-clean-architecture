
namespace KPO_HW2.Domain.ValueObject
{
    public record Size
    {
        public double Length { get; }
        public double Width { get; }
        public double Height { get; }

        public Size(double length, double width, double height)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Длина должна быть положительным числом", nameof(length));
            }
            if (width <= 0)
            { 
            throw new ArgumentException("Ширина должна быть положительным числом", nameof(width));
            }
            if (height <= 0)
            {
                throw new ArgumentException("Высота должна быть положительным числом", nameof(height));
            }
            Length = length;
            Width = width;
            Height = height;
        }

    }
}
