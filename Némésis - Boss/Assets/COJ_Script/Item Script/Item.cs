using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    // Quelles stats sont affectées par l'item

    public float howManybulleShot; // nb de balle par tir
    public float bulletLifeSpan; // portée de la balle
    public float delayBeforeFirstShot;// délai avant le premier tir
    public float delayBeforeNextShot;//délai avant le prochain tir
    public float bulletSpeed;//vitesse de la balle
    public Transform bulletSize;// taille de la balle
    public int bulletDamage;//dégat de la balle
    public float weaponAccuracy;//Précision de l'arme
    public int healthBonus;//Bonus de Point de vie
    public int playerSpeed;//Bonus de vitesse du joueur

    public bool isWeapon;// Si l'objet est une arme
    public int weaponRef;//Ref de l'arme

}
