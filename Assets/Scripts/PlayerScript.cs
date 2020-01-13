using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Drawing;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private float speed = 1f;

    private Transform _transformY;
    private Transform _transformX;
    private Vector3 _currentPosY;
    private Vector3 _currentPosX;

    [SerializeField]public Tilemap furnitureMap;
    [SerializeField]public Tilemap wallmap;
    [SerializeField]public Tilemap floormap;
    [SerializeField]public Tile wall;
    [SerializeField]public Tile floor;
    [SerializeField]public Grid grid;
    [SerializeField]public GameObject me;

    /*
    0: 북   UP
    1: 동   RIGHT
    2: 남   LEFT
    3: 서   DOWN
    */

    Tile putFurniture(string s){
        int sz = 20;
        float fsz = 100f;
        var texture = new Texture2D(36,36);
        string filePath = "\\Furnitures\\";
        byte[] fileData;
        var sprite = me.GetComponent<SpriteRenderer>().sprite;
        var pos =new Vector2[2];
        var tile =ScriptableObject.CreateInstance<Tile>();
        Tile tileFurniture(string str){
            texture = Resources.Load<Texture2D>(str);
                    for(int i = 0; i < texture.width;i++){
                        for(int j = 0; j < texture.height;j++){
                            if(texture.GetPixel(i,j) == UnityEngine.Color.black){

                            }else if(texture.GetPixel(i,j)==new UnityEngine.Color(128,128,128)){

                            }
                        }
                    }
                    texture.Apply();
                     sprite = Sprite.Create(texture, new Rect(0f,0f,texture.width,texture.height), new Vector2(0.5f,0.25f), fsz);
                     pos = new Vector2[4] {new Vector2(0f,0f),new Vector2(0f,1f),new Vector2(1f,0f),new Vector2(1f,1f)};

                    sprite.OverridePhysicsShape(new List<Vector2[]> {
                        new Vector2[] { new Vector2(0, 0), new Vector2(sz, 0), new Vector2(sz, sz), new Vector2(0, sz) }
                    });
                     tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = sprite;
                    return tile;
                }
        switch (s){
            case "bed":
                return tileFurniture("bed");
                break;
            case "book":
                return tileFurniture("book");
                break;
            case "chair":
                return tileFurniture("chair");
                break;
            case "chalkboard":
                return tileFurniture("chalkboard");
                break;
            case "computer":
                return tileFurniture("computer");
                break;
            case "cup":
                return tileFurniture("cup");
                break;
            case "refrigerator":
                return tileFurniture("bed");
                break;
            case "stand":
                return tileFurniture("stand");
                break;
            case "table":
                return tileFurniture("table");
                break;
            default:
                tile = ScriptableObject.CreateInstance<Tile>();
                return tile;
        }
    }

    
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
    void placeFurniture(string str){
        wallmap.SetTile(grid.WorldToCell(transform.position),putFurniture(str));
    }


    Tile generateWall (string str) {
        byte[] b64_bytes = System.Convert.FromBase64String(str);
        var texture = new Texture2D(36,50);
        texture.LoadImage( b64_bytes);
        texture.Apply();
        var sprite = Sprite.Create(texture, new Rect(0f,0f,texture.width,texture.height), new Vector2(0.5f,0.25f), 36.0f);
        var pos = new Vector2[4] {new Vector2(0f,0f),new Vector2(0f,1f),new Vector2(1f,0f),new Vector2(1f,1f)};

        sprite.OverridePhysicsShape(new List<Vector2[]> {
            new Vector2[] { new Vector2(0, 0), new Vector2(36, 0), new Vector2(36, 36), new Vector2(0, 36) }
        });
        var tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        return tile;
        //texture완성
    }
    void makeWall(string str){
        wallmap.SetTile(grid.WorldToCell(transform.position),generateWall(str));
    }

    
    Tile generateFloor (string str) {
        byte[] b64_bytes = System.Convert.FromBase64String(str);
        var texture = new Texture2D(36,36);
        texture.LoadImage( b64_bytes);
        texture.Apply();
        var sprite = Sprite.Create(texture, new Rect(0f,0f,texture.width,texture.height), new Vector2(0.5f,0.5f), 36.0f);
        var pos = new Vector2[4] {new Vector2(0f,0f),new Vector2(0f,1f),new Vector2(1f,0f),new Vector2(1f,1f)};

        var tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        return tile;
        //texture완성
    }

    void makeFloor(string str){
        floormap.SetTile(grid.WorldToCell(transform.position),generateWall(str));
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
            //Debug.Log(grid.WorldToCell(transform.position));
            string str = "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";
            makeFloor(str);
        }
            if (Input.GetKey(KeyCode.Y)){
            string str = "iVBORw0KGgoAAAANSUhEUgAAACQAAAAyCAIAAAC77e5JAAAAA3NCSVQICAjb4U/gAAAIFElEQVRYhUWYS3Ll0I5rF0A576ReRHVq/gMqi8BrUJnXHdsROmdzk/hR+t//+X8Sfx6TrYw98fw8ba0Wq/TxqAA10BalrdH/ZSU99rbgmWnZLgFJbNDuWpsd20auRv4PQxk/85utS43e+XkAMZXagiIk2S4z82MbcC0paXiFJdX3W5KsPwVL+mNfGdM/tiMeGdzH0rS1jZJEkoRJGGC9g/5+XwcBYJmCJDOq/vPzJ8k8dtv2601U0Qnc4TD+YQwEfmYklZUES/10IlpRx/M2LpLaeqRNqK0kM1dPszRJzQRJFbYlDbqTbNvGroV9n5PbdlBFhMrMMLrBqPEMIUlL5W39PIbaZoPVNn1rz/W9BUaVaz9SDWKsp5KeqTvC5Mcz6qjfOOW2z/MAGNt//Dx532f+pDt+biTjeSS5SYY/UUPR4CrS0CAk6e8UdX12Jzc1NKIHwZnQbtv4mT+MH2PP15YukA9R+f4sSWyLn5kBwvL9fHBc37/BSKNivt5ANTzX7kV0GWoN/0GB0fy0+/y4ETJ6iyS9+1oy0s8DAbV22YGUWgVyp0jNVirlGYRl/2kLsR1Fsslhsi02lFoSxOiY911LrZxkguS9A+32bQWVBG37eNhuW9f1AK1K68cCaNNGBatdSwzkI+z1zVSGepsxb5+2tbwCLfI8k312f/GfMvzcl+exf/wTUaF+95MFkR5SubgIi69VUraynKKRCsyrEo0fNan1WPPzODRqpOIJ+s1vEkhbNH+H/JCimEEzeuiJ1EiauSn+MXqqY/fMHNA03k9Y9cEenLyQiu8kCyw1umqEFApU8aCNEa0kFKVYWL0mWyaV/6oYoJCeDj2er0r3H4fqmTjU9nHLdtu0tiu44Wk+7EguI/978vTQQBagGJKRTXVE+YjTNkHSNjPTdvcX3JaeuvZvqwGS92RPpe2pjZHbfpcgIsrmrxBXh/LY1vgv9k4mH9v2gzsyLfivfESaT0hcQFH3xbIHMxXU9CFHrZPmD9+NJL1NlIj9rARg0YkXXaWhlr55p5vPPTQmazEoPvHOabGCpVF08JFbd2ZmfnzdGi9f65YFZF+VxYep/i2p/Wd5/9zs5CMHubR7H1aXmnoYpTMzyOVs86+B3XdfLGjb8JxPXWe6Keu2jf4+sWfNd/el34iUu1w1B7Abz1fZplYUSEK7UdTS5RgkHtu1TYJCieh+ZrDUarvVVmeHB5mIaqynvlPztzFeblpyLbWgUgX43Q34bdQkyfl1C0E5a5vzc0n1hrE58l6dJ6QWXNy6NgG8zRuXbXLtCXrokzIPlAra09gstsqKp40lkRbBP0aPQTqqpFLelwX6RbOmI5QmPSlEuMWt4LGfDw5S+jaCFichaoxEXSzpLf9ltCLN8POPcO2e8rU1hTxJQJyGQ66IdlT72caCAjMzSWCSVIymVvMOFCKkgugpTqDJTpCVY8f6lXpelf3KfOztHeksjWzvLlbzAirbkBuRo++K+wU9GvXtMH7mAy0xbattJflDg3MITbapwL0AcjSuOAWQK+m3b1s3OX6+e2l15d/dhK1+eYOe8Y9pAEzKSE1ktwET1bRpApf6PuMW7/YTCLS0USjVb7bEotK2htQQt12ZlGzo7QOnh/A5faoezE7SwjZJkzdJu0kacZo5+N/+ASQfFNoDSKVxK/ei11xA6Sd/bYWTlaQSCB25WOQDXE/b+Dt3JTUv+rsAtc/15jRLoX7V6bekRLXlbvK5qPk0bLYBsv1rPZQV1tUIapHJLlWb9vnCFlp4dG70c/IvlGO5vuLIgkhwT0JDFXoHh+jtZQ0cZeI9nuLSh2yZ1T66XPuU+kJ/OiryhaRujo4S3Y3Vd+1J/0VjFIXsXl6eXAgQ26g8byqtuG+PNXUT1Eo+gn42iSSabTG6B8LhlLto86Lpf5OZPqSkSNZnSAu8SU/X1PbvtoMrCN3tJolKm9D3fZPsbhK13QV2979Q/IvDJOo+obzxuBsdsuEro9mtrWrVqzHQlx2J7GhAeEK294zafChOcxj5Ls2TrUg9EZZbvV3Ffy20u7/4fEoT9Jgcq4FtqlNyiKSsw5nwEvrhK7uRnH1bst032Za3b/6hi02bDxrJ27zv26alb1Z036POVuq21wLnxEUVNHtu94AKrqskMba9qdTktYakcgjJ4JcOYlcS767Ce/b47C6YkMtoH01iK6LJc4DrATLHoL2etIZGak7L9ZsX9JJBHtFTKLfdN61vZRHaAmEraXddBbwk7bu7+0nLUkndtz3RTtK++TaaBnhPD1s2p5b99rnZlupidsVu2l2S7POPIq6aN4JVTMQDvcqp7g0Qhqj9mefNaxSqvjZB578K2y3tBj3uvndL9Ci9zWBdFeRrp6RlowuY8Yvs5I36M/PuIpJWRdqtbOi+XTpIchrYk0n1Zf3kFtnKZXdFnvk5NBbmVmN1ZSfnBHkXed8+j5M9WciuJEsjuru0uSgP6UoWT1XLm7c9bdHv76/9kJU/VQp92k0sWR8zJe37W6Frpmj5PQC3TUG7G5ms5Fd6qN7UnpP1Eu7NgaYlWTgPqy6rBH2J5tSBHNTEJqI4p1gr5qL27YXZR0e0S16aFFfb2N+e4QxTfVnxBb4UgZpX/mGTZrh41fy+BxTTexO1u7uv9fO0b/UnuzbtKfACvQlVv2RunCf0f19wXBDo/srPvTjjFFxLRidet+VwUrtmFWWMEw4B6u5eBvm/LOR9349PXSXa2+bm89VNz25askQW27f9T0iSzSlX/z/2pX24WGBK/QAAAABJRU5ErkJggg==";
            makeWall(str);
            
        }
                    if (Input.GetKey(KeyCode.F)){
            placeFurniture("computer");
            
        }
 
 
    }
}
