// 現状"VRM/MToon"のみ

namespace ConvertMtoon
{
    using UnityEditor;
    using UnityEngine;

    public class ConvertMtoon : EditorWindow
    {
        // Hierarchy上で選択しているオブジェクトをConvertする
        void DoConvert()
        {
            var gameObjects = Selection.activeGameObject; // リネーム対象のGameObject

            // Undoに登録
            Undo.RecordObject(gameObjects, "Convert Mtoon");

            // Convert
            changeShader(gameObjects, "VRM/MToon");

        }

        // EditorWindowの描画処理
        void OnGUI()
        {
            EditorGUILayout.LabelField("Hierarchy上で選択しているRootオブジェクトのShaderをVRM/MToonに変更します");
            GUILayout.Space(2f);


            // ボタンを表示
            if (GUILayout.Button("Convert"))
            {
                this.DoConvert();
            }

            EditorGUI.EndDisabledGroup();
        }

        // ウィンドウを開く
        [MenuItem("Purini/ConvertMtoon")]
        static void Open()
        {
            GetWindow<ConvertMtoon>();
        }

        // ウィンドウを開く
        public static void changeShader(GameObject targetGameObject, string ShaderName_to, string ShaderName_from = "")
        {
            // List<GameObject> ret = new List<GameObject>();
            foreach (Transform t in targetGameObject.GetComponentsInChildren<Transform>(true)) //include inactive gameobjects
            {
                if (t.GetComponent<Renderer>() != null)
                {
                    var materials = t.GetComponent<Renderer>().materials;
                    for (int i = 0; i < materials.Length; i++)
                    {
                        Material material = materials[i];
                        if (ShaderName_from == "")
                        {
                            material.shader = Shader.Find(ShaderName_to);
                            material.SetColor("_ShadeColor", Color.white);
                            var mainTex = material.GetTexture("_MainTex");
                            material.SetTexture("_ShadeTexture", mainTex);
                        }
                        else
                        {
                            if (material.shader.name == ShaderName_from)
                            {
                                material.shader = Shader.Find(ShaderName_to);
                            }
                        }
                    }
                }
            }
        }
    }
}
