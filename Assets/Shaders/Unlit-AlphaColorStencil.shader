Shader "Unlit/Transparent Color Stencil" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
    }

    SubShader {
        Tags {"IgnoreProjector" = "True" "Queue" = "Transparent" "RenderType" = "Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Stencil {
            Ref 1
            Comp NotEqual
            Pass Replace
        }

        Pass {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    UNITY_FOG_COORDS(0)
                };

                fixed4 _Color;

                v2f vert(appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    UNITY_TRANSFER_FOG(o, o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : COLOR {
                    fixed4 col = _Color;
                    UNITY_APPLY_FOG(i. fogCoord, col);
                    return col;
                }
            ENDCG
        }
    }
}