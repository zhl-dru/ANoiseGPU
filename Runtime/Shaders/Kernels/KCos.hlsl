RWStructuredBuffer<float> KCosOutputBuffer;

[numthreads(TN, TN, 1)]
void KCosMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];

	KCosOutputBuffer[index] = cos(source);
}
