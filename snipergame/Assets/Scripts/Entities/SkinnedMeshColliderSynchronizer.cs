// Date   : 24.02.2018 02:11
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
[ExecuteInEditMode]
public class SkinnedMeshColliderSynchronizer : MonoBehaviour
{

    private MeshCollider meshCollider;
    private SkinnedMeshRenderer meshRenderer;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("EnemyComplex");
    }

    public void SetMeshRenderer(SkinnedMeshRenderer meshRenderer)
    {
        this.meshRenderer = meshRenderer;
    }

    public void SynchronizeMeshCollider()
    {
        if (GameManager.main.DebugMode)
        {
            Debug.Log("<b>[<color=blue>ColliderSync</color>]:</b> Synchronizing skinned mesh collider.");
        }
        Mesh collisionMesh = new Mesh();
        meshRenderer.BakeMesh(collisionMesh);
        meshCollider.enabled = true;
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = collisionMesh;
    }

    public void ResetCollider()
    {
        meshCollider.enabled = false;
    }
}
