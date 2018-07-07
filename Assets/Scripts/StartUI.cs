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
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour 
{
    public void PlayGame()   //点击按钮转换到main场景
    {
        print("开始游戏");
        SceneManager.LoadScene("Main");
    }
    public void ExitGame()
    {
        //退出游戏
        Application.Quit();
    }
	void Start () 
    {
		
	}//end Start
	
	void Update () 
    {
		
	}//end Update
}
