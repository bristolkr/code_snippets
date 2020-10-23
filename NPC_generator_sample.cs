//This c# file was written to generate random NPC characters for a color- and shape-matching game built with Unity 2019. 
//It pulls input from the Unity Editor to generate NPCs. In the editor, the SerializeField parameters allow other developers 
//to specify the number of NPCs to generate, as well as the desired texture file, physics material, animation, and sound file to attach to each NPC.
//Because the game this file is pulled from needs to have specific colors for matching purposes, the colors are declared here and loaded into an array for random assignment.
//The generated NPCs have the necessary components to function within the game space attached, as well as any behavioral scripts. 
//This all happens at runtime, with no loss of performance.
//
//Copyright 2019, Kelly Bristol for Psychometric Studios Interactive, LLC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCGenerator : MonoBehaviour {

    public int _numberOfNPCs;

    [SerializeField]
    private Texture2D _texture;

    [SerializeField]
    private Material _material;

    [SerializeField]
    private RuntimeAnimatorController _animator;

    public AudioClip _mismatchTone;

    [SerializeField]
    private Texture2D _halo;

    AudioSource audioSource;

    private Color[] npcColors = new Color[6];

    

    public static Color npcYellow = new Color(1f, 1f, 0.392f, 1f);          //yellow
    public static Color npcRed = new Color(1f, 0.392f, 0.392f, 1f);         //red
    public static Color npcBlue = new Color(0.392f, 0.392f, 1f, 1f);        //blue
    public static Color npcGreen = new Color(0f, 0.84f, 0f, 1.0f);          //green
    public static Color npcOrange = new Color(1.0f, 0.51f, 0f, 1.0f);       //orange
    public static Color npcPurple = new Color(0.74f, 0f, 1.0f, 1.0f);       //purple
    public static Color npcRedOrange = new Color(1.0f, 0.57f, 0f, 1.0f);    //red-orange collision
    public static Color npcMagenta = new Color(0.87f, 0f, 0.5f, 1.0f);      //red-purple collision
    public static Color npcTeal = new Color(0f, 0.42f, 0.5f, 1.0f);         //blue-green collision
    public static Color npcBlurple = new Color(0.43f, 0f, 1.0f, 1.0f);      //blue-purple collision
    public static Color npcYellowGreen = new Color(0.5f, 0.92f, 0f, 1.0f);  //yellow-green collision
    public static Color npcYellowOrange = new Color(1.0f, 0.78f, 0f, 1.0f); //yellow-orange collision
    
    void Start ()
    {
        
       npcColors[0] = npcYellow;
       npcColors[1] = npcRed;
       npcColors[2] = npcBlue; 
       npcColors[3] = npcGreen; 
       npcColors[4] = npcOrange; 
       npcColors[5] = npcPurple;

        for (int i = 0; i < _numberOfNPCs; ++i)
        {
            var npcTransform = InstantiateNPCObject();

            npcTransform.position = new Vector3(Random.Range(-48f, 48f), Random.Range(-48f, -10f), 0f);
            Debug.Log("NPCs Loaded");
        }
    }

    private Transform InstantiateNPCObject()
    {
        var sprite = Sprite.Create(_texture, new Rect(0.0f, 0.0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        var npcTransform = new GameObject("NPC").transform;
        var spriteRenderer = npcTransform.gameObject.AddComponent<SpriteRenderer>();
        var spriteRigidbody = npcTransform.gameObject.AddComponent<Rigidbody2D>();
        var spriteCollider = npcTransform.gameObject.AddComponent<PolygonCollider2D>();
        var spriteAnimator = npcTransform.gameObject.AddComponent<Animator>();
        var spriteControl = npcTransform.gameObject.AddComponent<NPCBehavior>();
        var spriteTag = npcTransform.gameObject.tag = "NPC";
        var spriteAudio = npcTransform.gameObject.AddComponent<AudioSource>();

        spriteRigidbody.gravityScale = 0;
        spriteAnimator.runtimeAnimatorController = _animator;
        npcTransform.GetComponent<SpriteRenderer>().material = _material;
        spriteRenderer.sortingLayerName = "Default";
        spriteRenderer.sortingOrder = 2;
        spriteRenderer.material.color = npcColors[Random.Range(0, npcColors.Length)];
        spriteAudio.clip = _mismatchTone;

        spriteRenderer.sprite = sprite;
        return npcTransform;
    }
}
 

