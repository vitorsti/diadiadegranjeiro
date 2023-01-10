using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeParallax : MonoBehaviour
{
    public float speed;
    SpriteRenderer sr;
    public Sprite sprite;
    public Sprite[] sprites;

    public enum chooseSprite { multiple, single };
    public chooseSprite selectSpriteType;
    // Start is called before the first frame update
    void Awake()
    {
        if (sprite != null || sprites.Length != 0)
        {
            sr = GetComponent<SpriteRenderer>();
            if (selectSpriteType == chooseSprite.multiple)
            {
                sr.sprite = sprites[Random.Range(0, sprites.Length)];
            }
            if (selectSpriteType == chooseSprite.single)
            {
                sr.sprite = sprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "destroyer")
        {
            Destroy(this.gameObject);
        }
    }


}
