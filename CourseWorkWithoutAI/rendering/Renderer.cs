using CourseWorkWithoutAI.structures;

namespace CourseWorkWithoutAI.rendering;

public class Renderer
{

   private Bitmap bitmap;
    private double[,] zBuffer;

    public Renderer(Bitmap bitmap, double[,] zBuffer)
    {
        this.bitmap = bitmap;
        this.zBuffer = zBuffer;
    }

    public void resterizeTriangle(Vector V1, Vector V2, Vector V3, double i1, double i2, double i3,Color color)
    {
        if (V1.GetV2() > V2.GetV2()) {
            swap(ref V1, ref V2);
            swap(ref i1, ref i2);
        }
        if (V1.GetV2() > V3.GetV2()) {
            swap(ref V1, ref V3);
            swap(ref i1, ref i3);
        }
        if (V2.GetV2() > V3.GetV2()) {
            swap(ref V2, ref V3);
            swap(ref i2, ref i3);
        }
        
        int y0 = (int)V1.GetV2();
        int y1 = (int)V2.GetV2();
        int y2 = (int)V3.GetV2();
        
        int x0 = (int)V1.GetV1();
        int x1 = (int)V2.GetV1();
        int x2 = (int)V3.GetV1();
        
        double z0 = V1.GetV3();  
        double z1 = V2.GetV3();  
        double z2 = V3.GetV3();  

       
        for (int y = y0; y <= y2; y++)
        {

            if (y < 0)
                continue;

            double t02 = (double)(y - y0) / (y2 - y0);
            double xA = x0 + t02 * (x2 - x0);
            

            double zA = z0 + t02 * (z2 - z0);
            double iA = i1 + (i3 - i1) * t02;  
            
            double xB;
            double zB;  
            double iB;  
            
            if (y < y1)
            {
                if (y1 == y0) continue;

                double t01 = (double)(y - y0) / (y1 - y0);
                xB = x0 + t01 * (x1 - x0);
                

                zB = z0 + t01 * (z1 - z0);
                iB = i1 + (i2 - i1) * t01;
            }
            else 
            {
                if (y2 == y1) continue;

                double t12 = (double)(y - y1) / (y2 - y1);
                xB = x1 + t12 * (x2 - x1);
                

                zB = z1 + t12 * (z2 - z1);
                iB = i2 + (i3 - i2) * t12;
            }
            
            
            if (xA > xB)
            {
                swap(ref xA, ref xB);
                swap(ref zA, ref zB);  
                swap(ref iA, ref iB);  
            }
            
            int startX = Math.Max(0, (int)Math.Ceiling(xA));
            int endX = Math.Min(
                bitmap.Width - 1,
                (int)Math.Floor(xB));
            
            
            
            
            for (int x = startX; x <= endX; x++)
            {
                double phi = (xB == xA) ? 0 : (x - xA) / (xB - xA);
                double intensity = iA + (iB - iA) * phi;
                double depth = zA + (zB - zA) * phi;
                
                if (x < 0 || x >= bitmap.Width) continue;
                if (y < 0 || y >= bitmap.Height) continue;
                
                if (depth > zBuffer[x, y])  
                {
                    zBuffer[x, y] = depth; 
        
           
                    int r = (int)(color.R * intensity);
                    int g = (int)(color.G * intensity);
                    int b = (int)(color.B * intensity);
        
                    r = Math.Max(0, Math.Min(255, r));
                    g = Math.Max(0, Math.Min(255, g));
                    b = Math.Max(0, Math.Min(255, b));
        
                    PutPixel(x, y, Color.FromArgb(255, r, g, b));
                }
            }

        }

    }

    public void swap <T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
    
    private void PutPixel(int x, int y, Color color)
    {
        if (x < 0 || x >= bitmap.Width)
            return;

        if (y < 0 || y >= bitmap.Height)
            return;

        bitmap.SetPixel(x, y, color);
    }

}