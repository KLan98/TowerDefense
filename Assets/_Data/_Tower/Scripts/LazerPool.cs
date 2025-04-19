using System.Collections.Generic; // Needed for the Queue
using UnityEngine;

//  A Singleton is used when you want to make sure only one instance of a class exists, and you need easy global access to that one instance.
public class LazerPool : Load
{
    // This allows other scripts to easily access this pool.
    // It's a Singleton - meaning only one instance of this exists in the scene.
    public static LazerPool Instance;

    // The bullet prefab that will be cloned to create more bullets.
    [SerializeField] private LazerBeam lazerBeam;
    public LazerBeam LazerBeam => lazerBeam;

    // The number of bullets to preload into the pool at the start.
    [SerializeField] private int initialPoolSize = 5;

    // The pool itself - a fifo queue to store inactive bullets.
    private Queue<LazerBeam> pool = new Queue<LazerBeam>();

    protected override void LoadComponent()
    {
        this.LoadBulletPrefabType();

        // Check if there's already an instance of this class
        if (Instance == null)
        {
            // If not, set this object as the single instance
            Instance = this;
        }
        else if (Instance != this)
        {
            // If another instance already exists, destroy this one to enforce the Singleton
            Destroy(this.gameObject);
        }

        // Create and store bullets in the pool ahead of time.
        this.InitializePool();
    }

    // This method creates the initial pool fifo of inactive bullets.
    protected virtual void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            // Create a new bullet from the prefab.
            LazerBeam lazer = Instantiate(lazerBeam);

            // Deactivate the bullet so it's not visible or active.
            lazer.gameObject.SetActive(false);

            // Parent it to this pool object for cleaner hierarchy (optional).
            lazer.transform.SetParent(this.transform);

            // Add the bullet to the queue (the pool).
            pool.Enqueue(lazer);
        }
    }

    // Get a bullet from the pool fifo to use for shooting.
    public LazerBeam GetBullet()
    {
        // If the pool is empty or exceed the max pool size, create a new bullet.
        if (pool.Count == 0)
        {
            LazerBeam lazer = Instantiate(lazerBeam);
            lazer.gameObject.SetActive(false);
            Debug.Log("Pool size exceeded");
            return lazer;
        }

        // Take the next available bullet from the pool.
        return pool.Dequeue();
    }

    // Return a bullet to the pool after it's done (e.g., hit something).
    public virtual void ReturnBullet(LazerBeam lazer)
    {
        // Deactivate the bullet so it's invisible and inactive.
        lazer.gameObject.SetActive(false);

        // Add it back to the pool queue for future reuse.
        pool.Enqueue(lazer);
    }
    
    protected virtual void LoadBulletPrefabType()
    {
        if (this.lazerBeam != null) return;
        this.lazerBeam = GameObject.Find(Const.LAZER_PREFAB).GetComponentInChildren<LazerBeam>(true);
    }
}