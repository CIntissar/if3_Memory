using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{   
    public void Change(string sceneName)
    {
        PlayerPrefs.SetInt("col",4);
        PlayerPrefs.SetInt("row",3);
        //PlayerPrefs.SetString("level","normal");  -> String méthode
        SceneManager.LoadScene(sceneName);
    }

    public void EasyChange(string sceneName)
    {
        PlayerPrefs.SetInt("col",2);
        PlayerPrefs.SetInt("row",3);
        //PlayerPrefs.SetString("level","easy");  -> String méthode
        SceneManager.LoadScene(sceneName);
    }

    // METHODE THOMAS
    // public void LoadGameScene(int level)
    // {
    //     int row,col;
    //     switch(level)
    //     {
    //         case 1: 
    //         row = 2; 
    //         col = 3;
    //         break;

    //         case 2: 
    //         row = 3; 
    //         col = 4;
    //         break;

    //         default: 
    //         row = 4; 
    //         col = 5;
    //         break;
    //     }
    //     PlayerPrefs.SetInt("col",row);
    //     PlayerPrefs.SetInt("row",col);
    //     SceneManager.LoadScene("GameScene");
    // }

    public void Exit() {
        {
            Application.Quit();
        }
    }
}
