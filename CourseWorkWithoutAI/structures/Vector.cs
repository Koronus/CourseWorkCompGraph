using Microsoft.VisualBasic.CompilerServices;

namespace CourseWorkWithoutAI.structures;

public class Vector
{
    private double v1;
    private double v2;
    private double v3;
    private double w;


    public double GetV1()
    {
        return v1;
    }
    public double GetV2()
    {
        return v2;
    }
    public double GetV3()
    {
        return v3;
    }

    public double GetW()
    {
        return w;
    }


    public Vector(double v1, double v2, double v3)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
        w = 1;
    }

    public Vector(double v1, double v2, double v3, double w)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
        this.w = w;
    }

    public void normalize()
    {
        double lenVector = Math.Sqrt(Math.Pow(v1, 2) + Math.Pow(v2, 2) + Math.Pow(v3, 2));
        
        if (lenVector != 0)
        {
            v1 /= lenVector;
            v2 /= lenVector;
            v3 /= lenVector;
        }
        
       
    }
    
    public static Vector operator +(Vector A, Vector B)
    {
        return new Vector
        (
            A.v1 + B.v1,
            A.v2 + B.v2,
            A.v3 + B.v3
        );
    }
    
    public static Vector operator -(Vector A, Vector B)
    {
        return new Vector
        (
            A.v1 - B.v1,
            A.v2 - B.v2,
            A.v3 - B.v3
        );
    }

    public static double operator *(Vector A, Vector B)
    {
        return A.v1 * B.v1 + A.v2 * B.v2 + A.v3 * B.v3;
    }

    

    public static Vector operator ^(Vector A, Vector B)
    {
        return new Vector
        (
            A.v2 * B.v3 - A.v3 * B.v2,
            A.v3 * B.v1 - A.v1 * B.v3,
            A.v1 * B.v2 - A.v2 * B.v1
        );
    }

    public String toString()
    {
        return $"({this.v1}, {this.v2}, {this.v3})";
    }
    
}