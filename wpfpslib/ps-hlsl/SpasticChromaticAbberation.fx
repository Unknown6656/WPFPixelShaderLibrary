sampler2D input : register(S0);
float phase : register(C0);
float amount : register(C1);

static const float PI = 3.14159265f;


float4 main(float2 uv : TEXCOORD) : COLOR
{
    float phi = (abs(phase + uv.y) % 1.0) * 20 * PI;
    float f1 = sin(phi) + 0.2 * cos(16 * phi) * sin(2 * phi) + 0.5 * cos(4 * phi);
    float am = saturate(amount);
    float fam = am > 0.7 ? 0.7 - am : am;
    bool inv1 = uv.y % ((1.1 - fam) / 20) < 0.01;
    bool inv2 = uv.y % 0.05 < 0.025;
    bool inv3 = (uv.y + f1) % 0.09 < 0.002;

    if (inv1)
        f1 = -(1 + f1);

    f1 /= 15;

    uv.x = uv.x * (1 - am / 10) + uv.x * f1 * am;

    if (inv1)
        uv.x = saturate(uv.x - am * f1);

    float4 color;

    if (inv2)
    {
        float4 clr = tex2D(input, uv);
        float4 clrd1 = tex2D(input, float2(tan(uv.x - 0.5) + 0.5125, uv.y));
        float4 clrd2 = tex2D(input, float2(tan(uv.x - 0.5) + 0.4875, uv.y));

        color = float4(clrd1.r, clr.g, clrd2.b, 1);
    }

    if (inv3)
        color = tex2D(input, float2((uv.x * uv.x + uv.y + f1) % 1, uv.y));

    float r2 = (uv.x - 0.5) * (uv.x - 0.5) + (uv.y - 0.5) * (uv.y - 0.5);
    float f = 1 + r2 * (0.03 * sqrt(r2));
    float x = f * (uv.x - 0.5) + 0.5;
    float y = f * (uv.y - 0.5) + 0.5;

    float4 clr = tex2D(input, uv);
    float4 clrd = tex2D(input, float2(x, y));

    if (!inv2)
        color = float4(clrd.r, clr.g, clr.b, 1);

    float3 scnColor = normalize(float4(0xc8, 0xf4, 0xf4, 0xff)) * (color.rgb / color.a);
    float gray = dot(float3(0.3, 0.59, 0.11), scnColor);

    float3 muted = lerp(scnColor, gray.xxx, 0.15);
    float3 middle = lerp(float4(0, 0, 0, 1), normalize(float4(0xc8, 0xf4, 0xf4, 0xff)), gray);

    scnColor = lerp(muted * 2, middle, 0.4);

    return float4(scnColor * color.a, color.a) * am / 2 + clr * (1 - am / 2);
}