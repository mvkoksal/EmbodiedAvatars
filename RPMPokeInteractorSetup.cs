using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe.MetaMovement.Runtime;
using UnityEngine.XR.Interaction.Toolkit;

public class RPMPokeInteractorSetup : MonoBehaviour
{
    private MovementPrefabLoader avatarLoader;

    [SerializeField] private float pokeDepth = 3f;

    void Start()
    {
        avatarLoader = GetComponent<MovementPrefabLoader>();
        if (avatarLoader == null)
        {
            Debug.LogError("MovementPrefabLoader component missing!");
            return;
        }

        // When the avatar is loaded successfully, call OnAvatarLoaded and pass the loaded avatar GameObject to it
        // HOW IT WORKS: 
        // OnAvatarObjectLoaded is declared as a UnityEvent<GameObject> in MovementPrefabLoader. 
        // This means it expects listener methods with a GameObject parameter, and passes the 
        // loaded avatar GameObject to them.
        avatarLoader.OnAvatarObjectLoaded.AddListener(OnAvatarLoaded);
    }

    // Attach PokeInteractor to both fingertips of the avatar
    void OnAvatarLoaded(GameObject avatar)
    {
        AttachPokeInteractorToHand(avatar, true);
        AttachPokeInteractorToHand(avatar, false);
    }

    // Get the paths to the left and right fingertip, and return an error if the specified path doesn't exist
    void AttachPokeInteractorToHand(GameObject avatar, bool isRightHand)
    {
        string fingerTipName = isRightHand ? "RightHandIndex4" : "LeftHandIndex4";

        // Find the fingertip
        Transform fingerTip = FindDeepChild(avatar.transform, fingerTipName);
        if (fingerTip == null)
        {
            Debug.LogWarning($"Couldn't find fingertip '{fingerTipName}' in avatar.");
            return;
        }

        // Create a new GameObject and set it as a child of the fingertip
        // Set its transform to (0,0,0) relative to its parent
        GameObject pokeObject = new GameObject(isRightHand ? "RightPokeInteractor" : "LeftPokeInteractor");
        pokeObject.transform.SetParent(fingerTip, false);
        pokeObject.transform.localPosition = Vector3.zero;

        // Add a poke interactor
        var pokeInteractor = pokeObject.AddComponent<XRPokeInteractor>();
       // Set the poke depth
        pokeInteractor.pokeDepth = pokeDepth;
    }

    // Recursively search the children of a gameObject and return the <targetname> gameObject.
    Transform FindDeepChild(Transform parent, string targetName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == targetName)
                return child;

            Transform result = FindDeepChild(child, targetName);
            if (result != null)
                return result;
        }
        return null;
    }
}