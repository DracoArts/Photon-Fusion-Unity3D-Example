
# Welcome to DracoArts

![Logo](https://dracoarts-logo.s3.eu-north-1.amazonaws.com/DracoArts.png)




# Photon Fusion Unity3D Example
Photon Fusion is a high-performance networking framework built for Unity, designed to simplify the creation of real-time multiplayer games while ensuring low latency, scalability, and smooth synchronization between players. Developed by Photon Engine, Fusion provides a deterministic, tick-based networking model that supports client-side prediction, lag compensation, and server-authoritative gameplay, making it ideal for competitive, fast-paced, and large-scale multiplayer experiences.

## A. Deterministic Tick-Based Simulation
- Fusion operates on a fixed-tick system, where game logic updates at consistent intervals (e.g., 60Hz).

- Ensures predictable and synchronized behavior across all clients, critical for competitive games.

- Supports rewind and replay for lag compensation (e.g., accurate hit detection in FPS games).

## B. Network Topologies (Server Models)
- Fusion supports multiple networking approaches:


### Dedicated Server (Server-Authoritative)

  - A standalone, headless Unity instance controls all game logic.

-  Best for competitive games (e.g., MOBAs, shooters) where fairness is crucial.

### Client-Hosted (Listen Server)

- One player acts as the host, reducing server costs (peer-to-peer-like).

- Common in co-op or casual multiplayer games.

### Shared Authority (Cloud-Managed)

- Hybrid model where different players control different objects.

- Used in MMO-lite or social games where strict server authority isn’t required.

## C. State Synchronization
### Networked Objects & Properties: 
- Any game object can be synchronized across clients with minimal setup.

### Snapshot Interpolation:
 - Smoothly blends between network updates to reduce jitter.

### Area of Interest (AOI):
 - Optimizes bandwidth by only sending updates for nearby objects.

 # Key Features

 ## A. Client-Side Prediction & Lag Compensation
### Predictive Movement: 
- Players see their own actions instantly, while the server corrects discrepancies.

### Rewind & Reconciliation: 
- Corrects mispredictions (e.g., shooting a moving target).

### Rollback Netcode: 
- Similar to GGPO, ensuring smooth gameplay even under high latency.

## B. Bandwidth Optimization
### Delta Compression: 
- Only sends changed data, reducing network load.

### Interest Management: 
- Limits updates to relevant players (e.g., hiding faraway enemies).

### Efficient Serialization:
 - Uses binary protocols for faster transmission.

 ## C. Seamless Multiplayer Features
### Matchmaking & Lobbies: 
- Built-in tools for player grouping.

### Host Migration: 
- If the host disconnects, another player takes over without interruption.

### Cross-Platform Support:
 - Works on PC, mobile, consoles, and VR.

 # Use Cases & Game Genres


 ## A. Competitive Multiplayer (PvP)
### First-Person Shooters (FPS): 

- Accurate hit registration with lag compensation.

### Fighting & Sports Games: 
- Frame-perfect synchronization.

### Battle Royale (100+ players):
 - Scalable networking with minimal lag.

 ## B. Cooperative & Social Games
### Open-World RPGs:
 - Shared exploration with persistent worlds.

### Party & Casual Games: 
- Simple peer-to-peer hosting.

### VR/AR Experiences: 
- Synchronized interactions in shared spaces.


## C. Real-Time Strategy (RTS) & MOBAs
### Lockstep Networking: 
- Ensures all players see the same game state.

### Fog of War & Unit Control: 
- Efficiently syncs large numbers of entities.



##  Performance & Scalability
- Supports 200+ Concurrent Players: Optimized for large-scale games.

- Low CPU & Bandwidth Usage: Efficient networking stack.

- Cloud-Ready: Integrates with Photon Cloud, AWS, Azure, and Multiplay.

## Integration with Unity
- Works with Unity’s ECS & DOTS: For high-performance games.

- Supports Physics (Unity & Havok): Networked rigidbodies and collisions.

- UI & Scene Management: Syncs game states across level transitions.

# 1. Prerequisites

✅ Unity 2021.3.18+ (or 2022.3.x / 2023.x)

✅ Photon Fusion SDK [Photon Fusion](https://assetstore.unity.com/packages/tools/network/photon-fusion-267958)

✅ Photon Account (Free registration)

## 2. Step-by-Step Setup

## Step 1: Download Photon Fusion
- Go to the [Photon Engine Dashboard](https://dashboard.photonengine.com/).

 - Sign up or log in.

- Navigate to Fusion > Download SDK.

- Download the Photon Fusion Unity Package.

## Step 2: Import Fusion into Unity
- Open your Unity project.

- Go to Assets > Import Package > Custom Package.

- Select the downloaded Photon Fusion .unitypackage.

- Import all required files.

## Step 3: Generate a Photon App ID
- Go back to the Photon Dashboard.

- Click "Create a New App".

####  Select:

- Type: "Photon Fusion"

- Name: Your game’s name (e.g., "MyMultiplayerGame")

- Region: Best for your audience (e.g., "EU", "US", "Asia")

 - Click "Create".

- Copy the App ID (a long alphanumeric string).

## Step 4: Configure Fusion in Unity
- In Unity, go to Fusion > Fusion > Configure.

- Paste your App ID in the Photon App ID Fusion field.

- Select a Default Network Mode:

- Shared (Cloud-hosted)

- Server (Dedicated server)

- Host (Client-hosted)

- Click "Save".
## Usage/Examples

PlayerMovement

    using Fusion;
    using UnityEngine;

    public class PlayerMovement : NetworkBehaviour

     {

    [SerializeField]
    CharacterController characterController;
    public float playerSpeed=5f;
    public float jumpforce=5f;

    float Gravity = -8.91f;

    Vector3 velocity;

    bool jumping;


    void Start()
    {
        characterController.GetComponent<CharacterController>();
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {

            jumping = true;

        }

    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (HasStateAuthority == false)
        {

            return;
        }


        if (characterController.isGrounded == true)
        {

            velocity = new Vector3(0, -1, 0);

        }
        else
        {

            jumping = false;
        }
        velocity.y += Gravity * Runner.DeltaTime;

        if (jumping && characterController.isGrounded)
        {

            velocity.y += jumpforce;

        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Verticle");


        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * playerSpeed * Runner.DeltaTime;


        characterController.Move(movement + velocity * Runner.DeltaTime);
        if (movement != Vector3.zero)
        {

            gameObject.transform.forward = movement;
        }


    }


    }


Player Spwaner


    using Fusion;
    using UnityEngine;

    public class PlayerSpawn : SimulationBehaviour,IPlayerJoined
    {
    public GameObject playerPrefab;


    void Start()
    {
        


    }
    public void PlayerJoined(PlayerRef player){
          
          if(Runner.LocalPlayer==player){

            Runner.Spawn(playerPrefab, new Vector3(0,1,-1),Quaternion.identity);
          }

    }

    }


## Images 

![](https://github.com/AzharKhemta/Gif-File-images/blob/main/Photon%20Fusion%201.gif?raw=true)


## Authors

- [@MirHamzaHasan](https://github.com/MirHamzaHasan)
- [@WebSite](https://mirhamzahasan.com)


## 🔗 Links

[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/company/mir-hamza-hasan/posts/?feedView=all/)
## Documentation

[Photon Fusion 1](https://doc.photonengine.com/fusion/v1/fusion-intro)



## Tech Stack
**Client:** Unity  ,C#

**Plugin:** Photon Fusion Engine 



