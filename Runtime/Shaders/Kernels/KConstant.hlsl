RWStructuredBuffer<float> KConstantOutputBuffer;
float constant_value;

[numthreads(TN, TN, 1)]
void KConstantMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	KConstantOutputBuffer[index] = constant_value;
}