using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    public float recoilAmount = 0.1f; 
    public float recoilSpeed = 5f;    
    public float returnSpeed = 3f;    

    private Vector3 originalPosition; 
    private Vector3 targetPosition;  
    private bool isRecoiling = false;

    private void Start()
    {
        originalPosition = transform.localPosition;
        targetPosition = originalPosition;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * (isRecoiling ? recoilSpeed : returnSpeed));
    }

    public void ApplyRecoil()
    {
        targetPosition = originalPosition - transform.forward * recoilAmount;
        isRecoiling = true;

        Invoke(nameof(ResetRecoil), 0.1f);
    }

    private void ResetRecoil()
    {
        targetPosition = originalPosition;
        isRecoiling = false;
    }
}
