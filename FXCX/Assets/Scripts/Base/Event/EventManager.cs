using Game.Tools.CommonTools;
using System;
using System.Collections.Generic;

namespace Game.Lwn.Main
{

    class CloneActionList {
        public bool IsVaild = false;
        public List<Action<object[]>> cloneActionList;
    }
    class AsyncEvent {
        public string Event;
        public object[] Param;
        public bool IsToLua = false;
    }

    public class EventManager //: Singleton<EventManager>
    {
        private static Dictionary<string, List<Action<object[]>>> _gameEventList = new Dictionary<string, List<Action<object[]>>>();


        public static void RegisterEvent(string p_event,Action<object[]> p_param) {
            if (!_gameEventList.ContainsKey(p_event)) {
                List<Action<object[]>> actionList = new List<Action<object[]>>();
                _gameEventList.Add(p_event, actionList);
            }
            List<Action<object[]>> actList = _gameEventList[p_event];
            if (!actList.Contains(p_param))
            {
                actList.Add(p_param);

                if (actList.Count == 1)
                {
                    //TODO lua里面处理C#的事件,注册在lua里面，这样lua也可以Send在C#中注册的事件

                }
            }
            else {
                GameLog.Debug("-------------EventManager:RegisterEvent-----重复注册事件：{0}", p_event);
            }
        }

        public static void UnRegisterEvent(string p_event, Action<object[]> p_param) {
            if (!_gameEventList.ContainsKey(p_event))
            {
                return;
            }
            List<Action<object[]>> actList = _gameEventList[p_event];
            if (!actList.Contains(p_param)) {
                return;
            }
            var idx = actList.IndexOf(p_param);
            if (idx != -1) {
                actList.RemoveAt(idx);
                if (actList.Count == 0) {
                    //TODO 同时移除在lua中记载的C#的Event事件
                }
            }
        }


        private static List<CloneActionList> _clontEventListPool = new List<CloneActionList>();

        public static void SendEvent(string p_event,params object [] p_param) {
            if (!_gameEventList.ContainsKey(p_event)) {
                return;
            }

            var actionList = _gameEventList[p_event];
            if (actionList == null && actionList.Count <= 0) {
                return;
            }
            var clonelist = GetCloneActionList(actionList);
            if (clonelist != null && clonelist.cloneActionList.Count > 0) {
                clonelist.IsVaild = false;
                for (int i=0; i<clonelist.cloneActionList.Count; i++) {
                    Action<object[]> action = clonelist.cloneActionList[i];
                    if (action != null) {
                        action.Invoke(p_param);
                    }
                }
                clonelist.IsVaild = true;
            }
        }


        private static CloneActionList GetCloneActionList(List<Action<object[]>> list) {
            if (list == null || list.Count <= 0) {
                return null;
            }
            CloneActionList result = null;
            foreach (var item in _clontEventListPool) {
                if (item.IsVaild) {
                    result = item;
                    break;
                }
            }
            if (result == null) {
                result = new CloneActionList();
                result.cloneActionList = new List<Action<object[]>>();
            }
            result.cloneActionList.Clear();
            foreach (var listitem in list) {
                if (listitem == null) {
                    continue;
                }
                result.cloneActionList.Add(listitem);
            }
            return result;
        }

        private static List<AsyncEvent> _asyncEventList = new List<AsyncEvent>();

        public static void SendEventAsync(string p_event,params object[] p_param) {
            var asyncEvent = new AsyncEvent();
            asyncEvent.Event = p_event;
            asyncEvent.Param = p_param;
            _asyncEventList.Add(asyncEvent);
        }


        private static void OnExcuteAsyncEvent() {
            if (_asyncEventList.Count <=0 ) {
                return;
            }
            foreach (var item in _asyncEventList) {
                if (item != null) {
                    SendEvent(item.Event, item.Param);
                    if (item.IsToLua) {
                        //TODO--------------
                    }
                }
            }
            _asyncEventList.Clear();
        }

        public static void Tick(uint uDeltaTimeMS)
        {
            OnExcuteAsyncEvent();
        }



            /**
            public delegate void GameEventDelegate(object[] param);

            public Dictionary<string, List<GameEventDelegate>> MainEventList = new Dictionary<string, List<GameEventDelegate>>();

            public void RegisterDelegate(string eventKey, GameEventDelegate eventFunc) {
                if (MainEventList.ContainsKey(eventKey))
                {
                    List<GameEventDelegate> theEventFunList;
                    if (MainEventList.TryGetValue(eventKey, out theEventFunList))
                    {
                        theEventFunList.Add(eventFunc);
                        return;
                    }
                    else {
                        GameLog.Error("ERROR!!!-----EventManager:RegisterDelegate--------MainEventList.TryGetValue----false!!key = {0}", eventKey);
                    }
                }
                List<GameEventDelegate> eventFunList = new List<GameEventDelegate>();
                eventFunList.Add(eventFunc);
                MainEventList.Add(eventKey, eventFunList);
            }

            */
        }

}
