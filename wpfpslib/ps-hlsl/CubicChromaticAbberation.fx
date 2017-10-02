// Original Lens Distortion Algorithm from SSontech (Syntheyes)
// http://www.ssontech.com/content/lensalg.htm
// Modified by François Tarlier and Unknown6665

sampler2D input : register(s0);


float4 main(float2 uv : TEXCOORD0) : COLOR
{
    float k = -0.15;
    float kcube = 0.5;
    float r2 = (uv.x - 0.5) * (uv.x - 0.5) + (uv.y - 0.5) * (uv.y - 0.5);
    float f = 1 + r2 * (k + kcube * sqrt(r2));
    float x = f * (uv.x - 0.5) + 0.5;
    float y = f * (uv.y - 0.5) + 0.5;

    float4 clr = tex2D(input, uv);
    float4 clrd = tex2D(input, float2(x, y));

    return float4(clrd.r, clr.g, clr.b, 1);
}
