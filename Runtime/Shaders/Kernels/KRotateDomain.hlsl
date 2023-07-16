RWStructuredBuffer<float> KRotateDomainBuffer;

[numthreads(TN, TN, 1)]
void KRotateDomainMain(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float source = InputBuffer4[index];
	KRotateDomainBuffer[index] = source;
}

[numthreads(TN, TN, 1)]
void KRotateDomainCoordMain2D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float2 coord = Coord2Buffer[index];
	float ans = InputBuffer0[index];

	float x = coord.x, y = coord.y;
	float angle = ans * 360.0 * 3.14159265 / 180.0;
	float cosangle = cos(angle);
	float sinangle = sin(angle);

	float nx = x * cosangle + y * sinangle;
	float ny = x * sinangle + y * cosangle;

	DomainCoord2Buffer[index] = float2(nx, ny);
}

[numthreads(TN, TN, 1)]
void KRotateDomainCoordMain3D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float3 coord = Coord3Buffer[index];
	float ans = InputBuffer0[index];
	float ax = InputBuffer1[index];
	float ay = InputBuffer2[index];
	float az = InputBuffer3[index];

	float x = coord.x, y = coord.y, z = coord.z;
	float angle = ans * 360.0 * 3.14159265 / 180.0;
	float cosangle = cos(angle);
	float sinangle = sin(angle);

	float rm0 = 1.0 + (1.0 - cosangle) * (ax * ax - 1.0);
	float rm1 = -az * sinangle + (1.0 - cosangle) * ax * ay;
	float rm2 = ay * sinangle + (1.0 - cosangle) * ax * az;

	float rm3 = az * sinangle + (1.0 - cosangle) * ax * ay;
	float rm4 = 1.0 + (1.0 - cosangle) * (ay * ay - 1.0);
	float rm5 = -ax * sinangle + (1.0 - cosangle) * ay * az;

	float rm6 = -ay * sinangle + (1.0 - cosangle) * ax * az;
	float rm7 = ax * sinangle + (1.0 - cosangle) * ay * az;
	float rm8 = 1.0 + (1.0 - cosangle) * (az * az - 1.0);

	float nx = x * rm0 + y * rm1 + z * rm2;
	float ny = x * rm3 + y * rm4 + z * rm5;
	float nz = x * rm6 + y * rm7 + z * rm8;

	DomainCoord3Buffer[index] = float3(nx, ny, nz);
}

[numthreads(TN, TN, 1)]
void KRotateDomainCoordMain4D(uint3 id : SV_DispatchThreadID)
{
	if (id.x > resolution || id.y > resolution)return;
	int index = id.x + id.y * resolution;

	float4 coord = Coord4Buffer[index];
	float ans = InputBuffer0[index];
	float ax = InputBuffer1[index];
	float ay = InputBuffer2[index];
	float az = InputBuffer3[index];

	float x = coord.x, y = coord.y, z = coord.z, w = coord.w;
	float angle = ans * 360.0 * 3.14159265 / 180.0;
	float cosangle = cos(angle);
	float sinangle = sin(angle);

	float rm0 = 1.0 + (1.0 - cosangle) * (ax * ax - 1.0);
	float rm1 = -az * sinangle + (1.0 - cosangle) * ax * ay;
	float rm2 = ay * sinangle + (1.0 - cosangle) * ax * az;

	float rm3 = az * sinangle + (1.0 - cosangle) * ax * ay;
	float rm4 = 1.0 + (1.0 - cosangle) * (ay * ay - 1.0);
	float rm5 = -ax * sinangle + (1.0 - cosangle) * ay * az;

	float rm6 = -ay * sinangle + (1.0 - cosangle) * ax * az;
	float rm7 = ax * sinangle + (1.0 - cosangle) * ay * az;
	float rm8 = 1.0 + (1.0 - cosangle) * (az * az - 1.0);

	float nx = x * rm0 + y * rm1 + z * rm2;
	float ny = x * rm3 + y * rm4 + z * rm5;
	float nz = x * rm6 + y * rm7 + z * rm8;

	DomainCoord4Buffer[index] = float4(nx, ny, nz, w);
}