using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

// List of Item
abstract class Item : MonoBehaviour
{
    abstract public string GetName();
    abstract public float GetTime();
    abstract public void ApplyItemEffect(PlayerController playerController);
    abstract public void PlaySoundEffect(EffectManager effectManager);
    abstract public void PlayParticleEffect(ParticleSystem[] particleSystems);
    abstract public void ShowTimeUI(GameObject[] timeCanvasPrefabs);
    abstract public void RemoveItemEffect(PlayerController playerController);

    public const int PARTICLE_TEJAVA = 0;
    public const int PARTICLE_BOOST = 1;
    public const int PARTICLE_BOOST_RUN = 2;
    public const int PARTICLE_FLY = 3;
    public const int PARTICLE_DOUBLE = 4;
    public const int UI_BOOST = 0;
    public const int UI_FLY = 1;
    public const int UI_DOUBLE = 2;
}


class ItemTejava : Item
{
    private static string itemName = "Tejava";
    private static float time = 0.0f;

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }

    public override void ApplyItemEffect(PlayerController playerController)
    {
        playerController.AddScore();
    }

    public override void PlaySoundEffect(EffectManager effectManager)
    {
        effectManager.PlayItemTejavaSound();
    }

    public override void PlayParticleEffect(ParticleSystem[] particleSystems)
    {
        if (particleSystems[PARTICLE_TEJAVA])
        {
            particleSystems[PARTICLE_TEJAVA].Play();
        }
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {

    }

    public override void RemoveItemEffect(PlayerController playerController)
    {
        
    }
}

class ItemBoost : Item
{
    private static string itemName = "Item_Boost";
    private static float time = 2.0f;
    private static GameObject timeUI;
    private static Slider slider;

    void Update()
    {
        if (slider != null)
        {
            slider.value -= Time.deltaTime * (1.0f / time);
        }
    }

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }

    public override void ApplyItemEffect(PlayerController playerController)
    {
        playerController.BoostOn();

        Collider[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle")
                                        .Select(o => o.GetComponent<Collider>())
                                        .ToArray();
        Collider[] obstacleChildren = GameObject.FindGameObjectsWithTag("ObstacleChild")
                                        .Select(o => o.GetComponent<Collider>())
                                        .ToArray();

        foreach (Collider obstacle in obstacles)
        {
            if (obstacle != null)
            {
                Physics.IgnoreCollision(obstacle, playerController.GetComponent<Collider>(), true);
            }
        }
        foreach (Collider obstacleChild in obstacleChildren)
        {
            if (obstacleChild != null)
            {
                Physics.IgnoreCollision(obstacleChild, playerController.GetComponent<Collider>(), true);
            }
        }
    }
    
    public override void PlaySoundEffect(EffectManager effectManager)
    {
        effectManager.PlayItemBoostSound();
    }

    public override void PlayParticleEffect(ParticleSystem[] particleSystems)
    {
        if (particleSystems[PARTICLE_BOOST] && particleSystems[PARTICLE_BOOST_RUN])
        {
            particleSystems[PARTICLE_BOOST].Play();
            particleSystems[PARTICLE_BOOST_RUN].Play();
        }
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {
        timeUI = Instantiate(timeCanvasPrefabs[UI_BOOST], new Vector3(0, 0, 0), Quaternion.identity);
        slider = timeUI.transform.Find("BoostSlider").GetComponent<Slider>();
    }

    public override void RemoveItemEffect(PlayerController playerController)
    {
        playerController.BoostOff();
        
        Collider[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle")
                                        .Select(o => o.GetComponent<Collider>())
                                        .ToArray();
        Collider[] obstacleChildren = GameObject.FindGameObjectsWithTag("ObstacleChild")
                                        .Select(o => o.GetComponent<Collider>())
                                        .ToArray();

        foreach (Collider obstacle in obstacles)
        {
            if (obstacle != null)
            {
                Physics.IgnoreCollision(obstacle, playerController.GetComponent<Collider>(), false);
            }
        }
        foreach (Collider obstacleChild in obstacleChildren)
        {
            if (obstacleChild != null)
            {
                Physics.IgnoreCollision(obstacleChild, playerController.GetComponent<Collider>(), false);
            }
        }

        if (timeUI != null)
        {
            Destroy(timeUI);
        }
    }
}

class ItemThunder : Item
{
    private static string itemName = "Item_Thunder";
    private static float time = 0.0f;

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }

    public override void ApplyItemEffect(PlayerController playerController)
    {
        ItemEffect itemEffect = GameObject.Find("Duck").GetComponent<ItemEffect>();
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        var closestObstacles = obstacles.Where(o => o.transform.position.x > transform.position.x - 5)
                                        .Where(o => o.transform.position.x < transform.position.x + 20)
                                        .ToArray();
        
        itemEffect.SpawnThunderEffect(closestObstacles); // Destroy in Item Effect Code
    }
    
    public override void PlaySoundEffect(EffectManager effectManager)
    {
        effectManager.PlayItemThunderSound();
    }
    
    public override void PlayParticleEffect(ParticleSystem[] particleSystems)
    {
        
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {
        
    }

    public override void RemoveItemEffect(PlayerController playerController)
    {

    }
}

class ItemFly : Item
{
    private static string itemName = "Item_Fly";
    private static float time = 3.0f;
    private static GameObject timeUI;
    private static Slider slider;

    void Update()
    {
        if (slider != null)
        {
            slider.value -= Time.deltaTime * (1.0f / time);
        }
    }

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }
    
    public override void ApplyItemEffect(PlayerController playerController)
    {
        playerController.FlyOn();
    }

    public override void PlaySoundEffect(EffectManager effectManager)
    {
        effectManager.PlayItemFlySound();
    }

    public override void PlayParticleEffect(ParticleSystem[] particleSystems)
    {
        if (particleSystems[PARTICLE_FLY])
        {
            particleSystems[PARTICLE_FLY].Play();
        }
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {
        timeUI = Instantiate(timeCanvasPrefabs[UI_FLY], new Vector3(0, 0, 0), Quaternion.identity);
        slider = timeUI.transform.Find("FlySlider").GetComponent<Slider>();
    }

    public override void RemoveItemEffect(PlayerController playerController)
    {
        playerController.FlyOff();

        if (timeUI != null)
        {
            Destroy(timeUI);
        }
    }
}

class ItemDouble : Item
{
    private static string itemName = "Item_Double";
    private static float time = 3.0f;
    private static GameObject timeUI;
    private static Slider slider;

    void Update()
    {
        if (slider != null)
        {
            slider.value -= Time.deltaTime * (1.0f / time);
        }
    }

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }
    
    public override void ApplyItemEffect(PlayerController playerController)
    {
        playerController.DoubleOn();
    }
    
    public override void PlaySoundEffect(EffectManager effectManager)
    {
        effectManager.PlayItemDoubleSound();
    }

    public override void PlayParticleEffect(ParticleSystem[] particleSystems)
    {
        if (particleSystems[PARTICLE_DOUBLE])
        {
            particleSystems[PARTICLE_DOUBLE].Play();
        }
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {
        timeUI = Instantiate(timeCanvasPrefabs[UI_DOUBLE], new Vector3(0, 0, 0), Quaternion.identity);
        slider = timeUI.transform.Find("DoubleSlider").GetComponent<Slider>();
    }

    public override void RemoveItemEffect(PlayerController playerController)
    {
        playerController.DoubleOff();

        if (timeUI != null)
        {
            Destroy(timeUI);
        }
    }
}