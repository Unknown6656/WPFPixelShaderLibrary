sampler2D inputSampler : register(S0);
float2 Center : register(C0); // [0,0 - 1,1]
float Amplitude : register(C1); // [0 - 1]
float Frequency: register(C2); // [0 - 300]
float Phase: register(C3); // [±π]
float AspectRatio : register(C4); // W * H^-1


float4 main(float2 uv : TEXCOORD) : COLOR
{
		float2 dir = uv - Center;
		
		dir.y /= AspectRatio;
		
		float dist = length(dir);
		
		dir /= dist;
		dir.y *= AspectRatio;
		
		float falloff = saturate(1 - dist);
		float2 wave = {
				sin(Frequency * dist + Phase),
				cos(Frequency * dist + Phase),
		};
		
		falloff *= falloff;
		dist += Amplitude * wave.x * falloff;
		
		float4 color = tex2D(inputSampler,  Center + dist * dir);
		
		float light = 1 - Amplitude * 0.2 * (1 - saturate(wave.y * falloff));
		
		color.rgb *= light;
		
		return color;
}