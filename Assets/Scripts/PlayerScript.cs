using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private float speed = 1f;


    private Transform _transformY;
    private Transform _transformX;
    private Vector3 _currentPosY;
    private Vector3 _currentPosX;

    [SerializeField]public Tilemap wallmap;
    [SerializeField]public Tilemap floormap;
    [SerializeField]public Tile wall;
    [SerializeField]public Tile floor;
    [SerializeField]public Grid grid;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void back(){
        Vector3 move = transform.position;
        move.y -= speed;    
        transform.position = move;

    }
    void front(){
        Vector3 move = transform.position;
        move.y += speed;
        transform.position = move;

    }
    void left(){
        Vector3 move = transform.position;
        move.x -= speed;
        transform.position = move;

    }
    void right(){
        Vector3 move = transform.position;
        move.x += speed;
        transform.position = move;

    }
     Tile makeFloor () {
     // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
     var texture = new Texture2D(36, 36, TextureFormat.ARGB32, false);
 
     // set the pixel values
     for(int i = 0; i < 36; i++){
         for(int j = 0; j < 36;j++){
             texture.SetPixel(i, j, new Color(0.5f+i/72.0f,0.5f+j/72.0f,0.5f+(i+j)/144.0f));
         }
     }
     texture.Apply();
     
     var sprite = Sprite.Create(texture, new Rect(0f,0f,texture.width,texture.height*0.7f), new Vector2(0.5f,0.5f), 36.0f);
     var tile = ScriptableObject.CreateInstance<Tile>();
     tile.sprite = sprite;
     return tile;
     //texture완성
 }

      Tile makeWall () {
     // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
     var texture = new Texture2D(36, 36, TextureFormat.ARGB32, false);
 
     // set the pixel values
     for(int i = 0; i < 36; i++){
         for(int j = 0; j < 36;j++){
             texture.SetPixel(i, j, new Color(0.5f+i/72.0f,0.5f+j/72.0f,0.5f+(i+j)/144.0f));
         }
     }
     texture.Apply();
     
     var sprite = Sprite.Create(texture, new Rect(0f,0f,texture.width,texture.height*0.7f), new Vector2(0.5f,0.5f), 36.0f);
     var tile = ScriptableObject.CreateInstance<Tile>();
     tile.sprite = sprite;
     return tile;
     //texture완성
 }
    void Update()
    {
        Vector3 move = transform.position;



        if (Input.GetKey(KeyCode.S)) 
            back();

        if (Input.GetKey(KeyCode.W))
            front();

        if (Input.GetKey(KeyCode.A))
            left();

        if (Input.GetKey(KeyCode.D))
            right();

        if (Input.GetKey(KeyCode.T)){
            Debug.Log(grid.WorldToCell(transform.position));
            floormap.SetTile(grid.WorldToCell(transform.position),floor);
        }
            if (Input.GetKey(KeyCode.Y)){
            Debug.Log(grid.WorldToCell(transform.position));
            floormap.SetTile(grid.WorldToCell(transform.position),makeFloor());
        }
 
 
    }
}
