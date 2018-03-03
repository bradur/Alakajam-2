// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "FX/LevelGridShader"
{
	Properties
	{
		_Color("Color", Color) = (0.0, 1.0, 0.0, 1.0)
		_Scale("Scale", Float) = 1.0
		_Width("Main Width", Float) = 0.1
		_PlayerPos("Player position", Vector) = (0.0, 0.0, 0.0, 1.0)
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			fixed4 _Color;
			float _Scale;
			float _Width;
			fixed4 _PlayerPos;

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 wPos : W_POSITION;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.wPos = mul(unity_ObjectToWorld, v.vertex);
				o.vertex = mul(UNITY_MATRIX_VP, o.wPos);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				float minorScale = _Scale / 4.0;
				float minorWidth = _Width / 1.0;
				float alpha = 0.0, a = 0.0;
				float4 outputColor = _Color;

				a = 2/_Width * abs(abs(i.wPos.x % _Scale)/_Scale - 0.5)*2 + 2 - 2/_Width;
				alpha += max(a, 0.0);

				a = 2 / _Width * abs(abs(i.wPos.y % _Scale) / _Scale - 0.5) * 2 + 2 - 2 / _Width;
				alpha += max(a, 0.0);

				a = 2 / _Width * abs(abs(i.wPos.z % _Scale) / _Scale - 0.5) * 2 + 2 - 2 / _Width;
				alpha += max(a, 0.0);

				a = 2 / minorWidth * abs(abs(i.wPos.x % minorScale) / minorScale - 0.5) * 2 + 2 - 2 / minorWidth;
				alpha += max(a, 0.0);

				a = 2 / minorWidth * abs(abs(i.wPos.y % minorScale) / minorScale - 0.5) * 2 + 2 - 2 / minorWidth;
				alpha += max(a, 0.0);

				a = 2 / minorWidth * abs(abs(i.wPos.z % minorScale) / minorScale - 0.5) * 2 + 2 - 2 / minorWidth;
				alpha += max(a, 0.0);

				float dist = 100-length(_PlayerPos - i.wPos);
				//float dist = i.wPos.z;
				alpha = alpha*(sin((dist+_Time.y*2)*2) - 0.5)*0.5;

				outputColor.a = alpha;

				if (outputColor.a < 0.01) {
					discard;
				}

				if (outputColor.a > 1.0) {
					outputColor.a = 1.0;
				}

				return outputColor;
			}
			ENDCG
		}
	}
}
