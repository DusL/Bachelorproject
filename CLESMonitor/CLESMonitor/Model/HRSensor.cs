using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace CLESMonitor.Model
{
    public enum HRSensorType
    {
        Unknown,
        ManualInput,
        BluetoothZephyr
    }

    public class HRSensor
    {
        private const int DATA_MESSAGE_BYTE_COUNT = 60;
        private const int HEART_RATE_BYTE_INDEX = 12;

        public HRSensorType sensorType { get; set; }
        public double sensorValue; //heart rate, in beats/minute
        SerialPort serialPort;
        Thread thread;

        int[] dataMessage; //Representatie van de message bytes in int(32) per byte

        public HRSensor()
        {
            ThreadStart threadDelegate = new ThreadStart(Read);
            thread = new Thread(threadDelegate);
            thread.IsBackground = true;
        }

        /// <summary>
        /// Indicate to start measuring values into sensorValue
        /// </summary>
        public void startMeasuring()
        {
            if (sensorType == HRSensorType.BluetoothZephyr)
            {
                // Setup the COM connection
                try
                {
                    String serialPortName = "COM3"; //FIXME: hardcoded!
                    Console.WriteLine("Bezig met openen serialport {0}", serialPortName);
                    serialPort = new SerialPort(serialPortName);
                    Console.WriteLine("Serialport {0} geopend", serialPortName);
                    serialPort.Open();
                    thread.Start();
                }
                catch (IOException)
                {
                    Console.WriteLine("SerialPort IOException");
                }
            }
        }

        /// <summary>
        /// Indicate to stop measuring values into sensorValue
        /// </summary>
        public void endMeasuring()
        {
        }

        /// <summary>
        /// The HRSensor run-loop. Blijft lopen totdat het programma gesloten wordt.
        /// Cache reset after 60 bytes - FIXME: hardcoded!
        /// </summary>
        public void Read()
        {
            //Maak een array met de lengte = aantal bytes van een message.
            int[] incomingDataMessage = new int[DATA_MESSAGE_BYTE_COUNT]; 
            int byteNumber = 0;

            while (true)
            {
                try
                {
                    int byteInt = serialPort.ReadByte();
                    incomingDataMessage[byteNumber] = byteInt;

                    //Check whether the entire message has been received 
                    if (byteNumber == DATA_MESSAGE_BYTE_COUNT-1)
                    {
                        dataMessage = incomingDataMessage;
                        sensorValue = dataMessage[HEART_RATE_BYTE_INDEX];
                        Console.WriteLine("Heart rate = {0}", sensorValue);
                        byteNumber = 0;
                    }
                    else {
                        byteNumber++;
                    }
                }
                catch (TimeoutException) {
                    Console.WriteLine("SerialPort TimeoutException");
                }
            }
        }
    }
}
