using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Tools.CommonTools;

public class GameManager : MonoBehaviour {


    #region MonoBehaviour
    // Use this for initialization
    void Start () {
        GameLog.Debug("---------------------------------------GameStart-----------------------------------------");
	}
	
	// Update is called once per frame
	void Update () {
		
	}




    #region GUI

    #if GAMEDEBUG
    void OnGUI()
    {
        
    }

#endif //GAMEDEBUG



    #endregion //GUI


    #endregion //MonoBehaviour

    #region GameLogic

    

    #endregion
}
