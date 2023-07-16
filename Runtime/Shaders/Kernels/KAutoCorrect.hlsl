RWStructuredBuffer<float> KAutoCorrectOutputBuffer;

float ac_low;
float ac_high;
float ac_scale2;
float ac_offset2;
float ac_scale3;
float ac_offset3;
float ac_scale4;
float ac_offset4;

[numthreads(TN, TN, 1)]
void KAutoCorrectMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];

	KAutoCorrectOutputBuffer[index] = max(ac_low, min(ac_high, source * ac_scale2 + ac_offset2));
}

[numthreads(TN, TN, 1)]
void KAutoCorrectMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];

	KAutoCorrectOutputBuffer[index] = max(ac_low, min(ac_high, source * ac_scale3 + ac_offset3));
}

[numthreads(TN, TN, 1)]
void KAutoCorrectMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer0[index];

	KAutoCorrectOutputBuffer[index] = max(ac_low, min(ac_high, source * ac_scale4 + ac_offset4));
}