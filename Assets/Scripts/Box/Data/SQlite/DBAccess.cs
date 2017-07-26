using Mono.Data.Sqlite;
using System;
using UnityEngine;

/// <summary>
/// 表的操作方法封装  
/// 注：遇到string亦即表中数据类型为TEXT的值在写入前要前后用单引号括起来否则出错
/// </summary>
public class DBAccess
{
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader dbReader;

    public DBAccess(string path)
    {
        OpenDB(path);
    }

    public DBAccess()
    {
    }

    #region 基础操作 ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// 打开数据库连接
    /// </summary>
    /// <param name="path">数据库路径</param>
    public void OpenDB(string path)
    {
        try
        {
            dbConnection = new SqliteConnection(path);
            dbConnection.Open();
            Debug.Log("数据库连接成功！！！");
        }
        catch (Exception e)
        {
            string ex = e.ToString();
            Debug.Log(ex);
            if (dbConnection != null && dbConnection.State != System.Data.ConnectionState.Closed)
            {
                dbConnection.Close();
                dbConnection = null;
            }
        }
    }

    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void CloseDB()
    {
        if (dbCommand != null)
        {
            dbCommand.Dispose();
        }

        dbCommand = null;
        if (dbReader != null)
        {
            dbReader.Dispose();
        }

        dbReader = null;
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
        Debug.Log("已从数据库断开！！！");
    }

    #endregion


    #region 增 ///////////////////////////////////////////////////////////////////

    /// <summary>  
    /// 创建表  
    /// </summary>  
    /// <param name="tableName">表名</param>  
    /// <param name="propertys">字段名</param>  
    /// <param name="types">字段对应的数据类型</param>  
    /// <returns></returns>  
    public SqliteDataReader CreateTable(string name, string[] propertys, string[] types)
    {
        if (propertys.Length != types.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }
        string query = "CREATE TABLE " + name + " (" + propertys[0] + " " + types[0];

        for (int i = 1; i < propertys.Length; ++i)
        {
            query += ", " + propertys[i] + " " + types[i];
        }
        query += ")";

        return ExecuteQuery(query);
    }

    /// <summary>
    /// 增加一条数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="values">所有字段的值组成的字符串组</param>
    /// <returns></returns>
    public SqliteDataReader AddData(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 增加一条数据 但部分属性值为默认
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="propertys">字段名</param>
    /// <param name="values">对应字段名的值</param>
    /// <returns></returns>
    public SqliteDataReader AddDataWithPartValue(string tableName, string[] propertys, string[] values)
    {
        if (propertys.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }

        string query = "INSERT INTO " + tableName + "(" + propertys[0];
        for (int i = 1; i < propertys.Length; ++i)
        {
            query += ", " + propertys[i];
        }
        query += ") VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";

        return ExecuteQuery(query);
    }

    #endregion


    #region 改 ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// 修改数据  修改多个字段的值，查找条件是selectkey =值的那条
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="propertys">要修改的值的字段的名 可以多个</param>
    /// <param name="values">字段名对应的要修改的值 对应多个</param>
    /// <param name="keyName">字段名 一般是ID</param>
    /// <param name="keyValue">字段对应的值 一般就是ID值</param>
    /// <returns></returns>
    public SqliteDataReader UpdateData(string tableName, string[] propertys, string[] values, string keyName, string keyValue)
    {
        if (propertys.Length != values.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length");
        }

        string query = "UPDATE " + tableName + " SET " + propertys[0] + " = " + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + propertys[i] + " =" + values[i];
        }
        query += " WHERE " + keyName + " = " + keyValue + " ";

        return ExecuteQuery(query);
    }

    #endregion


    #region 删 ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// 删除数据 多个字段和值对应得上的都删除掉
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="propertys">键的字段名 一般是"ID"</param>
    /// <param name="values">键的值</param>
    /// <returns></returns>
    public SqliteDataReader DeleteData(string tableName, string[] propertys, string[] values)
    {
        //当字段名称和字段数值不对应时引发异常
        if (propertys.Length != values.Length)
        {
            throw new SqliteException("cols.Length!=colsvalues.Length ");
        }

        string query = "DELETE FROM " + tableName + " WHERE " + propertys[0] + " = " + values[0];

        for (int i = 1; i < values.Length; ++i)
        {

            query += " or " + propertys[i] + " = " + values[i];              // or：表示或者   and：表示并且 
        }
        Debug.Log(query);
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 整表删除
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = "DELETE FROM " + tableName;
        return ExecuteQuery(query);
    }

    #endregion


    #region 查 ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// 整表读取
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public SqliteDataReader ReadEntireTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 指定范围读表 暂未发现用途
    /// </summary>
    /// <param name="tableName">Table name.</param>
    /// <param name="items">Items.</param> 取的内容，可以多个
    /// <param name="propertys">字段名 例如ID</param>
    /// <param name="operations">运算符，一般写=</param>
    /// <param name="values">字段对应的值</param>
    /// <returns></returns>
    public SqliteDataReader ReadTable(string tableName, string[] items, string[] propertys, string[] operations, string[] values)
    {
        if (propertys.Length != operations.Length || operations.Length != values.Length)
        {
            throw new SqliteException("colNames.Length != operations.Length != colValues.Length");
        }

        string queryString = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            queryString += ", " + items[i];
        }
        queryString += " FROM " + tableName + " WHERE " + propertys[0] + " " + operations[0] + " " + values[0];
        for (int i = 0; i < propertys.Length; i++)
        {
            queryString += " AND " + propertys[i] + " " + operations[i] + " " + values[0] + " ";
        }

        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 数据库查询
    /// </summary>
    /// <param name="sqlQuery"></param>
    /// <returns></returns>
    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        dbReader = dbCommand.ExecuteReader();
        return dbReader;
    }

    /// <summary>
    /// 查询字段自否存在
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="property">要查的字段名</param>
    /// <returns></returns>
    public bool ExamineTableColumn(string tableName, string property)
    {
        string queryString = string.Format("SELECT {0} FROM {1}", property, tableName);
        SqliteDataReader reader = ExecuteQuery(queryString);
        if (reader == null)
            return false;
        else
            return true;
    }

    #endregion


    #region 封装使用 ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// 增加一条数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="values">所有字段的值组成的字符串组</param>
    public static void DBAdd(string tableName, string[] values)
    {
        DBAccess db = new DBAccess(DBReadWrite.DBReadWritePath);
        db.AddData(tableName, values);
        db.CloseDB();
    }

    /// <summary>
    /// 修改数据  修改多个字段的值，查找条件是selectkey =值的那条
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="propertys">要修改的值的字段的名 可以多个</param>
    /// <param name="values">字段名对应的要修改的值 对应多个</param>
    /// <param name="keyName">字段名 一般是ID</param>
    /// <param name="keyValue">字段对应的值 一般就是ID值</param>
    public static void DBUpdate(string tableName, string[] propertys, string[] values, string keyName, string keyValue)
    {
        DBAccess db = new DBAccess(DBReadWrite.DBReadWritePath);
        db.UpdateData(tableName, propertys, values, keyName, keyValue);
        db.CloseDB();
    }

    /// <summary>
    /// 删除数据 多个字段和值对应得上的都删除掉
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="propertys">键的字段名 一般是"ID"</param>
    /// <param name="values">键的值</param>
    public static void DBDelete(string tableName, string[] propertys, string[] values)
    {
        DBAccess db = new DBAccess(DBReadWrite.DBReadWritePath);
        db.DeleteData(tableName, propertys, values);
        db.CloseDB();
    }

    #endregion
}