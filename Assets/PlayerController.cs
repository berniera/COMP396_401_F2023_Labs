using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strenght = 100; //[0, 100]
    public float damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage( float damage) {
        if (strenght > 0) { 
            strenght -= damage;
        }

        if (strenght <= 0) {
            strenght += damage;
            strenght = 0;
            print("You died!");
        }
    }

    public void TakeHealingPotion(float healing_strength)
    {
        if (strenght < 0)
        {
            strenght += healing_strength;
        }

        if (strenght >= 0)
        {
            strenght = 0;
            print("You have max strength!");
        }
    }
}
