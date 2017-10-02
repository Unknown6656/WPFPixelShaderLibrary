sampler2D input : register(S0);
float amount : register(C0);


float4 main(float2 uv : TEXCOORD) : COLOR
{
	float f = saturate(amount);
   	float4 color = tex2D(input, uv);
   
   	return float4(color.a - color.rgb, color.a) * f + color * (1 - f);
}
