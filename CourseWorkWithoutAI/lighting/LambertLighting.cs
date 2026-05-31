using CourseWorkWithoutAI.polygon;
using CourseWorkWithoutAI.structures;

namespace CourseWorkWithoutAI.lighting;

public class LambertLighting
{
    private double ambient;
    private double k_d;
    private Vector lightDir;
    private double intensityV1;
    private double intensityV2;
    private double intensityV3;
    private List<Triangle> allTriangles;

    public LambertLighting(double ambient,double k_d,Vector lightDir,List<Triangle> allTriangles)
    {
        this.ambient = ambient;
        this.k_d = k_d;
        this.lightDir = lightDir;
        this.allTriangles = allTriangles;
    }

    public void defIllumVertex()
    {
        foreach (var triangle in allTriangles)
        {
            intensityV1 = ambient + k_d * Math.Max(0, lightDir * triangle.Normal());
            intensityV2 = ambient + k_d * Math.Max(0, lightDir * triangle.Normal());
            intensityV3 = ambient + k_d * Math.Max(0, lightDir * triangle.Normal());
            triangle.setIntensity1(intensityV1);
            triangle.setIntensity2(intensityV2);
            triangle.setIntensity3(intensityV3);
        }
    }
    
    
}