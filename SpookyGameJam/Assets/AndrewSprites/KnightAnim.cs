using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnim : MonoBehaviour {

    SpriteScript spriteAnim;
    public string spriteSheet;

    private void Start()
    {
        spriteAnim = GetComponent<SpriteScript>();
        //spriteAnim.SetSpriteSheet(spriteSheet);
        spriteAnim.SetAnimation("WalkForward");
        spriteAnim.SetFramesPerSecond(12);
    }

    private void Update()
    {
        
    }
}
