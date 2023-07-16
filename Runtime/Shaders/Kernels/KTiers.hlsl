RWStructuredBuffer<float> KTiersOutputBuffer;

int tiers_numtiers;
bool tiers_smooth;

[numthreads(TN, TN, 1)]
void KTiersMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float val = InputBuffer0[index];

	int numsteps = tiers_numtiers;
	if (tiers_smooth) --numsteps;
	float tb = floor(val * numsteps);
	float tt = tb + 1.0;
	float t = val * numsteps - tb;
	tb /= numsteps;
	tt /= numsteps;
	float u = tiers_smooth ? fade(t) : 0.0;

	KTiersOutputBuffer[index] = tb + u * (tt - tb);
}