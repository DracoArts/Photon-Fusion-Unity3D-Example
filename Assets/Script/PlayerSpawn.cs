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
