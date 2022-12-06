using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private List<SpriteRenderer> spriteRenderersGrounds;
    [SerializeField] private List<Sprite> spritesGround;

    [Header("Stones")]
    [SerializeField] private List<SpriteRenderer> spriteRenderersStones1;
    [SerializeField] private List<SpriteRenderer> spriteRenderersStones2;
    [SerializeField] private List<Sprite> spritesStones1;
    [SerializeField] private List<Sprite> spritesStones2;

    // Start is called before the first frame update
    void Awake() 
    {
        ChangeSprites();
    }

    public void ChangeSprites()
    {
        foreach (var item in spriteRenderersGrounds)
        {
            int num = Random.Range(0, spritesGround.Count);
            item.sprite = spritesGround[num];
        }

        foreach (var item in spriteRenderersStones1)
        {
            int num = Random.Range(0, spritesStones1.Count);
            item.sprite = spritesStones1[num];
        }

        foreach (var item in spriteRenderersStones2)
        {
            int num = Random.Range(0, spritesStones1.Count);
            item.sprite = spritesStones2[num];
        }
    }
}
