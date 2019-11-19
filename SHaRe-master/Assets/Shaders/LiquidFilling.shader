Shader "Custom/LiquidFilling"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_LiquidHeight("LiquidHeight", Float) = 5
		_LiquidColor("LiquidColor", Color) = (.34, .85, .92, 1) // color
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		Cull Off

		Pass
		{
			//ZWrite Off
			//Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float worldy : TEXCOORD1;
				float worldz : TEXCOORD2;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldy = mul(unity_ObjectToWorld, v.vertex).y;
				o.worldz = mul(unity_ObjectToWorld, v.vertex).z;
				return o;
			}

			float _LiquidHeight;
			fixed4 _LiquidColor;
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv);

				if (i.worldy > _LiquidHeight)
					discard;

				//if (i.worldz < WaterClipZ)
				//	discard;
				return _LiquidColor;
			}
			ENDCG
		}
	}
}
