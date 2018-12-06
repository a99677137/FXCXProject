using System;
using Game.Lwn.Main;
using Game.Tools.CommonTools;
namespace Game.Lwn.Main
{
    class GameConfigManager : Singleton<GameConfigManager>
    {
        private bool _gameInitConfig_Finish = false;
        private static bool _gameConfig_Finish = false;
        private static bool _gameTable_Finish = false;
        private static bool _gameLua_Finish = false;

        public void LoadGameInitConfig()
        {
            if (_gameInitConfig_Finish)
            {
                return;
            }
            GameLog.Debug("------------GameConfigManager:LoadGameInitConfig----------");
            ResourceManager.Instance.LoadGameInitConfig();
            _gameInitConfig_Finish = true;
        }

        private void LoadGameConfig()
        {
            if (_gameConfig_Finish)
            {
                return;
            }

            GameLog.Debug("------------GameConfigManager:LoadGameConfig----------");
            ResourceManager.Instance.LoadGameConfig();
            _gameConfig_Finish = true;

            //-------------Test-------------------
            int aaa = GameCFG.Instance.GetInt("Global", "TestInt");
            GameLog.Debug("------------GameConfigManager:Init----------aaa = {0}", aaa);
            string bbb = GameCFG.Instance.GetString("Global", "TestString");
            GameLog.Debug("------------GameConfigManager:Init----------bbb = {0}", bbb);
            //-------------Test End---------------
        }

        public void GameConfigInit()
        {
            LoadGameConfig();
            LoadGameTable();
        }

        public static void Tick(uint uDeltaTimeMS)
        {
            if (_gameConfig_Finish && _gameTable_Finish)
            {
                InitLua();
            }
        }


        private void LoadGameTable()
        {
            if (_gameTable_Finish)
            {
                return;
            }
            GameLog.Debug("------------GameConfigManager:LoadGameTable----------");
            TableManager.Instance.Init();

            //TODO-----
            _gameTable_Finish = true;
        }

        private static void InitLua()
        {
            if (_gameLua_Finish)
            {
                return;
            }
            GameLog.Debug("------------GameConfigManager:InitLua----------");
            LuaManager.Instance.Start();
            _gameLua_Finish = true;
            EventManager.SendEvent(DataEvent.FinishLoadGameConfig);
        }

    }

}