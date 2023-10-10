using UnityEngine;

public class TestFuzzyFunction : MonoBehaviour
{
    [Header("Parameters")]

    [Tooltip("a and b parameters for LeftShoulder")]
    public float a_lsh=0.1f, b_lsh=0.4f;

    [Tooltip("a and b parameters for RightShoulder")]
    public float a_rsh=0.3f, b_rsh=0.7f;

    [Tooltip("a, b, and c parameters for Triangular")]
    public float a_tri=0.15f, b_tri=0.45f, c_tri=0.85f;

    [Tooltip("a, b, c, and d parameters for Trapesoidal")]
    public float a_tra=0.2f, b_tra=0.4f, c_tra=0.55f, d_tra=0.8f;

    [Tooltip("a parameter for Crisp")]
    public float a_crips=0.35f;

    [Tooltip("dx is the interval between x-s of points")]
    public float dx=0.05f;

    [Header("Calculated Quantities")]
    [Tooltip("Number of points")]
    public int NumberOfPoints = System.Convert.ToInt32(1f / 0.05f) + 1;

    //
    public float[] xs;
    public float[] ys_lsh;
    public float[] ys_rsh;
    public float[] ys_tri;
    public float[] ys_tra;
    public float[] ys_crisp;
    public float[] ys_s_curve;

    // Start is called before the first frame update
    void Start()
    {
        xs = new float[NumberOfPoints];
        ys_lsh= new float[NumberOfPoints];
        ys_rsh = new float[NumberOfPoints];
        ys_tri = new float[NumberOfPoints];
        ys_tra = new float[NumberOfPoints];
        ys_crisp = new float[NumberOfPoints];
        ys_s_curve = new float[NumberOfPoints];

        LineRenderer lr_lsh = GameObject.Find("LSh_Visual").GetComponent<LineRenderer>();
        lr_lsh.positionCount = NumberOfPoints;

        LineRenderer lr_rsh = GameObject.Find("RSh_Visual").GetComponent<LineRenderer>();
        lr_rsh.positionCount = NumberOfPoints;

        LineRenderer lr_tri = GameObject.Find("Triangular").GetComponent<LineRenderer>();
        lr_tri.positionCount = NumberOfPoints;

        LineRenderer lr_tra = GameObject.Find("Trapesoidal").GetComponent<LineRenderer>();
        lr_tra.positionCount = NumberOfPoints;

        LineRenderer lr_cri = GameObject.Find("Crisp").GetComponent<LineRenderer>();
        lr_cri.positionCount = NumberOfPoints;

        LineRenderer lr_s_curve = GameObject.Find("SCurve").GetComponent<LineRenderer>();
        lr_s_curve.positionCount = NumberOfPoints;

        //LeftShoulder
        for (int i = 0; i < NumberOfPoints; i++)
        {
            xs[i] = i * dx;
            ys_lsh[i] = FuzzyFunctions.LeftShoulder(xs[i], a_lsh, b_lsh);
            ys_rsh[i] = FuzzyFunctions.RightShoulder(xs[i], a_rsh, b_rsh);
            ys_tri[i] = FuzzyFunctions.Triangular(xs[i], a_tri, b_tri, c_tri);
            ys_tra[i] = FuzzyFunctions.Trapesoidal(xs[i], a_tra, b_tra, c_tra, d_tra);
            ys_crisp[i] = FuzzyFunctions.Crisp(xs[i], a_crips);
            ys_s_curve[i] = FuzzyFunctions.SCurve(xs[i]);

            //For visualizing later you can use LineTrailer component
            lr_lsh.SetPosition(i, new Vector3(xs[i], ys_lsh[i], 0));
            lr_rsh.SetPosition(i, new Vector3(xs[i], ys_rsh[i], 0));
            lr_tri.SetPosition(i, new Vector3(xs[i], ys_tri[i], 0));
            lr_tra.SetPosition(i, new Vector3(xs[i], ys_tra[i], 0));
            lr_cri.SetPosition(i, new Vector3(xs[i], ys_crisp[i], 0));
            lr_s_curve.SetPosition(i, new Vector3(xs[i], ys_s_curve[i], 0));
        }
    }
}