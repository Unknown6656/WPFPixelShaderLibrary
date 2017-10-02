sampler2D input : register(s0);
float2 count : register(C0);


float4 main(float2 uv : TEXCOORD) : COLOR 
{ 
		float2 sz = 1.0 / count;
		float2 offs = uv;
		
		if (floor(offs.y / sz.y) % 2 >= 1)
				offs.x += sz / 2.0;
		
		float2 pos = floor(offs / sz);
		float2 cnt = pos * sz + sz / 2;
		
		cnt = clamp(cnt, 0, 1);
		
		return tex2D(input, cnt);
}
