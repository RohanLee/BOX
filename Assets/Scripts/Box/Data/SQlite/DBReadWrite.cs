using System.IO;
using UnityEngine;

/// <summary>
/// 此脚本实现所有配置数据的实例化 
/// 将ReadWrite数据库放入内存方便调用 
/// 所有数据拥有读写权限
/// 例如用户 角色 包裹等会发生变化的数据
/// </summary>
public class DBReadWrite : MonoBehaviour
{
    public static string DBReadWritePath;

    void Awake()
    {
        //所有平台通用的数据库文件储存地址
        //需要注意的是 项目文件夹名字要和Build Settings/Player Settings里的Product Name（项目名）一致 否则运行报错
        DBReadWritePath = Application.persistentDataPath + "/ReadWrite.db";

        //如已存在该文件则先删除再写入 保证自动更新
        if (File.Exists(DBReadWritePath))
            File.Delete(DBReadWritePath);

        //用 WWW 先从Unity中下载到数据库   
#if UNITY_EDITOR  
        WWW loadDB = new WWW("file://" + Application.dataPath + "/StreamingAssets/" + "ReadWrite.db");   //本地存储地址，以后可以换成网络的
#elif UNITY_ANDROID 
		WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "ReadWrite.db"); 
#elif UNITY_IPHONE
	    WWW loadDB = new WWW(Application.streamingAssetsPath + "/ReadWrite.db"); 
#endif

        while (!loadDB.isDone) { }

        //拷贝至指定存储位置
        File.WriteAllBytes(DBReadWritePath, loadDB.bytes);
#if UNITY_ANDROID
        DataReadWrite db = new DataReadWrite("URI=file:" + DBReadWritePath);
#else
		DataReadWrite db = new DataReadWrite("data source=" + DBReadWritePath); 
#endif
    }
}
