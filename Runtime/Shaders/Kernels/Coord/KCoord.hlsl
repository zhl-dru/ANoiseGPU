
[numthreads(TN, TN, 1)]
void KOriginCoordMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	Coord2Buffer[index] = getcoord2(id, start, stepsize);
}


[numthreads(TN, TN, 1)]
void KOriginCoordMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	Coord3Buffer[index] = getcoord3(id, start, stepsize);
}


[numthreads(TN, TN, 1)]
void KOriginCoordMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	Coord4Buffer[index] = getcoord4(id, start, stepsize);
}