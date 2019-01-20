// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/YUV420"
{
	Properties
	{
		Yp ("Y channel texture", 2D) = "white" {}
		CbCr("CbCr channel texture", 2D) = "black" {}
		_ScanColor("ScanColor", Color) = (0, 0.5, 0.5, 1.0)
		_ScanMode("ScanMode", Int) = 0
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

			sampler2D Yp;
			float4 Yp_ST;
			sampler2D CbCr;
			int _ScanMode;
			fixed4 _ScanColor;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, Yp);
				return o;
			}

			/*const float3 ycbcr_offs = float3(-0.0625, -0.5, -0.5);
			const float3x3 ycbcr_xfrm = float3x3(
				float3(1.164, 0.0, 1.596),
				float3(1.164, -0.392, -0.813),
				float3(1.164, 2.017, 0.0)
			);*/
			
			fixed4 frag (v2f i) : SV_Target
			{	
				float3 yuv = float3(  
					1.1643 * (	tex2D(Yp, i.uv).r - 0.0625),  //y
					tex2D(CbCr, i.uv).a - 0.5,  //v
					tex2D(CbCr, i.uv).r - 0.5  //u
		    	);  


				float3x3 yuv2rgb = { 
								1, 0, 1.2802,  
							    1, -0.214821, -0.380589,  
							    1, 2.127982, 0  };


				float3 rgb = mul(yuv2rgb , yuv); 
				fixed4 color = float4(rgb, 1);

				/*float y = tex2D(Yp, i.uv);
				float4 uv = tex2D(CbCr, i.uv);
				#if SHADER_API_METAL
				float3 ycbcr = float3(y, uv.rg);
				#else
				float3 ycbcr = float3(y, uv.ra);
				#endif
				float3 col = mul(ycbcr_xfrm, ycbcr + ycbcr_offs);
				fixed4 color = fixed4(fixed3(col.b,col.g,col.r), 1);*/

				if (_ScanMode != 0)
				{
					fixed2 pixelSize = 1.0 / fixed2(640.0, 480.0);

					fixed tl = tex2D(Yp, i.uv - pixelSize).x;
					fixed t = tex2D(Yp, i.uv + fixed2(0.0, -pixelSize.y)).x;
					fixed tr = tex2D(Yp, i.uv + fixed2(pixelSize.x, -pixelSize.y)).x;

					fixed cl = tex2D(Yp, i.uv + fixed2(-pixelSize.x, 0.0)).x;
					fixed cr = tex2D(Yp, i.uv + fixed2(pixelSize.x, 0.0)).x;

					fixed bl = tex2D(Yp, i.uv + fixed2(-pixelSize.x, pixelSize.y)).x;
					fixed b = tex2D(Yp, i.uv + fixed2(0.0, pixelSize.y)).x;
					fixed br = tex2D(Yp, i.uv + pixelSize).x;

					fixed sobelX = tl * -1.0 + tr * 1.0 + cl * -2.0 + cr * 2.0 + bl * -1.0 + br * 1.0;
					fixed sobelY = tl * -1.0 + t * -2.0 + tr * -1.0 + bl * 1.0 + b * 2.0 + br * 1.0;

					fixed sobelVal = sqrt(sobelX * sobelX + sobelY * sobelY);

					fixed scanLine = _SinTime.w * 0.7 + 0.5;
					fixed scanArea = 0.4;
					fixed distanceToScanline = 0;
					if (_ScanMode == 1) { //LR
						distanceToScanline = clamp(0.0, scanArea, abs(scanLine - i.uv.x)) / scanArea;
					}
					else {  //TB
						distanceToScanline = clamp(0.0, scanArea, abs(scanLine - i.uv.y)) / scanArea;
					}
					fixed4 c1 = lerp(color, _ScanColor, step(0.5, sobelVal));
					fixed4 c2 = lerp(c1, color, smoothstep(0.1, 0.9, distanceToScanline));
					return c2;
				}
				else {
					return color;
				}
			}
			ENDCG
		}
	}
}
