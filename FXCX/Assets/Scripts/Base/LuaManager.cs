using Game.Tools.CommonTools;
using LuaInterface;
using System;
using System.Threading;
using UnityEngine;

public class LuaManager : Singleton<LuaManager>
{
    private LuaState _lua;
    private bool _hasFinish = false;
    private bool _binderFinish = false;
    private string LuaRoot = "LuaRoot";

    private Thread initLuaBinder;


    public void Init() {
        GameLog.Debug("---------------------------LuaManager:Init--------------------------------------------");
        _lua = new LuaState();
        initLuaBinder = new Thread(StartLuaBinder);
        initLuaBinder.Start();
    }

    private void StartLuaBinder() {
        GameLog.Debug("==============================StartLuaBinder");
        //var startBinderTime = Time.realtimeSinceStartup;//get_realtimeSinceStartup can only be called from the main thread.
        DateTime startBinderTime = System.DateTime.Now;
        LuaBinder.Bind(_lua);
        //var endtBinderTime = Time.realtimeSinceStartup;
        DateTime endtBinderTime = System.DateTime.Now;
        TimeSpan delta = endtBinderTime - startBinderTime;
        GameLog.Debug("==============================LuaBinderTimeCost = {0}", delta.TotalMilliseconds.ToString());
        _binderFinish = true;
    }

    public void Start() {
        if (!_binderFinish || _hasFinish) {
            GameLog.Error("ERROR!!!---------------LuaManager:Start-----------------------(!_binderFinish || _hasFinish)");
            return;
        }
        GameLog.Debug("-----------LuaManager:Start-----------------------LuaInit!!!!!");
        var startLuaStartTime = Time.realtimeSinceStartup;
        _lua.Start();
        var endLuaStartTime = Time.realtimeSinceStartup;
        GameLog.Debug("==============================LuaStartTimeCost = {0}", (endLuaStartTime - startLuaStartTime).ToString());

        var startLuaDoFileTime = Time.realtimeSinceStartup;
        try
        {
            _lua.DoFile(LuaRoot);
        }
        catch (Exception e)
        {
            GameLog.Error("ERROR!!!==============================Lua Init Fail!!!!");
            GameLog.Error(e.Message);
        }
        var endLuaDoFileTime = Time.realtimeSinceStartup;
        GameLog.Debug("==============================LuaDoFileTimeCost = {0}", (endLuaDoFileTime - startLuaDoFileTime).ToString());

        var startLuaProjectTime = Time.realtimeSinceStartup;
        var luaStartFunc = _lua.GetFunction("Start");
        if (luaStartFunc != null) {
            luaStartFunc.Call();
        }
        var finishStartLuaProjectTime = Time.realtimeSinceStartup;
        GameLog.Debug("==============================LuaCallFuncTimeCost = {0}", (finishStartLuaProjectTime - startLuaProjectTime).ToString());
        //TODO  LuaEventManager

        Finish();
    }

    private void Finish() {
        initLuaBinder = null;
        _hasFinish = true;
    }

}
