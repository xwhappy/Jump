/**************************
 * Title：“”
 * Used By：
 * Function：
 * -
 * Author：v.
 * Date：
 * Version：1.0
 * Recording：
 **************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour 
{
    
    //public Text ScoreText;
    public float time;
    //public GameObject Player;
    void Start()
    {

    }//end_Start

    /*void Update()
    {
        if (Player.isExit)
        {
            //score++;
            ScoreText.text = score.ToString();
        }
    }//end_Update*/

    public void OnAgainClick()
    {
        Application.LoadLevel("Main");
        Time.timeScale = 1;
        Player.isBegin = true;
    }
    public void OnQuitClick()
    {
        Application.Quit();
    }
    public void OnReturnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        Player.isBegin = true;
    }
}
