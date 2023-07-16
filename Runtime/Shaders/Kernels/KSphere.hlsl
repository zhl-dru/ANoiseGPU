RWStructuredBuffer<float> KSphereOutputBuffer;

[numthreads(TN, TN, 1)]
void KSphereMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float2 coord = Coord2Buffer[index];
	float cx = InputBuffer0[index];
	float cy = InputBuffer0[index];
	float rad = InputBuffer4[index];

	float dx = coord.x - cx;
	float dy = coord.y - cy;
	float len = sqrt(dx * dx + dy * dy);
	float ii = (rad - len) / rad;
	if (ii < 0)ii = 0;
	if (ii > 1)ii = 1;

	KSphereOutputBuffer[index] = ii;
}

[numthreads(TN, TN, 1)]
void KSphereMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float3 coord = Coord3Buffer[index];
	float cx = InputBuffer0[index];
	float cy = InputBuffer1[index];
	float cz = InputBuffer2[index];
	float rad = InputBuffer4[index];

	float dx = coord.x - cx;
	float dy = coord.y - cy;
	float dz = coord.z - cz;
	float len = sqrt(dx * dx + dy * dy + dz * dz);
	float ii = (rad - len) / rad;
	if (ii < 0)ii = 0;
	if (ii > 1)ii = 1;

	KSphereOutputBuffer[index] = ii;
}

[numthreads(TN, TN, 1)]
void KSphereMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float4 coord = Coord4Buffer[index];
	float cx = InputBuffer0[index];
	float cy = InputBuffer1[index];
	float cz = InputBuffer2[index];
	float cw = InputBuffer3[index];
	float rad = InputBuffer4[index];

	float dx = coord.x - cx;
	float dy = coord.y - cy;
	float dz = coord.z - cz;
	float dw = coord.w - cw;
	float len = sqrt(dx * dx + dy * dy + dz * dz + dw * dw);
	float ii = (rad - len) / rad;
	if (ii < 0)ii = 0;
	if (ii > 1)ii = 1;

	KSphereOutputBuffer[index] = ii;
}