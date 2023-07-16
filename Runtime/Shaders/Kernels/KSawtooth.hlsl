RWStructuredBuffer<float> KSawtoothOutputBuffer;


[numthreads(TN, TN, 1)]
void KSawtoothMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float s = InputBuffer0[index];
	float p = InputBuffer1[index];

	KSawtoothOutputBuffer[index] = (s / p - floor(s / p + 0.5)) * 2.0;
}