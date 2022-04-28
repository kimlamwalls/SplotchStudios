using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public bool isAlive = true;
    public Animator deathMenuAnim;
    
    public Slider playerHealthBar;
    [SerializeField] public float Health = 100;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.value = Health;
    }

    public void Damage(int amount)
    {
        Health -= amount;
        if (Health <= 0) 
            Health = 0;
            Death();
    }

    public void Damage(float amount)
    {
        Health -= amount;
        if (Health <= 0) 
            Health = 0;
            Death();
    }
    
    public void Heal(int amount)
    {
        Health += amount;
        if (Health >= 100) Health = 100;
    }
    public void Heal(float amount)
    {
        Health += amount;
        if (Health >= 100) Health = 100;
    }
    
    public void Death()
        {
            isAlive = false;
            deathMenuAnim.SetTrigger("Show");
        }

}
