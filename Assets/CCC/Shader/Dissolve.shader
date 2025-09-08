Shader "Mine/Dissolve"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("使用透明度剪裁", Float) = 0
        _DissolveTex("溶解纹理", 2D) = "white" {}
        _DissolveColor("溶解颜色", Color) = (1,1,1)
        [KeywordEnum(Blend, Overlay)] _Mode("溶解模式", Float) = 0
        _Degree("溶解程度", Range(0,1)) = 0
        _Width("溶解区域宽度", Range(0,10)) = 0
        _Softness("溶解柔和", Range(0,1)) = 1
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "Default"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            #include "UITest.cginc" 

            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP
            #pragma multi_compile _MODE_BLEND _MODE_OVERLAY

            sampler2D _MainTex;
            fixed4 _TextureSampleAdd;
            sampler2D _DissolveTex;
            fixed4 _DissolveColor;
            fixed _Degree;
            fixed _Width;
            fixed _Softness;


struct VertData
{
	float4 vertex   : POSITION;
	fixed4 color    : COLOR;
	float2 texcoord : TEXCOORD0;
	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct FragData
{
	float4 vertex   : SV_POSITION;
	fixed4 color : COLOR;
	float2 texcoord  : TEXCOORD0;
	float4 worldPosition : TEXCOORD1;
	UNITY_VERTEX_OUTPUT_STEREO
};

FragData vert(VertData IN)
{
	FragData OUT;
	UNITY_SETUP_INSTANCE_ID(IN);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
	OUT.worldPosition = IN.vertex;
	OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
	OUT.texcoord = IN.texcoord;
	OUT.color = IN.color;
	return OUT;
}

 
            fixed4 frag(FragData IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
				_Width *= 0.1;
				//只要溶解程度degree小于0.01，则将宽度width和柔和度softness设为0，防止溶解程度为0时依然有溶解效果
				fixed value = step(0.01, _Degree);
				_Width *= value;
				_Softness *= value;
	
				//colorFactor 溶解颜色因子，当colorFactor大于0时，代表处在【溶解中】的区域，反之则处在【已溶解】或【未溶解】区域
				float colorFactor = _Width - abs(_Degree - tex2D(_DissolveTex, IN.texcoord).a);
				colorFactor = saturate(colorFactor * 20 / _Softness);
				float alphaFactor = _Width - (_Degree - tex2D(_DissolveTex, IN.texcoord).a);
				alphaFactor = saturate(alphaFactor * 20 / _Softness);

				#if _MODE_BLEND
					//当处于溶解中的区域时，混合溶解色，否则不混合颜色
					color.rgb += _DissolveColor.rgb * colorFactor;
				#endif

				#if _MODE_OVERLAY
					//当处于溶解中的区域时，覆盖溶解色，否则不覆盖颜色
					color.rgb = lerp(color.rgb, _DissolveColor.rgb, colorFactor);
				#endif

				//当处于溶解中、还未溶解时，透明度叠加，否则透明度为0
				color.a *= alphaFactor;
				//当溶解程度为1时，透明度总是为0
				color.a *= (1 - step(1, _Degree));
                // 应用溶解特效
                //color = ApplyDissolve(color, _DissolveColor.rgb, tex2D(_DissolveTex, IN.texcoord).a, _Degree, _Width, _Softness);

                #ifdef UNITY_UI_ALPHACLIP
                clip(color.a - 0.001);
                #endif

                return color;
            }
            ENDCG
        }
    }
}