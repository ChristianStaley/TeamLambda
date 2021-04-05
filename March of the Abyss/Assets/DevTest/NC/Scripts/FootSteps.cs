using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] gravelClips;
    [SerializeField]
    private AudioClip[] grassClips;
    [SerializeField]
    private AudioClip[] waterClips;
    [SerializeField]
    private AudioClip[] woodClips;
    [SerializeField]
    private AudioClip[] mudClips;

    private AudioSource audioSource;
    private TerrainDetector terrainDetector;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        terrainDetector = new TerrainDetector();
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        switch (terrainTextureIndex)
        {
            case 0:
            default:
                return gravelClips[UnityEngine.Random.Range(0, gravelClips.Length)];
            case 1:
                return grassClips[UnityEngine.Random.Range(0, grassClips.Length)];
            case 2:
                return waterClips[UnityEngine.Random.Range(0, waterClips.Length)];
            case 3:
                return woodClips[UnityEngine.Random.Range(0, woodClips.Length)];
            case 4:
                return mudClips[UnityEngine.Random.Range(0, mudClips.Length)];
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
