using System;
using UnityEngine;
using Game.Tools.CommonTools;
using Game.Lwn.Main;

namespace Game.Lwn.Main
{

    public class GameManager : MonoBehaviour
    {


        public static DateTime m_oldTime = new DateTime(1970, 1, 1);
        private double lastTime;

        #region MonoBehaviour

        void Awake()
        {
#if !GAMEDEBUG
        GameLog.CloseLog();
#endif
            Init();
            MainProcedure.Instance.Init();
        }

        // Use this for initialization
        void Start()
        {
            GameLog.Debug("***************GameManager:Start**************************************");
            //Test---------------------------
            EventManager.RegisterEvent(DataEvent.TestEvent, OnTestEvent);
            EventManager.RegisterEvent(DataEvent.TestAsyncEvent, OnTestAsyncEvent);
            //Test---------------------------
        }



        // Update is called once per frame
        void Update()
        {
            try
            {
#if GCALLOC
            UnityEngine.Profiling.Profiler.BeginSample("Tick((uint)gapTime)");
#endif
                TimeSpan span = DateTime.Now.Subtract(m_oldTime);
                double curTime = span.TotalMilliseconds;
                double gapTime = curTime - lastTime;
                lastTime = curTime;
                Tick((uint)gapTime);
#if GCALLOC
            UnityEngine.Profiling.Profiler.EndSample();
#endif
            }
            catch (Exception e)
            {
#if GCALLOC
            UnityEngine.Profiling.Profiler.EndSample();
#endif
                GameLog.Error(e.ToString());
            }
        }


        #region GUI

#if GAMEDEBUG
        private bool IsShowGUILog = false;
        private Vector2 scrollPosition;
        void OnGUI()
        {
            if (IsShowGUILog == false)
            {
                if (GUI.Button(new Rect(Screen.width - 85, Screen.height / 2 + 40, 80, 60), "ShowLog"))
                {
                    IsShowGUILog = true;
                }
                GUILayout.TextField(GameLog.StrLogPath);
            }
            else
            {
                if (GUI.Button(new Rect(Screen.width - 85, Screen.height / 2 + 40, 80, 60), "HideLog"))
                {
                    IsShowGUILog = false;
                }
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width * 2 / 3), GUILayout.Height(Screen.height - 5));
                GUILayout.TextArea(GameLog.GUIStringBulider.ToString());
                GUILayout.EndScrollView();
            }

        }

#endif //GAMEDEBUG



        #endregion //GUI


        #endregion //MonoBehaviour

        #region GameLogic

        void Init()
        {
            GameLog.Debug("***************GameManager:Init**************************************");
        }

        private int a = 0;
        void Tick(uint uDeltaTimeMS)
        {
            a += 1;
            GameLog.Debug("<color=#9900ff>----------------------CurFrame = {0}---------------</color>", a);
            //GameLog.Debug("***************uDeltaTimeMS = {0}", uDeltaTimeMS);
            EventManager.Tick(uDeltaTimeMS);
            MainProcedure.Instance.Tick(uDeltaTimeMS);
        }


        public void OnTestEvent(object[] param)
        {
            string test = param[0].ToString();
            GameLog.Debug("<color=#00ff11>----------------------------OnTestEvent:{0}------------------------------</color>", test);
            EventManager.UnRegisterEvent(DataEvent.TestEvent, OnTestEvent);
        }
        public void OnTestAsyncEvent(object[] param)
        {
            string test = param[0].ToString();
            GameLog.Debug("<color=#00ff11>---------------------------OnTestAsyncEvent:{0}------------------------------</color>", test);
            EventManager.UnRegisterEvent(DataEvent.TestAsyncEvent, OnTestAsyncEvent);
        }
        #endregion
    }
}