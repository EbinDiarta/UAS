using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    public float offsetX = 3.3f;
    public float btskiri;
    public float offsetY = 2.32f;
    public float fixedZ = -10f;

    public float batasPasar = 20f;
    public float batasAtas = -10f;
    public float batasTengah = -40f;
    public float batasBawah = -65f;
    public float batasPalingBawah = -90f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = HitungPosisiKamera();

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }

    Vector3 HitungPosisiKamera()
    {
        Vector3 desiredPosition = new Vector3(
            target.position.x + offsetX,
            target.position.y + offsetY,
            fixedZ
        );

        if (desiredPosition.x < btskiri)
        {
            desiredPosition.x = btskiri;
        }

        float y = target.position.y;

        if (y > batasPasar)
        {
            desiredPosition.x = Mathf.Min(desiredPosition.x, 27f);
        }
        else if (y > batasAtas)
        {
            desiredPosition.x = Mathf.Min(desiredPosition.x, 68f);
        }
        else if (y > batasTengah)
        {
            desiredPosition.x = Mathf.Min(desiredPosition.x, 40f);
        }
        else if (y > batasBawah)
        {
            desiredPosition.x = Mathf.Min(desiredPosition.x, 20f);
        }
        else if (y <= -103.7342f)
        {
            desiredPosition.x = Mathf.Min(desiredPosition.x, 12f);
        }
        else
        {
            desiredPosition.x = Mathf.Min(desiredPosition.x, 7f);
        }

        return desiredPosition;
    }

    public void SnapToTarget()
    {
        if (target == null) return;

        transform.position = HitungPosisiKamera();
    }
}