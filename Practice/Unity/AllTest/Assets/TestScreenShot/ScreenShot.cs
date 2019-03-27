using UnityEngine;
using System.Collections;
using System;

public class ScreenShot : MonoBehaviour {
    Texture result = null;
    Camera mainCamera = null;
    public GameObject uicameraObj = null;
    void OnGUI() {
        if (GUI.Button(new Rect(5,5,100,80),"Application_ScreenShot")) {
            StartCoroutine(ApplicationCaptureScreenShot("JustTest"));
        }
        if (GUI.Button(new Rect(5, 105, 100, 80), "Texture2D_ScreenShot"))
        {
            StartCoroutine(Texture2DScreenShot());
        }
        if (GUI.Button(new Rect(5, 205, 100, 80), "RenderTexture_ScreenShot"))
        {
            mainCamera = Camera.main;
            Camera uicamera = null;
            if (uicameraObj != null) {
                uicamera = uicameraObj.GetComponent<Camera>();
            }
            if (uicamera != null)
            {
                Camera[] list = { mainCamera, uicamera };
                RenderTextureScreenShot(list);
            }
            else {
                Camera[] list = { mainCamera};
                RenderTextureScreenShot(list);
            }
            
        }
        if (GUI.Button(new Rect(5, 305, 100, 80), "Clear"))
        {
            result = null;
        }
        if (result != null) {
            GUI.DrawTexture(new Rect(110, 5, 500, 500), result);
        }
    }

    #region Texture2D_ScreenShot
    public IEnumerator Texture2DScreenShot() {
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();
        result = screenShot;
    }
    #endregion

    #region RenderTexture_ScreenShot
    /// <summary>
    /// 可以挑选部分摄像机截取内容
    /// </summary>
    /// <param name="list"></param>
    public void RenderTextureScreenShot(params Camera [] list)
    {
        if (list == null || list.Length<1) {
            return;
        }
        var width = Screen.width;
        var height = Screen.height;
        var format = UnityEngine.RenderTextureFormat.ARGB32;
        var rt = RenderTexture.GetTemporary(width, height, 16, format);
        for (int i =0;i<list.Length;i++) {
            var cam = list[i];
            if (cam != null) {
                cam.targetTexture = rt;
                cam.Render();
            }
        }
        RenderTexture.active = rt;
        Texture2D texture = new Texture2D(width, height,TextureFormat.ARGB32,false);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0,false);
        texture.Apply();
        for (int i = 0; i < list.Length; i++)
        {
            var cam = list[i];
            if (cam != null)
            {
                cam.targetTexture = null;
            }
        }
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        rt = null;
        result = texture;
    }
    #endregion

        #region Application_ScreenShot
    public IEnumerator ApplicationCaptureScreenShot(string imgName)
    {
        imgName = System.DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + imgName;
        ScreenCapture.CaptureScreenshot(imgName);
        string filePath = "";
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            filePath = Application.persistentDataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            filePath = Application.dataPath;
            filePath = filePath.Replace("/Assets", null);
        }
        filePath = filePath + "/" + imgName;
        yield return new WaitUntil(() => //由于Application.CaptureScreenshot并未立即生成图片~而是需要等一帧或
        {
            return System.IO.File.Exists(filePath); ;
        });
        try
        {
            StartCoroutine(LoadImage(filePath, (Texture2D tex) =>
            {
                if (tex != null){
                    Debug.LogError("ApplicationCaptureScreenShot----LoadImageSuccess!");
                    result = tex;
                } else{
                    Debug.LogError("ApplicationCaptureScreenShot----LoadImageFail!");
                }
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }));
        }
        catch (System.Exception e)
        {
            Debug.LogError("ApplicationCaptureScreenShot----e.Message ="+e.Message);
        }
    }

    private static IEnumerator LoadImage(string path, Action<Texture2D> callBack)
    {
        path = "file://" + path;
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone && string.IsNullOrEmpty(www.error)){
            try{
                Texture2D texture = www.texture;
                if (callBack != null)
                {
                    callBack(texture);
                }
                www.Dispose();
            }catch (System.Exception e){
                Debug.LogError("LoadIamge Error!!!----e.Message="+ e.Message);
            }
        }
        else
        {
            Debug.LogError("LoadIamge Error!!! www.error="+ www.error);
            if (callBack != null)
            {
                callBack(null);
            }
            yield break;
        }

    }
    #endregion
}
