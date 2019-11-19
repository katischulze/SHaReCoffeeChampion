Shader "Custom/test"
{
	Properties
	{
		_Base("Local Base Y", Float) = 0.0
		_Height("Local Height", Float) = 0.5
		_FillHeight("Fill Height", Float) = 0.5

	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		Cull Off
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float3 uv : TEXCOORD0;
	};

	struct v2f
	{
		float localY : TEXCOORD0;
		float4 pos : SV_POSITION;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.localY = v.vertex.y;
		return o;
	}

	float _Base, _Height, _FillHeight;

	fixed4 _MyColor;

	fixed4 frag(v2f i) : SV_Target
	{
		clip(_FillHeight - (i.localY - _Base) / _Height);

	return _MyColor;
	}
		ENDCG
	}
	}
		Fallback "VertexLit"
}