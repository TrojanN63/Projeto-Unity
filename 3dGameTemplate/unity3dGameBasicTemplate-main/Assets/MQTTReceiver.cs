using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class MQTTReceiver : MonoBehaviour
{
    private MqttClient client;
    public string brokerAddress = "127.0.0.1";
    public string topic = "dev984ss";
    
    private float ax, ay, az;
    private float gx, gy, gz;
    private float mx, my, mz;
    
    private float vx,vy,vz;
    
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody>();
        client = new MqttClient(brokerAddress);
        client.MqttMsgPublishReceived += OnMessageReceived;
        client.Connect(Guid.NewGuid().ToString()); 
        client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE});
        if (client.IsConnected)
	{
    	    Debug.Log("Conectado ao broker MQTT.");
	}
	else
	{
    	    Debug.LogError("Falha ao conectar ao broker MQTT.");
	}
    }
    
    void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
    	string message = System.Text.Encoding.UTF8.GetString(e.Message);
    	Debug.Log("Dados recebidos:" + message);
    	
    	string[] values = message.Split(',');
    	if (values.Length >= 9)
    	{
    		ax = float.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture);
    		ay = float.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture);
    		az = float.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture);
    		gx = float.Parse(values[3], System.Globalization.CultureInfo.InvariantCulture);
    		gy = float.Parse(values[4], System.Globalization.CultureInfo.InvariantCulture);
    		gz = float.Parse(values[5], System.Globalization.CultureInfo.InvariantCulture);
    		mx = float.Parse(values[6], System.Globalization.CultureInfo.InvariantCulture);
    		my = float.Parse(values[7], System.Globalization.CultureInfo.InvariantCulture);
    		mz = float.Parse(values[8], System.Globalization.CultureInfo.InvariantCulture);
    		
    		vx += ax;
    		vy += ay;
    		vz += az;
	}
}
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(gx,gy,gz);
        rb.velocity = new Vector3(vx,vy,vz);
    }
    
}