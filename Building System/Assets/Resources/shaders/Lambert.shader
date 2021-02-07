Shader "Unlit/Lambert"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1,0,0,1)
        _MainTex("Main texture",2D) = "white"
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            uniform fixed4 _MainColor;
            uniform fixed4 _LightColor0;
            uniform sampler2D _MainTex;
            uniform fixed4 _MainTex_ST;
            //
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv     : TEXCOORD1;
            };
            
            struct v2f
            {
                float4 clipPos  : SV_POSITION;
                float4 worldPos : TEXCOORD0;
                float4 col      : COLOR;  
                float2 uv       : TEXCOORD2;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.clipPos  = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                
                half3 normalDirection = normalize(mul(half4(v.normal,0), unity_WorldToObject).xyz);
				half3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				half3 difuseReflection = _LightColor0 * max(0,dot(normalDirection, lightDirection));
				half3 lightFinal = difuseReflection + UNITY_LIGHTMODEL_AMBIENT.xyz;
                
                o.col = half4(lightFinal,0) * _MainColor; 
                o.uv = v.uv;
                //
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv) * i.col;
            }
            ENDCG
        }
    }
}
