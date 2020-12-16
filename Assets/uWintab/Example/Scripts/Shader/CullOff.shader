Shader "Unlit/culloff"
{
	Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 100
		Cull off
	
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha 

		Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
        
            float4 _MainTex_ST;
			
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos( v.vertex );
                o.uv = v.uv;
                return o;
            }
			
            
            fixed4 frag (v2f i, fixed facing : VFACE) : SV_Target
            {                
                return (facing > 0 ) ? tex2D(_MainTex, i.uv) : (tex2D(_MainTex, i.uv))*float4(0.2, 0.2, 0.2, 1.0);
            }
            ENDCG
        }
			
		
	}
}