using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Collectable
{
    public string name;
}


public class Potion:Collectable
{
    public Potion() 
    { 
        this.name = "Potion1";
    }
}

public class Antidote : Collectable
{
    public Antidote()
    {
        this.name = "Antidote1";
    }
}
//public class Collectable : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
