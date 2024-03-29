using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterPrefabs;
    [SerializeField] private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private List<LayerMask> playerLayers;
    [SerializeField] private List<Color> pissColor;
    
    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }
    
    
    public void AddPlayer(PlayerInput playerInput)
    {
        players.Add(playerInput);
        
        //need to use the parent due to the structure of the prefab
        Transform playerParent = playerInput.transform.parent;
        playerParent.position = startingPoints[players.Count - 1].position;
        playerInput.transform.GetComponentInChildren<ParticlesController>().paintColor =
            pissColor[playerInput.playerIndex % pissColor.Count];
        //convert layer mask (bit) to an integer 
        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        //set the layer
        playerParent.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layerToAdd;
        //add the layer
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        //set the action in the custom cinemachine Input Handler
        //playerParent.GetComponentInChildren<InputHandler>().horizontal = playerInput.actions.FindAction("Look");
        int modelIndex = players.Count % characterPrefabs.Count;
        Instantiate(characterPrefabs[modelIndex], playerInput.transform.position, 
                                    Quaternion.identity, playerInput.transform);
        
    }
}
