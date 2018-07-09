using UnityEngine;
using UnityEngine.UI;

public class Controll: MonoBehaviour
{
    Gyroscope gyro;
    bool gyroEnabled;
    
	Quaternion rot = new Quaternion(0, 0, 1, 0);

    Scrollbar SB;
	const int moveCoef = 16;
    bool IsMove;

    void Awake()
	{
		Application.targetFrameRate = 60;
	}

    void Start()
	{
		Transform cameraContainer = transform.parent;
        SB = GameObject.Find("Canvas/Scrollbar").GetComponent<Scrollbar>();
		gyroEnabled = EnableGyro(cameraContainer);
    }

    void Update()
	{
		if(gyroEnabled)
		{
			transform.localRotation = gyro.attitude * rot;	
		}
		
        if(IsMove)
		{
			float deltaPosition = Time.deltaTime * (SB.value - 0.5f) * moveCoef;
			transform.position += transform.forward * deltaPosition;
        }
    }

	bool EnableGyro(Transform cameraContainer) {
        if (SystemInfo.supportsGyroscope)
		{
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.rotation = Quaternion.Euler(90f, 90f, 0f);
            return true;
        }
        return false;
    }

    public void StartMove()
	{
		IsMove = true;
	}

    public void EndMove() {
		IsMove = false;
		SB.value = 0.5f;
	}
}