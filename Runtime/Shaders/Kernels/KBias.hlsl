RWStructuredBuffer<float> KBiasOutputBuffer;

[numthreads(TN, TN, 1)]
void KBiasMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];
	float bias = InputBuffer1[index];

	KBiasOutputBuffer[index] = pow(abs(source), log(bias) / log(0.5));
}
