using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    public Slider playerHealthBar;
    [SerializeField] int Health = 100;

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
    }

    public void Heal(int amount)
    {
        Health += amount;
    }


}
