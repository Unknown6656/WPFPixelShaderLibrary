sampler2D input : register(s0);
/// <defaultValue>0.04</defaultValue>
float density : register(c0);


static const float PI = 3.14159265f;


float2 f(float2 uv, float d)
{
    //return uv + 0.01 * sin(2 * PI * uv / d);
    return uv + tan((PI / 2 + PI * uv) / d) / 100;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float d = clamp(density, 0, 1);
    float2 coord = uv + d - 2 * f(uv % d, d);

    coord %= 1;

    float4 clr = tex2D(input, coord);

    if (clr.a < 1)
    {
        clr.rgb -= clamp(clr.a / 2, 0, 1) + 0.5f;
        clr.a = 1;
    }

    return clr;
}