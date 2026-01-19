Shader "Custom/GlassWithRefraction"
{
    Properties
    {
        _Tint("Tint Color", Color) = (0.9,0.95,1,1)
        _EdgeTint("Edge Tint", Color) = (1,1,1,1)
        _Thickness("Thickness (units)", Range(0.0, 1.0)) = 0.2
        _Refraction("Refraction Strength", Range(0.0, 0.1)) = 0.02
        _Roughness("Surface Roughness", Range(0.0,1.0)) = 0.08
        _FresnelPower("Fresnel Power", Range(0.5,6.0)) = 2.0
        _SpecularIntensity("Specular Intensity", Range(0,4)) = 1.2
        _NormalMap("Normal Map", 2D) = "bump" {}
        _DistortionNoise("Distortion/Noise", 2D) = "white" {}
        _NoiseScale("Noise Scale", Range(0.1,10)) = 2.0
        _NoiseSpeed("Noise Speed", Range(0,5)) = 0.2
        _Opacity("Base Opacity", Range(0,1)) = 0.95
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        // Grab screen for simple refraction
        GrabPass { "_GrabTexture" }

        Pass
        {
            Name "FORWARD"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #include "UnityCG.cginc"

            sampler2D _GrabTexture;
            sampler2D _NormalMap;
            sampler2D _DistortionNoise;
            float4 _Tint;
            float4 _EdgeTint;
            float _Thickness;
            float _Refraction;
            float _Roughness;
            float _FresnelPower;
            float _SpecularIntensity;
            float _NoiseScale;
            float _NoiseSpeed;
            float _Opacity;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
                float2 uv : TEXCOORD3;
                float4 screenPos : TEXCOORD4;
            };

            float3 UnpackNormalFromMap(float4 nmap)
            {
                // standard unpack: (0..1)->(-1..1)
                return normalize(nmap.xyz * 2.0 - 1.0);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 worldView = _WorldSpaceCameraPos - o.worldPos;
                o.viewDir = normalize(worldView);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // fetch normal map and perturb normal
                float2 uv = i.uv;
                float3 nmap = UnpackNormalFromMap(tex2D(_NormalMap, uv));
                // transform tangent-space normal to world approx using worldNormal as basis (cheap)
                // (for more accuracy need tangent/binormal)
                float3 finalNormal = normalize( lerp(i.worldNormal, nmap, saturate(1 - _Roughness)) );

                // compute fresnel
                float fresnel = pow(1.0 - saturate(dot(finalNormal, i.viewDir)), _FresnelPower);

                // distortion/noise to fake refraction / imperfections
                float2 noiseUV = uv * _NoiseScale + float2(_Time.y * _NoiseSpeed, -_Time.y * _NoiseSpeed);
                float2 noise = tex2D(_DistortionNoise, noiseUV).rg - 0.5; // -0.5..0.5
                // scale noise by roughness
                float2 distortion = noise * (_Refraction * (1.0 + _Roughness*8.0));

                // Screen-space refraction: offset screen UV by view direction projected
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                // shift based on view dir projected to screen
                float2 dir2 = (i.viewDir.xy) * _Refraction * _Thickness * 10.0;
                float2 refractUV = screenUV + dir2 + distortion * _Thickness;

                // sample grab texture with clamping to viewport
                float4 grabbed = tex2D(_GrabTexture, refractUV);

                // absorption by thickness: tint more in center, lighter at edges (meniscus)
                float3 absorption = lerp(_Tint.rgb, _EdgeTint.rgb, saturate(fresnel * 2.0));
                // final base color = grab * tint * (1 - thickness * factor)
                float3 baseColor = grabbed.rgb * absorption;

                // lighting: approximate specular using Blinn-Phong
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float ndotl = saturate(dot(finalNormal, lightDir));
                float3 halfVec = normalize(lightDir + i.viewDir);
                float spec = pow( saturate(dot(finalNormal, halfVec)), 64.0 * (1.0 - _Roughness) );
                float3 specColor = _SpecularIntensity * spec * _EdgeTint.rgb;

                // combine
                float3 col = baseColor + specColor * fresnel;
                float alpha = saturate(_Opacity - _Thickness * 0.5 + fresnel * 0.3);

                // apply simple fog
                UNITY_APPLY_FOG(i.screenPos, col);

                return float4(col, alpha);
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}
