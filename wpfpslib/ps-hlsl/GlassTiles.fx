// contributed by Fakhruddin Faizal
// http://hdprogramming.blogspot.com/
// modifications by Walt Ritscher and Unknown6656

sampler2D inputSampler : register(s0);
float Tiles : register(C0);
float BevelWidth : register(C1);
float Offset : register(C3);
float4 GroutColor : register(C2);


float4 main(float2 uv : TEXCOORD) : COLOR
{
    float2 newUV1;

    newUV1.xy = uv.xy + tan((Tiles * 2.5) * uv.xy + Offset) * (BevelWidth / 100);

    float4 c1 = tex2D(inputSampler, newUV1);

    if ((newUV1.x < 0) ||
        (newUV1.x > 1) ||
        (newUV1.y < 0) ||
        (newUV1.y > 1))
        c1 = GroutColor;

    c1.a = 1;

    return c1;
}