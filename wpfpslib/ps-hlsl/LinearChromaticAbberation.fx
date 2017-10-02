sampler2D input : register(s0);
float amount : register(C0);
float angle : register(C1);

static const float PI = 3.14159265f;


float4 main(float2 uv : TEXCOORD0) : COLOR
{
    float a = clamp(amount, 0, 1) / 8;
    float f = clamp(angle, 0, 1) * PI * 2;
    float x1 = sin(-f) * a + (uv.x - 0.5) + 0.5;
    float y1 = cos(-f) * a + (uv.y - 0.5) + 0.5;
    float x2 = sin(PI - f) * a + (uv.x - 0.5) + 0.5;
    float y2 = cos(PI - f) * a + (uv.y - 0.5) + 0.5;

    float4 clr = tex2D(input, uv);
    float4 clrd1 = tex2D(input, float2(x1, y1));
    float4 clrd2 = tex2D(input, float2(x2, y2));

    return float4(clrd1.r, clr.g, clrd2.b, 1);
}
