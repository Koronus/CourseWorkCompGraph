namespace CourseWorkWithoutAI.structures;

public class Matrix
{
    private double[,] mt = new double[4, 4];
    
    public Matrix()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                mt[i,j] = (i == j) ? 1.0 : 0.0;
            }
        }
    }

    public Matrix(
        double i00, double i01, double i02, double i03,
        double i10, double i11, double i12, double i13,
        double i20, double i21, double i22, double i23,
        double i30, double i31, double i32, double i33
    )
    {
        mt[0, 0] = i00;     mt[1, 0] = i10;     mt[2, 0] = i20;     mt[3, 0] = i30;
        mt[0, 1] = i01;     mt[1, 1] = i11;     mt[2, 1] = i21;     mt[3, 1] = i31;
        mt[0, 2] = i02;     mt[1, 2] = i12;     mt[2, 2] = i22;     mt[3, 2] = i32;
        mt[0, 3] = i03;     mt[1, 3] = i13;     mt[2, 3] = i23;     mt[3, 3] = i33;
        
    }

    public Matrix Translate(double tx, double ty, double tz)
    {
        
        mt[0, 3] = tx;
        mt[1, 3] = ty;
        mt[2, 3] = tz;
        return this;
    }
    
    
    public  Matrix Scale(double sx, double sy, double sz,double k)
    {
        
        mt[0, 0] = sx;
        mt[1, 1] = sy;
        mt[2, 2] = sz;
        mt[3, 3] = k;
        return this;
    }
    
   
    public  Matrix RotateX(double degree)
    {
        
        double rad = degree * Math.PI / 180.0;
        double cos = Math.Cos(rad);
        double sin = Math.Sin(rad);
        
        mt[1, 1] = cos;
        mt[1, 2] = -sin;
        mt[2, 1] = sin;
        mt[2, 2] = cos;
        return this;
    }
    
  
    public  Matrix RotateY(double degree)
    {

        double rad = degree * Math.PI / 180.0;
        double cos = Math.Cos(rad);
        double sin = Math.Sin(rad);
        
        mt[0, 0] = cos;
        mt[0, 2] = -sin;
        mt[2, 0] = sin;
        mt[2, 2] = cos;
        return this;
    }
    
   
    public  Matrix RotateZ(double degree)
    {
        
        double rad = degree * Math.PI / 180.0;
        double cos = Math.Cos(rad);
        double sin = Math.Sin(rad);
        
        mt[0, 0] = cos;
        mt[0, 1] = -sin;
        mt[1, 0] = sin;
        mt[1, 1] = cos;
        return this;
    }
    
    public Matrix LookAt(Vector center, Vector up , Vector eye)
    {
        Vector z = center - eye;
        Vector x = up ^ z;
        Vector y = z ^ x;
        
        mt[0, 0] = x.GetV1(); mt[0, 1] = x.GetV2(); mt[0, 2] = x.GetV3(); mt[0,3] = -eye.GetV1();
        mt[1, 0] = y.GetV1(); mt[1, 1] = y.GetV2(); mt[1, 2] = y.GetV3(); mt[1,3] = -eye.GetV2();
        mt[2, 0] = z.GetV1(); mt[2, 1] = z.GetV2(); mt[2, 2] = z.GetV3(); mt[2,3] = -eye.GetV3();

        return this;
    }
    
    public Matrix ProjOrto(double l, double r, double b, double t, double n, double f)
    {

        mt[0, 0] = 2.0 / (r - l);
        mt[1, 1] = 2.0 / (t - b);
        mt[2, 2] = -2.0 / (f - n);
        mt[0, 3] = -(r + l) / (r - l);
        mt[1, 3] = -(b + t) / (b - t);
        mt[2, 3] = -(f + n) / (f - n);
        
        // Console.WriteLine(mt[0, 0]);
        // Console.WriteLine(mt[1, 1]);
        // Console.WriteLine(mt[2, 2]);
        // Console.WriteLine(mt[0, 3]);
        // Console.WriteLine(mt[1, 3]);
        // Console.WriteLine(mt[2, 3]);

        return this;
    }

    public Matrix ViewPort(double width, double height, double x, double y,double depth)
    {
        mt[0, 0] = width / 2.0;    mt[0, 3] = x + width / 2.0;
        mt[1, 1] = height / 2.0;   mt[1, 2] = y + height / 2.0;
        mt[2, 2] = depth / 2.0;    mt[2, 3] = x + depth / 2.0;

        return this;
    }

    public static Matrix operator *(Matrix m1, Matrix m2)
    {
        Matrix result = new Matrix();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                result.mt[i, j] = 0;
            }
        }

        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    result.mt[i, j] += m1.mt[i, k] * m2.mt[k, j];
                }
            }
        }

        return result;
    }

    public void toString()
    {
      
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Console.WriteLine(i+","+j+") "+mt[i, j]);
            }
        }
    }
}