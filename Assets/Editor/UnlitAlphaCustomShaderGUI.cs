// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

using UnityEngine;

namespace UnityEditor
{
    internal class UnlitAlphaCustomShaderGUI : ShaderGUI
    {
        bool m_FirstTimeApply = true;

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props) {
            MaterialProperty stencil = FindProperty("_Stencil", props);

            Material material = materialEditor.target as Material;
            if(m_FirstTimeApply) {
                MaterialChanged(material);
                m_FirstTimeApply = true;
            }

            EditorGUI.BeginChangeCheck();

            base.OnGUI(materialEditor, props);

            if(EditorGUI.EndChangeCheck()) {
                foreach(var obj in stencil.targets) {
                    MaterialChanged((Material)obj);
                }
            }
        }

        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader) {
            base.AssignNewShaderToMaterial(material, oldShader, newShader);
            MaterialChanged(material);
        }

        static void MaterialChanged(Material material) {
            if(material.GetInt("_Stencil") > 0) {
                material.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.NotEqual);
                material.SetInt("_StencilPass", (int)UnityEngine.Rendering.StencilOp.Replace);
            } else {
                material.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.Always);
                material.SetInt("_StencilPass", (int)UnityEngine.Rendering.StencilOp.Keep);
            }
        }
    }
} // namespace UnityEditor
