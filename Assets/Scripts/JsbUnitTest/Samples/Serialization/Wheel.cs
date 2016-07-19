using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;


public class Wheel : MonoBehaviour
{


    public enum Pos
    {
        LeftFront,
        RightFront,
        LeftBack,
        RightBack
    }
    public Pos ePos;
    float speed = 1f;
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    float accum = 0f;
	void Update () {
        accum += Time.deltaTime;
        if (accum > speed)
        {
            switch (ePos)
            {
                case Pos.LeftFront:
                    Debug.Log("LeftFront go..");
                    break;
                case Pos.RightFront:
                    Debug.Log("RightFront go..");
                    break;
                case Pos.LeftBack:
                    Debug.Log("LeftBack go..");
                    break;
                case Pos.RightBack:
                    Debug.Log("RightBack go..");
                    break;
            }
            accum = 0;
        }
	}
}
