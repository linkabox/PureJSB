using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class CollectionItem
{
    public string name;
    public int id;

    public override string ToString()
    {
        return "id: " + id + " name: " + name;
    }
}

public class CollectionTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TestList();
        TestDictionary();
        TestArray();
    }

    void TestList()
    {
        Debug.LogError("##########TestList##########");
        var sb = new StringBuilder();
        var list = new List<CollectionItem>();
        list.Add(new CollectionItem { name = "Petter", id = 1 });
        list.Add(new CollectionItem { name = "Andy", id = 2 });
        list.Add(new CollectionItem { name = "Bob", id = 3 });
        list.Add(new CollectionItem { name = "George", id = 4 });
        list.Add(new CollectionItem { name = "Kelly", id = 5 });
        var target = list.Find(ListFindPredicate);
        Debug.Log("Find Target: " + target.ToString());

        Debug.Log("=======FindIndex=======");
        int targetIndex = list.FindIndex(ListFindPredicate);
        Debug.Log("FindIndex$$Predicate$1: " + targetIndex);
        targetIndex = list.FindIndex(1, ListFindPredicate);
        Debug.Log("FindIndex$$Int32$$Predicate$1: " + targetIndex);
        targetIndex = list.FindIndex(1, 2, ListFindPredicate);
        Debug.Log("FindIndex$$Int32$$Int32$$Predicate$1: " + targetIndex);

        Debug.Log("=======FindLastIndex=======");
        targetIndex = list.FindLastIndex(ListFindPredicate);
        Debug.Log("FindLastIndex$$Predicate$1: " + targetIndex);
        targetIndex = list.FindLastIndex(1, ListFindPredicate);
        Debug.Log("FindLastIndex$$Int32$$Predicate$1: " + targetIndex);
        targetIndex = list.FindLastIndex(1, 2, ListFindPredicate);
        Debug.Log("FindLastIndex$$Int32$$Int32$$Predicate$1: " + targetIndex);

        Debug.Log("=======FindAll=======");
        var targetList = list.FindAll((item) =>
       {
           if (item.name.Contains("e"))
               return true;
           return false;
       });
        foreach (var item in targetList)
        {
            sb.AppendLine(item.ToString());
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======GetRange=======");
        targetList = list.GetRange(1, 3);
        foreach (var item in targetList)
        {
            sb.AppendLine(item.ToString());
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======InsertRange=======");
        var newList = new List<CollectionItem>(list);
        var insertList = new List<CollectionItem>();
        insertList.Add(new CollectionItem { name = "I_ONE", id = 88 });
        insertList.Add(new CollectionItem { name = "I_TWO", id = 89 });
        newList.InsertRange(2, insertList);
        foreach (var item in newList)
        {
            sb.AppendLine(item.ToString());
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======CopyTo=======");
        var array1 = new CollectionItem[10];
        list.CopyTo(array1);
        foreach (var item in array1)
        {
            if (item != null)
                sb.AppendLine(item.ToString());
            else
                sb.AppendLine("item is Null");
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;

        var array2 = new CollectionItem[10];
        list.CopyTo(array2, 2);
        foreach (var item in array2)
        {
            if (item != null)
                sb.AppendLine(item.ToString());
            else
                sb.AppendLine("item is Null");
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;

        var array3 = new CollectionItem[10];
        list.CopyTo(1, array3, 2, 3);
        foreach (var item in array3)
        {
            if (item != null)
                sb.AppendLine(item.ToString());
            else
                sb.AppendLine("item is Null");
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;
    }

    bool ListFindPredicate(CollectionItem item)
    {
        if (item.id == 2)
            return true;
        return false;
    }

    void TestDictionary()
    {
        Debug.LogError("##########TestDictionary##########");
        Dictionary<int, CollectionItem> dic = new Dictionary<int, CollectionItem>(10);
        Debug.Log("=======Add=======");
        dic.Add(1, new CollectionItem { name = "Petter", id = 1 });
        dic.Add(2, new CollectionItem { name = "Andy", id = 2 });
        dic.Add(3, new CollectionItem { name = "Bob", id = 3 });
        dic.Add(4, new CollectionItem { name = "George", id = 4 });
        dic.Add(5, new CollectionItem { name = "Kelly", id = 5 });
        Debug.Log("dic Count: " + dic.Count);

        Debug.Log("=======Remove=======");
        Debug.Log("Remove 1:" + dic.Remove(1));
        Debug.Log("Remove 2:" + dic.Remove(2));
        Debug.Log("Remove 90:" + dic.Remove(90));
        Debug.Log("dic Count: " + dic.Count);

        Debug.Log("=======set_Item=======");
        dic[90] = new CollectionItem { name = "Duck", id = 90 };
        Debug.Log("dic Count: " + dic.Count);

        Debug.Log("=======Clear=======");
        dic.Clear();
        Debug.Log("dic Count: " + dic.Count);
    }

    void TestArray()
    {
        var sb = new StringBuilder();
        Debug.LogError("##########TestArray##########");
        CollectionItem[] array = new CollectionItem[5];
        array[0] = new CollectionItem { name = "Petter", id = 1 };
        array[1] = new CollectionItem { name = "Andy", id = 2 };
        array[2] = new CollectionItem { name = "Bob", id = 3 };
        array[3] = new CollectionItem { name = "George", id = 4 };
        array[4] = new CollectionItem { name = "Kelly", id = 5 };

        Debug.Log("=======CopyTo=======");
        var array1 = new CollectionItem[10];
        array.CopyTo(array1, 1);
        foreach (var item in array1)
        {
            if (item != null)
                sb.AppendLine(item.ToString());
            else
                sb.AppendLine("item is Null");
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Array.Copy=======");
        var array2 = new CollectionItem[10];
        System.Array.Copy(array, array2, 3);
        foreach (var item in array2)
        {
            if (item != null)
                sb.AppendLine(item.ToString());
            else
                sb.AppendLine("item is Null");
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======ConvertAll=======");
        string[] nameArray = System.Array.ConvertAll(array, (item) =>
         {
             return item.name;
         });
        foreach (var item in nameArray)
        {
            if (item != null)
                sb.AppendLine(item.ToString());
            else
                sb.AppendLine("item is Null");
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Int32 Array CopyTo=======");
        var ints1 = new int[8];
        var ints2 = new int[] {-1, 3, 0, 7};
        ints2.CopyTo(ints1, 0);
        sb.Append("ints1:");
        foreach (int i in ints1)
        {
            sb.Append(i + ",");
        }
        sb.AppendLine();
        sb.Append("ints2:");
        foreach (int i in ints2)
        {
            sb.Append(i + ",");
        }
        sb.AppendLine();
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Float32 Array CopyTo=======");
        var floats1 = new float[8];
        var floats2 = new float[] { -1.89f, 3.14f, 0f, 0.4f };
        floats2.CopyTo(floats1, 0);
        sb.Append("floats1:");
        foreach (float i in floats1)
        {
            sb.Append(i + ",");
        }
        sb.AppendLine();
        sb.Append("floats2:");
        foreach (float i in floats2)
        {
            sb.Append(i + ",");
        }
        sb.AppendLine();
        Debug.Log(sb.ToString());
        sb.Length = 0;
    }
}
