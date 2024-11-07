using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PoolControler : MonoBehaviour
{
    [Header("---- POOL CONTROLER TO INIT POOL ----")]
    [Header("Put object pool to list Pool or Resources/Pool")]
    [Header("Preload: Init Poll")]
    [Header("Spawn: Take object from pool")]
    [Header("Despawn: return object to pool")]
    [Header("Collect: return objects type to pool")]
    [Header("CollectAll: return all objects to pool")]

    [Space]
    [Header("Pool")]
    public List<PoolAmount> Pool;

    [Header("Particle")]
    public ParticleAmount[] Particle;


    public void Awake()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            SimplePool.Preload(Pool[i].prefab, Pool[i].amount, Pool[i].root, Pool[i].collect);
        }

        for (int i = 0; i < Particle.Length; i++)
        {
            ParticlePool.Preload(Particle[i].prefab, Particle[i].amount, Particle[i].root);
            ParticlePool.Shortcut(Particle[i].particleType, Particle[i].prefab);
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(PoolControler))]
public class PoolControlerEditor : Editor
{
    PoolControler pool;

    private void OnEnable()
    {
        pool = (PoolControler)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Quick Root"))
        {
            for (int i = 0; i < pool.Pool.Count; i++)
            {
                if (pool.Pool[i].root == null)
                {
                    Transform tf = new GameObject(pool.Pool[i].prefab.poolType.ToString()).transform;
                    tf.parent = pool.transform;
                    pool.Pool[i].root = tf;
                }
            }

            for (int i = 0; i < pool.Particle.Length; i++)
            {
                if (pool.Particle[i].root == null)
                {
                    Transform tf = new GameObject(pool.Particle[i].particleType.ToString()).transform;
                    tf.parent = pool.transform;
                    pool.Particle[i].root = tf;
                }
            }
        }

        if (GUILayout.Button("Get Prefab Resource"))
        {
            GameUnit[] resources = Resources.LoadAll<GameUnit>("Pool");

            for (int i = 0; i < resources.Length; i++)
            {
                bool isDuplicate = false;
                for (int j = 0; j < pool.Pool.Count; j++)
                {
                    if (resources[i].poolType == pool.Pool[j].prefab.poolType)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    Transform root = new GameObject(resources[i].name).transform;

                    PoolAmount newPool = new PoolAmount(root, resources[i], SimplePool.DEFAULT_POOL_SIZE, true);

                    pool.Pool.Add(newPool);
                }
            }
        }
    }
}

#endif

[System.Serializable]
public class PoolAmount
{
    [Header("-- Pool Amount --")]
    public Transform root;
    public GameUnit prefab;
    public int amount;
    public bool collect;

    public PoolAmount(Transform root, GameUnit prefab, int amount, bool collect)
    {
        this.root = root;
        this.prefab = prefab;
        this.amount = amount;
        this.collect = collect;
    }
}


[System.Serializable]
public class ParticleAmount
{
    public Transform root;
    public ParticleType particleType;
    public ParticleSystem prefab;
    public int amount;
}


public enum ParticleType
{
    Hit_1,
    Hit_2,
    Hit_3,

    LevelUp_1,
    LevelUp_2,
    LevelUp_3,

    Explosion,
    ColorButton
}

public enum PoolType
{
    None,

    Cube,
    House,
    Pyramid,
    Cloud,
    Cake,
    Dino,
    Elephant,
    Pig,
    Chicken,
    TraningBooster,
    Cookie,
    Juice,
    Cafe,
    BabyFox,
    Beaver,
    Flamingo,
    Box,
    Giraffe,
    TRex,
    Rainbow,
    Candle,
    Milk,
    Minecraft,
    Watermelon,
    Ship,
    Robot,
    Monkey,
    Heart,
    Icecream,
    Unicorn,
    Dog,
    Tank,
    Squirrel,
    RaceCar,
    Pizza,
    Worm,
    CatHead,
    Kitchen,
    Castor,
    Duck,
    Boy,
    ScareCrow,
    Octopus,
    Bee,
    Crane,
    Panda,
    Pikachu,
    Sushi,
    Camel,
    Mario,
    Tractor,
    CatIceCream,
    Room,
    Dragon,
    Player,
    RobotTank,
    Television,
    HugeRobot,
    Hamburger,
    BirthdayCake,
    McDonald,
    Carot,
    Labubu,
    Loopy,
    Capybara,
    MintCream,
    PineApple,
    PizzaPiece,
    Socola,
    StrawCake,
    HotDog,
    ConeCream,
    CoupleCream,
    PopCorn,
    Ghost,
    Pumpkin,
    BabyGhost,
    Spider,
    PumpkinHat,
    PumpkinKitten,
    CakeHLW,
    BoyWitch,
    BreadCat,
    Plane,
    SushiSet,
    Viking,
    DeathHLW,
    GirlWitch,
    CupCakeHLW

}

