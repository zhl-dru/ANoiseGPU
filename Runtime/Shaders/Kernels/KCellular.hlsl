RWStructuredBuffer<float> KCellularOutputBuffer;

float cellular_seed;
float cellular_jitter;
float cellular_frequency;
int cellular_octaves;
float cellular_lacunarity;
float cellular_gain;

int cellular_ftype;

[numthreads(TN, TN, 1)]
void KCellularMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float2 coord = Coord2Buffer[index];
	coord += cellular_seed;

	float n = 0.0;
	switch (cellular_ftype)
	{
	case 0:
		n = fBm_F0(coord,cellular_octaves,cellular_frequency,cellular_jitter,cellular_lacunarity,cellular_gain);
		break;
	case 1:
		n = fBm_F1_F0(coord,cellular_octaves,cellular_frequency,cellular_jitter,cellular_lacunarity,cellular_gain);
		break;
	}

	KCellularOutputBuffer[index] = n;
}

[numthreads(TN, TN, 1)]
void KCellularMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float3 coord = Coord3Buffer[index];
	coord += cellular_seed;
	coord *= cellular_frequency;

	float2 i = inoise(coord, cellular_jitter);

	float n = 0.0;
	switch (cellular_ftype)
	{
	case 0:
		n = sqrt(i.x);
		break;
	case 1:
		n = sqrt(i.y) - sqrt(i.x);
		break;
	}

	KCellularOutputBuffer[index] = n;
}

[numthreads(TN, TN, 1)]
void KCellularMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float4 coord = Coord4Buffer[index];
	coord += cellular_seed;
	coord *= cellular_frequency;

	float2 i = inoise(coord, cellular_jitter);

	float n = 0.0;
	switch (cellular_ftype)
	{
	case 0:
		n = sqrt(i.x);
		break;
	case 1:
		n = sqrt(i.y) - sqrt(i.x);
		break;
	}

	KCellularOutputBuffer[index] = n;
}