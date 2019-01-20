// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Background" {
	Properties {
		_MainTex ("Texture", 2D) = "white" { }
		_ScanColor("ScanColor", Color) = (0, 0.5, 0.5, 1.0)
		_ScanMode("ScanMode", Int) = 0
	}
    SubShader {
    	Tags {
            "IgnoreProjector"="True"
            "Queue"="Background"
            "RenderType"="Opaque"
        }
        Pass {
		Tags { "LightMode" = "ForwardBase" }
		CGPROGRAM

		#pragma target 3.0
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		sampler2D _MainTex;
		
		struct v2f {
			float4 pos : SV_POSITION;
			float2  uv : TEXCOORD0;
		};
		
		float4 _MainTex_ST;
		int _ScanMode;
		fixed4 _ScanColor;
		v2f vert (appdata_base v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos (v.vertex);
			o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
			return o;
		}
		
		fixed luminance(in fixed3 rgb)
		{
		    return 0.2126 * rgb.r + 0.7152 * rgb.g + 0.0722 * rgb.b;
		}

		fixed4 frag (v2f i) : SV_Target
		{
		    fixed4 textureColor = tex2D(_MainTex, i.uv);
			
			if (_ScanMode != 0)
			{
				fixed2 pixelSize = 1.0 / fixed2(640.0, 480.0);

				fixed tl = luminance(tex2D(_MainTex, i.uv - pixelSize).rgb);
				fixed t = luminance(tex2D(_MainTex, i.uv + fixed2(0.0, -pixelSize.y)).rgb);
				fixed tr = luminance(tex2D(_MainTex, i.uv + fixed2(pixelSize.x, -pixelSize.y)).rgb);

				fixed cl = luminance(tex2D(_MainTex, i.uv + fixed2(-pixelSize.x, 0.0)).rgb);
				fixed cr = luminance(tex2D(_MainTex, i.uv + fixed2(pixelSize.x, 0.0)).rgb);

				fixed bl = luminance(tex2D(_MainTex, i.uv + fixed2(-pixelSize.x, pixelSize.y)).rgb);
				fixed b = luminance(tex2D(_MainTex, i.uv + fixed2(0.0, pixelSize.y)).rgb);
				fixed br = luminance(tex2D(_MainTex, i.uv + pixelSize).rgb);

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
				fixed4 c1 = lerp(textureColor, _ScanColor, step(0.5, sobelVal));
				fixed4 c2 = lerp(c1, textureColor, smoothstep(0.1, 0.9, distanceToScanline));
				return c2;
			}
			else {
				return textureColor;
			}
		}
		ENDCG
	}
  }
  Fallback "Unlit/Transparent"
}