using UnityEngine;

public class EnemyRemover : MonoBehaviour
{
    public static EnemyRemover instance;
    public bool removeObj = false;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void remove()
    {
        Debug.Log("Enemy has been removed");
        gameObject.SetActive(false);
    }
}
