RWStructuredBuffer<float> KNormalizeCoordsOutputBuffer;

[numthreads(TN, TN, 1)]
void KNormalizeCoordsMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer1[index];

	KNormalizeCoordsOutputBuffer[index] = source;
}

[numthreads(TN, TN, 1)]
void KNormalizeCoordsMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float2 coord = Coord2Buffer[index];
	float lv = InputBuffer0[index];

	float x = coord.x;
	float y = coord.y;

	if (x == 0 && y == 0)
	{
		DomainCoord2Buffer[index] = float2(0, 0);
	}
	else
	{
		float len = magnitude(x, y);
		DomainCoord2Buffer[index] = float2(x / len * lv, y / len * lv);
	}
}

[numthreads(TN, TN, 1)]
void KNormalizeCoordsMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float3 coord = Coord3Buffer[index];
	float lv = InputBuffer0[index];

	float x = coord.x;
	float y = coord.y;
	float z = coord.z;

	if (x == 0 && y == 0 && z == 0)
	{
		DomainCoord3Buffer[index] = float3(0, 0, 0);
	}
	else
	{
		float len = magnitude(x, y, z);
		DomainCoord3Buffer[index] = float3(x / len * lv, y / len * lv, z / len * lv);
	}
}

[numthreads(TN, TN, 1)]
void KNormalizeCoordsMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float4 coord = Coord4Buffer[index];
	float lv = InputBuffer0[index];

	float x = coord.x;
	float y = coord.y;
	float z = coord.z;
	float w = coord.w;

	if (x == 0 && y == 0 && z == 0 && w == 0)
	{
		DomainCoord4Buffer[index] = float4(0, 0, 0, 0);
	}
	else
	{
		float len = magnitude(x, y, z, w);
		DomainCoord4Buffer[index] = float4(x / len * lv, y / len * lv, z / len * lv, w / len * lv);
	}
}