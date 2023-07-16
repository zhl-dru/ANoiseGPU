RWStructuredBuffer<float> KGainOutputBuffer;


[numthreads(TN, TN, 1)]
void KGainMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float s = InputBuffer0[index];
	float g = InputBuffer1[index];

	if (s < 0.5)
	{
		KGainOutputBuffer[index] = pow(abs(s * 2.0), log(1.0 - g) / log(0.5));
	}
	else
	{
		KGainOutputBuffer[index] = 1.0 - (pow(abs(2.0 - (s * 2.0)), log(1.0 - g) / log(0.5)) / 2.0);
	}
}