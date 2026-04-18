using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



namespace CustomExtensions
{
    public static class StringExtensions
    {
        public static void FancyDebug(this string str)
        {
            Debug.LogFormat("This string contains {0} charaters. ", str.Length);
        } 
    }
}
//public class CustomExtensions : MonoBehaviour
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
