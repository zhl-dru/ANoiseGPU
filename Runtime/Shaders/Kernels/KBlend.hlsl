RWStructuredBuffer<float> KBlendOutputBuffer;

[numthreads(TN, TN, 1)]
void KBlendMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float control = InputBuffer0[index];
	float low = InputBuffer1[index];
	float high = InputBuffer2[index];
	KBlendOutputBuffer[index] = lerp(low, high, control);
}
