using UnityEngine;

public static class Utils {
    public static bool EnemyCloseEnough(Vector3 thisPos, Vector3 enemyPos, float distance) {
        return Vector3.Distance(thisPos, enemyPos) <= distance ? true : false;                    
    }

    public static bool EnemyInFront(Vector3 thisPos, Vector3 enemyPos, Vector3 thisForward, float cosGuardFOVover2InRAD) {        
        Vector3 enemyHeading = (enemyPos - thisPos).normalized;

        float cosAngle = Vector3.Dot(enemyHeading, thisForward);
        return cosAngle > cosGuardFOVover2InRAD ? true : false;
    }

    public static bool SenseEnemy(Vector3 thisPos, Vector3 enemyPos, Vector3 thisForward, float distance, float cutoff) {
        return EnemyInFront(thisPos, enemyPos, thisForward, distance)
            && EnemyCloseEnough(thisPos, enemyPos, cutoff) ? true : false;
    }

    public static float[] MySortAscending(float[] xs)
    {
        //in ascending order...
        float[] res = new float[xs.Length];
        //Naive sorting
        //x0 x1 ... xn-1
        //start with x0,
        //compare each other value x1... xn-1 with x0; if it is less, swap them, else continue
        //go to x1, repeat..

        for(int i = 0; i < xs.Length-1; i++) { 
            for(int j = i + 1; j < xs.Length; j++)            
                if (xs[j] < xs[i]) {
                    //swap xs[i] with xs[j]
                    float temp = xs[i];
                    xs[i] = xs[j];
                    xs[j] = temp;
                }          
        }

        //Analysis:
        //Let n=xs.Length;
        //Then it can be proved that is an O(n^2) algorith, in the worst case
        //Proof:
        //Outer cycle run n-1 times
        //n-1, n-1, n-3,..., n-(n-1)=1
        //(n-1)+(n-2)+...+1=(n-1)*(n) / 2 = n^2 / 2 -n/2
        //f(n) = n^2/2-n/2
        //What is O(f(n)) = O(n^2)
        //
        //n/2, n/2 elements are not sorted O(n^2/4)= O(n^2)
        return res;
    }

    public static float MyMax(float[] xs) {
        if (xs.Length == 0) 
            throw new System.Exception("xs has no elements");        

        if(xs.Length == 1)       
            return xs[0];        

        float res = xs[0];

        for (int i = 0; i < xs.Length; i++) 
            if (xs[i] > res) 
                res = xs[i];                         

        return res;
    } 
    
    public static bool Contains(float[] xs, float x) {
        for(int i = 0; i < xs.Length; i++)        
            if (xs[i] == x)               
                return true;                    

        return false;
    }
}
