using System;
using Game.Tools.CommonTools;
using Game.Lwn.Base;

class ScenesManager:Singleton<ScenesManager>
{
    private static int LoginSceneId = 0;
    private bool IsStartLoadLoginScene = false;

    public void InitLoginScene()
    {
        if (IsStartLoadLoginScene) {
            return;
        }
        GameLog.Debug("--------------------ScenesManager:InitLoginScene-----------------LoginSceneId = {0}", LoginSceneId);
        IsStartLoadLoginScene = true;

        LoadLoginSceneFinish();//临时测试流程
    }

    private void LoadLoginSceneFinish() {
        IsStartLoadLoginScene = false;

        EventManager.SendEvent(DataEvent.FinishLoadLoginScenes);
    }
}
