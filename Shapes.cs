using System.Diagnostics;

// "Clean" code
class shape_base
{
    public shape_base() { }
    public virtual float Area() { return 0; }
}

class square : shape_base
{
    private float Side;
    public square(float sideInit)
    {
        Side = sideInit;
    }
    public override float Area()
    {
        return Side * Side;
    }
}

class rectangle : shape_base
{
    private float Width, Height;
    public rectangle(float widthInit, float heightInit)
    {
        Width = widthInit;
        Height = heightInit;
    }
    public override float Area()
    {
        return Width * Height;
    }
}

class triangle : shape_base
{
    private float Width, Height;
    public triangle(float widthInit, float heightInit)
    {
        Width = widthInit;
        Height = heightInit;
    }
    public override float Area()
    {
        return 0.5f * Width * Height;
    }
}

class circle : shape_base
{
    private float Radius;
    public circle(float radius)
    {
        Radius = radius;
    }
    public override float Area()
    {
        return 3.14f * Radius;
    }
}


// Fast code

enum shape_type
{
    Square,
    Rectangle,
    Triangle,
    Circle,

    Shape_Count
}

struct shape_union
{
    public shape_type Type;
    public float Width;
    public float Height;
}


public static class Shapes
{
    public static void Run()
    {
        shape_base[] shapes = new shape_base[1048576];
        Random rand = new Random();
        for (int i = 0; i < shapes.Length; i++)
        {
            int r = rand.Next(0, 3);
            switch (r)
            {
                case 0:
                    shapes[i] = new square(i);
                    break;
                case 1:
                    shapes[i] = new rectangle(i, i);
                    break;
                case 2:
                    shapes[i] = new triangle(i, i);
                    break;
                case 3:
                    shapes[i] = new circle(i);
                    break;
                default:
                    break;
            }
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        TotalAreaVTBL(shapes.Length, shapes);
        sw.Stop();
        TimeSpan time0 = sw.Elapsed;

        sw.Reset();
        sw.Start();
        TotalAreaVTBL(shapes.Length, shapes);
        sw.Stop();
        TimeSpan time1 = sw.Elapsed;


        shape_union[] shapes_struct = new shape_union[1048576];
        for (int i = 0; i < shapes_struct.Length; i++)
        {
            shapes_struct[i] = new shape_union();
            int r = rand.Next(0, 3);
            switch (r)
            {
                case 0:
                    shapes_struct[i].Type = shape_type.Square;
                    break;
                case 1:
                    shapes_struct[i].Type = shape_type.Rectangle;
                    break;
                case 2:
                    shapes_struct[i].Type = shape_type.Triangle;
                    break;
                case 3:
                    shapes_struct[i].Type = shape_type.Circle;
                    break;
                default:
                    break;
            }
        }

        sw.Reset();
        sw.Start();
        TotalAreaSwitch(shapes_struct.Length, shapes_struct);
        sw.Stop();
        TimeSpan time2 = sw.Elapsed;

        sw.Reset();
        sw.Start();
        TotalAreaSwitch(shapes_struct.Length, shapes_struct);
        sw.Stop();
        TimeSpan time3 = sw.Elapsed;


        sw.Reset();
        sw.Start();
        TotalAreaUnion(shapes_struct.Length, shapes_struct);
        sw.Stop();
        TimeSpan time4 = sw.Elapsed;

        sw.Reset();
        sw.Start();
        TotalAreaUnion(shapes_struct.Length, shapes_struct);
        sw.Stop();
        TimeSpan time5 = sw.Elapsed;


        Console.WriteLine("TotalAreaVTBL:     " + "1,000x   " + time0);
        Console.WriteLine("TotalAreaVTBL:     " + $"{(time0 / time1).ToString("0.000")}x   " + time1);
        Console.WriteLine("TotalAreaSwitch:   " + $"{(time0 / time2).ToString("0.000")}x   " + time2);
        Console.WriteLine("TotalAreaSwitch:   " + $"{(time0 / time3).ToString("0.000")}x   " + time3);
        Console.WriteLine("TotalAreaUnion:    " + $"{(time0 / time4).ToString("0.000")}x   " + time4);
        Console.WriteLine("TotalAreaUnion:    " + $"{(time0 / time5).ToString("0.000")}x   " + time5);

    }

    /// 
    /// Compute 0
    /// 


    static float TotalAreaVTBL(int ShapeCount, shape_base[] Shapes)
    {
        float Accum = 0.0f;
        for (int i = 0; i < ShapeCount; i++)
        {
            Accum += Shapes[i].Area();
        }

        return Accum;
    }


    //
    // Compute 1
    //
    static float GetAreaSwitch(shape_union Shape)
    {
        float Result = 0.0f;

        switch (Shape.Type)
        {
            case shape_type.Square:
                Result = Shape.Width * Shape.Height;
                break;
            case shape_type.Rectangle:
                Result = Shape.Width * Shape.Height;
                break;
            case shape_type.Triangle:
                Result = 0.5f * Shape.Width * Shape.Height;
                break;
            case shape_type.Circle:
                Result = 3.14f * Shape.Width * Shape.Height;
                break;
            case shape_type.Shape_Count:
                break;
            default:
                break;
        }

        return Result;
    }

    static float TotalAreaSwitch(int ShapeCount, Span<shape_union> Shapes)
    {
        float Accum = 0.0f;
        for (int i = 0; i < ShapeCount; i++)
        {
            Accum += GetAreaSwitch(Shapes[i]);
        }

        return Accum;
    }

    static readonly float[] CTable = { 1.0f, 1.0f, 0.5f, 3.14f };
    static float GetAreaUnion(shape_union Shape)
    {
        float Result = CTable[(int)Shape.Type] * Shape.Width * Shape.Height;
        return Result;
    }

    static float TotalAreaUnion(int ShapeCount, Span<shape_union> Shapes)
    {
        float Accum = 0.0f;
        for (int i = 0; i < ShapeCount; i++)
        {
            Accum += GetAreaUnion(Shapes[i]);
        }

        return Accum;
    }
}