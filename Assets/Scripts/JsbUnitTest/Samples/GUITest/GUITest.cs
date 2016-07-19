using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;
using System.Text;

public class GUITest : MonoBehaviour {

	private Rect consoleRect;
	private Vector3 _consoleScrollPos;
	private bool _hide;
	private Vector3 _logScrollPos;
	private StringBuilder _logBuilder;
	// Use this for initialization
	void Start () {
		consoleRect = new Rect (0,0,Screen.width * 0.8f,Screen.height*0.8f);
		_logBuilder = new StringBuilder ();

		StartCoroutine (RefreshFPS());
		StartCoroutine (RefreshMemory());
	}
	
	// Update is called once per frame
	void Update () {
		accum += Time.timeScale / Time.deltaTime;
		++frames;
	}

	void OnGUI(){
		consoleRect = GUI.Window (0,consoleRect,DrawConsoleWindow,"GameConsole");
	}

	void DrawConsoleWindow(int windowId){
		if (GUILayout.Button ("Hide")) {
			_hide = !_hide;
			consoleRect = _hide ? 
				new Rect(consoleRect.xMin,consoleRect.yMin,100f,50f): 
				new Rect (0,0,Screen.width * 0.8f,Screen.height*0.8f);
		}

		if (!_hide) {
			_consoleScrollPos = GUILayout.BeginScrollView (_consoleScrollPos);
			DrawFPSInfo ();
			DrawMemoryInfo ();
			DrawLogPanel ();
			GUILayout.EndScrollView ();
		}

		GUI.DragWindow ();
	}

	void DrawLogPanel(){
		if (GUILayout.Button ("Clear")) {
			_logBuilder.Length = 0;
		}
		_logScrollPos = GUILayout.BeginScrollView (_logScrollPos,"TextArea",GUILayout.Height(400f));
		GUILayout.Label (_logBuilder.ToString());
		GUILayout.EndScrollView ();
	}

	#region FPS Display
	public float fpsFrequency = 0.5f;
	public int nbDecimal = 1;
	private float accum = 0f;
	private int frames = 0;
	private Color fpsColor = Color.white;
	private string sFPS = "";

	IEnumerator RefreshFPS(){
		while (true) {
			float fps = accum / frames;
			sFPS = fps.ToString ("f" + Mathf.Clamp(nbDecimal,0,10));

			fpsColor = (fps >= 30f) ? Color.green : ((fps > 10f) ? Color.yellow : Color.red);
			accum = 0f;
			frames = 0;

			yield return new WaitForSeconds (fpsFrequency);
		}
	}

	void DrawFPSInfo(){
		GUI.color = fpsColor;
		GUILayout.Label (sFPS +" FPS");
		GUI.color = Color.white;
	}
	#endregion

	#region Memory Display
	public float memoryFrequency = 5f;

	private long _useHeapSize = 0;
	private long _monoUsedSize = 0;
	private long _monoHeapSize = 0;

	private IEnumerator RefreshMemory(){
		while (true) {
			_useHeapSize = Profiler.usedHeapSize;
			_monoUsedSize = Profiler.GetMonoUsedSize ();
			_monoHeapSize = Profiler.GetMonoHeapSize ();

			yield return new WaitForSeconds (memoryFrequency);
		}
	}

	void DrawMemoryInfo(){
		GUILayout.Label ("UseHeapSize: "+ _useHeapSize);
		GUILayout.Label ("MonoUsedSize: "+ _monoUsedSize);
		GUILayout.Label ("MonoHeapSize: "+ _monoHeapSize);
	}
	#endregion
}
