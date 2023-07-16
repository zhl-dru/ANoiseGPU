RWStructuredBuffer<float> KTranslateDomainOutputBuffer;


[numthreads(TN, TN, 1)]
void KTranslateDomainMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer4[index];
	KScaleOffsetOutputBuffer[index] = source;
}

[numthreads(TN, TN, 1)]
void KTranslateDomainCoordMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float2 coord = Coord2Buffer[index];
	float axs = InputBuffer0[index];
	float ays = InputBuffer1[index];

	DomainCoord2Buffer[index] = float2(coord.x + axs, coord.y + ays);
}

[numthreads(TN, TN, 1)]
void KTranslateDomainCoordMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float3 coord = Coord3Buffer[index];
	float axs = InputBuffer0[index];
	float ays = InputBuffer1[index];
	float azs = InputBuffer2[index];

	DomainCoord3Buffer[index] = float3(coord.x + axs, coord.y + ays, coord.z + azs);
}

[numthreads(TN, TN, 1)]
void KTranslateDomainCoordMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float4 coord = Coord4Buffer[index];
	float axs = InputBuffer0[index];
	float ays = InputBuffer1[index];
	float azs = InputBuffer2[index];
	float aws = InputBuffer3[index];

	DomainCoord4Buffer[index] = float4(coord.x + axs, coord.y + ays, coord.z + azs, coord.w + aws);
}