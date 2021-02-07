// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Anil/Transparent " {
 Properties {
    _Color("Color",Color) = (0,1,0,1)
 }
 
 SubShader {
     Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
     LOD 100
     
     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha 
     
     Pass {  
         CGPROGRAM

             #pragma vertex vert
             #pragma fragment frag
             
             #pragma multi_compile_instancing

             struct VertexInput {
                 half4 vertex : POSITION;
                 half2 texcoord : TEXCOORD0;
                 half3 normal : NORMAL;
             };
 
             struct VertexOutput {
                 half4 vertex : SV_POSITION;
                 half2 texcoord : TEXCOORD0;
                 half4 col : COLOR;
             };
 
             uniform sampler2D _MainTex;
             uniform half4 _MainTex_ST;
             
             uniform half4 _Color = (0,1,0,1);

             uniform half4 _LightColor0;

             VertexOutput vert (VertexInput v)
             {
                 VertexOutput o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.texcoord = v.texcoord;

                 half3 normalDirection = normalize(mul(half4(v.normal,0), unity_WorldToObject).xyz);
				 half3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				 half3 difuseReflection = _LightColor0 * max(0,dot(normalDirection, lightDirection));
				 half3 lightFinal = difuseReflection + UNITY_LIGHTMODEL_AMBIENT.xyz;

                 o.col = _Color * half4(lightFinal,1);

                 return o;
             }
             
             fixed4 frag (VertexOutput i) : SV_Target
             {
                 return tex2D(_MainTex, i.texcoord) * i.col;
             }
         ENDCG
     }
 }
 
 }