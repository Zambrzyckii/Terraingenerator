namespace TerrainGenerator;

public static class PerlinNoise
{
    public static int[] P = new int[255];
    public static float GetNoise(float x, float y)
    {
        int gridX = (int)Math.Floor(x) & 255;
        int gridY = (int)Math.Floor(y) & 255;

        float localX = x - (float)Math.Floor(x);
        float localY = y - (float)Math.Floor(y);

        float u = Fade(localX);
        float v = Fade(localY);

        int hashTopLeft = P[P[gridX] + gridY + 1];
        int hashTopRight = P[P[gridX + 1] + gridY + 1];
        int hashBottomLeft = P[P[gridX] + gridY];
        int hashBottomRight = P[P[gridX + 1] + gridY];

        float dotTopLeft = Grad(hashTopLeft, localX, localY - 1);
        float dotTopRight = Grad(hashTopRight, localX - 1, localY - 1);
        float dotBottomLeft = Grad(hashBottomLeft, localX, localY);
        float dotBottomRight = Grad(hashBottomRight, localX - 1, localY);

        float lerpTop = Lerp(dotTopLeft, dotTopRight, u);
        float lerpBottom = Lerp(dotBottomLeft, dotBottomRight, u);

        float finalNoiseValue = Lerp(lerpBottom, lerpTop, v);

        return finalNoiseValue;
    }
    
    public static int[] GeneratePermutationTable(int seed)
    {
        int[] permutation = new int[256];
        int[] p = new int[512]; 
        
        Random random = new Random(seed);
        
        for (int i = 0; i < 256; i++)
        {
            permutation[i] = i;
        }
        
        for (int i = 255; i > 0; i--)
        {
            int swapIndex = random.Next(i + 1);
            
            int temp = permutation[i];
            permutation[i] = permutation[swapIndex];
            permutation[swapIndex] = temp;
        }
        
        for (int i = 0; i < 256; i++)
        {
            p[i] = permutation[i];           
            p[i + 256] = permutation[i];     
        }

        return p;
    }

    private static float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    private static float Lerp(float a, float b, float t)
    {
        return a + t * (b - a);
    }

    private static float Grad(int hash, float distanceX, float distanceY)
    {
        return (hash % 2 == 0 ? distanceX : -distanceX) + (hash % 3 == 0 ? distanceY : -distanceY);
    }
}