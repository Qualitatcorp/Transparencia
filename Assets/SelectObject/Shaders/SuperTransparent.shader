Shader "supertransparente" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
    _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _SpecMap ("Specular Map (R)", 2D) = "white" {}
    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}
 
SubShader {
    Tags {"Queue"="TransparentCutout" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 200
   
CGPROGRAM
#pragma surface surf BlinnPhong alpha
//removed from above line - "alphatest:_Cutoff", added "alpha"
// NOTE: Doing it this way prevents ANYTHING from writing to the alpha channel including
// specular which means NO GLOW possible on this shader
#pragma exclude_renderers flash
 
sampler2D _MainTex;
sampler2D _SpecMap;
fixed4 _Color;
half _Shininess;
 
struct Input {
    float2 uv_MainTex;
};
 
void surf (Input IN, inout SurfaceOutput o) {
    fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
    fixed4 spec = tex2D(_SpecMap, IN.uv_MainTex);
    o.Albedo = tex.rgb * _Color.rgb;
    o.Gloss = spec.r * tex.a; // multiply red channel of specular map by alpha for final gloss
    o.Alpha = tex.a * _Color.a;
    o.Specular = _Shininess;
}
ENDCG
}
 
FallBack "Transparent/Cutout/VertexLit"
}