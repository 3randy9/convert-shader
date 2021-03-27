// ����"VRM/MToon"�̂�

namespace ConvertMtoon
{
    using UnityEditor;
    using UnityEngine;

    public class ConvertMtoon : EditorWindow
    {
        // Hierarchy��őI�����Ă���I�u�W�F�N�g��Convert����
        void DoConvert()
        {
            var gameObjects = Selection.activeGameObject; // ���l�[���Ώۂ�GameObject

            // Undo�ɓo�^
            Undo.RecordObject(gameObjects, "Convert Mtoon");

            // Convert
            changeShader(gameObjects, "VRM/MToon");

        }

        // EditorWindow�̕`�揈��
        void OnGUI()
        {
            EditorGUILayout.LabelField("Hierarchy��őI�����Ă���Root�I�u�W�F�N�g��Shader��VRM/MToon�ɕύX���܂�");
            GUILayout.Space(2f);


            // �{�^����\��
            if (GUILayout.Button("Convert"))
            {
                this.DoConvert();
            }

            EditorGUI.EndDisabledGroup();
        }

        // �E�B���h�E���J��
        [MenuItem("Purini/ConvertMtoon")]
        static void Open()
        {
            GetWindow<ConvertMtoon>();
        }

        // �E�B���h�E���J��
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