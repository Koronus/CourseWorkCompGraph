using CourseWorkWithoutAI.polygon;
using CourseWorkWithoutAI.structures;

namespace CourseWorkWithoutAI.parser;

public class Normalizator
{
    private Dictionary<int, Vector> sumNormal = new Dictionary<int, Vector>();
    private Dictionary<int, int> countNormal = new Dictionary<int, int>();
    private List<Triangle> allTriangles;
    private int count = 1;
    
    private string figure;
    private string currentFigure ="";

    private bool check1;
    private bool check2;
    private bool check3;

    private int countTriangle = 3;

    public Normalizator(List<Triangle> allTriangles)
    {
        this.allTriangles = allTriangles;
    }

    public void AverageNormalVertex()
    {
        foreach (Triangle triangle in allTriangles)
        {
            check1 = sumNormal.ContainsKey(triangle.GetIndexV1());
            check2 = sumNormal.ContainsKey(triangle.GetIndexV2());
            check3 = sumNormal.ContainsKey(triangle.GetIndexV3());

            addDicts(check1,triangle.GetIndexV1(),triangle);
            addDicts(check2,triangle.GetIndexV2(),triangle);
            addDicts(check3,triangle.GetIndexV3(),triangle);
            
        }
        createNormalVertexTrianle();
    }

    private void addDicts(bool check,int IndexV, Triangle triangle)
    {
        switch (check)
        {
            case true:
                sumNormal[IndexV] =  sumNormal[IndexV] + triangle.Normal();
                countNormal[IndexV]++;
                break;
            case false:
                sumNormal.Add(IndexV, triangle.Normal());
                countNormal.Add(IndexV, 1);
                break;
            
        }
    }

    public void createNormalVertexTrianle()
    {
        foreach (var triangle in  allTriangles)
        {
            triangle.setN1( new Vector(
                sumNormal[triangle.GetIndexV1()].GetV1() / countNormal[triangle.GetIndexV1()], 
                sumNormal[triangle.GetIndexV1()].GetV2() / countNormal[triangle.GetIndexV1()],
                sumNormal[triangle.GetIndexV1()].GetV3() / countNormal[triangle.GetIndexV1()])
            );
            
            triangle.setN2( new Vector(
                sumNormal[triangle.GetIndexV2()].GetV1() / countNormal[triangle.GetIndexV2()], 
                sumNormal[triangle.GetIndexV2()].GetV2() / countNormal[triangle.GetIndexV2()],
                sumNormal[triangle.GetIndexV2()].GetV3() / countNormal[triangle.GetIndexV2()])
            );
            
            triangle.setN3( new Vector(
                sumNormal[triangle.GetIndexV3()].GetV1() / countNormal[triangle.GetIndexV3()], 
                sumNormal[triangle.GetIndexV3()].GetV2() / countNormal[triangle.GetIndexV3()],
                sumNormal[triangle.GetIndexV3()].GetV3() / countNormal[triangle.GetIndexV3()])
            );
        }
    }

    public void Print()
    {
        foreach (var dictonary in sumNormal)
        {
            Console.WriteLine(count +" ("+dictonary.Key +") ("+dictonary.Value.toString()+")" );
            count++;
        }

        count = 1;
        foreach (var dictonary in countNormal)
        {
            Console.WriteLine(count +" ("+dictonary.Key +") ("+dictonary.Value+")" );
            count++;
        }
        count = 1;

        foreach (var triangle in allTriangles)
        {
            Vector n1 = triangle.getN1();
            n1.normalize();
            Vector n2 = triangle.getN2();
            n2.normalize();
            Vector n3 = triangle.getN3();
            n3.normalize();
            Console.WriteLine(n1.toString() +" "+n2.toString() +" " + n3.toString() );
        }
    }

        
}