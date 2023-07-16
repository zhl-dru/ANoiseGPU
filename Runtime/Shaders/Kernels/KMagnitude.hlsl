RWStructuredBuffer<float> KMagnitudeOutputBuffer;

[numthreads(TN, TN, 1)]
void KMagnitudeMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float x = InputBuffer0[index];
	float y = InputBuffer1[index];
	KMagnitudeOutputBuffer[index] = magnitude(x, y);
}

[numthreads(TN, TN, 1)]
void KMagnitudeMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float x = InputBuffer0[index];
	float y = InputBuffer1[index];
	float z = InputBuffer2[index];
	KMagnitudeOutputBuffer[index] = magnitude(x, y, z);
}

[numthreads(TN, TN, 1)]
void KMagnitudeMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float x = InputBuffer0[index];
	float y = InputBuffer1[index];
	float z = InputBuffer2[index];
	float w = InputBuffer3[index];
	KMagnitudeOutputBuffer[index] = magnitude(x, y, z, w);
}