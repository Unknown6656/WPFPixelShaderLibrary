sampler2D input : register(S0);
float Range : register(C0);


float abs(float f)
{
    return f < 0 ? -f : f;
}

float constr(float f)
{
    return f < 0 ? 0 : f > 1 ? 1 : f;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 clr = tex2D(input, uv);
    float3x3 hmat =
    {
        -1, 0, 1,
        -2, 0, 2,
        -1, 0, 1,
    };
    float3x3 vmat =
    {
        1, 2, 1,
        0, 0, 0,
        -1, -2, -1,
    };
    float sx = 128, sy = 128;
    float r = clamp(Range, 0.000001, 1);
		int fx, fy;

    for (fy = -1; fy <= 1; ++fy)
        for (fx = -1; fx <= 1; ++fx)
        {
            float2 nuv = { uv[0] + fy * r * 0.125f, uv[1] + fx * r * 0.125f };
            float4 nclr = tex2D(input, nuv);
            float gray = (nclr.r + nclr.g + nclr.g) * 255 / 3;

            sx += hmat[fy + 1][fx + 1] * gray;
            sy += vmat[fy + 1][fx + 1] * gray;
        }
        
    float sz = (abs(sx - 128) + abs(sy - 128)) / 4.0f;
    
    return float4(
        constr(sx / 255),
        constr(sy / 255),
        (sz > 64 ? 191 : sz < 0 ? 255 : 255 - sz) / 255,
        1
    );
}
