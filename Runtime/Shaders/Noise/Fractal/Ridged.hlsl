float ridged(float2 coord, int octave, float frequency, float lacunarity, float gain, uint ntype)
{
	float sum = 0.0;
	float amp = 1.0;
	coord *= frequency;

	float previousnoise = 0.0;
	for (int i = 0; i < octave; i++)
	{
		float n = abs(getnoise(coord + i, ntype));
		n = 0.9 - n;
		n = n * n;

		sum += n * amp;
		sum += n * amp * previousnoise;
		previousnoise = n;
		amp *= gain;

		coord *= lacunarity;
	}
	return sum;
}

float ridged(float3 coord, int octave, float frequency, float lacunarity, float gain, uint ntype)
{
	float sum = 0.0;
	float amp = 1.0;
	coord *= frequency;

	float previousnoise = 0.0;
	for (int i = 0; i < octave; i++)
	{
		float n = abs(getnoise(coord + i, ntype));
		n = 0.9 - n;
		n = n * n;

		sum += n * amp;
		sum += n * amp * previousnoise;
		previousnoise = n;
		amp *= gain;

		coord *= lacunarity;
	}
	return sum;
}

float ridged(float4 coord, int octave, float frequency, float lacunarity, float gain, uint ntype)
{
	float sum = 0.0;
	float amp = 1.0;
	coord *= frequency;

	float previousnoise = 0.0;
	for (int i = 0; i < octave; i++)
	{
		float n = abs(getnoise(coord + i, ntype));
		n = 0.9 - n;
		n = n * n;

		sum += n * amp;
		sum += n * amp * previousnoise;
		previousnoise = n;
		amp *= gain;

		coord *= lacunarity;
	}
	return sum;
}