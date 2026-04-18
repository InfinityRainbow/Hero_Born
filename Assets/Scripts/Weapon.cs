using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct Weapon
{
    public string name;
    public int damage;

    public Weapon(string name, int damage)
    {
        this.name = name;
        this.damage = damage;
    }
    public void PrintWeaponStats()//Î´µ÷ÓÃ
    {
        Debug.LogFormat("Weapon:{0} - {1} DMG", this.name, this.damage);
    }
}

[Serializable]
public class WeaponShop
{
    public List<Weapon> inventory;
}

//public class Weapon : MonoBehaviour
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
