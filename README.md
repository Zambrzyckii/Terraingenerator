# 3D Terrain Generator

![C#](https://img.shields.io/badge/C%23-512BD4?style=for-the-badge&logoColor=white)
![.NET 10](https://img.shields.io/badge/.NET_10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Raylib](https://img.shields.io/badge/Raylib-000000?style=for-the-badge&logo=raylib&logoColor=white)

A real-time 3D terrain generator written in C# with Raylib-cs. The heights come from a hand-written Perlin noise with a seeded permutation table, layered into four octaves of fractional Brownian motion. The result is a 240x200 wireframe landscape that is recalculated and redrawn every frame as it scrolls. At startup a voxel-style BOB logo built from cubes rises out of view.

<p align="center">
  <img src="./TerrainGenerator/Assets/demo.gif" alt="Wireframe terrain scrolling in real time" width="720">
</p>

## Technologies

* C#
* .NET 10
* Raylib-cs
* System.Numerics

## How to run

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download) and run these commands.

```bash
git clone https://github.com/Zambrzyckii/terraingenerator.git
cd terraingenerator/TerrainGenerator
dotnet run
```
