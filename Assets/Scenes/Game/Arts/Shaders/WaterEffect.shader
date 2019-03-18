Shader "Unlit/WaterEffect"
{
	Properties
	{
		_MainTex ("Water Texture", 2D) = "white" {}
		_Noise ("Noise", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
		_Speed ("Wave Speed", Range(0 , 2)) = 1
		_Amount ("Wave Amount", Range(0, 2)) = 1
		_Scale ("Wave Scale", Range(0, 10)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque"  "Queue" = "Transparent"}
		LOD 100
        Blend OneMinusDstColor One
        Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
                float4 scrPos : TEXCOORD2;//
                float4 worldPos : TEXCOORD4;//
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _Noise;
			fixed4 _Color;
			float _Speed;
			float _Amount;
			float _Scale;
			
			v2f vert (appdata v)
			{
				v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                float4 tex = tex2Dlod(_Noise, float4(v.uv.xy, 0, 0));//extra noise tex
                //v.vertex.y += sin(_Time.z * _Speed + (v.vertex.x * v.vertex.z * _Amount * tex));//movement
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);               
                o.scrPos = ComputeScreenPos(o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);   
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
//			    float2 uv = i.worldPos.xz - _Position.xz;				
//				uv /= (_OrthographicCamSize * 2);				
//				uv += 0.5;
//				
//				// Ripples
//				float ripples = tex2D(_GlobalEffectRT, uv ).b;
//
//				// mask to prevent bleeding
//				float4 mask = tex2D(_MaskInt, uv);				
//				ripples *= mask.a;
//
//
                fixed distortx = tex2D(_Noise, (i.uv* _Scale)  + (_Time.x * _Speed)).r ;// distortion 
//				distortx +=  (ripples *2);
                i.uv += sin(_Time.z * _Speed) + distortx;           
                half4 col = tex2D(_MainTex, (i.uv * _Scale));// texture times tint;        
                col *= _Color;
                col = saturate(col) * col.a ;
			   
              return col;
			}
			ENDCG
		}
	}
}