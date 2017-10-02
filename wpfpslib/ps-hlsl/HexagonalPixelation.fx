sampler2D input : register(S0);
float amount : register(C0);

#define H (clamp(amount, 0.0001, 1) / 2)
#define S ((3.0 /2.0) * H / sqrt(3.0))


float2 GetHexCoord(int2 hex)
{
		int i = hex.x;
		int j = hex.y;
		float2 coord;
		
		coord.x = i * S;
		coord.y = j * H + (i % 2) * H / 2.0;
	
		return coord;
}

int2 GetHexIndex(float2 coord)
{
		int2 hex;
		int it = int(floor(coord.x / S));
		
		float yts = coord.y - float(it % 2) * H / 2.0;
		int jt = int(floor((1.0 / H) * yts));
		
		float xt = coord.x - it * S;
		float yt = yts - jt * H;
		
		int deltaj = (yt > H / 2.0) ? 1 : 0;
		
		float fcond = S * (2.0 / 3.0) * abs(0.5 - yt / H);
		
		if (xt > fcond) {
				hex.x = it;
				hex.y = jt;
		}
		else {
				hex.x = it - 1;
 				hex.y = jt - (hex.x % 2) + deltaj;
		}
		
		return hex;
}

float4 main(float2 uv : TEXCOORD0) : COLOR
{
		int2 hex = GetHexIndex(uv);
		float2 coord = GetHexCoord(hex);
		float4 clr = tex2D(input, coord);
		
		return clr;
}