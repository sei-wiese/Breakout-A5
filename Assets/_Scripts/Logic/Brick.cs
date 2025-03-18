using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Coroutine destroyRoutine = null;
    [SerializeField] private AudioSource brickExplosionAudioSource;

    private void OnCollisionEnter(Collision other)
    {
        if (destroyRoutine != null) return;
        if (!other.gameObject.CompareTag("Ball")) return;
        destroyRoutine = StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // two physics frames to ensure proper collision
        
        GameManager.Instance.OnBrickDestroyed(transform.position);
               if (brickExplosionAudioSource != null && brickExplosionAudioSource.clip != null)
        {
            brickExplosionAudioSource.Play();
        }
        // Hide Block
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(brickExplosionAudioSource.clip.length);
        Destroy(gameObject);
    }
}
