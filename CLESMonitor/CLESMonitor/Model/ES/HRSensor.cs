using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace CLESMonitor.Model.ES
{
    /// <summary>
    /// The HRSensor class represents a Heart Rate sensor. With heart beat information,
    /// it can be used as a metric to measure emotional state.
    /// </summary>
    public class HRSensor
    {
        /// <summary>
        /// The way in which the HRSensor will receive its raw data
        /// </summary>
        public enum Type
        {
            /// <summary>Initial unknown value</summary>
            Unknown,
            /// <summary>The sensor value is set from the outside (another class)</summary>
            ManualInput,
            /// <summary>Using a Zephyr Bluetooth monitor, linked to a SerialPort</summary>
            BluetoothZephyr
        }

        /// <summary>The type of the sensor</summary>
        public Type type { get; set; }
        /// <summary>The value of the sensor, expressed in beats/minute</summary>
        public double sensorValue;
        /// <summary>The serialport name to use, only valid in conjunction with Type.BluetoothZephyr</summary>
        public string serialPortName;

        private const int DATA_MESSAGE_BYTE_COUNT = 60;
        private const int HEART_RATE_BYTE_INDEX = 12;

        private SerialPort serialPort;
        private ManualResetEvent updateThreadStop; 
        private Thread updateThread;
        private int[] dataMessage; //representation of the last received package expressed in int(32) per byte

        /// <summary>
        /// Constructor method
        /// </summary>
        public HRSensor(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// Indicate to start measuring values into sensorValue
        /// </summary>
        public void startMeasuring()
        {
            if (type == Type.BluetoothZephyr && serialPortName != null)
            {
                try
                {
                    // Setup the COM connection
                    serialPort = new SerialPort(serialPortName);
                    Console.WriteLine("Serialport " + serialPortName + " openen..");
                    serialPort.Open();
                    Console.WriteLine("Serialport " + serialPortName + " geopend");                    

                    // Setup a runloop to listen for data packages
                    ThreadStart threadDelegate = new ThreadStart(updateRunLoop);
                    updateThread = new Thread(threadDelegate);
                    updateThread.IsBackground = true;
                    updateThreadStop = new ManualResetEvent(false);
                    updateThread.Start();
                }
                catch (IOException)
                {
                    Console.WriteLine("IOException tijdens openen serialport " + serialPortName);
                }
            }
        }

        /// <summary>
        /// Indicate to stop measuring values into sensorValue
        /// </summary>
        public void stopMeasuring()
        {
            // Stop the runloop
            if (updateThreadStop != null) {
                updateThreadStop.Set();
            }
        }

        /// <summary>
        /// The sensor run loop. This will keep checking for incoming data packets
        /// from the sensor and update the sensor value.
        /// </summary>
        private void updateRunLoop()
        {
            // Maak een array met de lengte = aantal bytes van een message.
            int[] incomingDataMessage = new int[DATA_MESSAGE_BYTE_COUNT];
            int byteNumber = 0;

            while (serialPort.IsOpen)
            {
                if (serialPort.BytesToRead > 0)
                {
                    int byteInt = serialPort.ReadByte();
                    incomingDataMessage[byteNumber] = byteInt;

                    // Check whether the entire message has been received 
                    if (byteNumber == DATA_MESSAGE_BYTE_COUNT - 1)
                    {
                        dataMessage = incomingDataMessage;
                        sensorValue = dataMessage[HEART_RATE_BYTE_INDEX];
                        byteNumber = 0;
                    }
                    else {
                        byteNumber++;
                    }
                }

                if (updateThreadStop.WaitOne(0))
                {
                    serialPort.Close();
                    Console.WriteLine("Serialport " + serialPortName + " gesloten");
                }
            }
        }
    }
}
