// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASESampleShaders/NormalExtrusion"
{
	Properties
	{
		_ExtrusionPoint("ExtrusionPoint", Float) = 0
		_ExtrusionAmount("Extrusion Amount", Range( -1 , 20)) = 0.5
		_DistortionScale("DistortionScale", Range( 0 , 1)) = 0
		_Blending("Blending", Range( 0 , 1)) = 0
		_RippleScale("RippleScale", Range( 0 , 1)) = 0
		_RippleSpeed("RippleSpeed", Range( 0 , 1)) = 0
		_SmallWaves("SmallWaves", 2D) = "bump" {}
		_White("White", Float) = 1
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZTest LEqual
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float4 screenPos;
		};

		uniform float3 _Vector0;
		uniform sampler2D _GrabTexture;
		uniform sampler2D _SmallWaves;
		uniform float _RippleScale;
		uniform float _RippleSpeed;
		uniform float _DistortionScale;
		uniform float _White;
		uniform float _Blending;
		uniform float _ExtrusionPoint;
		uniform float _ExtrusionAmount;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			v.vertex.xyz += ( ase_vertexNormal * max( ( sin( ( ( ase_vertex3Pos.y + _Time.x ) / _ExtrusionPoint ) ) / _ExtrusionAmount ) , 0.0 ) );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Vector0;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPos35 = ase_screenPos;
			#if UNITY_UV_STARTS_AT_TOP
			float scale35 = -1.0;
			#else
			float scale35 = 1.0;
			#endif
			float halfPosW35 = ase_screenPos35.w * 0.5;
			ase_screenPos35.y = ( ase_screenPos35.y - halfPosW35 ) * _ProjectionParams.x* scale35 + halfPosW35;
			ase_screenPos35.xyzw /= ase_screenPos35.w;
			float4 screenColor32 = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD( ( float4( ( UnpackNormal( tex2D( _SmallWaves, ( _RippleScale * (( ( _Time.y * _RippleSpeed ) + ase_screenPos35 )).xxxx ).xy ) ) * _DistortionScale ) , 0.0 ) + ase_screenPos35 ) ) );
			float4 temp_cast_2 = (_White).xxxx;
			float4 lerpResult31 = lerp( screenColor32 , temp_cast_2 , _Blending);
			o.Emission = lerpResult31.rgb;
			o.Metallic = lerpResult31.r;
			o.Smoothness = lerpResult31.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13701
235;34;915;413;1163.701;351.7981;1.3;False;False
Node;AmplifyShaderEditor.TimeNode;43;-2418.226,-282.6647;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;44;-2473.528,-57.51469;Float;False;Property;_RippleSpeed;RippleSpeed;3;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-2147.968,-155.065;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0;False;1;FLOAT
Node;AmplifyShaderEditor.GrabScreenPosition;35;-1532.202,-16.14916;Float;False;0;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-1871.968,-141.0651;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;FLOAT4;0;False;1;FLOAT4
Node;AmplifyShaderEditor.PosVertexDataNode;18;-980.0377,98.29941;Float;False;0;0;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SwizzleNode;40;-2031.968,-357.0651;Float;False;FLOAT4;0;0;0;0;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.TimeNode;25;-981.2358,327.3;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;37;-2148.674,-554.7599;Float;False;Property;_RippleScale;RippleScale;3;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;21;-767.0372,392.2995;Float;False;Property;_ExtrusionPoint;ExtrusionPoint;-1;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-776.0372,190.0994;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-1771.487,-458.7546;Float;False;2;2;0;FLOAT;0,0,0;False;1;FLOAT4;0;False;1;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;39;-1413.438,-279.7302;Float;False;Property;_DistortionScale;DistortionScale;3;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;36;-1532.788,-526.955;Float;True;Property;_SmallWaves;SmallWaves;4;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;19;-624.0369,223.2994;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;5.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;3;-550.2001,333.0997;Float;False;Property;_ExtrusionAmount;Extrusion Amount;-1;0;0.5;-1;20;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-1124.433,-389.379;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SinOpNode;20;-479.9368,132.2993;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;24;-299.237,166.3993;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;10.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-786.576,-238.4591;Float;False;2;2;0;FLOAT3;0.0,0,0,0;False;1;FLOAT4;0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;27;-651.3422,-3.098799;Float;False;Property;_Blending;Blending;3;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;30;-546.0428,-100.5996;Float;False;Property;_White;White;4;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.NormalVertexDataNode;2;-237.2496,-72.3555;Float;False;0;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ScreenColorNode;32;-588.9422,-279.9994;Float;False;Global;_GrabScreen0;Grab Screen 0;4;0;Object;-1;False;1;0;FLOAT4;0,0,0,0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMaxOpNode;26;-140.5377,221.3996;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;31;-226.2433,-255.2996;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;3.5,1;Float;False;2;2;0;FLOAT3;0.0,0,0;False;1;FLOAT;0.0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.Vector3Node;45;-145.1167,-445.2906;Float;False;Property;_Vector0;Vector 0;7;0;0,0,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;154,-266;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;ASESampleShaders/NormalExtrusion;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;3;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0.0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;42;0;43;2
WireConnection;42;1;44;0
WireConnection;41;0;42;0
WireConnection;41;1;35;0
WireConnection;40;0;41;0
WireConnection;22;0;18;2
WireConnection;22;1;25;1
WireConnection;38;0;37;0
WireConnection;38;1;40;0
WireConnection;36;1;38;0
WireConnection;19;0;22;0
WireConnection;19;1;21;0
WireConnection;34;0;36;0
WireConnection;34;1;39;0
WireConnection;20;0;19;0
WireConnection;24;0;20;0
WireConnection;24;1;3;0
WireConnection;33;0;34;0
WireConnection;33;1;35;0
WireConnection;32;0;33;0
WireConnection;26;0;24;0
WireConnection;31;0;32;0
WireConnection;31;1;30;0
WireConnection;31;2;27;0
WireConnection;4;0;2;0
WireConnection;4;1;26;0
WireConnection;0;0;45;0
WireConnection;0;2;31;0
WireConnection;0;3;31;0
WireConnection;0;4;31;0
WireConnection;0;11;4;0
ASEEND*/
//CHKSM=463D3FFB612EB992032F616E430D1982853AA3EF