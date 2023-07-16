RWStructuredBuffer<float> KDivOutputBuffer;

[numthreads(TN, TN, 1)]
void KDivMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float s1 = InputBuffer0[index];
	float s2 = InputBuffer1[index];

	KDivOutputBuffer[index] = s1 / s2;
}