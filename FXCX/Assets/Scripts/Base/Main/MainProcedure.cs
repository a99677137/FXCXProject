using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Game.Tools.CommonTools;
using UnityEngine;

namespace Game.Lwn.Base
{
    public class MainProcedure:Singleton<MainProcedure>,ICommon
    {
        public enum LogicType:int {
            None            = -1,
            Init            = 0,
            Config          = 1,
            DownLoad        = 2,
            GameInit        = 3,
            Login           = 4,
            Game            = 5,
            Wait            = 99,
        }

        private LogicType _CurLogicType = LogicType.None;

        public LogicType CurLoginType {
            get { return _CurLogicType; }
        }

        public void Init() {
            GameLog.Debug("---------------------------------------MainProcedure:Init---------------------------------------");
            _CurLogicType = LogicType.Init;

            GameLog.Debug("------------MainProcedure:Init----------Application.dataPath = {0}", Application.dataPath);
            GameLog.Debug("------------MainProcedure:Init----------Application.streamingAssetsPath = {0}", Application.streamingAssetsPath);
            GameLog.Debug("------------MainProcedure:Init----------Application.persistentDataPath = {0}", Application.persistentDataPath);
            GameLog.Debug("------------MainProcedure:Init----------Application.temporaryCachePath = {0}", Application.temporaryCachePath);

        }

        private void LoadGameInitConfig() {
            GameLog.Debug("------------MainProcedure:LoadGameInitConfig----------");
        }

        private void LoadGameConfig() {
            ResourceManager.Instance.LoadGameConfig();

            //-------------Test-------------------
            int aaa = GameCFG.Instance.GetInt("Global", "TestInt");
            GameLog.Debug("------------MainProcedure:Init----------aaa = {0}", aaa);
            string bbb = GameCFG.Instance.GetString("Global", "TestString");
            GameLog.Debug("------------MainProcedure:Init----------bbb = {0}", bbb);
            //-------------Test End---------------
        }


        private void SwitchProcedure(LogicType next)
        {
            if (_CurLogicType == LogicType.None)
            {
                GameLog.Error("---------------------MainProcedure:SwitchProcedure----------_CurLogicType= LogicType.None!!!!");
                return;
            }
            //GameLog.Debug("---------------------MainProcedure:SwitchProcedure----------_CurLogicType = {0}, next = {1}", _CurLogicType, next);
            _CurLogicType = next;
        }
        
        public void Tick(uint uDeltaTimeMS) {
            switch (_CurLogicType) {
                case LogicType.Init:
                    GameLog.Debug("--------------------MainProcedure:Tick----------Init----");
                    SwitchProcedure(LogicType.Config);
                    break;
                case LogicType.Config:
                    GameLog.Debug("--------------------MainProcedure:Tick----------Config----");
                    LoadGameInitConfig();
                    SwitchProcedure(LogicType.DownLoad);
                    break;
                case LogicType.DownLoad:
                    GameLog.Debug("--------------------MainProcedure:Tick----------DownLoad----");
                    //TODO

                    SwitchProcedure(LogicType.GameInit);
                    break;
                case LogicType.GameInit:
                    GameLog.Debug("--------------------MainProcedure:Tick----------GameInit----");
                    LoadGameConfig();
                    InitLoginScene();
                    SwitchProcedure(LogicType.Login);
                    break;
                case LogicType.Login:
                    GameLog.Debug("--------------------MainProcedure:Tick----------Login----");
                    //Test----------------------------
                    EventManager.SendEvent(DataEvent.TestEvent, "Hello Event!");
                    EventManager.SendEventAsync(DataEvent.TestAsyncEvent, "Hello AsyncEvent!");
                    //Test----------------------------
                    SwitchProcedure(LogicType.Game);
                    break;
                case LogicType.Game:
                    GameLog.Debug("--------------------MainProcedure:Tick----------Game----");
                    //Test----------------------------
                    EventManager.SendEvent(DataEvent.TestEvent, "Hello Event2!");
                    EventManager.SendEvent(DataEvent.TestAsyncEvent, "Hello AsyncEvent2!");
                    //Test----------------------------
                    break;
            }
        }

        public void Release() { }

        public void Destroy() { }


        #region Logic
        private void InitLoginScene()
        {

        }
        #endregion
    }
}
