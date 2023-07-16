RWStructuredBuffer<float> KClampOutputBuffer;

float clamp_low, clamp_high;

[numthreads(TN, TN, 1)]
void KClampMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];

	KClampOutputBuffer[index] = clamp(source, clamp_low, clamp_high);
}