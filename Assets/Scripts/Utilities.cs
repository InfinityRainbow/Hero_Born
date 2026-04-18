using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public  static class Utilities 
{
    // Start is called before the first frame update
    public static int PlayerDeaths = 0;

    public static string UpdateDeathCount(ref int countReference)
    {
        countReference += 1;
        return "next time you'll be at number" + countReference;
    }
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    public  static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
   
    public static bool RestartLevel(int sceneIndex)
    {
        //“Ï≥£¥¶¿Ì
        if (sceneIndex < 0)
        {
            throw new System.ArgumentException("Scene index cannot be negative");
        }




        Debug.Log("Player deaths:" + PlayerDeaths);
        string message = UpdateDeathCount(ref PlayerDeaths);
        Debug.Log("Player deaths:" + PlayerDeaths);
        Debug.Log(message);
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;
        return true;
    }
}
