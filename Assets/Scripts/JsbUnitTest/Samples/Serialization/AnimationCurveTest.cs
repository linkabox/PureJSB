using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;


public class AnimationCurveTest : MonoBehaviour
{
	public AnimationCurve curve;
	void Start () 
	{
		string s = "";
		s += "length " + curve.length + "\n";
		s += "prevWrapMode " + curve.preWrapMode + "\n";
		s += "postWrapMode " + curve.postWrapMode + "\n";

		s += "\n";

		for (float f = -3f; f < 3f; f += 0.4f) {
			s += f.ToString() + " = " + curve.Evaluate(f) + "\n";
		}

		print (s);
	}

	void Update () {
	
	}
}
