Shader "LwnLearning/Learn1"
{
	Properties
	{
		[HDR]_Color("Test Color",Color) = (1,1,1,1)
	}
		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;

			float4 vert(float4 vertex : POSITION) : SV_POSITION
			{
				return UnityObjectToClipPos(vertex);
			}
			fixed4 frag() : SV_TARGET
			{
				return _Color;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
