using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider playerHealthBar;
    [SerializeField] public float Health = 100;
    [SerializeField] public int MaxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.value = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.value = Health;
        if (Health == 0f)
        {
            var SceneController = GameObject.Find("SceneController");
            var test = SceneController.GetComponent<SceneChanger>();
            test.DeathScene();
        }
    }

    public void Damage(int amount)
    {
        Health -= amount;
        if (Health <= 0) 
            Health = 0;
    }

    public void Damage(float amount)
    {
        Health -= amount;
        if (Health <= 0) 
            Health = 0;
    }

    public void HealToMax() => Health = MaxHealth;
    public void Heal(int amount)
    {
        Health += amount;
        if (Health >= MaxHealth) Health = MaxHealth;
    }
    public void Heal(float amount)
    {
        Health += amount;
        if (Health >= MaxHealth) Health = MaxHealth;
    }
}
