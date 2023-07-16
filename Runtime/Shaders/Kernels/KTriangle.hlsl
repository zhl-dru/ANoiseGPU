RWStructuredBuffer<float> KTriangleOutputBuffer;

[numthreads(TN, TN, 1)]
void KTriangleMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float val = InputBuffer0[index];
	float period = InputBuffer1[index];
	float offset = InputBuffer2[index];

	if (offset >= 1) KTriangleOutputBuffer[index] = sawtooth(val, period);
	else if (offset <= 0) KTriangleOutputBuffer[index] = 1.0 - sawtooth(val, period);
	else
	{
		float s1 = (offset - sawtooth(val, period)) >= 0 ? 1.0 : 0.0;
		float s2 = (1.0 - offset - (sawtooth(-val, period))) >= 0 ? 1.0 : 0.0;
		KTriangleOutputBuffer[index] = sawtooth(val, period) * s1 / offset + (sawtooth(-val, period) * s2 / (1.0 - offset));
	}
}