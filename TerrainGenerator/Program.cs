using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography;
using TerrainGenerator;

PerlinNoise.P = PerlinNoise.GeneratePermutationTable(123);
string[] logoMap = {
    //B       //O       //B
    "01111   11111   01111",
    "10001   10001   10001",
    "10001   10001   10001",
    "01111   10001   01111",
    "10001   10001   10001",
    "10001   10001   10001",
    "01111   11111   01111"
};
int cols = 240;
int rows = 200;
float spacing = 2.0f;
float offset = 0.0f;
float speed = 0.1f;
float frequency = 0.01f; 
float amplitude = 60.0f; 
float[,] terrain;
float startX = -20.0f; 
float startY = -30.0f;  
float startZ = 20.0f;  
float blockSize = 2.0f; 


Raylib.InitWindow(1600, 900, "Terrain generator");
Raylib.SetTargetFPS(60);

terrain = new float[cols, rows];


Camera3D camera = new Camera3D();
camera.Position = new Vector3(0.0f, 50.0f, -20.0f);
camera.Target = new Vector3(0.0f, 0.0f, 100.0f);
camera.Up = new Vector3(0.0f, 100.0f, 0.0f);
camera.FovY = 60.0f;
camera.Projection = CameraProjection.Perspective;

while (!Raylib.WindowShouldClose())
{
    offset -= speed;
    for (int x = 0; x < cols; x++)
    {
        for (int z = 0; z < rows; z++)
        {

            terrain[x, z] = CalculateFractalElevation(x, z, 4);
        }
    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    //Raylib.UpdateCamera(ref camera, CameraMode.Free);
    Raylib.BeginMode3D(camera);
    for (int row = 0; row < logoMap.Length; row++)
    {
        for (int col = 0; col < logoMap[row].Length; col++)
        {
            if (logoMap[row][col] == '1')
            {
                Vector3 blockPos = new Vector3(
                    startX + (col * blockSize), 
                    startY - (row * blockSize), 
                    startZ
                );
                Raylib.DrawCube(blockPos, blockSize, blockSize, blockSize, Color.Black);
                Raylib.DrawCubeWires(blockPos, blockSize, blockSize, blockSize, Color.Gold);
            }
        }
    }
    startY = startY > 200.0f ? startY : startY + 0.25f;
    float worldWidth = cols * spacing;
    float offsetX = worldWidth / 2.0f;
    
    for (int x = 0; x < cols - 1; x++)
    {
        for (int z = 0; z < rows - 1; z++)
        {
            float physicalX = (x * spacing) - offsetX;
            float physicalNextX = ((x + 1) * spacing) - offsetX;

            float yCurrent = terrain[x, z];
            float yRight   = terrain[x + 1, z];
            float yBottom  = terrain[x, z + 1];

            Vector3 current = new Vector3(physicalX, yCurrent, z * spacing);
            Vector3 right   = new Vector3(physicalNextX, yRight, z * spacing);
            Vector3 bottom  = new Vector3(physicalX, yBottom, (z + 1) * spacing);

            Raylib.DrawLine3D(current, right, Color.Gold);
            Raylib.DrawLine3D(current, bottom, Color.Gold);
            
        }
    }

    Raylib.EndMode3D();
    Raylib.DrawText("Bob 3D terrain generator", 10, 10, 20, Color.White);
    Raylib.EndDrawing();
}

Raylib.CloseWindow();

float CalculateFractalElevation(float x, float z, int octaves)
{
    float totalElevation = 0f;
    float currentFreq = frequency;
    float currentAmp = amplitude;
    float physicalZ = z + offset;
    for (int i = 0; i < octaves; i++)
    {
        totalElevation += PerlinNoise.GetNoise(x * currentFreq, physicalZ * currentFreq) * currentAmp;
        
        currentAmp *= 0.5f; 
        
        currentFreq *= 2.0f; 
    }

    return totalElevation;
}