using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDAPIWrapperSpace;

public enum GloveHand { Left, Right }
public enum GloveFinger { Thumb, Index, Middle, Ring, Pinky }

public class GloveoneController : MonoBehaviour
{
	private NDAPI _nd;
	private int _resultValue = 0;

	// Use this for initialization
	void Start()
	{
		//instance of library
		_nd = new NDAPI();

		//connect with service NDSvc
		_resultValue = _nd.connectToServer();

		if (_resultValue == (int)Error.ND_ERROR_SERVICE_UNAVAILABLE)
		{
			print("Error: Service Unavailable");
		}
	}

	public void PulseFinger(Location hand, GloveFinger finger, float intensity = 0.8f, uint duration = 1000)
	{

		int numDevices = _nd.getNumberOfDevices();
		int[] devices = new int[numDevices];

		//get Id's from service
		_resultValue = _nd.getDevicesId(devices);

		if (_resultValue >= 0)
		{
			print("There is " + numDevices + " device(s)n");

			for (int i = 0; i < devices.Length; i++)
			{
				print("Hand: " + (Location)_nd.getDeviceLocation(devices[i]) + ", Target: " + hand);

				if ((Location)_nd.getDeviceLocation(devices[i]) == hand)
				{
					int numberActuators = _nd.getNumberOfActuators(devices[i]);

					if (_resultValue > 0)
					{
						print("Start sending pulses to device with Id: " + devices[i] + "n");
						int fingerIndex = (int)finger;

						_resultValue = _nd.setActuatorPulse((Actuator)fingerIndex, intensity, duration, devices[i]);

						if (_resultValue < 0)
						{
							print("Error sending pulse to Actuator: " + fingerIndex);
						}
						else print("Pulse sent to Actuator: " + fingerIndex);
					}
				}
			}
		}
		else print("Error getting devicesId: " + numDevices);
	}
}
