
float getnoise(float2 coord,uint ntype)
{
	switch (ntype)
	{
	case PERLIN:
		return cnoise(coord);
	case SIMPLEX:
		return snoise(coord);
	default:
		return 0.0;
	}
}

float getnoise(float3 coord, uint ntype)
{
	switch (ntype)
	{
	case PERLIN:
		return cnoise(coord);
	case SIMPLEX:
		return snoise(coord);
	default:
		return 0.0;
	}
}

float getnoise(float4 coord, uint ntype)
{
	switch (ntype)
	{
	case PERLIN:
		return cnoise(coord);
	case SIMPLEX:
		return snoise(coord);
	default:
		return 0.0;
	}
}