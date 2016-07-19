using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;
using System;

//此类做CS and JS bindings


public class LoginMgr
{
    private static LoginMgr _instance;
    public static LoginMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoginMgr();
            }
            return _instance;
        }
    }

     public event Action onHaConnected;
}


//此类转JS


public class TestLogin
{
    private void Dofun()
    {
	    LoginMgr.Instance.onHaConnected += HandleonHaConnected;
    }

    private void HandleonHaConnected()
    {
    }
}


public class EventTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
