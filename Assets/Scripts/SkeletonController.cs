using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonController : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    public AudioClips[] clips;
    [System.Serializable]
    public class AudioClips
    {
        public string name;
        public AudioClip clip;
    }
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>(); 

    public GameObject stopSign;
    bool stopSignActive = false;
    bool alive = true;
    public GameObject afterDeathPrefab;

    public static List<SkeletonController> skeletonsList = new List<SkeletonController>();

    [HideInInspector]
    public MovementController controller;

    public Text id;

    static bool audioIsPlaying;

    void Awake()
    {
        skeletonsList.Clear();

        for (int i = 0; i < clips.Length; i++)
        {
            audioClips.Add(clips[i].name, clips[i].clip);
        }
    }

    void Start()
    {
        skeletonsList.Add(this);
        animator = GetComponent<Animator>();
        controller = GetComponent<MovementController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (alive)
        {
            id.text = (skeletonsList.IndexOf(this)+1).ToString();
            stopSignActive = controller.stop;
            stopSign.SetActive(stopSignActive);

            audioIsPlaying = audioSource.isPlaying;
        }
    }

    public void Kill()
    {
        PlayAudio("death");
		Death();
        animator.SetTrigger("death");
    }

    void Death()
    {
        alive = false;
        controller.stop = true;
		skeletonsList.Remove(this);
    }

    public void Sacrifice()
    {
        PlayAudio("sacrifice");
		Death();
        animator.SetTrigger("sacrifice");
    }

    void AnimSacrifice()
    {
        Destroy(gameObject);
    }

    void AnimKill()
    {
        Instantiate(afterDeathPrefab, transform.position + new Vector3(0, 0.05f, 0), Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            Kill();
        }
    }

    public void PlayAudio(string name)
    {
        if(audioIsPlaying)
            return;
        audioSource.clip = audioClips[name];
        audioSource.Play();
        audioIsPlaying = audioSource.isPlaying;
    }
}
