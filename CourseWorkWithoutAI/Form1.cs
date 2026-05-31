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

    private Bitmap bitmap;

    private Renderer render;
    
    private float cameraAngle = 0;
    private float cameraDistance = 12;
    private float cameraPitch = 20;
    
    
    private Matrix MT = new Matrix();
    
    public Form1()
    {
        InitializeComponent();
        DoubleBuffered = true;
        

        bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
        
        
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
        
        this.KeyPreview = true;
        this.KeyDown += Form1_KeyDown;
        
    }
    
    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:   cameraAngle -= 2.5f; break;
            case Keys.Right:  cameraAngle += 2.5f; break;
            case Keys.Up:     cameraPitch += 2.5f; break;
            case Keys.Down:   cameraPitch -= 2.5f; break;
            case Keys.Add:    cameraDistance -= 0.5f; break;
            case Keys.Subtract: cameraDistance += 0.5f; break;
        }
        cameraPitch = Math.Max(-89, Math.Min(89, cameraPitch));
        cameraDistance = Math.Max(5, Math.Min(20, cameraDistance));
        
        
        Invalidate();
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

        Vector lightDir = new Vector(1, -1, -1);
        lightDir.normalize();
        lambertLighting = new LambertLighting(ambient,k_d,lightDir,allTriangle);
        lambertLighting.defIllumVertex();
        
        double radius = 12;
        
        Vector center = new Vector(0, 0, -2.5);
        double radAngle = cameraAngle * Math.PI / 180.0;
        double radPitch = cameraPitch * Math.PI / 180.0;
        
        Vector eye = new Vector(
            cameraDistance * Math.Cos(radAngle) * Math.Cos(radPitch),
            cameraDistance * Math.Sin(radPitch),
            cameraDistance * Math.Sin(radAngle) * Math.Cos(radPitch)
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