//参考小丑牌Balatro/resources/shaders/background.fs
Shader "Balatro/BalatroBg"
{
    Properties 
    {
        [PerRendererData] _MainTex("MainTex", 2D) = "white" {} // 主纹理
        [Header(ScreenRatio)]
        _screenRatio("Screen RatioXY", vector) = (1, 0.5625, 0, 0) // 屏幕比例
        [Header(ColorControl)]
        _color1("Color1", Color) = (0.0, 0.6159, 1, 1) // 颜色1
        _color2("Color2", Color) = (1, 1, 1, 1) // 颜色2
        _color3("Black Color3", Color) = (0.3098, 0.3882, 0.4039, 1) // 颜色3
        [Header(Transition)]
        _vortSpeed("Vort Speed", float) = 1 // 旋涡速度
        _vortScale("Vort Scale", float) = 30 // 旋涡缩放
        _vortOffset("Vort Offset", float) = 0 // 旋涡偏移
        _midFlash("Mid Flash", float) = 0 // 中间闪光
        [Header(Pixel)]
        [Toggle] _PIXELIT("Pixel It", int) = 0 // 像素化开关
        _pixelSize("Pixel Size", float) = 1 // 像素大小
    }
    SubShader 
    {
        Tags { "QUEUE" = "Transparent" "RenderPipeline" = "UniversalPipeline" }
        Cull Off // 关闭背面剔除

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature _PIXELIT_ON // 根据是否启用像素化效果，编译不同的代码路径

            // 包含一些Unity的Shader库文件，用于访问颜色、核心功能、光照和Shader图功能等
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl" 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            struct Attributes
            {
                float3 positionOS : POSITION; // 顶点位置
                float2 uv : TEXCOORD0; // 纹理坐标
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0; // 传递给片元着色器的纹理坐标
                float4 positionCS : SV_POSITION; // 顶点在裁剪空间中的位置
            };

            CBUFFER_START(UnityPerMaterial) // 定义材质相关的常量缓冲区
                float4 _color1, _color2, _color3; // 颜色参数
                float4 _screenRatio; // 屏幕比例
                float _midFlash, _vortOffset, _vortSpeed, _vortScale; // 旋涡和闪光效果的参数
                float _pixelSize; // 像素大小
            CBUFFER_END

            TEXTURE2D(_MainTex); // 定义纹理
            SAMPLER(sampler_MainTex); // 定义纹理采样器
            #define T _Time.y // 定义时间变量

            half4 PaintEffect(half2 screen_coords)
            {
                half2 uv = (screen_coords - 0.5) * _screenRatio; // 转换屏幕坐标到纹理坐标

                #if _PIXELIT_ON // 如果启用了像素化效果
                half2 pixelSize = max(0.001, _pixelSize) / _ScreenParams.xy; // 计算像素大小
                uv = floor(uv / pixelSize) * pixelSize; // 应用像素化效果
                #endif

                half uv_len = length(uv); // 计算纹理坐标的长度

                // 添加旋涡效果
                half speed = T * _vortSpeed;
                half new_pixel_angle = atan2(uv.y, -uv.x) + (2.2 + 0.4 * min(6., speed)) * uv_len - min(6., speed) * speed * 0.02 + _vortOffset;
                half2 sv = half2(uv_len * cos(new_pixel_angle), uv_len * sin(new_pixel_angle));

                // 应用扭曲效果
                sv *= _vortScale;
                for (int i = 0; i < 5; i++)
                {
                    sv += 0.5 * half2(cos(5.1123314 + 0.353 * sv.y + speed * 0.131121), sin(sv.x - 0.113 * speed));
                    sv -= 1.0 * cos(sv.x + sv.y) - 1.0 * sin(sv.x * 0.711 - sv.y);
                }

                // 计算烟雾效果
                half smoke_res = min(2, max(-2, 1.5 + length(sv) * 0.12 - 0.17 * (min(10, T * 3.2 - 4))));
                if (smoke_res < 0.2)
                {
                    smoke_res = (smoke_res - 0.2) * 0.6 + 0.2;
                }

                // 颜色混合
                half c1p = max(0., 1. - 2. * abs(1. - smoke_res));
                half c2p = max(0., 1. - 2. * smoke_res);
                half cb = 1. - min(1., c1p + c2p);
                half4 ret_col = _color1 * c1p + _color2 * c2p + half4(cb * _color3.rgb * 0.6, cb * _color1.a);

                // 计算中间闪光效果
                half mod_flash = max(_midFlash * 0.8, max(c1p, c2p) * 5. - 4.4) + _midFlash * max(c1p, c2p);
                return lerp(ret_col, 1, mod_flash); // 返回最终颜色
            }

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.uv = v.uv;
                o.positionCS = TransformObjectToHClip(v.positionOS); // 将顶点坐标转换为裁剪空间坐标
                return o;
            }

            half4 frag(Varyings i) : SV_TARGET
            {
                half2 ScreenUv = i.positionCS.xy / _ScreenParams.xy; // 计算屏幕坐标
                half4 finalCol = PaintEffect(ScreenUv); // 计算最终颜色
                return finalCol;
            }

            ENDHLSL
        }
    }
}