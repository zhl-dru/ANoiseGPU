RWStructuredBuffer<float> KFGOutputBuffer;

float fg_spacing;
int fg_axis;

[numthreads(TN, TN, 1)]
void KFunctionGradientMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float v1 = InputBuffer0[index];
	float v2 = InputBuffer1[index];
	KFGOutputBuffer[index] = (v1 - v2) * (1 / fg_spacing);
}


[numthreads(TN, TN, 1)]
void KGradientCoordMinusMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float2 coord = Coord2Buffer[index];
	switch (fg_axis)
	{
	case 0:
		DomainCoord2Buffer[index] = float2(coord.x - fg_spacing, coord.y);
		break;
	case 1:
		DomainCoord2Buffer[index] = float2(coord.x, coord.y - fg_spacing);
		break;
	default:
		DomainCoord2Buffer[index] = float2(0, 0);
		break;
	}
}


[numthreads(TN, TN, 1)]
void KGradientCoordMinusMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float3 coord = Coord3Buffer[index];
	switch (fg_axis)
	{
	case 0:
		DomainCoord3Buffer[index] = float3(coord.x - fg_spacing, coord.y, coord.z);
		break;
	case 1:
		DomainCoord3Buffer[index] = float3(coord.x, coord.y - fg_spacing, coord.z);
		break;
	case 2:
		DomainCoord3Buffer[index] = float3(coord.x, coord.y, coord.z - fg_spacing);
		break;
	default:
		DomainCoord3Buffer[index] = float3(0, 0, 0);
		break;
	}
}


[numthreads(TN, TN, 1)]
void KGradientCoordMinusMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float4 coord = Coord4Buffer[index];
	switch (fg_axis)
	{
	case 0:
		DomainCoord4Buffer[index] = float4(coord.x - fg_spacing, coord.y, coord.z, coord.w);
		break;
	case 1:
		DomainCoord4Buffer[index] = float4(coord.x, coord.y - fg_spacing, coord.z, coord.w);
		break;
	case 2:
		DomainCoord4Buffer[index] = float4(coord.x, coord.y, coord.z - fg_spacing, coord.w);
		break;
	case 3:
		DomainCoord4Buffer[index] = float4(coord.x, coord.y, coord.z, coord.w - fg_spacing);
		break;
	default:
		DomainCoord4Buffer[index] = float4(0, 0, 0, 0);
		break;
	}
}


[numthreads(TN, TN, 1)]
void KGradientCoordPlusMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float2 coord = Coord2Buffer[index];
	switch (fg_axis)
	{
	case 0:
		DomainCoord2Buffer[index] = float2(coord.x + fg_spacing, coord.y);
		break;
	case 1:
		DomainCoord2Buffer[index] = float2(coord.x, coord.y + fg_spacing);
		break;
	default:
		DomainCoord2Buffer[index] = float2(0, 0);
		break;
	}
}


[numthreads(TN, TN, 1)]
void KGradientCoordPlusMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float3 coord = Coord3Buffer[index];
	switch (fg_axis)
	{
	case 0:
		DomainCoord3Buffer[index] = float3(coord.x + fg_spacing, coord.y, coord.z);
		break;
	case 1:
		DomainCoord3Buffer[index] = float3(coord.x, coord.y + fg_spacing, coord.z);
		break;
	case 2:
		DomainCoord3Buffer[index] = float3(coord.x, coord.y, coord.z + fg_spacing);
		break;
	default:
		DomainCoord3Buffer[index] = float3(0, 0, 0);
		break;
	}
}


[numthreads(TN, TN, 1)]
void KGradientCoordPlusMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float4 coord = Coord4Buffer[index];
	switch (fg_axis)
	{
	case 0:
		DomainCoord4Buffer[index] = float4(coord.x + fg_spacing, coord.y, coord.z, coord.w);
		break;
	case 1:
		DomainCoord4Buffer[index] = float4(coord.x, coord.y + fg_spacing, coord.z, coord.w);
		break;
	case 2:
		DomainCoord4Buffer[index] = float4(coord.x, coord.y, coord.z + fg_spacing, coord.w);
		break;
	case 3:
		DomainCoord4Buffer[index] = float4(coord.x, coord.y, coord.z, coord.w + fg_spacing);
		break;
	default:
		DomainCoord4Buffer[index] = float4(0, 0, 0, 0);
		break;
	}
}