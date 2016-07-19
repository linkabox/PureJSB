using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class IOTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TestPath();
    }

    private void TestPath()
    {
        var sb = new StringBuilder();

        Debug.Log("=======Path Property=======");
        sb.AppendLine("DirectorySeparatorChar: " + Path.DirectorySeparatorChar);
        sb.AppendLine("AltDirectorySeparatorChar: " + Path.AltDirectorySeparatorChar);
        sb.AppendLine("PathSeparator: " + Path.PathSeparator);
        sb.AppendLine("VolumeSeparatorChar: " + Path.VolumeSeparatorChar);
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Path Combine=======");
        sb.AppendLine(Path.Combine("D:/JSB_Test/","testDir/file.txt"));
        sb.AppendLine(Path.Combine("D:/JSB_Test/", "/testDir/file.txt"));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Path GetXXX=======");
        string filePath = Path.Combine(Application.dataPath,"happy.jpg");
        sb.AppendLine("GetDirectoryName: " + Path.GetDirectoryName(filePath));
        sb.AppendLine("GetExtension: " + Path.GetExtension(filePath));
        sb.AppendLine("GetFileName: " + Path.GetFileName(filePath));
        sb.AppendLine("GetFileNameWithoutExtension: " + Path.GetFileNameWithoutExtension(filePath));
        sb.AppendLine("GetPathRoot: " + Path.GetPathRoot(filePath));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        sb.AppendLine("HasExtension: " + Path.HasExtension(filePath));
        sb.AppendLine("ChangeExtension: " + Path.ChangeExtension(filePath,"png"));
        sb.AppendLine("ChangeExtension: " + Path.ChangeExtension(filePath, ".png"));
        Debug.Log(sb.ToString());
        sb.Length = 0;
    }
}
