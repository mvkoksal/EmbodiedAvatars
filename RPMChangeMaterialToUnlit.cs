using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe.MetaMovement.Runtime;

public class RPMChangeMaterialToUnlit : MonoBehaviour
{
    private MovementPrefabLoader avatarLoader;

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

    private void OnAvatarLoaded(GameObject avatar)
    {
        if (avatar == null)
        {
            Debug.Log("Avatar GameObject is null.");
            return;
        }

        ReplaceMaterialsWithUnlit(avatar);
    }

    // Change the material of all renderers except the Renderer_Body and Renderer_Hair to glTF/Unlit.
    // Set the metallic and roughness factors of Renderer_Hair.
    private void ReplaceMaterialsWithUnlit(GameObject avatar)
    {
        var renderers = avatar.GetComponentsInChildren<SkinnedMeshRenderer>(true);

        foreach (var renderer in renderers)
        {
            if (renderer.gameObject.name == "Renderer_Body")
            {
                continue;
            }

            if (renderer.gameObject.name == "Renderer_Hair")
            {
                Material mat = renderer.material;
                mat.SetFloat("metallicFactor", 0f);
                mat.SetFloat("roughnessFactor", 1f);
                continue;
            }

            var originalMat = renderer.material;

            if (originalMat != null && originalMat.shader.name == "glTF/PbrMetallicRoughness")
            {
                var newMat = new Material(Shader.Find("glTF/Unlit"));
                newMat.CopyPropertiesFromMaterial(originalMat);
                newMat.name = originalMat.name;
                renderer.material = newMat;
            }
        }
    }
}