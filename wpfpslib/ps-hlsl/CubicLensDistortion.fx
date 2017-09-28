//http://www.francois-tarlier.com/blog/cubic-lens-distortion-shader/

sampler InputTexture : register(s0);
float DistortionCoefficient : register(C0);
float CubicDistortion : register(C1);
float Zoom : register(C2);

float4 main(float2 inputCoord : TEXCOORD0) : COLOR
{
    float r2 = (inputCoord.x - 0.5) * (inputCoord.x - 0.5) + (inputCoord.y - 0.5) * (inputCoord.y - 0.5);
    float f = CubicDistortion == 0.0 ? DistortionCoefficient : DistortionCoefficient + CubicDistortion * sqrt(r2);

    f *= r2;
    f++;

    float x = f / Zoom * (inputCoord.x - 0.5) + 0.5;
    float y = f / Zoom * (inputCoord.y - 0.5) + 0.5;
    float3 inputDistord = tex2D(InputTexture, float2(x, y));

    return float4(
        inputDistord.r,
        inputDistord.g,
        inputDistord.b,
        1
    );
}