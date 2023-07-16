RWStructuredBuffer<float> KScaleOffsetOutputBuffer;


[numthreads(TN, TN, 1)]
void KScaleOffsetMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];
	float scale = InputBuffer1[index];
	float offset = InputBuffer2[index];

	KScaleOffsetOutputBuffer[index] = source * scale + offset;
}