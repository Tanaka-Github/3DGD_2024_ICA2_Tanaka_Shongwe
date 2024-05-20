Shader "Custom/UVModifier"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Speed("Speed", float) = 1.0
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert

            struct Input
            {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            float _Speed;

            void surf(Input IN, inout SurfaceOutput o)
            {
                float2 offset = float2(sin(_Time.y * _Speed), cos(_Time.x * _Speed));
                IN.uv_MainTex += offset;
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}