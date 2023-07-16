RWStructuredBuffer<float> KGradientOutputBuffer;

float gradient_gx, gradient_gy, gradient_gz, gradient_gw;
float gradient_x, gradient_y, gradient_z, gradient_w;
float gradient_vlen;

[numthreads(TN, TN, 1)]
void KGradientMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float2 coord = Coord2Buffer[index];
	KGradientOutputBuffer[index] = (((coord.x - gradient_gx) * gradient_x) + ((coord.y - gradient_gy) * gradient_y)) / gradient_vlen;
}

[numthreads(TN, TN, 1)]
void KGradientMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float3 coord = Coord3Buffer[index];
	KGradientOutputBuffer[index] = ((coord.x - gradient_gx) * gradient_x + (coord.y - gradient_gy) * gradient_y + (coord.z - gradient_gz) * gradient_z) / gradient_vlen;
}

[numthreads(TN, TN, 1)]
void KGradientMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float4 coord = Coord4Buffer[index];
	KGradientOutputBuffer[index] = ((coord.x - gradient_gx) * gradient_x + (coord.y - gradient_gy) * gradient_y + (coord.z - gradient_gz) * gradient_z + (coord.w - gradient_gw) * gradient_w) / gradient_vlen;
}