// SimpleSonarShader scripts and shaders were written by Drew Okenfuss.
// For this shader to work, the object must have values passed in to it from the SimpleSonarShader_Parent.cs script.
// By default, this happens by having the object be a child of SimpleSonarShader_Parent.
Shader "MadeByProfessorOakie/SimpleSonarShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Metallic("Metallic", 2D) = "white" {}
		_RingColor("Ring Color", Color) = (1,1,1,1)
		_RingColorIntensity("Ring Color Intensity", float) = 2
		_RingSpeed("Ring Speed", float) = 1
		_RingWidth("Ring Width", float) = 0.1
		_RingIntensityScale("Ring Range", float) = 1
		_RingTex("Ring Texture", 2D) = "white" {}
		_Normal("Normal Texture", 2D) = "white" {}
		_MetallicRange("Metallic Slider",Range(0.0,1.0))=0.0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard 

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 5.0



	sampler2D _MainTex;
	sampler2D _RingTex;
	sampler2D _Metallic;
	sampler2D _Normal;


	struct Input {
		float2 uv_MainTex;
		float2 uv_Metallic;
		float2 uv_Normal;
		float3 worldPos;
	};

	// The size of these arrays is the number of rings that can be rendered at once.
	// If you want to change this, you must also change QueueSize in SimpleSonarShader_Parent.cs
	half4 _hitPts[20];
	half _StartTime;
	half _Intensity[20];


	fixed4 _Color;
	fixed4 _RingColor;
	half _RingColorIntensity;
	half _RingSpeed;
	half _RingWidth;
	half _RingIntensityScale;
	half _MetallicRange;


	void surf(Input IN, inout SurfaceOutputStandard o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		fixed4 metal = tex2D(_Metallic, IN.uv_Metallic);
        o.Metallic =  metal.r;
        o.Smoothness = metal.a * _MetallicRange;
		o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));
		half DiffFromRingCol = abs(o.Albedo.r - _RingColor.r) + abs(o.Albedo.b - _RingColor.b) + abs(o.Albedo.g - _RingColor.g);

		
		for (int i = 0; i < 20; i++) {

			half d = distance(_hitPts[i], IN.worldPos);
			half intensity = _Intensity[i] * _RingIntensityScale;
			half val = (1 - (d / intensity));

			if (d < (_Time.y - _hitPts[i].w) * _RingSpeed && d >(_Time.y - _hitPts[i].w) * _RingSpeed - _RingWidth && val > 0) {
				half posInRing = (d - ((_Time.y - _hitPts[i].w) * _RingSpeed - _RingWidth)) / _RingWidth;

				
				float angle = acos(dot(normalize(IN.worldPos - _hitPts[i]), float3(1,0,0)));
				val *= tex2D(_RingTex, half2(1 - posInRing, angle));
				half3 tmp = _RingColor * val -c * (1 - val);

				
				half tempDiffFromRingCol = abs(tmp.r - _RingColor.r) + abs(tmp.b - _RingColor.b) + abs(tmp.g - _RingColor.g);
				if (tempDiffFromRingCol < DiffFromRingCol)
				{
					// Update values using our predicted ones.
					DiffFromRingCol = tempDiffFromRingCol;
					o.Albedo.r = tmp.r;
					o.Albedo.g = tmp.g;
					o.Albedo.b = tmp.b;
					o.Albedo.rgb *= _RingColorIntensity;
					o.Emission.r = tmp.r;
					o.Emission.g = tmp.g;
					o.Emission.b = tmp.b;
				}
			}
		}

	}

	ENDCG
	}
		FallBack "Diffuse"
}
