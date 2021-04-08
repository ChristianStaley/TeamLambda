using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexiAmbiCube : MonoBehaviour
{
    // To Implement Ambis put in player prefab, the main music player sound source and find the appropriate string for the ambience you want to play


    [SerializeField]
    private AudioClip[] ambiClips;
    public AudioSource ambiPlayer;

    public GameObject player;
    private Collider pCollider;

    public string fittingAmbi;

    // Start is called before the first frame update
    void Start()
    {
        pCollider = player.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == pCollider)
        {
            SelectClip();
            PlayAmbi();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == pCollider)
        {
            PlayNoAmbi();
        }
    }

    private AudioClip SelectClip()
    {
        if (fittingAmbi == ("swampAmbi0"))
        {
            return ambiClips[0];
        }

        if (fittingAmbi == ("lakesideAmbi1"))
        {
            return ambiClips[1];
        }

        if (fittingAmbi == ("cursedforestAmbi2"))
        {
            return ambiClips[2];
        }

        if (fittingAmbi == ("streetAmbi3"))
        {
            return ambiClips[3];
        }

        if (fittingAmbi == ("seagullAmbi4"))
        {
            return ambiClips[4];
        }

        if (fittingAmbi == ("oceanseagullsAmbi5"))
        {
            return ambiClips[5];
        }

        if (fittingAmbi == ("churchAmbi6"))
        {
            return ambiClips[6];
        }

        if (fittingAmbi == ("cfireAmbi7"))
        {
            return ambiClips[7];
        }

        else
        {
            return ambiClips[4];
        }
    }

    private void PlayAmbi()
    {
        AudioClip clip = SelectClip();
        ambiPlayer.clip = clip;
        ambiPlayer.Play();
    }

    private void PlayNoAmbi()
    {
        ambiPlayer.clip = null;
        ambiPlayer.Play();
    }
}
