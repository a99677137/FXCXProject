﻿using System.IO;
using UnityEngine;

public class TableItemVO
{
    private int id = -1;
    public int Id
    {
        get { return id; }
    }
    private string name;
    public string Name {
        get { return name; }
    }
    private string path = "";
    public string Path
    {
        get { return path; }
    }
    private string absolutePath = "";
    public string AbsolutePath
    {
        get { return absolutePath; }
    }
    private bool isCSharpTable = false;
    public bool IsCSharpTable
    {
        get { return isCSharpTable; }
    }
    private bool isLuaTable = false;
    public bool IsLuaTable
    {
        get { return isLuaTable; }
    }
    private bool isSplit = false;
    public bool IsSplit
    {
        get { return isSplit; }
    }
    private bool isImmediately = false;
    public bool IsImmediately
    {
        get { return isImmediately; }
    }
    private string nameSpace = "";
    public string NameSpace
    {
        get { return nameSpace; }
    }

    public TableItemVO(int _id,string _name,string _path,string _absolutPath, string _nameSpace, bool _isLua, bool _isCsharp, bool _isSplit, bool _isImmediately) {
        id = _id;
        name = _name;
        path = _path;
        absolutePath = _absolutPath;
        isCSharpTable = _isCsharp;
        isLuaTable = _isLua;
        isSplit = _isSplit;
        isImmediately = _isImmediately;
        nameSpace = _nameSpace;
        Debug.Log("Init TableItem! path = " + path);
    }
}
