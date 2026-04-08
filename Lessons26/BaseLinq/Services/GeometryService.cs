namespace BaseLinq.Services;

public class GeometryService
{
    public event Action<string>? OperationPerformed;

    public double CalculateTriangleArea(double triangleBase, double triangleHeight)
    {
        double area = 0.5 * triangleBase * triangleHeight;
        OnOperationPerformed("Calculated triangle area");
        return area;
    }

    public double CalculateRectangleArea(double width, double height)
    {
        double area = width * height;
        OnOperationPerformed("Calculated rectangle area");
        return area;
    }

    private void OnOperationPerformed(string message)
    {
        OperationPerformed?.Invoke(message);
    }
}