RWStructuredBuffer<float> KPowOutputBuffer;


[numthreads(TN, TN, 1)]
void KPowMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];
	float power = InputBuffer1[index];

	KPowOutputBuffer[index] = pow(abs(source), power);
}