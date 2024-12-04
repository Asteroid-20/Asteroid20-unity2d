using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveManager : MonoBehaviour
{
    public CharacterInfoList cil;
    public PackageItemList pil;

    //读存档
    public void LoadData()
    {
        if(File.Exists(Application.persistentDataPath+"/Saves/Save01.txt"))
        {
            //打开数据流（未做出多存档变量$$$
            FileStream file =File.Open(Application.persistentDataPath+"/Saves/Save01.txt",FileMode.Open);
            //读取数据流 反序列化
            BinaryFormatter bf = new BinaryFormatter();
            string json = (string)bf.Deserialize(file);
            string[] jsonlist = json.Split("20GAME");
            //将读取的json写入ScriptableObject里
            JsonUtility.FromJsonOverwrite(jsonlist[0],cil);
            JsonUtility.FromJsonOverwrite(jsonlist[1],pil);
            //关闭数据流
            file.Close();
        }
        print("读档");


    }

    public void SaveData()
    {
        //检测是否存在文件夹  
        if(!Directory.Exists(Application.persistentDataPath+"/Saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath+"/Saves");
        }
        //打开数据流 创建文件（未做出多存档变量$$$
        FileStream file =File.Create(Application.persistentDataPath+"/Saves/Save01.txt");
        //将读取的pil写入json里
        
        string json =JsonUtility.ToJson(cil)+"20GAME"+JsonUtility.ToJson(pil);
        //写入数据流
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file,json);
        //关闭数据流
        file.Close();
        print("存档");
    }
    // //存读档
    // void SaveData()
    // {
    //     string json = JsonUtility.ToJson(gs);
    //     string path = Application.streamingAssetsPath + "/Save" + SaveNo.ToString() + ".json";
    //     using(StreamWriter sw = new StreamWriter(path))
    //     {
    //     sw.WriteLine(json);//写入
    //     sw.Close();//关闭
    //     sw.Dispose();//释放资源
    //     }
    //     UnityEditor.EditorApplication.isPlaying = false;//保存并退出
    // }
    // void LoadData()
    // {
    //     string json;
    //     string path = Application.streamingAssetsPath + "/Save" + SaveNo.ToString() + ".json";
    //     using(StreamReader sr = new StreamReader (path))
    //     {
    //     json = sr.ReadToEnd();
    //     sr.Close();
    //     }
    //     JsonUtility.FromJson<GameSave>(json);
    // }
}
