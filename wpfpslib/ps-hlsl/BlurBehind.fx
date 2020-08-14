sampler2D input : register(s0);
float2 upperLeftCorner : register(C0);
float2 lowerRightCorner : register(C1);
float2 spacing : register(C2);




const float PI = 3.14159265358979323846;
const float EPSILON = 0.0001;
const float SIGMA = 2;

float gauss(float n)
{
    return (float)((1.0 / sqrt(2 * PI * SIGMA)) * exp(-(n * n) / (2 * SIGMA * SIGMA)));
}

float4 gaussianblur(float2 coord : TEXCOORD0) : COLOR
{
    const int COUNT = 2;
    float total = 0;

    float kernel[] = {
        0.023528, 0.033969, 0.038393, 0.033969, 0.023528,
        0.033969, 0.049045, 0.055432, 0.049045, 0.033969,
        0.038393, 0.055432, 0.062651, 0.055432, 0.038393,
        0.033969, 0.049045, 0.055432, 0.049045, 0.033969,
        0.023528, 0.033969, 0.038393, 0.033969, 0.023528
    };
    total = 1;

    /*
    float kernel[COUNT * 2 + 1];
    for (int i = -COUNT; i <= COUNT; ++i)
    {
        float w = gauss((float)i / COUNT);

        kernel[COUNT + i] = w;
        total += w;
    }
    */

    float4 color = 0;

    for (int y = -COUNT; y <= COUNT; ++y)
        for (int x = -COUNT; x <= COUNT; ++x)
        {
            float2 offs = { spacing.x * x, spacing.y * y };

            color += kernel[COUNT + x] * kernel[COUNT + y] * tex2D(input, coord + offs);
        }

    total *= total;
    color /= total;

    return color;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
    if (uv.x < upperLeftCorner.x || uv.y < upperLeftCorner.y || uv.x > lowerRightCorner.x || uv.y > lowerRightCorner.y)
        return tex2D(input, uv);
    else
        return gaussianblur(uv);
}
