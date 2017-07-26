using System.IO;
using UnityEngine;

/// <summary>
/// 此脚本实现所有配置数据的实例化 
/// 将ReadOnly数据库放入内存方便调用 
/// 所有数据只读
/// 例如物品 成就 任务等无需在游戏内修改的表
/// </summary>
public class DBReadOnly : MonoBehaviour
{
    private string dbReadOnlyPath;

    void Awake()
    {
        //所有平台通用的数据库文件储存地址
        //需要注意的是 项目文件夹名字要和Build Settings/Player Settings里的Product Name（项目名）一致 否则运行报错
        dbReadOnlyPath = Application.persistentDataPath + "/ReadOnly.db";

        //如已存在该文件则先删除再写入 保证自动更新
        if (File.Exists(dbReadOnlyPath))
            File.Delete(dbReadOnlyPath);

        //用 WWW 先从Unity中下载到数据库   
#if UNITY_EDITOR  
        WWW loadDB = new WWW("file://" + Application.dataPath + "/StreamingAssets/" + "ReadOnly.db");   //本地存储地址，以后可以换成网络的
#elif UNITY_ANDROID 
		WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "ReadOnly.db"); 
#elif UNITY_IPHONE
	    WWW loadDB = new WWW(Application.streamingAssetsPath + "/ReadOnly.db"); 
#endif

        while (!loadDB.isDone) { }

        //拷贝至指定存储位置
        File.WriteAllBytes(dbReadOnlyPath, loadDB.bytes);
#if UNITY_ANDROID
        DataReadOnly db = new DataReadOnly("URI=file:" + dbReadOnlyPath);
#else
		DataReadOnly db = new DataReadOnly("data source=" + dbReadOnlyPath); 
#endif
    }
}
