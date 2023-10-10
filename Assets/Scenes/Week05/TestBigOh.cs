using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TestBigOh : MonoBehaviour
{

    public int NumberOfElements = 1000;
    public float[] xs;
    public float[] xs_sorted;

    // Start is called before the first frame update
    void Start()
    {
        xs = new float[NumberOfElements];
        xs_sorted = new float[NumberOfElements];
        ArrayList xs_al = new ArrayList();
        //TimeSpan timeSpan = new TimeSpan(0);
        //Populate
        //Populate xs
        //Populate xs_al
        Stopwatch sw = new Stopwatch();
        sw.Start();
        print($"Start populating: n={NumberOfElements}");
        for (int i = 0; i < xs.Length; i++)
        {
            xs[i] = UnityEngine.Random.value;
            xs_al.Add(xs[i]);
        }
        long populatingMSsw = sw.ElapsedMilliseconds;
        print($"populatingMSsw={populatingMSsw}");

        //Sort with arrayList.Sort
        //Measure how long it took for sorting
        
        print($"Start ArrayList.Sort");
        xs_al.Sort();
        print($"End ArrayList.Sort");

        long alMSsw = sw.ElapsedMilliseconds - populatingMSsw;
        print($"alMSsw={alMSsw}");
        
        //Sort with MySortAscending
        //Measure how long it took for sorting
        print($"Start Utils.MySortAscending");
        xs_sorted = Utils.MySortAscending(xs);
        print($"End Utils.MySortAscending");
        long xsMSsw = sw.ElapsedMilliseconds - alMSsw;
        print($"xsMSsw={xsMSsw}");

        print("Start Utils.Contains()");
        bool found = Utils.Contains(xs, 0.343f);
        print("End Utils.Contains(): Does it contain the value? " + found);

        long xsMSsw2 = sw.ElapsedMilliseconds - xsMSsw;
        print($"xsMSsw2={xsMSsw2}");

        print("Start xs_al.Contains()");
        bool found2 = xs_al.Contains(0.343f);
        print("End xs_al.Contains(): Does it contain the value? " + found2);

        long xsMSsw3 = sw.ElapsedMilliseconds - xsMSsw2;
        print($"xsMSsw3={xsMSsw3}");

        print($"My max is {Utils.MyMax(xs)}");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
