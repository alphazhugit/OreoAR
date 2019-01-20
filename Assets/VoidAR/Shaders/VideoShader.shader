// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/VideoShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "black" {}
		_FillMode("FillMode", Int) = 0
		_ScaleX("ScaleX", float) = 1.0
		_ScaleY("ScaleY", float) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		
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
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			int _FillMode;
			float _ScaleX;
			float _ScaleY;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{	
				float4 color ;
				
				
				float2 newUV;
				
				if ( _FillMode == 0 )
				{
					color =  tex2D(_MainTex, i.uv);
				}
				else if ( _FillMode == 1 )
				{
					if( _ScaleX < 1.0 )
					{
						float xStart = 0.5 - _ScaleX / 2;
						float xEnd = 0.5 + _ScaleX / 2;
						if ( i.uv[0] <  xStart || i.uv[0] > xEnd  ){
							color =  (0,0,0,0);
						} else {
							newUV[1] = i.uv[1];
							newUV[0] = (i.uv[0] - xStart) / _ScaleX;
							color =  tex2D(_MainTex, newUV);
						}
					}
					else
					{
						float yStart = 0.5 - _ScaleY / 2;
						float yEnd = 0.5 + _ScaleY / 2;
						if ( i.uv[1] <  yStart || i.uv[1] > yEnd  ){
							color =  (0,0,0,0);
						} else {
							newUV[0] = i.uv[0];
							newUV[1] = (i.uv[1] - yStart) / _ScaleY;
							color =  tex2D(_MainTex, newUV);
						}
					}
				}
				else if ( _FillMode == 2 ) 
				{
					if ( _ScaleY > 1.0 ) {
						float newHeight = 1.0 / _ScaleY ;
						float yStart = 0.5 - newHeight /2;
						newUV[0] = i.uv[0];
						newUV[1] = i.uv[1] * newHeight + yStart;
						color =  tex2D(_MainTex, newUV);
					} else {
						float yStart = 0.5 - _ScaleY / 2;
						float yEnd = 0.5 + _ScaleY / 2;
						if ( i.uv[1] <  yStart || i.uv[1] > yEnd  ){
							color =  (0,0,0,0);
						} else {
							newUV[0] = i.uv[0];
							newUV[1] = (i.uv[1] - yStart) / _ScaleY;
							color =  tex2D(_MainTex, newUV);
						}
					}
				}
				else if ( _FillMode == 3 )
				{
					if ( _ScaleX > 1.0 ) {
						float newWidth = 1.0 / _ScaleX ;
						float xStart = 0.5 - newWidth /2;
						newUV[0] = i.uv[0] * newWidth + xStart;
						newUV[1] = i.uv[1];
						color =  tex2D(_MainTex, newUV);
					} else {
						float xStart = 0.5 - _ScaleX / 2;
						float xEnd = 0.5 + _ScaleX / 2;
						if ( i.uv[0] <  xStart || i.uv[0] > xEnd  ){
							color =  (0,0,0,0);
						} else {
							newUV[1] = i.uv[1];
							newUV[0] = (i.uv[0] - xStart) / _ScaleX;
							color =  tex2D(_MainTex, newUV);
						}
					}
				}
				return color;
			}
			ENDCG
		}
	}
}
