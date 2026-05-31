using CourseWorkWithoutAI.lighting;
using CourseWorkWithoutAI.parser;
using CourseWorkWithoutAI.polygon;
using CourseWorkWithoutAI.rendering;
using CourseWorkWithoutAI.structures;

namespace CourseWorkWithoutAI;

public partial class Form1 : Form
{
    private ObjParser objParser = new ObjParser();
    private List<Triangle> allTriangle = new List<Triangle>();
    private Normalizator normalizator;
    private LambertLighting lambertLighting;
    private double ambient;
    private double k_d;
    
    private Vector V1;
    private Vector V2;
    private Vector V3;
    
    private float angle = 0;
    
    private double[,] zbuffer;
    //private Color[,] pixelBuffer;
    private Bitmap bitmap;

    private Renderer render;
    
    
    private Matrix MT = new Matrix();
    
    public Form1()
    {
        InitializeComponent();
        DoubleBuffered = true;
        

        bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
        
        // Инициализация Z-буфера
        zbuffer = new double[ClientSize.Width, ClientSize.Height];
        for (int i = 0; i < zbuffer.GetLength(0); i++)
            for (int j = 0; j < zbuffer.GetLength(1); j++)
                zbuffer[i, j] = -1000000.0;
        
        ambient = 0.3;
        k_d = 1.2;
        objParser.Parse();
        allTriangle.AddRange(objParser.tetrahedrTriangle);
        allTriangle.AddRange(objParser.conusTriangle);
        allTriangle.AddRange(objParser.cylinderTriangle);

        normalizator = new Normalizator(allTriangle);
        normalizator.AverageNormalVertex();
        
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        timer.Interval = 40;
        timer.Tick += (s, e) => { angle += 3f; Invalidate(); };
        timer.Start();
    }
    
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
            return;

        bitmap?.Dispose();

        bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
        zbuffer = new double[ClientSize.Width, ClientSize.Height];

        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.Clear(Color.Gray);
        }
        
        for (int i = 0; i < zbuffer.GetLength(0); i++)
            for (int j = 0; j < zbuffer.GetLength(1); j++)
                zbuffer[i, j] = -1000000.0;
        // foreach (Triangle triangle in allTriangle)
        // {
        //     Console.WriteLine(triangle.toString());
        // }
        
        
        
       // normalizator.Print();

        Vector lightDir = new Vector(2, -1, 0);
        lightDir.normalize();
        lambertLighting = new LambertLighting(ambient,k_d,lightDir,allTriangle);
        lambertLighting.defIllumVertex();
        
        double radius = 12;
        double rad = angle * Math.PI / 180.0;
        
        Vector center = new Vector(0, 0, -2.5);
        
        Vector eye = new Vector(
            radius * Math.Cos(rad),
            4,
            radius * Math.Sin(rad)
            );
        
        
        Vector up = new Vector(0, 1, 0);
        

        Matrix scale = new Matrix();
        scale.Scale(1, 1, 1, 1);
        
        Matrix model = scale; 
        
        
        

        Matrix lookAt = new Matrix();
        lookAt.LookAt(center, up, eye);
       
        Matrix ortoProj = new Matrix();
        ortoProj.ProjOrto(-5, 5, -3.75, 3.75, -10, 10);
        
        Matrix viewport = new Matrix();
        viewport.ViewPort(Width, Height, 0, 0, 255);

        MT =  viewport * ortoProj * lookAt * model;

        render = new Renderer(bitmap, zbuffer);
        
        

        foreach (var triangle in  allTriangle)
        {
            V1 = MT * triangle.getV1();
            V2 = MT * triangle.getV2();
            V3 = MT * triangle.getV3();

     
            
            render.resterizeTriangle(V1, V2, V3, triangle.getIntensity1(),triangle.getIntensity2(),triangle.getIntensity3(),triangle.getColor());
            

        }
        
        
        e.Graphics.DrawImage(bitmap, 0, 0);

    }
}