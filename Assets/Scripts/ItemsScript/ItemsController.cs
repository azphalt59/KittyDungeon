using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int healthChange;
    public float moveSpeedChange;
    public float attackSpeedChange;
    public int damageChange;
    public float fishBulletSize;

    PlayerLife playerLife;
    Movement playerMovement;
    FishLauncher fishLauncher;
    FishBulletController fishBulletController;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.imageItem;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            HealPlayer(healthChange);
            MoveSpeedChange(moveSpeedChange);
            AttackSpeedChange(attackSpeedChange);
            DamageChange(damageChange);

            Destroy(gameObject);

        }
    }

    public void HealPlayer(int healAmount)
    {
        Debug.Log("Je récupère " + healAmount + " hp");
        playerLife.health = Mathf.Min(playerLife.maxHealth, playerLife.health + healAmount);
    }
    public void MoveSpeedChange(float speed)
    {
        Debug.Log("Ma moove speed se multiplie par " + speed);
        playerMovement.speed *= speed;
    }
    public void AttackSpeedChange(float attackSpeed)
    {
        Debug.Log("Mon Attack speed augmente de " + attackSpeed);
        fishLauncher.fishCooldown -= attackSpeed;
    }
    public void DamageChange(int damage)
    {
        Debug.Log("Mes projectiles font + " + damage + " damages");
        fishBulletController.fishBulletDamage += damage;
    }


}
