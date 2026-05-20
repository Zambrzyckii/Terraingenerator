# 3D Terrain Generator
A real-time, 3D terrain generator built from scratch in C# using the Raylib-cs library. This project demonstrates applied mathematics, rendering optimization techniques and custom algorithmic implementations for procedural generation

## Technologies
* C#
* Raylib-cs
* System.Numerics

## Features

### 1. Custom Perlin noise implementation
Implemented a custom Perlin noise algorithm utilizing permutation tables
* **Performance** - Replaced standard moudlo operators with btiwise AND operators to handle coordinates efficiently and prevent endless terrain generation, drastically reducing CPU cycles per frame.

### 2. Fractional Brownian Motion
To achieve realistic, jagged mountain peaks rather than smooth unnatural waves, the terrain elevation is calculated using Fractional Brownian Motion. The algorithm layers multiple octaves of Perlin noise, dynamically scaling amplitude and frequency to simulate continental shifts and local rock deatils simultaneously

### 3. Spatial Matrix Rendering & Voxel Logic
* Handles real-time matrix transformations to center the 3D world around the camera.
* Implements spatial Voxel-based rendering for 3D typography 

## Performance and optimization
The current grid size 240x200 forces the CPU to processa and push approximetly thousands vertices and draw calls per frame to the GPU in immediate mode. Maintaining a stable 60 FPS demonstrates the efficiency of the underlying C# math loops and the lightweight nature of the Raylib wrapper

## How to run
1. Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed
2. Clone the repo
3. Navigate to project directory : ``` cd TerrainGenerator ```
4. Run the application ``` dotnet run ```


