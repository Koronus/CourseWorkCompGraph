using CourseWorkWithoutAI.structures;

namespace CourseWorkWithoutAI.polygon;

public class Triangle
{
    private Vector V1;
    private Vector V2;
    private Vector V3;

    private Vector N1;
    private Vector N2;
    private Vector N3;
    
    private int indexV1;
    private int indexV2;
    private int indexV3;

    private double intensityV1;
    private double intensityV2;
    private double intensityV3;

    private string figure;
    
    private Color color;
    
    public Triangle(Vector v1, Vector v2, Vector v3,int indexV1,int indexV2,int indexV3 ,string figure,Color color)
    {
        V1 = v1;
        V2 = v2;
        V3 = v3;
        this.indexV1 = indexV1;
        this.indexV2 = indexV2;
        this.indexV3 = indexV3;
        this.figure = figure;
        this.color = color;
    }

    public Vector Normal()
    {
        Vector a = V2 - V1;
        Vector b = V3 - V1;
        Vector result = b ^ a;
        result.normalize();

        return result;
    }

    public string GetFigure()
    {
        return figure;
    }

    public Vector getV1()
    {
        return V1;
    }

    public Vector getV2()
    {
        return V2;
    }

    public Vector getV3()
    {
        return V3;
    }

    public int GetIndexV1()
    {
        return indexV1;
    }
    
    public int GetIndexV2()
    {
        return indexV2;
    }
    
    public int GetIndexV3()
    {
        return indexV3;
    }

    public void setN1(Vector n1)
    {
        N1 = n1;
    }
    
    public void setN2(Vector n2)
    {
        N2 = n2;
    }
    
    public void setN3(Vector n3)
    {
        N3 = n3;
    }

    public Vector getN1()
    {
        return N1;
    }
    
    public Vector getN2()
    {
        return N2;
    }
    
    public Vector getN3()
    {
        return N3;
    }

    public void setIntensity1(double intensity)
    {
        intensityV1 = intensity;
    }
    
    public void setIntensity2(double intensity)
    {
        intensityV2 = intensity;
    }
    
    public void setIntensity3(double intensity)
    {
        intensityV3 = intensity;
    }

    public double getIntensity1()
    {
        return intensityV1;
    }
    
    public double getIntensity2()
    {
        return intensityV2;
    }
    
    public double getIntensity3()
    {
        return intensityV3;
    }

    public Color getColor()
    {
        return color;
    }
    
    public String toString()
    {
        // return $"({this.V1.toString()}), ({this.V2.toString()}), ({this.V3.toString()}),({this.indexV1}),({this.indexV2}),({this.indexV3}),({this.figure}),{this.color},({this.N1}),({this.N2}),({this.N3})";
         return $"({this.N1}),({this.N2.toString()}),({this.N3.toString()})";
    }
}