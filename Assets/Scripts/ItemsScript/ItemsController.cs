using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string nameItem;
    public string descriptionItem;
    public Sprite imageItem;
}
public class ItemsController : MonoBehaviour
{
    public Item item;
    [Header("Stats")]
    public int healthChange;
    public float moveSpeedChange;
    public float attackSpeedChange;
    public int damageChange;
    public float fishBulletSize;

    private GameObject player;

    PlayerLife playerLife;
    Movement playerMovement;
    FishLauncher fishLauncher;
    FishBulletController fishBulletController;
    PlayerDmg playerDmg;
    public GameObject popUp;
    public AudioSource sonitem;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sonitem = GameObject.Find("sonItem").GetComponent<AudioSource>();
        GetComponent<SpriteRenderer>().sprite = item.imageItem;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            sonitem.Play();
            HealPlayer(healthChange);
            MoveSpeedChange(moveSpeedChange);
            AttackSpeedChange(attackSpeedChange);
            DamageChange(damageChange);
            Popup();
        }

    }
    public void Popup()
    {
        Debug.Log("ZZ");
        Destroy(this.gameObject);
    }
    public void HealPlayer(int healAmount)
    {
        Debug.Log("Je récupère " + healAmount + " hp");
        playerLife = player.GetComponent<PlayerLife>();
        playerLife.health = Mathf.Min(playerLife.maxHealth, playerLife.health + healAmount);
    }
   
    public void MoveSpeedChange(float speed)
    {
        Debug.Log("Ma moove speed se multiplie par " + speed);
        playerMovement = player.GetComponent<Movement>();
        playerMovement.speed *= speed;
    }
    public void AttackSpeedChange(float attackSpeed)
    {
        Debug.Log("Mon Attack speed augmente de " + attackSpeed);
        fishLauncher = player.GetComponent<FishLauncher>();
        fishLauncher.fishCooldown -= attackSpeed;
    }
    public void DamageChange(int damage)
    {
        Debug.Log("Mes projectiles font + " + damage + " damages");
        playerDmg = player.GetComponent<PlayerDmg>();
        playerDmg.fishDamage += damage;
    }


}
