RWStructuredBuffer<float> KFractalOutputBuffer;

int fractal_seed;
int fractal_ntype;
int fractal_ftype;

int fractal_octave;
float fractal_frequency;
float fractal_lacunarity;
float fractal_gain;


[numthreads(TN, TN, 1)]
void KFractalMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;
	float n = 0.0;
	float2 coord = Coord2Buffer[index];
	coord += fractal_seed;
	switch (fractal_ftype)
	{
	case FBM:
		n = fbm(coord, fractal_octave, fractal_frequency, fractal_lacunarity, fractal_gain, fractal_ntype);
		break;
	case BILLOWY:
		n = billowy(coord, fractal_octave, fractal_frequency, fractal_lacunarity, fractal_gain, fractal_ntype);
		break;
	case RIDGED:
		n = ridged(coord, fractal_octave, fractal_frequency, fractal_lacunarity, fractal_gain, fractal_ntype);
		break;
	}
	KFractalOutputBuffer[index] = n;
}

[numthreads(TN, TN, 1)]
void KFractalMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;
	float n = 0.0;
	float3 coord = Coord3Buffer[index];
	coord += fractal_seed;
	switch (fractal_ftype)
	{
	case FBM:
		n = fbm(coord, fractal_octave, fractal_frequency, fractal_lacunarity, fractal_gain, fractal_ntype);
		break;
	case BILLOWY:
		n = billowy(coord, fractal_octave, fractal_frequency, fractal_lacunarity, fractal_gain, fractal_ntype);
		break;
	case RIDGED:
		n = ridged(coord, fractal_octave, fractal_frequency, fractal_lacunarity, fractal_gain, fractal_ntype);
		break;
	}
	KFractalOutputBuffer[index] = n;
}

[numthreads(TN, TN, 1)]
void KFractalMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;
	float n = 0.0;
	float4 coord = Coord4Buffer[index];
	coord += fractal_seed;
	switch (fractal_ftype)
	{
	case FBM:
		n = fbm(coord, fractal_octave, fractal_frequency, fractal_lacunarity, fractal_gain, fractal_ntype);
		break;
	case BILLOWY:
		n = billowy(coord, fractal_octave, fractal_frequency, fractal_lacunarity, fractal_gain, fractal_ntype);
		break;
	case RIDGED:
		n = ridged(coord, fractal_octave, fractal_frequency, fractal_lacunarity, fractal_gain, fractal_ntype);
		break;
	}
	KFractalOutputBuffer[index] = n;
}
