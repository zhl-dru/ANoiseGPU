RWStructuredBuffer<float> KBCOutputBuffer;

[numthreads(TN, TN, 1)]
void KBrightContrastMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];
	float bright = InputBuffer1[index];
	float threshold = InputBuffer2[index];
	float factor = InputBuffer3[index];

	KBCOutputBuffer[index] = (source + bright - threshold) * factor + threshold;
}