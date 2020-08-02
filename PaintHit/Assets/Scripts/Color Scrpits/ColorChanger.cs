using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.CompareTag("red"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            target.gameObject.GetComponent<MeshRenderer>().enabled = true;
            target.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            GetComponent<Rigidbody>().AddForce(Vector3.down * 50, ForceMode.Impulse);
            HeartsFun(target.gameObject);
            Destroy(gameObject, 0.5f);
        }
        else
        {
            GameObject.Find("hitSound").GetComponent<AudioSource>().Play();
            base.gameObject.GetComponent<Collider>().enabled = false;
            GameObject gameObject2 = Instantiate(Resources.Load("splash1")) as GameObject;
            gameObject2.transform.parent = target.gameObject.transform;
            Destroy(gameObject2, 0.1f);
            target.gameObject.name = "color";
            target.gameObject.tag = "red";
            StartCoroutine(ChangeColor(target.gameObject));
        }
    }

    IEnumerator ChangeColor(GameObject g)
    {
        yield return new WaitForSeconds(0.1f);
        var meshRenderer = g.gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
        meshRenderer.material.color = BallHandler.OneColor;
        Destroy(gameObject);
    }

    private void HeartsFun(GameObject g)
    { 
        int @int = PlayerPrefs.GetInt("hearts");
        if (@int == 1)
        {
            FindObjectOfType<BallHandler>().FailGame();
            FindObjectOfType<BallHandler>().HeartsLow();
        }
    }
}
