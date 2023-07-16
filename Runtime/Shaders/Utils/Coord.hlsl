/*
	因为在GPU与CPU之间传输的数据量越小越好,因此将坐标放到GPU中进行计算
	以下的函数根据SV_DispatchThreadID和[width, height]计算实际的坐标值
	因为我的需求,我使用以下方法:
		它们从2D坐标映射,返回直接映射,映射到单轴Wrapping和映射到双轴Wrapping作为2D,3D和4D坐标
	返回值在[0, 1]的范围内.
		此外,由于数量极大,我并不一次性的计算这些值,而是每次计算其中的一块,所以使用两个分辨率
	如果需要不同的的坐标计算方法,比如time作为坐标值,可以重写这些函数
*/
static const float PI = 3.14159265f;

float2 getcoord2(uint3 id, float2 start, float stepsize)
{
	float nx = id.x * stepsize + start.x;
	float ny = id.y * stepsize + start.y;

	return float2(nx, ny);
}

float3 getcoord3(uint3 id, float2 start, float stepsize)
{
	float x1 = 0, x2 = 1;
	float dx = x2 - x1;

	float s = id.x * stepsize + start.x;
	float t = id.y * stepsize + start.y;

	float nx = x1 + cos(s * 2 * PI) * dx / (2 * PI);
	float ny = x1 + sin(s * 2 * PI) * dx / (2 * PI);
	float nz = t;

	return float3(nx, ny, nz);
}

float4 getcoord4(uint3 id, float2 start, float stepsize)
{
	float x1 = 0;
	float x2 = 2;
	float y1 = 0;
	float y2 = 2;
	float dx = x2 - x1;
	float dy = y2 - y1;

	float s = id.x * stepsize + start.x;
	float t = id.y * stepsize + start.y;

	float nx = x1 + cos(s * 2 * PI) * dx / (2 * PI);
	float ny = y1 + cos(t * 2 * PI) * dy / (2 * PI);
	float nz = x1 + sin(s * 2 * PI) * dx / (2 * PI);
	float nw = y1 + sin(t * 2 * PI) * dy / (2 * PI);

	return float4(nx, ny, nz, nw);
}