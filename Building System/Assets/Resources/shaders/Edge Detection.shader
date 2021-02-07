Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1,0,0,1)
        _SecondColor("Second Color", Color) = (1,0,0,1)
        _XPosition("X Position of the Line", Float) = 0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            uniform fixed4 _MainColor;
            uniform float _XPosition;
            uniform float4 _SecondColor;
            uniform fixed4 _LightColor0;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            
            struct v2f
            {
                float4 clipPos  : SV_POSITION;
                float4 worldPos : TEXCOORD0;
                float4 col      : COLOR;  
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
                
                o.col = half4(lightFinal,0); 

                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;

                if(i.worldPos.x > _XPosition){
                    col = _MainColor;
                }
                 
                if(i.worldPos.x < _XPosition){
                    col = _SecondColor;
                }

                return col * i.col;
            }
            ENDCG
        }
    }
}
