Shader "ballroom/volunmetric" {
    Properties {
        _MainTex ("RGB：颜色 A：透贴", 2d) = "gray"{}
        _MainCol ("颜色", color) = (1.0, 1.0, 1.0, 1.0)

        _Opacity ("透明度", range(0, 1)) = 0.5
        _FresnelPow ("菲涅尔次幂", Range(0, 1)) = 0.5
        _DownSideFade("下边缘柔和程度", Range(0, 30)) = 15
        _DownSidePos("下边缘位置", float) = 8.4

    }
    SubShader {
        Tags {
//            "Queue"="Transparent"               // 调整渲染顺序
            "Queue"="Overlay"
            "RenderType"="Transparent"          // 对应改为Cutout
            "ForceNoShadowCasting"="True"       // 关闭阴影投射
            "IgnoreProjector"="True"            // 不响应投射器
            
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off

            Blend One OneMinusSrcAlpha          // 修改混合方式One/SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            // 输入参数
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform half _Opacity;
            uniform float _FresnelPow;
            uniform float3 _MainCol;     // RGB够了 float3
            uniform float _DownSideFade;
            uniform float _DownSidePos;

            // 输入结构
            struct VertexInput {
                float4 vertex : POSITION;       // 顶点位置 总是必要
                float2 uv : TEXCOORD0;          // UV信息 采样贴图用
                float4 normal : NORMAL;
            };
            // 输出结构
            struct VertexOutput {
                float4 pos : SV_POSITION;       // 顶点位置 总是必要
                float4 posWS : TEXCOORD2;
                float2 uv : TEXCOORD0;          // UV信息 采样贴图用
                float3 nDirWS : TEXCOORD1;
                // float dist;
            };
            // 输入结构>>>顶点Shader>>>输出结构
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                    o.pos = UnityObjectToClipPos( v.vertex);    // 顶点位置 OS>CS
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);       // UV信息 支持TilingOffset
                    o.posWS = mul(unity_ObjectToWorld, v.vertex);
                    o.nDirWS = UnityObjectToWorldNormal(v.normal);
                    
                return o;
            }
            // 输出结构>>>像素
            half4 frag(VertexOutput i) : COLOR {
                half4 var_MainTex = tex2D(_MainTex, i.uv);      // 采样贴图 RGB颜色 A透贴

                float3 vDirWS = normalize(_WorldSpaceCameraPos.xyz - i.posWS.xyz);
                float3 nDir = normalize(i.nDirWS);
                float vdotn = dot(vDirWS, nDir);

                float posY = i.posWS.y;
                posY = clamp(posY + _DownSidePos,0,_DownSideFade);

                float fresnel = pow(1-pow(max(0.0, 1.0 - vdotn), _FresnelPow), 3) ; 
                half3 finalRGB = var_MainTex.rgb * _MainCol;
                // float zDepth = i.pos.z / i.pos.w;
                // zDepth = zDepth * 0.5 + 0.5; //on OpenGL/ES 也就是oculus
                half opacity = var_MainTex.a * _Opacity * fresnel * posY / _DownSideFade;
                float zDepth = distance(_WorldSpaceCameraPos, i.posWS);

                opacity = opacity *  clamp(zDepth/ 5, 0, 1);
                
                return half4(finalRGB * opacity, opacity);
                // return half4(clamp(zDepth,0,1),clamp(zDepth,0,1),clamp(zDepth,0,1),1);
                // return half4(zDepth, zDepth, zDepth,1);
            }
            
            ENDCG
        }
    }
}