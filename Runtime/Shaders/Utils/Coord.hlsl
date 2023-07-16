/*
	��Ϊ��GPU��CPU֮�䴫���������ԽСԽ��,��˽�����ŵ�GPU�н��м���
	���µĺ�������SV_DispatchThreadID��[width, height]����ʵ�ʵ�����ֵ
	��Ϊ�ҵ�����,��ʹ�����·���:
		���Ǵ�2D����ӳ��,����ֱ��ӳ��,ӳ�䵽����Wrapping��ӳ�䵽˫��Wrapping��Ϊ2D,3D��4D����
	����ֵ��[0, 1]�ķ�Χ��.
		����,������������,�Ҳ���һ���Եļ�����Щֵ,����ÿ�μ������е�һ��,����ʹ�������ֱ���
	�����Ҫ��ͬ�ĵ�������㷽��,����time��Ϊ����ֵ,������д��Щ����
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