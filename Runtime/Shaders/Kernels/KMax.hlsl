RWStructuredBuffer<float> KMaxOutputBuffer;

[numthreads(TN, TN, 1)]
void KMaxMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float s1 = InputBuffer0[index];
	float s2 = InputBuffer1[index];

	KMaxOutputBuffer[index] = s1 > s2 ? s1 : s2;
}