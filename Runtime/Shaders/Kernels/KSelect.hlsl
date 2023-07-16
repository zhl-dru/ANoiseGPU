RWStructuredBuffer<float> KSelectOutputBuffer;


[numthreads(TN, TN, 1)]
void KSelectMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float low = InputBuffer0[index];
	float high = InputBuffer1[index];
	float control = InputBuffer2[index];
	float threshold = InputBuffer3[index];
	float falloff = InputBuffer4[index];

	if (falloff > 0.0)
	{
		if (control < (threshold - falloff))
		{
			KSelectOutputBuffer[index] = low;
		}
		else if (control > (threshold + falloff))
		{
			KSelectOutputBuffer[index] = high;
		}
		else
		{
			float lower = threshold - falloff;
			float upper = threshold + falloff;
			float blend = fade((control - lower) / (upper - lower));
			KSelectOutputBuffer[index] = lerp(low, high, blend);
		}
	}
	else
	{
		if (control < threshold) KSelectOutputBuffer[index] = low;
		else KSelectOutputBuffer[index] = high;
	}
}