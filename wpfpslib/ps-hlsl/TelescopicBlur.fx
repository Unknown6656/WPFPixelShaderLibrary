// from Affirma Consulting Blog
// http://affirmaconsulting.wordpress.com/2010/12/30/tool-for-developing-hlsl-pixel-shaders-for-wpf-and-silverlight/

sampler2D input : register(s0);
float2 center : register(C0);
float amount : register(C1);
float radius : register(C2);

#define ITERATIONS 48

float4 main(float2 uv : TEXCOORD) : COLOR
{
    uv -= center;
    
    float edge = saturate(radius) * 4 + .5;
    float dist = pow(pow(uv.x, edge) + pow(uv.y, edge),2);
    float a = saturate(amount) * 3;
    float4 c = 0;
    
    [unroll(ITERATIONS)] for(int i = 0; i < ITERATIONS; ++i)
    {
        float scale = 1.0 - ((dist * a * i * 17.0) / (30.0 * ITERATIONS));
        float2 coord = uv * scale;
        
        c += tex2D(input, coord + center);
    }
    
    return c / ITERATIONS;
}
