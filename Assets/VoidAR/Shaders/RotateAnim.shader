Shader "Custom/RotateAnim" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_OverTex("Base Texture (RGB)", 2D) = "white" {}
		_RotationSpeed("Rotation Speed", Float) = 5.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _OverTex;

			struct Input {
				float2 uv_OverTex;
			};

			float _RotationSpeed;
			fixed4 _Color;

			#define ANGLE (_Time.z * _RotationSpeed)

			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed2 center = fixed2(0.5, 0.5);
				float2x2 rotate = float2x2(cos(ANGLE), -sin(ANGLE), sin(ANGLE), cos(ANGLE));
				fixed2 uv_OverTex = mul(rotate, IN.uv_OverTex - center) + center;
				fixed4 over = tex2D(_OverTex, uv_OverTex);
				fixed4 c = over * _Color;
				o.Emission = c.rgb;
			}
			ENDCG
		}
		FallBack "Diffuse"
}
