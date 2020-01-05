using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootRoom : MonoBehaviour
{

    public bool generation;

    public int maxRoom;
    public int rightRoomNumber;
    public int leftRoomNumber;


    // Listing des salles normales
    public int normalRoomMax;
    public GameObject[] normalRoomListe = new GameObject[1];

    public GameObject normal1;
    //public GameObject normal2;
    //public GameObject normal3;


    //public int secretRoom;
    public int shopRoomMax;
    public GameObject shopRoom;


    public int treasorRoomMax;
    public GameObject treasorRoom;

    public GameObject KeyRoom;
    public bool keyRight;
    public bool keyLeft;

    // Génration d'une salle, utilisation de coordonnées sur la grille
    public int maxX;
    public int maxY;
    public int maxZ;

    public int Corridor;
    public int xRoom;
    public int yRoom;
    public int zRoom;
    public int directionGen;

    public bool[,,] grid = new bool[6, 6, 6];// x for vertical, y for right and z for left

    public float xDimensions;
    public float yDimensions;

    public float xCoordonate;
    public float yCoordonate;

    public GameObject[] listeRight = new GameObject[17];
    public GameObject[] listeLeft = new GameObject[17];
    //public List<GameObject> roomList = new List<GameObject>();

    //Gestion de la génération
    public bool genRight;
    public bool genLeft;

    public int maxRight;
    public int maxLeft;

    // door

    public float doorX;
    public float doorY;

    public float doorCoordX;
    public float doorCoordY;

    //public GameObject doorRight;
    //public GameObject doorUp;
    public GameObject tph;
    public GameObject tpv;

    //public List<GameObject> normalListe = new List<GameObject>();

    void Start()
    {
        generation = true;

        xDimensions = 2*23.565f; //23.565f
        yDimensions = 2*13.312f;//13.312f

        doorX = 23.565f;
        doorY = 13.312f;


        grid[0, 0, 0] = true;
        rightRoomNumber = 0;
        leftRoomNumber = 0;
        maxX = 0;
        maxY = 0;
        maxZ = 0;

        Corridor = 0;

        int geter;

        maxRight = Random.Range((maxRoom/3),(maxRoom/3)*2);
        maxLeft = maxRoom - maxRight;
        
        genRight = true;
        
        int shopR = Random.Range(1, maxRight);
        int TresorOneR = Random.Range(1, maxRight);
        if (TresorOneR == shopR)
        {
            TresorOneR++;
        }
        int TresorTwoR = Random.Range(1, maxRight);
        if (TresorTwoR == shopR || TresorTwoR == TresorOneR)
        {
            TresorTwoR = shopR-1;
        }


        for (int i = 0; i<= maxRight; i++)
        {
            if ( i == shopR)
            {
                listeRight[i] = shopRoom;
               // Debug.Log("Salle shop droite remplie en case : " + i);
            }
            else if (i == TresorOneR)
            {
                listeRight[i] = treasorRoom;
                //Debug.Log("Salle trésor 1 droite remplie en case : " + i);
            }
            else if (i == TresorTwoR)
            {
                listeRight[i] = treasorRoom;
                //Debug.Log("Salle trésor 2 droite remplie en case : " + i);
            } else
            {
                geter = Random.Range(0, normalRoomListe.Length);
                listeRight[i] = normalRoomListe[geter];
                //Debug.Log("Salle " + geter + " normale droite remplie en case : " + i);
            }
        }

        int shopL = Random.Range(1, maxLeft);
        int TresorOneL = Random.Range(1, maxLeft);
        if (TresorOneL == shopL)
        {
            TresorOneL++;
        }
        int TresorTwoL = Random.Range(1, maxLeft);
        if (TresorTwoL == shopL || TresorTwoL == TresorOneL)
        {
            TresorTwoL = shopL - 1;
        }

        for (int i = 0; i <= maxLeft; i++)
        {
            if (i == shopL)
            {
                listeLeft[i] = shopRoom;
                //Debug.Log("Salle shop gauche remplie en case : " + i);
            }
            else if (i == TresorOneL)
            {
                listeLeft[i] = treasorRoom;
                //Debug.Log("Salle trésor 1 gauche remplie en case : " + i);
            }
            else if (i == TresorTwoL)
            {
                listeLeft[i] = treasorRoom;
                //Debug.Log("Salle trésor 2 gauche remplie en case : " + i);
            }
            else
            {
                geter = Random.Range(0, normalRoomListe.Length);
                listeLeft[i] = normalRoomListe[geter];
                //Debug.Log("Salle " + geter + " normale gauche remplie en case : " + i);
            }
        }
        
    }   

    
    


    void Update()
    {
        if (generation == true) // Début de la génération
        {
           //Génération droite
            if (Corridor == 0) // si corridor = 0 alors la salle va se générer à droite
            {
                xRoom = Random.Range(0, maxX + 1);
                yRoom = Random.Range(0, maxY + 1);

                if (genRight == true & grid[xRoom, yRoom, 0] == true)
                {
                    
                    directionGen = Random.Range(0, 4);

                    if(directionGen == 0 && yRoom < 4 && xRoom != 0) // génère vers le haut
                    {
                        if (grid[xRoom, yRoom+1, 0] == false )
                        {

                            grid[xRoom, yRoom+1, 0] = true;
                            if (yRoom == maxY)
                            {
                                maxY++;
                            }
                            rightRoomNumber++;

                            xCoordonate = xDimensions * xRoom;
                            yCoordonate = yDimensions * (yRoom+1);

                            Vector2 genRightUp = new Vector2(xCoordonate, yCoordonate);
                            GameObject currentRoom = Instantiate(listeRight[rightRoomNumber], genRightUp, Quaternion.identity);

                            //Debug.Log(xRoom + " " + yRoom + " " + 0 + " génère en haut");
                        }
                        else
                        {
                            directionGen++;
                        }
                    }
                    if (directionGen == 1 && xRoom < 4) // génère vers la droite
                    {
                        if (grid[xRoom+1, yRoom, 0] == false )
                        {
                            grid[xRoom + 1, yRoom, 0] = true;
                            if (xRoom == maxX)
                            {
                                maxX++;
                            }

                            rightRoomNumber++;
                            xCoordonate = xDimensions * (xRoom+1);
                            yCoordonate = yDimensions * yRoom;

                            Vector2 genRightRight = new Vector2(xCoordonate, yCoordonate);
                            GameObject currentRoom = Instantiate(listeRight[rightRoomNumber], genRightRight, Quaternion.identity);


                            //Debug.Log(xRoom + " " + yRoom + " " + 0 + " génère à droite");
                        }
                        else
                        {
                            directionGen++;
                        }
                    }
                    if (directionGen == 2 && yRoom > 0) // génère vers le bas
                    {
                        if ( grid[xRoom, yRoom - 1, 0] == false ) {
                            grid[xRoom, yRoom - 1, 0] = true;
                            rightRoomNumber++;

                            xCoordonate = xDimensions * xRoom;
                            yCoordonate = yDimensions * (yRoom-1);

                            Vector2 genRightDown = new Vector2(xCoordonate, yCoordonate);
                            GameObject currentRoom = Instantiate(listeRight[rightRoomNumber], genRightDown, Quaternion.identity);


                            //Debug.Log(xRoom + " " + yRoom + " " + 0 + " génère en bas");
                        }
                        else
                        {
                            directionGen++;
                        }
                    }
                    if (directionGen == 3 && xRoom > 1) // génère vers la gauche
                    {
                        if (grid[xRoom - 1, yRoom, 0] == false )
                        {
                            grid[xRoom - 1, yRoom, 0] = true;
                            rightRoomNumber++;

                            xCoordonate = xDimensions * (xRoom-1);
                            yCoordonate = yDimensions * yRoom;

                            Vector2 genRightLeft = new Vector2(xCoordonate, yCoordonate);
                            GameObject currentRoom = Instantiate(listeRight[rightRoomNumber], genRightLeft, Quaternion.identity);



                           // Debug.Log(xRoom + " " + yRoom + " " + 0 + " génère à gauche");
                        }
                    }
                }
                if (rightRoomNumber == maxRight & genRight == true ) //une fois toutes les salles posées, on génère la salle clée sur la droite et la génération passe à la partie gauche
                {
                    
                    for (int i = 4; i>=0; i--) // test les salles les plus éloignée à droite du point de départ pour en créer une annexe clé
                    {
                        if (grid[maxX,i,0] == true) // vérifie les salles en partant du plus haut x 
                        {
                            if(maxX < 4)
                            {
                                if (grid[maxX+1, i, 0] == false)
                                { 
                                    Corridor = 1;
                                    genRight = false;
                                    genLeft = true;

                                    grid[maxX +1 , i, 0] = true;

                                    xCoordonate = xDimensions * (maxX+1);
                                    yCoordonate = yDimensions * i;

                                    Vector2 genRightRight = new Vector2(xCoordonate, yCoordonate);
                                    GameObject currentRoom = Instantiate(KeyRoom, genRightRight, Quaternion.identity);

                                    keyRight = true;
                                    //Debug.Log("La salle de coordonnées : " + maxX + "+1" + " " + i + " " + 0 + " est une salle clé !");
                                    break;
                                }
                            }
                            else if (i < 4)
                            {
                                if (grid[maxX, i+1, 0] == false)
                                {
                                    Corridor = 1;
                                    genRight = false;
                                    genLeft = true;

                                    grid[maxX, i + 1, 0] = true;

                                    xCoordonate = xDimensions * maxX;
                                    yCoordonate = yDimensions * (i+1);

                                    Vector2 genRightUp = new Vector2(xCoordonate, yCoordonate);
                                    GameObject currentRoom = Instantiate(KeyRoom, genRightUp, Quaternion.identity);

                                    keyRight = true;
                                    //Debug.Log("La salle de coordonnées : " + maxX + " " + i + "+1" + " " + 0 + " est une salle clé !");
                                    break;
                                }
                                
                            }
                            else if (i < 1)
                            {
                                if (grid[maxX, i-1, 0] == false)
                                {
                                    Corridor = 1;
                                    genRight = false;
                                    genLeft = true;

                                    grid[maxX, i-1, 0] = true;

                                    xCoordonate = xDimensions * maxX;
                                    yCoordonate = yDimensions * (i - 1);

                                    Vector2 genRightDown = new Vector2(xCoordonate, yCoordonate);
                                    GameObject currentRoom = Instantiate(KeyRoom, genRightDown, Quaternion.identity);

                                    keyRight = true;
                                    //Debug.Log("La salle de coordonnées : " + maxX  + " " + i + "-1" + " " + 0 + " est une salle clé !");
                                    break;
                                }
                                
                            }
                            else if (maxX != 1)
                            {
                                if (grid[maxX-1, i, 0] == false)
                                {
                                    Corridor = 1;
                                    genRight = false;
                                    genLeft = true;

                                    grid[maxX-1, i, 0] = true;

                                    xCoordonate = xDimensions * (maxX - 1);
                                    yCoordonate = yDimensions * i;

                                    Vector2 genRightLeft = new Vector2(xCoordonate, yCoordonate);
                                    GameObject currentRoom = Instantiate(KeyRoom, genRightLeft, Quaternion.identity);

                                    keyRight = true;
                                    //Debug.Log("La salle de coordonnées : " + maxX + "-1" + " " + i  + " " + 0 + " est une salle clé !");
                                    break;
                                }
                                
                            }
                        }
                    }
                }

                if (keyRight == true && rightRoomNumber == maxRight) {
                    for(int j = 0; j <= maxY; j++)
                    {
                        for(int i = 0; i<=maxX; i++)
                        {
                            if ( grid[i,j,0] == true)
                            {
                                if ( i <= maxX & grid[i+1,j,0] == true)
                                {
                                    doorCoordX = doorX * (2*i+1);
                                    doorCoordY = doorY * (2*j);

                                    //Debug.Log("Salle " + i + " " +j + " " +0 + " créé une porte droite ");

                                    Vector2 genRightRight = new Vector2(doorCoordX, doorCoordY);
                                    GameObject currentRoom = Instantiate(tph, genRightRight, Quaternion.identity);
                                }
                                
                                if (j <= maxY & grid[i, j + 1, 0] == true)
                                {
                                    doorCoordX = doorX * (2*i);
                                    doorCoordY = doorY * (2*j+1);

                                    //Debug.Log("Salle " + i + " " + j + " " + 0 + " créé une porte haut ");

                                    Vector2 genRightUp = new Vector2(doorCoordX, doorCoordY);
                                    GameObject currentRoom = Instantiate(tpv, genRightUp, Quaternion.identity);
                                }
                            }
                        }
                    }
                }
            }

            

            //Génération gauche

            else // si corridor = 1 une salle va se générer à gauche
            {
                yRoom = Random.Range(0, maxY + 1);
                zRoom = Random.Range(0, maxZ + 1);

                if (genLeft == true & grid[0, yRoom , zRoom] == true)
                {
                    directionGen = Random.Range(0, 4);
                
                    if (directionGen == 0 && yRoom < 4 && zRoom != 0) // génère vers le haut
                    {
                        if (grid[0, yRoom + 1, zRoom] == false)
                        {

                            grid[0, yRoom + 1, zRoom] = true;
                            if (yRoom == maxY)
                            {
                                maxY++;
                            }
                            leftRoomNumber++;

                            xCoordonate = xDimensions * zRoom * (-1);
                            yCoordonate = yDimensions * (yRoom+1) ;

                            Vector2 genLeftUp = new Vector2(xCoordonate, yCoordonate);
                            GameObject currentRoom = Instantiate(listeLeft[leftRoomNumber], genLeftUp, Quaternion.identity);

                            //Debug.Log(0 + " " + yRoom + " " + zRoom + " génère en haut");
                        }
                        else
                        {
                            directionGen++;
                        }
                    }
                    if (directionGen == 1 && zRoom < 4) // génère vers la gauche
                    {
                        if (grid[0, yRoom , zRoom + 1] == false)
                        {
                            grid[0, yRoom, zRoom + 1] = true;
                            if (zRoom == maxZ)
                            {
                                maxZ++;
                            }
                            leftRoomNumber++;


                            xCoordonate = xDimensions * (zRoom+1) *(- 1);
                            yCoordonate = yDimensions * yRoom;

                            Vector2 genLeftLeft = new Vector2(xCoordonate, yCoordonate);
                            GameObject currentRoom = Instantiate(listeLeft[leftRoomNumber], genLeftLeft, Quaternion.identity);


                            //Debug.Log(0 + " " + yRoom + " " + zRoom + " génère à gauche");
                        }
                        else
                        {
                            directionGen++;
                        }
                    }
                    if (directionGen == 2 && yRoom > 0) // génère vers le bas
                    {
                        if (grid[0, yRoom-1, zRoom] == false)
                        {
                            grid[0, yRoom-1, zRoom] = true;
                            leftRoomNumber++;

                            xCoordonate = xDimensions * (-1) * zRoom;
                            yCoordonate = yDimensions * (yRoom-1);

                            Vector2 genLeftDown = new Vector2(xCoordonate, yCoordonate);
                            GameObject currentRoom = Instantiate(listeLeft[leftRoomNumber], genLeftDown, Quaternion.identity);

                            //Debug.Log(0 + " " + yRoom + " " + zRoom + " génère en bas");
                        }
                        else
                        {
                            directionGen++;
                        }
                    }
                    if (directionGen == 3 && zRoom > 1) // génère vers la droite
                    {
                        if (grid[0, yRoom , zRoom - 1] == false)
                        {
                            grid[0, yRoom , zRoom - 1] = true;
                            leftRoomNumber++;


                            xCoordonate = xDimensions * (-1) * (zRoom -1);
                            yCoordonate = yDimensions * yRoom;

                            Vector2 genLeftRight = new Vector2(xCoordonate, yCoordonate);
                            GameObject currentRoom = Instantiate(listeLeft[leftRoomNumber], genLeftRight, Quaternion.identity);

                            //Debug.Log(0 + " " + yRoom + " " + zRoom + " génère à gauche");
                        }
                    }
                }
            }

            if (leftRoomNumber == maxLeft & genLeft == true) //une fois toutes les salles posées, on génère la salle clée sur la droite et la génération passe à la partie gauche
            {

                for (int i = 4; i >= 0; i--) // test les salles les plus éloignée à droite du point de départ pour en créer une annexe clé
                {
                    if (grid[0, i, maxZ] == true) // vérifie les salles en partant du plus haut x 
                    {
                        if (maxZ < 4)
                        {
                            if (grid[0, i, maxZ +1 ] == false)
                            {
                                genLeft = false;
                                grid[0, i, maxZ + 1] = true;

                                xCoordonate = xDimensions * (maxZ + 1) * (-1);
                                yCoordonate = yDimensions * i;

                                Vector2 genLeftLeft = new Vector2(xCoordonate, yCoordonate);
                                GameObject currentRoom = Instantiate(KeyRoom, genLeftLeft, Quaternion.identity);

                                keyLeft = true;
                                //Debug.Log("La salle de coordonnées : " + 0 + " " + i + " " + maxZ + "+1" + " est une salle clé !");
                                break;
                            }
                        }
                        else if (i < 4)
                        {
                            if (grid[0, i + 1, maxZ] == false)
                            {
                                genLeft = false;
                                grid[0, i + 1, maxZ] = true;

                                xCoordonate = xDimensions * maxZ * (-1);
                                yCoordonate = yDimensions * (i + 1);

                                Vector2 genLeftUp = new Vector2(xCoordonate, yCoordonate);
                                GameObject currentRoom = Instantiate(KeyRoom, genLeftUp, Quaternion.identity);

                                keyLeft = true;
                                //Debug.Log("La salle de coordonnées : " + 0 + "-1" + " " + i + " " + maxZ + " est une salle clé !");
                                break;
                            }

                        }
                        else if (i != 1)
                        {
                            if (grid[0, i - 1, maxZ] == false)
                            {
                                genLeft = false;
                                grid[0, i - 1, maxZ] = true;

                                xCoordonate = xDimensions * (-1) * maxZ;
                                yCoordonate = yDimensions * (i - 1);

                                Vector2 genLeftDown = new Vector2(xCoordonate, yCoordonate);
                                GameObject currentRoom = Instantiate(KeyRoom, genLeftDown, Quaternion.identity);

                                keyLeft = true;
                                //Debug.Log("La salle de coordonnées : " + 0 + "-1" + " " + i + " " + maxZ + " est une salle clé !");
                                break;
                            }

                        }
                        else if (maxZ != 1)
                        {
                            if (grid[0, i, maxZ - 1] == false)
                            {
                                genLeft = false;
                                grid[0, i, maxZ - 1] = true;

                                xCoordonate = xDimensions * (-1) * (maxZ - 1);
                                yCoordonate = yDimensions * i;

                                Vector2 genLeftRight = new Vector2(xCoordonate, yCoordonate);
                                GameObject currentRoom = Instantiate(KeyRoom, genLeftRight, Quaternion.identity);

                                keyLeft = true;
                                //Debug.Log("La salle de coordonnées : " + 0 + " " + i + " " + maxZ + "-1" + " est une salle clé !");
                                break;
                            }
                        }
                    }
                }
            }
            if (keyLeft == true && leftRoomNumber == maxLeft)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    for (int i = 0; i <= maxZ; i++)
                    {
                        if (grid[0, j, i] == true)
                        {
                            if (i <= maxZ & grid[0, j, i + 1] == true)
                            {
                                doorCoordX = doorX * (2 * i + 1)*(-1);
                                doorCoordY = doorY * (2 * j);

                                //Debug.Log("Salle " + 0 + " " + j + " " + i + " créé une porte droite ");

                                Vector2 genLeftRight = new Vector2(doorCoordX, doorCoordY);
                                GameObject currentRoom = Instantiate(tph, genLeftRight, Quaternion.identity);
                            }

                            if (j <= maxY & grid[0, j + 1, i] == true)
                            {
                                doorCoordX = doorX * (2 * i)*(-1);
                                doorCoordY = doorY * (2 * j + 1);

                                //Debug.Log("Salle " + 0 + " " + j + " " + i + " créé une porte haut ");

                                Vector2 genLeftUp = new Vector2(doorCoordX, doorCoordY);
                                GameObject currentRoom = Instantiate(tpv, genLeftUp, Quaternion.identity);
                            }
                        }
                    }
                }
            }
        }
        if ( rightRoomNumber + leftRoomNumber == maxRoom)
        {
            generation = false;
        }
    }
}