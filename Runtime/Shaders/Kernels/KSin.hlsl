RWStructuredBuffer<float> KSinOutputBuffer;


[numthreads(TN, TN, 1)]
void KSinMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];
	KSinOutputBuffer[index] = sin(source);
}