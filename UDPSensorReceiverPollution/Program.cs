using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPSensorReceiverPollution
{
    class Program
    {
        static void Main(string[] args)
        {
            // int[] data = new int[15];
            double co = 0;
            double nox = 0;
            string level = " ";
            int number = 0;
            double sumCO = 0, sumNOx = 0;

            //Creates a UdpClient for reading incoming data.
            UdpClient udpReceiver = new UdpClient(9000);

            // This IPEndPoint will allow you to read datagrams sent from any ip-source on port 9000


            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 9000);

            // receivingUdpClient.Connect(RemoteIpEndPoint); what is this used for ??

            // Blocks until a message returns on this socket from a remote host.
            Console.WriteLine("Receiver is blocked");

            try
            {
                while (true)
                {
                    Byte[] receiveBytes = udpReceiver.Receive(ref RemoteIpEndPoint);


                    string receivedData = Encoding.ASCII.GetString(receiveBytes);
                    if (receivedData.Equals("STOP.Secret")) throw new Exception("Receiver stopped");

                    Console.WriteLine("Sender: " + receivedData.ToString());
                    //Console.WriteLine("This message was sent from " +
                    //                            RemoteIpEndPoint.Address.ToString() +
                    //                            " on their port number " +
                    //                            RemoteIpEndPoint.Port.ToString());
                    //if (receivedData.Equals("STOP")) throw new Exception("Receiver stopped");


                    // string[] textLines = receivedData.Split(' '); is possible but a little more difficult 
                    //best to split by '\n' or \r\ and then split by ' ' or  ':'
                    string[] textLines = receivedData.Split('\n');

                    for (int index = 0; index < textLines.Length; index++)
                        Console.Write(textLines[index]);

                    // used for testing format 
                    //foreach (string s in textLines)
                    //{
                    //    Console.WriteLine(s);
                    //}
                    // Console.ReadLine();

                    string[] list1 = textLines[3].Split(':');
                    string text1 = list1[1];
                    string[] list2 = textLines[4].Split(':');
                    string text2 = list2[1];
                    string[] list3 = textLines[5].Split(':');
                    string text3 = list3[1];

                    //Alternatively,split by ' ' and substrings, but risky if values with too many digits before decimal-delimeter
                    //string text1 = textLines[2].Substring(0, 3);
                    //string text2 = textLines[3].Substring(0, 3);
                    //string text3 = textLines[4].Substring(0, 3);

                    //Console.WriteLine("text1: " + text1);
                    //Console.WriteLine("text2: " + text2);
                    //Console.WriteLine("text3: " + text3);

                    //Console.ReadLine();

                    co = Double.Parse(text1);
                    nox = Int32.Parse(text2);
                    level = text3;
                    sumCO = sumCO + co;
                    sumNOx = sumNOx + nox;


                    //Console.WriteLine("Numerical values");

                    Console.WriteLine("CO: " + co);
                    Console.WriteLine("Nox: " + nox);
                    Console.WriteLine("Level: " + level);

                    //Console.ReadLine(); //for reading the data slowly
                    Thread.Sleep(1000);

                    number++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Average CO:" + sumCO / number);
            Console.WriteLine("Average NOx:" + sumNOx / number);
        }
    }
}
