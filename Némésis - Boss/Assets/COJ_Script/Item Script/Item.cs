using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public string price = "new price";
    public string description = "new description";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    // Quelles stats sont affectées par l'item

    public int howManybulleShot; // nb de balle par tir
    public float bulletLifeSpan; // portée de la balle
    public float delayBeforeFirstShot;// délai avant le premier tir
    public float delayBeforeNextShot;//délai avant le prochain tir
    public float bulletSpeed;//vitesse de la balle
    public Transform bulletSize;// taille de la balle
    public float bulletDamage;//dégat de la balle
    public float weaponAccuracy;//Précision de l'arme
    public float healthBonus;//Bonus de Point de vie
    public float playerSpeed;//Bonus de vitesse du joueur
    public GameObject bulletPrefab;// Forme de la balle (dépend du type d'arme)

    public bool isWeapon;// Si l'objet est une arme
    public int weaponRef;//Ref de l'arme

}
