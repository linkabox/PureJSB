using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;
using System.Text;


public class V3Test : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    float elapsed = 0;
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 1f)
        {
            elapsed = 0f;
            var sb = new StringBuilder();
            elapsed = 0f;
            Vector3 v = new Vector3(2, 3, 6);
            Vector3 w = new Vector3(7, 23, 1);

            var n = v.normalized;
            object[] arr = new object[] { n.x, n.y, n.z };
            Debug.Log(sb.AppendFormat("v.normalized = ({0}, {1}, {2})", arr).ToString());

            sb.Remove(0, sb.Length);
            var cross = Vector3.Cross(v, w);
            arr = new object[] { cross.x, cross.y, cross.z };
            Debug.Log(sb.AppendFormat("Cross(v, w) = ({0}, {1}, {2})", arr).ToString());

            Debug.Log("v.magnitude = " + v.magnitude);
            Debug.Log("w.magnitude = " + w.magnitude);
            Debug.Log("Dot(v, w) = " + Vector3.Dot(v, w));
            Debug.Log("Angle(v, w) = " + Vector3.Angle(v, w));

            var proj = Vector3.Project(v, w);
            Debug.Log("Project(v,w) = " + proj.ToString());

            v.Normalize();
            w.Normalize();
            Debug.Log("normalized v = " + v.ToString());
            Debug.Log("normalized w = " + w.ToString());
        }
	}
}
