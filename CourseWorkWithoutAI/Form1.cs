using CourseWorkWithoutAI.parser;
using CourseWorkWithoutAI.polygon;
using CourseWorkWithoutAI.structures;

namespace CourseWorkWithoutAI;

public partial class Form1 : Form
{
    private ObjParser objParser = new ObjParser();
    private List<Triangle> allTriangle = new List<Triangle>();
    private Normalizator normalizator;
    
    public Form1()
    {
        InitializeComponent();
        this.Width = 800;
        this.Height = 600;
        objParser.Parse();
        allTriangle.AddRange(objParser.tetrahedrTriangle);
        // allTriangle.AddRange(objParser.conusTriangle);
         //allTriangle.AddRange(objParser.cylinderTriangle);

        normalizator = new Normalizator(allTriangle);
        
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        // foreach (Triangle triangle in allTriangle)
        // {
        //     Console.WriteLine(triangle.toString());
        // }
        normalizator.AverageNormalVertex();
        normalizator.Print();
        
        
        Vector eye = new Vector(0, 1, 0);
        eye.normalize();
        
        Vector up = new Vector(1, 0, 2);
        up.normalize();
        
        Vector center = new Vector(0, 0, 0);
        center.normalize();

        Matrix lookAt = new Matrix();
        lookAt.LookAt(center, up, eye);
        //
        // Console.WriteLine(center.toString());
        // Console.WriteLine(up.toString());
        // Console.WriteLine(eye.toString());
        //
        // lookAt.toString();
        
        Matrix m1 =  new Matrix(0,0,1,0,0,1,0,0,-1,0,0,0,0,0,0,1);
        Matrix m2 =  new Matrix(1,0,0,2,0,1,0,3,0,0,1,4,0,0,0,1);

        Matrix result = m2 * m1;
        
        //result.toString();
        
        Matrix ortoProj = new Matrix();
        ortoProj.ProjOrto(-5,5,-4,4,-10,10);
    }
}