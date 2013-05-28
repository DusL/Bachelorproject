using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Ports;
using System.Threading;

namespace CLESMonitor.Model
{
    public class HRSensor
    {
        private const int DATA_MESSAGE_BYTE_COUNT = 60;
        private const int HEART_RATE_BYTE_INDEX = 12;

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

        //TODO: Dynamische instelling van ComPort

        /// <summary>
        /// Een verbinding maken met de opgeven comport, anders een serialPort exeption gooien.
        /// </summary>
        public void setUpSerialPort()
        {
            try
            {
                String serialPortName = "COM3";
                Console.WriteLine("Bezig met openen serialport {0}", serialPortName);
                serialPort = new SerialPort(serialPortName);
                Console.WriteLine("Serialport {0} geopend", serialPortName);
                serialPort.Open();
                thread.Start();
            }
            catch (IOException) {
                Console.WriteLine("SerialPort IOException");
            }
        }

        //TODO: Lees uit je pakketje uit hoelan je message is. Geen fixed message size.

        /// <summary>
        /// De runloop van HRSensor. Blijft lopen totdat het programma gesloten wordt.
        /// Cashe reset na 60 Bytes
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
