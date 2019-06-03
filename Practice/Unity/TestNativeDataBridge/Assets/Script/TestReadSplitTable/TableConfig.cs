using System;



[Serializable]
public class TableConfig
{
    public int Id = -1;
    public string TableName = "";
    public string NameSpace = "";
    public string Path = "";
    public bool IsSpit = false;

    public override string ToString()
    {
        string val = "Id={0},TableName={1},Path={2},NameSpace={3},IsSpit={4}";
        return string.Format(val, Id, TableName, Path, NameSpace, IsSpit.ToString());
    }
}

