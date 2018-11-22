using UnityEditor;
using UnityEngine;

public class TableToolPanel : EditorWindow
{

    public static bool IsStart = false;

    [MenuItem("TableTool/Generate Table")]
    static void Init() {
        TableToolManager.Instance.TablePath = TableToolManager.Instance.TableConfigPath;
        TableToolPanel tableTool = (TableToolPanel)EditorWindow.GetWindow(typeof(TableToolPanel));
        tableTool.minSize = new Vector2(400,800);
        tableTool.ShowPopup();
    }

    void Update() {
        if (IsStart) {
            TableToolManager.Instance.Tick();
        }
    }

    void OnDestroy() {
        TableToolManager.Instance.Destroy();
    }

    //bool groupEnabled;

    void OnGUI() {

        GUILayout.Label("Table Setting", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        //Debug.Log("TablePath = " + TableToolManager.Instance.TablePath);
        TableToolManager.Instance.TablePath = EditorGUILayout.TextField("TablePath", TableToolManager.Instance.TablePath);
        if (GUILayout.Button("ChangePath",GUILayout.MaxWidth(80))){
            TableToolManager.Instance.TablePath = EditorUtility.OpenFolderPanel("Select folder of the tables", TableToolManager.BasePath, "");
            if (string.IsNullOrEmpty(TableToolManager.Instance.TablePath))
            {
                TableToolManager.Instance.TablePath = TableToolManager.Instance.TableConfigPath;
            }
            //this.Repaint();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (GUILayout.Button("Transport",GUILayout.MaxWidth(80))) {
            print("Start to transport!");
            TableToolManager.Instance.GO();
            IsStart = true;
        }


        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //myBool = EditorGUILayout.Toggle("Toggle", false);
        //myFloat = EditorGUILayout.Slider("Slider", 1.23f, -3, 3);
        //EditorGUILayout.EndToggleGroup();


        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.TextArea(TableToolManager.Instance.PrintMsg);

    }

    public void print(string msg) {
        TableToolManager.Instance.Print(msg);
    }
}
