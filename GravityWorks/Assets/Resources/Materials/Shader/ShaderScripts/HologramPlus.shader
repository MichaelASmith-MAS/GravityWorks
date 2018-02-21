// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HologramPlus"
{
	Properties
	{
		_Float1("Float 1", Range( -1 , 20)) = 2.705883
		_Float0("Float 0", Float) = 1.92
		_Hologramcolor("Hologram color", Color) = (0.3973832,0.7720588,0.7410512,0)
		_Speed("Speed", Range( 0 , 100)) = 26
		_ScanLines("Scan Lines", Range( 0 , 10)) = 3
		_Opacity("Opacity", Range( 0 , 1)) = 0.5
		_RimNormalMap("Rim Normal Map", 2D) = "bump" {}
		_RimPower("Rim Power", Range( 0 , 10)) = 5
		_Intensity("Intensity", Range( 1 , 10)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float2 texcoord_0;
			float3 viewDir;
			INTERNAL_DATA
		};

		uniform float4 _Hologramcolor;
		uniform float _ScanLines;
		uniform float _Speed;
		uniform sampler2D _RimNormalMap;
		uniform float _RimPower;
		uniform float _Intensity;
		uniform float _Opacity;
		uniform float _Float0;
		uniform float _Float1;


		float3 mod289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			v.vertex.xyz += ( ase_vertexNormal * max( ( sin( ( ( ase_vertex3Pos.y + _Time.x ) / _Float0 ) ) / _Float1 ) , 0.0 ) );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float4 HologramColor32 = _Hologramcolor;
			float3 ase_worldPos = i.worldPos;
			float Speed156 = _Speed;
			float temp_output_13_0 = sin( ( ( ( _ScanLines * ase_worldPos.y ) + (( 1.0 - ( Speed156 * _Time ) )).x ) * UNITY_PI ) );
			float clampResult15 = clamp( (0.0 + (temp_output_13_0 - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) , 0.0 , 1.0 );
			float4 lerpResult16 = lerp( float4(1,1,1,0) , float4(0,0,0,0) , clampResult15);
			float2 temp_cast_0 = (( ( ase_worldPos.z / 100.0 ) * _Time.x )).xx;
			float simplePerlin2D137 = snoise( temp_cast_0 );
			float myVarName3146 = ( simplePerlin2D137 * temp_output_13_0 );
			float4 temp_cast_1 = (myVarName3146).xxxx;
			float4 ScanLines30 = ( lerpResult16 - temp_cast_1 );
			float3 normalizeResult57 = normalize( i.viewDir );
			float dotResult55 = dot( UnpackNormal( tex2D( _RimNormalMap, ( ( ( Speed156 / 1000.0 ) * _Time ) + float4( i.texcoord_0, 0.0 , 0.0 ) ).xy ) ) , normalizeResult57 );
			float temp_output_60_0 = pow( ( 1.0 - saturate( dotResult55 ) ) , ( 10.0 - _RimPower ) );
			float Rim65 = temp_output_60_0;
			o.Emission = ( ( HologramColor32 * ( ScanLines30 + Rim65 ) ) * _Intensity ).rgb;
			o.Alpha = _Opacity;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = IN.tSpace0.xyz * worldViewDir.x + IN.tSpace1.xyz * worldViewDir.y + IN.tSpace2.xyz * worldViewDir.z;
				surfIN.worldPos = worldPos;
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13701
2683;128;1789;767;1445.135;-216.8605;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;168;-1574.711,-440.4086;Float;False;614.0698;167.2261;Comment;2;6;156;Speed;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1524.711,-388.1825;Float;False;Property;_Speed;Speed;1;0;26;0;100;0;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;170;697.3615,-268.2825;Float;False;2377.06;920.5361;Comment;26;26;157;27;10;8;2;106;105;107;3;144;11;143;150;13;145;137;14;18;15;149;17;16;146;155;30;Scan Lines;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;157;751.6345,346.4541;Float;False;156;0;1;FLOAT
Node;AmplifyShaderEditor.RegisterLocalVarNode;156;-1194.641,-390.4085;Float;False;Speed;-1;True;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.TimeNode;26;747.3615,450.254;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;169;730.4416,761.2018;Float;False;2344.672;617.4507;Comment;18;58;57;119;55;63;62;64;68;60;65;59;66;163;158;162;167;166;165;Rim;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;975.3997,412.4301;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT4;0;False;1;FLOAT4
Node;AmplifyShaderEditor.GetLocalVarNode;162;780.4416,811.2017;Float;False;156;0;1;FLOAT
Node;AmplifyShaderEditor.WorldPosInputsNode;2;990.5686,226.6762;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;8;1133.496,382.9441;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;10;961.0613,133.3299;Float;False;Property;_ScanLines;Scan Lines;2;0;3;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;106;1343.936,270.0212;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;6.06;False;1;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;105;1310.179,386.4001;Float;False;True;False;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;167;1032.16,841.6293;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;1000.0;False;1;FLOAT
Node;AmplifyShaderEditor.TimeNode;165;784.0046,920.0408;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;166;1162.997,895.115;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT4;0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleAddOpNode;3;1536.598,251.2432;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.PiNode;107;1551.431,382.0112;Float;False;1;0;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.WorldPosInputsNode;144;1134.944,-218.2825;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;158;989.0927,1137.359;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TimeNode;143;1047.455,-73.44765;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;58;1544.235,1080.576;Float;False;Tangent;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;1742.608,254.6943;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;150;1382.508,-166.1341;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;100.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;163;1333.539,909.5425;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SinOpNode;13;1897.121,338.9451;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;145;1477.025,-20.2284;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;119;1524.852,856.5836;Float;True;Property;_RimNormalMap;Rim Normal Map;4;0;Assets/AmplifyShaderEditor/Examples/Assets/Textures/SceneTextures/TarpNormal.tif;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.NormalizeNode;57;1698.267,1107.663;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.TFHCRemapNode;14;2105.787,299.3632;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;2;FLOAT;1.0;False;3;FLOAT;0.0;False;4;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.NoiseGeneratorNode;137;1680.947,-60.58369;Float;False;Simplex2D;1;0;FLOAT2;100,100;False;1;FLOAT
Node;AmplifyShaderEditor.DotProductOpNode;55;1877.361,984.7217;Float;False;2;0;FLOAT3;0,0,0,0;False;1;FLOAT3;0.0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;17;2112.385,-69.04982;Float;False;Constant;_Color0;Color 0;2;0;1,1,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;15;2299.734,289.3212;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.PosVertexDataNode;183;-1220.889,276.1791;Float;False;0;0;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TimeNode;182;-1222.087,505.1796;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;149;1934.053,53.56419;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;62;1594.805,1240.172;Float;False;Property;_RimPower;Rim Power;5;0;5;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;18;2110.486,105.6777;Float;False;Constant;_Color1;Color 1;2;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SaturateNode;63;2065.632,1041.276;Float;False;1;0;FLOAT;1.23;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;185;-1016.888,367.9792;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;184;-1007.888,570.1786;Float;False;Property;_Float0;Float 0;-1;0;1.92;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;16;2491.902,118.2426;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleSubtractOpNode;68;2010.642,1168.883;Float;False;2;0;FLOAT;10.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;64;2234.637,1083.575;Float;False;1;0;FLOAT;0;False;1;FLOAT
Node;AmplifyShaderEditor.RegisterLocalVarNode;146;2088.677,-163.0517;Float;False;myVarName3;-1;True;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;186;-864.887,401.1791;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;5.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleSubtractOpNode;155;2677.105,50.65098;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.PowerNode;60;2427.435,1106.975;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;35;-2281.517,-488.6837;Float;False;590.8936;257.7873;Comment;2;32;28;Hologram Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.SinOpNode;188;-720.7866,310.1791;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;187;-791.0499,510.9792;Float;False;Property;_Float1;Float 1;-1;0;2.705883;-1;20;0;1;FLOAT
Node;AmplifyShaderEditor.RegisterLocalVarNode;65;2841.113,1123.13;Float;False;Rim;-1;True;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.GetLocalVarNode;33;-776.9076,-324.7064;Float;False;65;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;28;-2231.517,-438.6837;Float;False;Property;_Hologramcolor;Hologram color;0;0;0.3973832,0.7720588,0.7410512,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.GetLocalVarNode;114;-783.5459,-416.9459;Float;False;30;0;1;COLOR
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;2840.42,67.42638;Float;False;ScanLines;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleAddOpNode;71;-520.487,-338.0593;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.GetLocalVarNode;127;-557.7931,-444.785;Float;False;32;0;1;COLOR
Node;AmplifyShaderEditor.SimpleDivideOpNode;189;-540.0864,344.2791;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;10.0;False;1;FLOAT
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-1963.584,-399.5394;Float;False;HologramColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.NormalVertexDataNode;191;-486.5141,187.5713;Float;False;0;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMaxOpNode;190;-381.3875,399.2794;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;132;-542.2174,-228.0388;Float;False;Property;_Intensity;Intensity;6;0;1;1;10;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-302.4253,-399.4778;Float;False;2;2;0;COLOR;0.0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;133;-142.8775,-291.8895;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;192;-216.3121,305.106;Float;False;2;2;0;FLOAT3;0.0,0,0;False;1;FLOAT;0.0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.GetLocalVarNode;66;2384.412,1263.652;Float;False;32;0;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;2669.835,1122.175;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;49;-317.1664,-56.16708;Float;False;Property;_Opacity;Opacity;3;0;0.5;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;89.8217,-401.0934;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;HologramPlus;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Transparent;0.5;True;True;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;True;2;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;156;0;6;0
WireConnection;27;0;157;0
WireConnection;27;1;26;0
WireConnection;8;0;27;0
WireConnection;106;0;10;0
WireConnection;106;1;2;2
WireConnection;105;0;8;0
WireConnection;167;0;162;0
WireConnection;166;0;167;0
WireConnection;166;1;165;0
WireConnection;3;0;106;0
WireConnection;3;1;105;0
WireConnection;11;0;3;0
WireConnection;11;1;107;0
WireConnection;150;0;144;3
WireConnection;163;0;166;0
WireConnection;163;1;158;0
WireConnection;13;0;11;0
WireConnection;145;0;150;0
WireConnection;145;1;143;1
WireConnection;119;1;163;0
WireConnection;57;0;58;0
WireConnection;14;0;13;0
WireConnection;137;0;145;0
WireConnection;55;0;119;0
WireConnection;55;1;57;0
WireConnection;15;0;14;0
WireConnection;149;0;137;0
WireConnection;149;1;13;0
WireConnection;63;0;55;0
WireConnection;185;0;183;2
WireConnection;185;1;182;1
WireConnection;16;0;17;0
WireConnection;16;1;18;0
WireConnection;16;2;15;0
WireConnection;68;1;62;0
WireConnection;64;0;63;0
WireConnection;146;0;149;0
WireConnection;186;0;185;0
WireConnection;186;1;184;0
WireConnection;155;0;16;0
WireConnection;155;1;146;0
WireConnection;60;0;64;0
WireConnection;60;1;68;0
WireConnection;188;0;186;0
WireConnection;65;0;60;0
WireConnection;30;0;155;0
WireConnection;71;0;114;0
WireConnection;71;1;33;0
WireConnection;189;0;188;0
WireConnection;189;1;187;0
WireConnection;32;0;28;0
WireConnection;190;0;189;0
WireConnection;126;0;127;0
WireConnection;126;1;71;0
WireConnection;133;0;126;0
WireConnection;133;1;132;0
WireConnection;192;0;191;0
WireConnection;192;1;190;0
WireConnection;59;0;60;0
WireConnection;59;1;66;0
WireConnection;0;2;133;0
WireConnection;0;9;49;0
WireConnection;0;11;192;0
ASEEND*/
//CHKSM=214C5ED35913350C384EEECFD1F8343073A60798