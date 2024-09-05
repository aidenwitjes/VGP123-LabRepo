using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minXClamp = 6.35f;
    public float maxXClamp = 244.2f;

    public float minYClamp = 0.0f;
    public float maxYClamp = 11.25f;

    //This function always runs after fixed update - unity specifies that this is where camera movement should happen
    private void LateUpdate()
    {
        PlayerController pc = GameManager.Instance.PlayerInstance;
        Vector3 cameraPos = transform.position;

        cameraPos.x = Mathf.Clamp(pc.transform.position.x,minXClamp, maxXClamp);
        cameraPos.y = Mathf.Clamp(pc.transform.position.y, minYClamp, maxYClamp);

        transform.position = cameraPos;
    }
}
