// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "InvisShader"
{
	Properties
	{
		_Blending("Blending", Range( 0 , 1)) = 0
		_DistortionMap("DistortionMap", 2D) = "bump" {}
		_DistortionScale("DistortionScale", Range( 0 , 1)) = 0
		_RippleScale("RippleScale", Range( 0 , 20)) = 0
		_RippleSpeed("RippleSpeed", Range( 0 , 1)) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 screenPos;
		};

		uniform sampler2D _GrabTexture;
		uniform sampler2D _DistortionMap;
		uniform float _RippleScale;
		uniform float _RippleSpeed;
		uniform float _DistortionScale;
		uniform float _Blending;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = float3(0,0,0);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPos7 = ase_screenPos;
			#if UNITY_UV_STARTS_AT_TOP
			float scale7 = -1.0;
			#else
			float scale7 = 1.0;
			#endif
			float halfPosW7 = ase_screenPos7.w * 0.5;
			ase_screenPos7.y = ( ase_screenPos7.y - halfPosW7 ) * _ProjectionParams.x* scale7 + halfPosW7;
			ase_screenPos7.xyzw /= ase_screenPos7.w;
			float4 screenColor2 = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD( ( float4( ( UnpackNormal( tex2D( _DistortionMap, ( _RippleScale * (( ( _Time.y * _RippleSpeed ) + ase_screenPos7 )).xy ) ) ) * _DistortionScale ) , 0.0 ) + ase_screenPos7 ) ) );
			float4 temp_cast_1 = (1.0).xxxx;
			float4 lerpResult3 = lerp( screenColor2 , temp_cast_1 , _Blending);
			o.Emission = lerpResult3.rgb;
			o.Metallic = lerpResult3.r;
			o.Smoothness = lerpResult3.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13701
2794;92;1269;488;1062.564;392.3266;2.345804;False;False
Node;AmplifyShaderEditor.RangedFloatNode;17;-993.1657,77.83988;Float;False;Property;_RippleSpeed;RippleSpeed;4;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.TimeNode;11;-919.5135,-87.34866;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-683.7664,14.14009;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0,0;False;1;FLOAT
Node;AmplifyShaderEditor.GrabScreenPosition;7;-659.6015,478.7556;Float;False;0;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-462.3949,103.0676;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;FLOAT4;0;False;1;FLOAT4
Node;AmplifyShaderEditor.SwizzleNode;12;-306.403,-13.46894;Float;False;FLOAT2;0;1;2;3;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.RangedFloatNode;14;-453.6638,-197.7597;Float;False;Property;_RippleScale;RippleScale;3;0;0;0;20;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-137.7653,-121.0596;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT2;0;False;1;FLOAT2
Node;AmplifyShaderEditor.RangedFloatNode;10;97.60957,50.06079;Float;False;Property;_DistortionScale;DistortionScale;2;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;6;32.78445,-215.0082;Float;True;Property;_DistortionMap;DistortionMap;1;0;Assets/InvisShader/SmallWaves.png;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;423.1429,-47.94216;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;8;616.8543,224.825;Float;False;2;2;0;FLOAT3;0,0,0,0;False;1;FLOAT4;0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.ScreenColorNode;2;874.9144,70.19635;Float;False;Global;_GrabScreen0;Grab Screen 0;0;0;Object;-1;False;1;0;FLOAT4;0,0,0,0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;4;907.3844,268.8913;Float;False;Constant;_White;White;0;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;5;849.3844,353.8913;Float;False;Property;_Blending;Blending;0;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.Vector3Node;1;1033.4,-345.8996;Float;False;Constant;_Vector0;Vector 0;0;0;0,0,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;3;1122.384,161.8913;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1328,-115;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;InvisShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Translucent;0.5;True;True;0;False;Opaque;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;11;2
WireConnection;16;1;17;0
WireConnection;15;0;16;0
WireConnection;15;1;7;0
WireConnection;12;0;15;0
WireConnection;13;0;14;0
WireConnection;13;1;12;0
WireConnection;6;1;13;0
WireConnection;9;0;6;0
WireConnection;9;1;10;0
WireConnection;8;0;9;0
WireConnection;8;1;7;0
WireConnection;2;0;8;0
WireConnection;3;0;2;0
WireConnection;3;1;4;0
WireConnection;3;2;5;0
WireConnection;0;0;1;0
WireConnection;0;2;3;0
WireConnection;0;3;3;0
WireConnection;0;4;3;0
ASEEND*/
//CHKSM=2F26485E8AD668E4F7DF4B0A1C199B57B46A4F8C