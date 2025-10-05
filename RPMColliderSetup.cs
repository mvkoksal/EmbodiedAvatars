using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe.MetaMovement.Runtime;
using UnityEngine.XR.Interaction.Toolkit;

public class RPMColliderSetup : MonoBehaviour
{
    private MovementPrefabLoader avatarLoader;

    private readonly string leftFootPath = "Armature/Hips/LeftUpLeg/LeftLeg/LeftFoot";
    private readonly string rightFootPath = "Armature/Hips/RightUpLeg/RightLeg/RightFoot";

    // Start is called before the first frame update
    void Start()
    {
        avatarLoader = GetComponent<MovementPrefabLoader>();
        if (avatarLoader == null)
        {
            Debug.LogError("MovementPrefabLoader is missing!");
            return;
        }
        avatarLoader.OnAvatarObjectLoaded.AddListener(OnAvatarLoaded);
    }

    // Called when the avatar is loaded
    private void OnAvatarLoaded(GameObject avatar)
    {
        if (avatar == null)
        {
            Debug.Log("Avatar GameObject is null!");
            return;
        }
        
        // Add colliders to the feet
        AddFootCollider(avatar, leftFootPath, "LeftFootCollider");
        AddFootCollider(avatar, rightFootPath, "RightFootCollider");

        // Add a body collider
        CapsuleCollider collider = avatar.AddComponent<CapsuleCollider>();
        collider.center = new Vector3(0, 1, 0);
        collider.radius = 0.7f;
    }

    private void AddFootCollider(GameObject avatar, string path, string debugName)
    {

        Transform foot = avatar.transform.Find(path);
        if (foot == null)
        {
            Debug.LogWarning($"{debugName} path not found: {path}");
            return;
        }

        var col = foot.gameObject.AddComponent<SphereCollider>();
        col.radius = 0.15f;
        col.isTrigger = false;
    }
}
