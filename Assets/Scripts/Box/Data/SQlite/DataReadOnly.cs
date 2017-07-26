using Mono.Data.Sqlite;
using System.Collections.Generic;

/// <summary>
/// 将只读数据库中的所有表存入字典
/// </summary>
public class DataReadOnly
{
    public static Dictionary<int, int> dictionary;//建立跟表对应的字典 有几个就建几个

    public DataReadOnly(string dbPath)
    {
        ReadOneTable(dbPath);//将不同表的读取方法放入这里
    }

    public DataReadOnly()
    {

    }

    //某张表的读取方法示例
    void ReadOneTable(string dbPath)
    {
        DBAccess db = new DBAccess(dbPath);//打开数据库连接
        SqliteDataReader reader = db.ReadEntireTable("item");//读取指定的表
        while (reader.Read())
        {
            /*
            Clas clas = new Clas();  类实例化
            clas.ID = reader.GetInt32(reader.GetOrdinal("ID"));  int型读取规范
            clas.name = reader["name"].ToString();  string型读取规范


            dictionary[clas.ID] = clas;  最后向对应字典增加实力 同时确定索引为哪个字段
            */
        }
        db.CloseDB();//关闭数据库连接
    }
}
