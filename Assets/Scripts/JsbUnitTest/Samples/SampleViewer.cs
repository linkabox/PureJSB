using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;


public class SampleViewer : MonoBehaviour
{
    static SampleViewer inst = null;
    bool showScenesList = true;
    void Awake()
    {
        // switching level won't destroy JSEngine
        // but we'd like to destroy manually
        if (JSEngine.inst != null)
        {
            Destroy(JSEngine.inst.gameObject);
        }
        if (SampleViewer.inst != null)
        {
            Destroy(gameObject);
        }
        else
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {

    }


    struct stScene
    {
        public string levelName;
        public string showText;
        public stScene(string a, string b = "") 
        { 
            levelName = a; 
            showText = (b.Length > 0 ? b : a);
        }
    }

    stScene[] scenes = new stScene[]
    {
        new stScene("V3Test"),
        new stScene("V3Test_JS"),
        
        new stScene("PerformanceTest1"),
        new stScene("PerformanceTest1_JS"),

        new stScene("ListTest"),
        new stScene("ListTest_JS"),

        new stScene("DictionaryTest"),
        new stScene("DictionaryTest_JS"),

        new stScene("DelegateTest"),
        new stScene("DelegateTest_JS"),

        new stScene("TestCoroutine"),
        new stScene("TestCoroutine_JS"),

        new stScene("JSImpTest1"),
        new stScene("JSImpTest1_JS"),
  
        new stScene("Car"),
        new stScene("Car_JS"),

        new stScene("SerializeSimple"),
        new stScene("SerializeSimple_JS"),

        new stScene("SerializeStruct"),  
        new stScene("SerializeStruct_JS"),  
        
        new stScene("XmlTest"),  
        new stScene("XmlTest_JS"),  
    };

    Vector2 scrollPosition = Vector2.zero;
    void OnGUI()
    {
        if (showScenesList)
        {
            float h = Screen.height / 10;

            scrollPosition = GUI.BeginScrollView(
                new Rect(0, 0, Screen.width, Screen.height),
                scrollPosition,
                new Rect(0, 0, Screen.width, scenes.Length * h), false, false);

            for (var i = 0; i < scenes.Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 4, h * i, Screen.width / 2, h), scenes[i].showText))
                {
                    showScenesList = false;
                    Application.LoadLevel(scenes[i].levelName);
                    break;
                }
            }
            GUI.EndScrollView();
        }
        else
        {
            var w = Screen.width / 10;
            var h = Screen.height / 10;
            if (w < 100) w = 150;
            if (GUI.Button(new Rect(Screen.width - w, 0, w, h), "Back To Scene List"))
            {
                showScenesList = true;
                Application.LoadLevel("SampleViewer");
            }
        }
   }
}