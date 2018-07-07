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
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour {

    public float Factor ;            //决定一秒钟所跳距离的参数,，修改其来调试跳跃的远近

    public float MaxDistance = 3;   //最大距离
    public GameObject Stage;     //随机盒子

    public GameObject Particle;  //粒子效果

    //public GameObject bg;
    public GameObject Button_Again;
    public GameObject Button_Quit;
    public GameObject Button_Return;
    public GameObject GameOver;

    public Transform Camera;

    public Text ScoreText;   //分数

    public Transform Head;

    public Transform Body;

    Rigidbody rigidbody;    //创建刚体组件
    float startTime;

    GameObject currentStage;   //当前盒子

   

    Collider lastCollisionCollider;  //记录上一次发生碰撞的Collider是什么

    Vector3 cameraRelativePosition;

    int score;

    public static bool isBegin= true;

	void Start () {

        rigidbody = GetComponent<Rigidbody>();      //获取组件
        rigidbody.centerOfMass = Vector3.zero;  //把重心改为最底部

        currentStage = Stage;
        lastCollisionCollider = currentStage.GetComponent<Collider>();   
        SpawnStage(); //生成一个盒子c

        cameraRelativePosition = Camera.position - transform.position; //相机的相对位置

        //bg.SetActive(false);
        Button_Again.SetActive(false);
        GameOver.SetActive(false);
        Button_Quit.SetActive(false);
        Button_Return.SetActive(false);
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))   //按下
        {
            startTime = Time.time;   //按下的时间

            Particle.SetActive(true);
        }
        
        if(Input.GetKeyUp(KeyCode.Space))    //松开
        {
            var elapse = Time.time - startTime;   //按住的时间
            OnJump(elapse);

            Particle.SetActive(false);

            Body.transform.DOScale(0.1f, 1);
            Head.transform.DOLocalMoveY(0.29f, 0.2f);

            currentStage.transform.DOLocalMoveY(0.25f, 0.2f);
            currentStage.transform.DOScale(new Vector3(1, 0.5f, 1), 0.2f);
        }

        if (Input.GetKey(KeyCode.Space))  //按下空格键每一帧都会调用一次
        {
            Body.transform.localScale += new Vector3(1, -1, 1) * 0.05f * Time.deltaTime;
            Head.transform.localPosition += new Vector3(0, -1, 0) * 0.1f * Time.deltaTime;

            currentStage.transform.localScale += new Vector3(0, -1, 0) * 0.15f * Time.deltaTime;
            currentStage.transform.localPosition += new Vector3(0, -1, 0) * 0.15f * Time.deltaTime;

        }
	}

    void OnJump(float elapse)  //根据按住的时间决定跳跃的远近，elapse为按住的时间大小
    {
        rigidbody.AddForce(new Vector3(1, 1, 0) * elapse * Factor, ForceMode.Impulse);
        //给它一个力，让它朝斜上方跳跃 vertor向量  瞬间的作用力
    }

    void SpawnStage()   //生成盒子
    {
        var stage = Instantiate(Stage);
        //生成盒子的位置是什么
        /*stage.transform.position = currentStage.transform.position + 
            new Vector3(MaxDistance, 0, 0);*/  //调试MaxDistance来寻找生成盒子的位置范围
        stage.transform.position = currentStage.transform.position + 
            new Vector3(Random.Range(1.1f, MaxDistance), 0, 0);
        //
    }

    void OnCollisionEnter(Collision collision)  //刚体里的一个事件 碰撞
    {
        Debug.Log(collision.gameObject.name); //打印出刚体碰撞的物体的名字
        if(collision.gameObject.name.Contains("Stage") && collision.collider!=lastCollisionCollider)
            //若还是跳到之前的台子上则不生成新的
        {
            lastCollisionCollider = collision.collider; //赋值为当前的collider
            currentStage = collision.gameObject;
            SpawnStage();
            MoveCamera();

            score++;
            ScoreText.text = score.ToString();
        }

        if(collision.gameObject.name == "Ground")
        {
            //本局游戏结束，重新开始
            //SceneManager.LoadScene(0);
            isBegin = false;
            //bg.SetActive(true);
            Button_Again.SetActive(true);
            GameOver.SetActive(true);
            Button_Quit.SetActive(true);
            Button_Return.SetActive(true);
        }
    }
    void MoveCamera()
    {
        Camera.DOMove(transform.position + cameraRelativePosition, 1);//动画时长为1
        // Camera.position = transform.position + _cameraRelativePosition;
    }
}
