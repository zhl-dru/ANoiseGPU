RWStructuredBuffer<float> KScaleDomainOutputBuffer;

[numthreads(TN, TN, 1)]
void KScaleDomainMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer4[index];
	KScaleDomainOutputBuffer[index] = source;
}

[numthreads(TN, TN, 1)]
void KScaleDomainCoordMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float2 coord = Coord2Buffer[index];
	float sxs = InputBuffer0[index];
	float sys = InputBuffer1[index];

	DomainCoord2Buffer[index] = float2(coord.x * sxs, coord.y * sys);
}

[numthreads(TN, TN, 1)]
void KScaleDomainCoordMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float3 coord = Coord3Buffer[index];
	float sxs = InputBuffer0[index];
	float sys = InputBuffer1[index];
	float szs = InputBuffer2[index];

	DomainCoord3Buffer[index] = float3(coord.x * sxs, coord.y * sys, coord.z * szs);
}

[numthreads(TN, TN, 1)]
void KScaleDomainCoordMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float4 coord = Coord4Buffer[index];
	float sxs = InputBuffer0[index];
	float sys = InputBuffer1[index];
	float szs = InputBuffer2[index];
	float sws = InputBuffer3[index];

	DomainCoord4Buffer[index] = float4(coord.x * sxs, coord.y * sys, coord.z * szs, coord.w * sws);
}