float sawtooth(float x, float p)
{
	return 2.0 * (x / p - floor(0.5 + x / p)) * 0.5 + 0.5;
}

float magnitude(float x, float y)
{
	return sqrt(x * x + y * y);
}

float magnitude(float x, float y, float z)
{
	return sqrt(x * x + y * y + z * z);
}

float magnitude(float x, float y, float z, float w)
{
	return sqrt(x * x + y * y + z * z + w * w);
}

uint hash(uint s)
{
	s ^= 2747636419u;
	s *= 2654435769u;
	s ^= s >> 16;
	s *= 2654435769u;
	s ^= s >> 16;
	s *= 2654435769u;
	return s;
}

float random(uint seed)
{
	return float(hash(seed)) / 4294967295.0; // 2^32-1
}