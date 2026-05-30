using System.Globalization;
using System.Text;
using CourseWorkWithoutAI.polygon;
using CourseWorkWithoutAI.structures;

namespace CourseWorkWithoutAI.parser;

public class ObjParser
{
    private string pathFile = @"C:\Users\79829\RiderProjects\CourseWorkWithoutAI\CourseWorkWithoutAI\data\26.obj";

    private Vector currentVector;
    private Dictionary<int, Vector> tetrahedrVertex = new Dictionary<int, Vector>();
    private Dictionary<int, Vector> conusVertex = new Dictionary<int, Vector>();
    private Dictionary<int, Vector> cylinderVertex = new Dictionary<int, Vector>();
    
    private Triangle currentTrainge;
    public List<Triangle> tetrahedrTriangle = new List<Triangle>();
    public List<Triangle> conusTriangle = new List<Triangle>();
    public List<Triangle> cylinderTriangle = new List<Triangle>();

    private int countLine = 1;
    private string[] arrLines;
    private string[] partsLine;
    private string[] partsFacesLine;
    private string nameObj;
    
    private Color color;

    private string[] splitSubArray(string line)
    {
        return line.Split(' ','/');
    }

    public void Parse()
    {
        arrLines = File.ReadAllLines(pathFile);
        foreach (string line in arrLines)
        {
            partsLine = splitSubArray(line);
            switch (partsLine[1])
            {
                case "Solid":
                    nameObj = partsLine[1];
                    break;
                case "Конус":
                    nameObj = partsLine[1];
                    break;
                case "Цилиндр":
                    nameObj = partsLine[1];
                    break;
            }


            switch (partsLine[0])
            {
                case "v":
                    addVertexs(nameObj);
                    countLine++;
                    break;
                case "f":
                      addTriangles(nameObj, line);
                    break;
            }

            
        }

        // int i = 1;
        // foreach (Triangle triangle in tetrahedrTriangle)
        // {
        //     
        //     Console.WriteLine(i + ") " + triangle.toString());
        //     i++;
        // }
        // foreach (Triangle triangle in conusTriangle)
        // {
        //     
        //     Console.WriteLine(i + ") " + triangle.toString());
        //     i++;
        // }
        // foreach (Triangle triangle in cylinderTriangle)
        // {
        //     
        //     Console.WriteLine(i + ") " + triangle.toString());
        //     i++;
        // }
        
    }

    private void addVertexs(string nameObj)
    {
        double V1 = double.Parse(partsLine[1], CultureInfo.InvariantCulture);
        double V2 = double.Parse(partsLine[2], CultureInfo.InvariantCulture);
        double V3 = double.Parse(partsLine[3], CultureInfo.InvariantCulture);
        currentVector = new Vector(V1, V2, V3);
        switch (nameObj)
        {
            case "Solid":
                tetrahedrVertex.Add(countLine, currentVector);
                break;
            case "Конус":
                conusVertex.Add(countLine, currentVector);
                break;
            case "Цилиндр":
                cylinderVertex.Add(countLine, currentVector);
                break;
        }
    }

    private void addTriangles(string nameObj,string line)
    {
        partsFacesLine = splitSubArray(line);
        int index1 = int.Parse(partsFacesLine[1], CultureInfo.InvariantCulture);
        int index2 = int.Parse(partsFacesLine[4], CultureInfo.InvariantCulture);
        int index3 = int.Parse(partsFacesLine[7], CultureInfo.InvariantCulture);
        
       
        switch (nameObj)
        {
            case "Solid":
                tetrahedrTriangle.Add(new Triangle(tetrahedrVertex[index1], tetrahedrVertex[index2], tetrahedrVertex[index3],index1,index2,index3,nameObj,Color.Red));
                break;
            case "Конус":
                conusTriangle.Add(new Triangle(conusVertex[index1], conusVertex[index2], conusVertex[index3],index1,index2,index3,nameObj,Color.Blue));
                break;
            case "Цилиндр":
                cylinderTriangle.Add(new Triangle(cylinderVertex[index1], cylinderVertex[index2], cylinderVertex[index3],index1,index2,index3,nameObj,Color.Green));
                break;
        }
    }
        
}