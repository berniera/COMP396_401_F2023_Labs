using System;
using UnityEngine;

public static class FuzzyFunctions {

    public static float LeftShoulder(float x, float a, float b) {
        float result = 0;
        //Guards
        //  0 <= x <= 1
        if (x < 0 || x > 1) 
            throw new Exception($"x={x} : It should be in [0,1]");        

        // 0 <= a < b <= 1
        if(!(0 <= a && a < b && b <= 1))
            throw new Exception($"a={a}, b={b}: They should obey 0 <= a < b <= 1");        

        if (x <= a)
            result = 1;        
        else if (x <= b) 
            result = (b - x) / (b - a);               

        return result;
    }

    public static float RightShoulder(float x, float a, float b)
    {
        float result = 1;
        if (x < 0 || x > 1)        
            throw new Exception($"x={x} : It should be in [0,1]");        

        if (!(0 <= a && a < b && b <= 1))        
            throw new Exception($"a={a}, b={b}: They should obey 0 <= a < b <= 1");        

        if (x <= a)        
            result = 0;        
        else if (x <= b)        
            result = (x - a) / (b - a);        

        return result;
    }

    public static float Triangular(float x, float a, float b, float c) {
        float res;

        if (x < 0 || x > 1)        
            throw new Exception($"x={x} : It should be in [0,1]");
        
        if (!(0 <= a && a < b && b <= c && c <= 1)) 
            throw new Exception($"a={a}, b={b}, c={c}: They should obey 0 <= a < b <= c <= 1");        

        if (x <= a) 
            res = 0;        
        else if (x <= b && a < x) 
            res = (x - a) / (b - a);        
        else if (x <= c) 
            res = (c - x) / (c - b);        
        else 
            res = 0;
        
        return res;
    }

    public static float Trapesoidal(float x, float a, float b, float c, float d) {
        float res;

        if (x < 0 || x > 1)
            throw new Exception($"x={x} : It should be in [0,1]");

        if (!(0 <= a && a < b && b <= c && c < d && c <= 1))
            throw new Exception($"a={a}, b={b}, c={c}, d={d}: They should obey 0 <= a < b < c <= d ");

        if (x <= a)        
            res = 0;        
        else if (x <= b && a < x)        
            res = (x - a) / (b - a);        
        else if (x <= c && b < x)        
            res = 1;        
        else if (x <= d && c < x)        
            res = 1 + (c - x) / (d - c);        
        else 
            res = 0;        

        return res;
    }

    //Optional
    public static float Crisp(float x, float a) {
        return (x == a) ? 1 : 0;    
    }

    public static float SCurve(float x) {
        if (x < 0 || x > 1)
            throw new Exception($"x={x} : It should be in [0,1]");

        if (0 <= x && x <= 0.5)
            return Mathf.Pow(x, 2) * 2;
        else if (x <= 1 && x > 0.5)
            return 0.5f + Mathf.Sqrt(x - 0.5f) * (0.5f / Mathf.Sqrt(0.5f));
        else
            return 1;
    }
}
