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
    private Animator animator;
    private SkinnedMeshColliderSynchronizer skinnedMeshColliderSynchronizer;
    private SkinnedMeshRenderer skinMesh;
    private Material originalMaterial;

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
        skinMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterial = skinMesh.material;
        skinnedMeshColliderSynchronizer.SetMeshRenderer(skinMesh);
        gameObject.layer = LayerMask.NameToLayer("EnemyPrimitive");

        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        //var rb2 = GetComponent<Rigidbody>();
        //rb2.isKinematic = false;
        //rb2.useGravity = true;

        foreach (var c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
        GetComponent<Collider>().enabled = true;

        animator = GetComponent<Animator>();
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


    public void SeeThroughIfDead()
    {
        if (dead)
        {
            skinMesh.material = GameManager.main.GetSeeThroughMaterial();
        }
    }

    private bool dead = false;

    public void DontSeeThroughIfDead()
    {
        if (dead)
        {
            skinMesh.material = originalMaterial;
            skinMesh.material.color = Color.red;
        }
    }

    
    public void enableRagdoll()
    {
        dead = true;
        SeeThroughIfDead();
        GetComponent<EnemyMovement>().enabled = false;
        animator.enabled = false;
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        var rb2 = GetComponent<Rigidbody>();
        if (rb2 != null)
        {
            rb2.isKinematic = true;
            rb2.useGravity = false;
        }

        skinnedMeshColliderSynchronizer.gameObject.SetActive(false);

        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }
        GetComponent<Collider>().enabled = false;
        
    }
}
