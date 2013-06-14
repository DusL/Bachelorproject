using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace CLESMonitor.Model.ES
{
    /// <summary>
    /// The way in which the HRSensor will recieve its raw data
    /// </summary>
    public enum HRSensorType
    {
        /// <summary>Initial unknown value</summary>
        Unknown,
        /// <summary>The sensor value is set from the outside (another class)</summary>
        ManualInput,
        /// <summary>Using a Zephyr Bluetooth monitor, linked to a SerialPort</summary>
        BluetoothZephyr
    }

    /// <summary>
    /// The HRSensor class represents a Heart Rate sensor. With heart beat information,
    /// it can be used as a metric to measure emotional state.
    /// </summary>
    public class HRSensor
    {
        /// <summary>The type of the sensor</summary>
        public HRSensorType type { get; set; }
        /// <summary>The value of the sensor, expressed in beats/minute</summary>
        public double sensorValue;

        private const int DATA_MESSAGE_BYTE_COUNT = 60;
        private const int HEART_RATE_BYTE_INDEX = 12;

        private SerialPort serialPort;
        private Thread updateThread;
        private int[] dataMessage; //representation of the last received package expressed in int(32) per byte

        /// <summary>
        /// Indicate to start measuring values into sensorValue
        /// </summary>
        public void startMeasuring()
        {
            if (type == HRSensorType.BluetoothZephyr)
            {
                try
                {
                    // Setup the COM connection
                    String serialPortName = "COM3"; //FIXME: hardcoded!
                    Console.WriteLine("Bezig met openen serialport {0}", serialPortName);
                    serialPort = new SerialPort(serialPortName);
                    Console.WriteLine("Serialport {0} geopend", serialPortName);
                    serialPort.Open();

                    // Setup a runloop to listen for data packages
                    ThreadStart threadDelegate = new ThreadStart(updateRunLoop);
                    updateThread = new Thread(threadDelegate);
                    updateThread.IsBackground = true;
                    updateThread.Start();
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
        public void stopMeasuring()
        {
            if (type == HRSensorType.BluetoothZephyr)
            {
                // Stop the runloop
                updateThread.Abort();
                // Close down the COM connection
                serialPort.Close();
            }
        }

        /// <summary>
        /// The sensor run loop. This will keep checking for incoming data packets
        /// from the sensor and update the sensor value.
        /// Packet size is assumed fixed at 60 bytes -> FIXME: hardcoded!
        /// </summary>
        private void updateRunLoop()
        {
            // Maak een array met de lengte = aantal bytes van een message.
            int[] incomingDataMessage = new int[DATA_MESSAGE_BYTE_COUNT];
            int byteNumber = 0;

            while (true)
            {
                try
                {
                    int byteInt = serialPort.ReadByte();
                    incomingDataMessage[byteNumber] = byteInt;

                    // Check whether the entire message has been received 
                    if (byteNumber == DATA_MESSAGE_BYTE_COUNT - 1)
                    {
                        dataMessage = incomingDataMessage;
                        sensorValue = dataMessage[HEART_RATE_BYTE_INDEX];
                        Console.WriteLine("Heart rate = {0}", sensorValue);
                        byteNumber = 0;
                    }
                    else
                    {
                        byteNumber++;
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("SerialPort TimeoutException");
                }
            }
        }
    }
}
