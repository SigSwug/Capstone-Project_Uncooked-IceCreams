Shader "Projector/LightWithIntensity" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _ShadowTex ("Cookie", 2D) = "" {}
        _FalloffTex ("FallOff", 2D) = "" {}
        _Intensity ("Intensity", Float) = 1
        _EmissiveColor ("Emissive Color", Color) = (1,1,1,1) // Set default emissive color to white for brightness
        _EmissiveIntensity ("Emissive Intensity", Float) = 2 // Increased default intensity
        _EmissiveTex ("Emissive Map", 2D) = "" {}
    }
    
    Subshader {
        Tags {"Queue"="Transparent"}
        Pass {
            ZWrite Off
            ColorMask RGB
            // Change blend mode to add colors instead of multiplying them
            Blend One One
            Offset -1, -1
    
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"
            
            struct v2f {
                float4 uvShadow : TEXCOORD0;
                float4 uvFalloff : TEXCOORD1;
                UNITY_FOG_COORDS(2)
                float4 pos : SV_POSITION;
            };
            
            float4x4 unity_Projector;
            float4x4 unity_ProjectorClip;
            
            v2f vert (float4 vertex : POSITION)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);
                o.uvShadow = mul (unity_Projector, vertex);
                o.uvFalloff = mul (unity_ProjectorClip, vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            
            fixed4 _Color;
            sampler2D _ShadowTex;
            sampler2D _FalloffTex;
            float _Intensity;
            fixed4 _EmissiveColor;
            float _EmissiveIntensity;
            sampler2D _EmissiveTex;
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
                texS.rgb *= _Color.rgb;
                texS.a = 1.0 - texS.a;
    
                fixed4 texF = tex2Dproj (_FalloffTex, UNITY_PROJ_COORD(i.uvFalloff));
                fixed4 res = texS * _Intensity * texF.a;

                // Sample the emissive texture and add its contribution
                fixed4 emissiveTex = tex2Dproj(_EmissiveTex, UNITY_PROJ_COORD(i.uvShadow));
                res.rgb += (emissiveTex.rgb * _EmissiveColor.rgb) * _EmissiveIntensity;

                UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(0,0,0,0));
                return res;
            }
            ENDCG
        }
    }
}
