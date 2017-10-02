sampler2D input : register(s0);
float2 position : register(C0);
float aspectratio : register(C1);
float radius : register(C2);
float dist : register(C3);
float size : register(C4);


float4 main(float2 uv : TEXCOORD) : COLOR 
{
		float irad = saturate(radius / 100);
		float2 offs = uv - position;
		float2 ratio = { clamp(aspectratio, 0.001, 1000), 1 };
		float rad = length(offs / ratio);
		
		if (rad < irad * 6 * saturate(size))
				return float4(0, 0, 0, 1);
		else
    {
				float defm = 2 * irad / pow(rad * pow(dist, 0.5), 1.7); 
				
				offs *= 1 - defm; 
				offs += position;
				
				return tex2D(input, offs);
    }
}