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
            DownLoad        = 1,
            GameInit        = 2,
            Login           = 3,
            Game            = 4,
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

        public void SwitchProcedure(LogicType next)
        {
            if (_CurLogicType == LogicType.None)
            {
                GameLog.Error("---------------------MainProcedure:SwitchProcedure----------_CurLogicType= LogicType.None!!!!");
                return;
            }
            GameLog.Debug("---------------------MainProcedure:SwitchProcedure----------_CurLogicType = {0}, next = {1}", _CurLogicType, next);
            _CurLogicType = next;
        }

        public void Tick(uint uDeltaTimeMS) {
            switch (_CurLogicType) {
                case LogicType.Init:

                    break;
            }
        }

        public void Release() { }

        public void Destroy() { }

    }
}
