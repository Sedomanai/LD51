# 2D LeanUnity Project Template
    Project template for Unity: 2D Lean

## Packages
    com.unity.2d.psdimporter: 6.0.5
    com.unity.2d.sprite: 1.0.0
    com.unity.2d.tilemap: 1.0.0
    com.unity.ide.visualstudio: 2.0.16
    com.unity.inputsystem: 1.4.1
    com.unity.shadergraph": 12.1.7
    com.unity.textmeshpro: 3.0.6
    com.unity.ugui: 1.0.0
    com.unity.modules.animation: 1.0.0
    com.unity.modules.audio: 1.0.0
    com.unity.modules.imgui: 1.0.0
    com.unity.modules.jsonserialize: 1.0.0
    com.unity.modules.particlesystem: 1.0.0
    com.unity.modules.physics2d: 1.0.0
    com.unity.modules.tilemap: 1.0.0
    com.unity.modules.ui: 1.0.0
    com.unity.modules.uielements: 1.0.0
    com.unity.modules.umbra: 1.0.0

## Layers
    Default (Default, Transparent FX, Ignore Raycast, Water, UI)
    Camera Trigger 2D
    Camera Subject 2D
    Camera Box 2D
    Camera 2D
    Player
    Player Static Collider
    Enemy
    Enemy Static Collider
    Wall
    Projectile
    Checker
    Check Listener
    Garbage Collector

## Project Settings

### Player
    Company Name : Elang
    Product Name : [Product Name] (Make sure to replace it with new product name)
    Version      : 0.0.01

### Physics 2D
    Gravity           : -50 in y axis
    Default Material  : Default Material (Assets/Template Materials/Default Material)
    Layer Collision Matrix :
        Camera Subject 2D / Camera Trigger 2D
        Camera 2D / Camera Box 2D
        Enemy / Player Static Collider
        Enemy / Wall
        Player / Enemy Static Collider
        Player / Wall
        Projectile / Player
        Projectile / Enemy
        Projectile / Wall
        Checker / Check Listener

### Enter Play Mode
    Enter Play Mode Options : True
        Reload Domain & Reload Scene : False

## Assets
    
### Scenes
    SampleScene
### Template
    Default Material.physicsMaterial2D