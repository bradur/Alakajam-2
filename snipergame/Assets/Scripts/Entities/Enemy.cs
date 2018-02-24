// Date   : 24.02.2018 06:56
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
public class Enemy : MonoBehaviour
{

    private BoxCollider boxCollider;
    private SkinnedMeshColliderSynchronizer skinnedMeshColliderSynchronizer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(0f, 1f, 0f);
        boxCollider.size = new Vector3(0.6f, 2f, 1.35f);
        boxCollider.isTrigger = true;
        skinnedMeshColliderSynchronizer = GetComponentInChildren<SkinnedMeshColliderSynchronizer>();
        if (skinnedMeshColliderSynchronizer == null)
        {
            GameObject gameObject = new GameObject("ComplexCollider");
            skinnedMeshColliderSynchronizer = gameObject.AddComponent<SkinnedMeshColliderSynchronizer>();
        }
        skinnedMeshColliderSynchronizer.transform.SetParent(transform, false);
        skinnedMeshColliderSynchronizer.transform.localPosition = Vector3.zero;
        skinnedMeshColliderSynchronizer.transform.localRotation = Quaternion.identity;
        skinnedMeshColliderSynchronizer.SetMeshRenderer(GetComponentInChildren<SkinnedMeshRenderer>());
        gameObject.layer = LayerMask.NameToLayer("EnemyPrimitive");
    }

    public void GunRayCastHit()
    {
        if (GameManager.main.DebugMode)
        {
            Debug.Log("<b>[<color=yellow>Enemy</color>]:</b> Primitive collider hit by ray.");
        }
        skinnedMeshColliderSynchronizer.SynchronizeMeshCollider();
        boxCollider.enabled = false;
    }

    public void ResetCollider()
    {
        boxCollider.enabled = true;
        skinnedMeshColliderSynchronizer.ResetCollider();
    }
}
