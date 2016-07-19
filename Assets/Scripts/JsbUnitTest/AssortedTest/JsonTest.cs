using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        TestJson();
    }

    public void TestJson()
    {
        //PerTest.RefObject refObj = new PerTest.RefObject();
        //refObj.x = Int32.MaxValue;
        //refObj.y = Int32.MinValue;
        //refObj.String = "hello";

        //string json = JsonMapper.ToJson(refObj);
        //Debug.LogError(json);
        //var jsonRefObj = JsonMapper.ToObject<PerTest.RefObject>(json);
        //Debug.LogError(jsonRefObj.x + "|" + jsonRefObj.y + "|" + jsonRefObj.String);
        //jsonRefObj.PrintLog();

        //EntityList entityList = new EntityList();
        List<Entity> entityList = new List<Entity>();
        Entity entity = new Entity();
        for (int i = 0; i < entity.map.Count; i++)
        {
            Debug.LogError(entity.map[i]);
        }
        entity.id = 20160217;
        entity.name = "bozo";

        entity.coordinate = new Vector2(-1f, 3.14f);
        entity.pos = new Vector3(-16.1f, 0.0f, 15.8f);
        Debug.LogError(entity.pos.GetHashCode());
        Debug.LogError(Vector3.MoveTowards(entity.pos, Vector3.zero, 2f));
        entity.v4 = new Vector4(-1f, 2.12f, 0f, 3f);
        entity.quaternion = new Quaternion(-21f, 31.48f, 77f, 1f);
        entity.color32 = Color.green;
        entity.color = Color.cyan;
        entity.bounds = new Bounds(entity.pos, Vector3.one * 3);
        entity.rect = new Rect(0f, 0f, 800f, 600f);
        entity.rectOffset = new RectOffset(10, 20, 30, -40);

        entity.defaultAction = new EntityAction
        {
            id = 6666,
            content = "Happy Day"
        };

        entity.simpleList.Add("abc");
        entity.simpleList.Add("efg");
        entity.simpleList.Add("ko");

        entity.simpleDic.Add("4001", "What's up");
        entity.simpleDic.Add("4002", "Good bye");
        var action = new EntityAction
        {
            id = 1,
            content = "Hello World"
        };
        //Add方式添加
        entity.complexDic.Add(action.id.ToString(), action);
        entity.complexList.Add(action);

        //[]方式添加
        action = new EntityAction
        {
            id = 2,
            content = "Help me"
        };
        entity.complexDic[action.id.ToString()] = action;
        entity.complexList.Add(action);

        //entityList.list.Add(entity);
        //entityList.list.Add(entity);
        entityList.Add(entity);
        entityList.Add(entity);
        string entityJson = JsHelper.ToJson(entity, true);
        Debug.LogError(entityJson);
        string listJson = JsHelper.ToJson(entityList);
        Debug.LogError(listJson);

        Debug.LogError("============Log jsonDic============");
        var jsonDic = JsHelper.ToObject<Dictionary<string, object>>(JsHelper.ToJson(Vector3.down));
        string log = "";
        foreach (var item in jsonDic)
        {
            log += "k:" + item.Key + " v:" + item.Value + "\n";
        }
        Debug.LogError(log);

        Debug.LogError("============Log jsonEntity============");
        Entity jsonEntity = JsHelper.ToObject<Entity>(entityJson);
        jsonEntity.PrintLog();
        Debug.LogError("============Log jsonEntityList============");
        List<Entity> jsonEntityList = JsHelper.ToCollection<List<Entity>, Entity>(listJson);

        foreach (var item in jsonEntityList)
        {
            PrintEntityInfo(item);
        }
    }

    private void PrintEntityInfo(Entity jsonEntity)
    {
        var sb = new StringBuilder();
        sb.AppendLine("name: " + jsonEntity.name);
        sb.AppendLine("id: " + jsonEntity.id);

        sb.AppendLine("coordinate: " + jsonEntity.coordinate);
        sb.AppendLine("pos: " + jsonEntity.pos);
        sb.AppendLine("v4: " + jsonEntity.v4);
        sb.AppendLine("quaternion: " + jsonEntity.quaternion);
        sb.AppendLine("color32: " + jsonEntity.color32);
        sb.AppendLine("color: " + jsonEntity.color);
        sb.AppendLine("bounds: " + jsonEntity.bounds);
        sb.AppendLine("rect: " + jsonEntity.rect);
        sb.AppendLine("rectOffset: " + jsonEntity.rectOffset);

        sb.AppendLine("defaultAction:" + jsonEntity.defaultAction.id + " | " + jsonEntity.defaultAction.content);
        sb.AppendLine("simpleList: " + jsonEntity.simpleList.Count);
        foreach (var item in jsonEntity.simpleList)
        {
            sb.AppendLine(item);
        }
        sb.AppendLine("complexList: " + jsonEntity.complexList.Count);
        foreach (var item in jsonEntity.complexList)
        {
            sb.AppendLine(item.content);
        }
        sb.AppendLine("simpleDic: " + jsonEntity.simpleDic.Count);
        foreach (var item in jsonEntity.simpleDic)
        {
            sb.AppendLine(item.Value);
        }
        sb.AppendLine("complexDic: " + jsonEntity.complexDic.Count);
        foreach (var item in jsonEntity.complexDic)
        {
            sb.AppendLine(item.Value.content);
        }
        Debug.LogError(sb.ToString());
        jsonEntity.PrintLog();
    }

    #region Nested type: Entity

    public class Entity
    {
        public Bounds bounds;
        public Color color;
        public Color32 color32;
        public Dictionary<string, EntityAction> complexDic = new Dictionary<string, EntityAction>();
        public List<EntityAction> complexList = new List<EntityAction>();

        public Vector2 coordinate;

        public EntityAction defaultAction;
        public int id;
        public List<string> map = new List<string> { "abc", "hello", "echo" };
        public string name;
        public Vector3 pos;
        public Quaternion quaternion;
        public Rect rect;
        public RectOffset rectOffset;

        public Dictionary<string, string> simpleDic = new Dictionary<string, string>();
        public List<string> simpleList = new List<string>();
        public Vector4 v4;

        public void PrintLog()
        {
            Debug.LogError("id: " + id + "\nname: " + name);
        }
    }

    #endregion

    #region Nested type: EntityAction

    public class EntityAction
    {
        public string content;
        public int id;

        public string DumpInfo()
        {
            return "id: " + id + " content: " + content;
        }
    }

    #endregion

    #region Nested type: EntityList

    public class EntityList
    {
        public List<Entity> list = new List<Entity>();
    }

    #endregion
}