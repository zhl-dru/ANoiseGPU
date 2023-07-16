

float fbm(float2 coord, int octave, float frequency, float lacunarity, float gain, uint ntype)
{
	float sum = 0.0;
	float amp = 1.0;
	coord *= frequency;

	for (int i = 0; i < octave; i++)
	{
		float n = getnoise(coord + i, ntype);
		sum += n * amp;
		amp *= gain;

		coord *= lacunarity;
	}
	return sum;
}

float fbm(float3 coord, int octave, float frequency, float lacunarity, float gain, uint ntype)
{
	float sum = 0.0;
	float amp = 1.0;
	coord *= frequency;

	for (int i = 0; i < octave; i++)
	{
		float n = getnoise(coord + i, ntype);
		sum += n * amp;
		amp *= gain;

		coord *= lacunarity;
	}
	return sum;
}

float fbm(float4 coord, int octave, float frequency, float lacunarity, float gain, uint ntype)
{
	float sum = 0.0;
	float amp = 1.0;
	coord *= frequency;

	for (int i = 0; i < octave; i++)
	{
		float n = getnoise(coord + i, ntype);
		sum += n * amp;
		amp *= gain;

		coord *= lacunarity;
	}
	return sum;
}