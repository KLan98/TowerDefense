using System.Collections.Generic; // Needed for the Queue
using UnityEngine;

//  A Singleton is used when you want to make sure only one instance of a class exists, and you need easy global access to that one instance.
public class BulletPool : Load
{
    // This allows other scripts to easily access this pool.
    // It's a Singleton - meaning only one instance of this exists in the scene.
    public static BulletPool Instance;

    // The bullet prefab that will be cloned to create more bullets.
    [SerializeField] private TowerBullet towerBullet;
    public TowerBullet TowerBullet => towerBullet;

    // The number of bullets to preload into the pool at the start.
    [SerializeField] private int initialPoolSize = 100;

    // The pool itself - a fifo queue to store inactive bullets.
    private Queue<TowerBullet> pool = new Queue<TowerBullet>();

    protected override void LoadComponent()
    {
        this.LoadTowerBullet();

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
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            // Create a new bullet from the prefab.
            TowerBullet bullet = Instantiate(towerBullet);

            // Deactivate the bullet so it's not visible or active.
            bullet.gameObject.SetActive(false);

            // Parent it to this pool object for cleaner hierarchy (optional).
            bullet.transform.SetParent(this.transform);

            // Add the bullet to the queue (the pool).
            pool.Enqueue(bullet);
        }
    }

    // Get a bullet from the pool fifo to use for shooting.
    public TowerBullet GetBullet()
    {
        // If the pool is empty or exceed the max pool size, create a new bullet.
        if (pool.Count == 0)
        {
            TowerBullet bullet = Instantiate(towerBullet);
            bullet.gameObject.SetActive(false);
            Debug.Log("Pool size exceeded");
            return bullet;
        }

        // Take the next available bullet from the pool.
        return pool.Dequeue();
    }

    // Return a bullet to the pool after it's done (e.g., hit something).
    public void ReturnBullet(TowerBullet bullet)
    {
        // Deactivate the bullet so it's invisible and inactive.
        bullet.gameObject.SetActive(false);

        // Add it back to the pool queue for future reuse.
        pool.Enqueue(bullet);
    }

    protected virtual void LoadTowerBullet()
    {
        if (this.towerBullet != null) return;
        // This line is trying to find a prefab or object in the scene by name, and then load a TowerBullet component from one of its child objects, even if that child is inactive (disabled).
        this.towerBullet = GameObject.Find(Const.BULLET_PREFAB).GetComponentInChildren<TowerBullet>(true);
    }
}