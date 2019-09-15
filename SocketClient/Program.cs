using SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace SocketClient
{
    class Program
    {
        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static void Main(string[] args)
        {
            socket.Connect("127.0.0.1", 1030);

            while (true)
            {
                using (StreamWriter streamWriter = new StreamWriter("E:\\result1.txt"))
                {
                    Console.Clear();
                    Console.WriteLine("Command Who write information about creator of the program and variant");
                    Console.WriteLine("Command Announcement adds your announcement in a line from 0 to 8");
                    Console.WriteLine("Enter 1 if you choose command Who");
                    Console.WriteLine("Enter 2 if you want command Annoucement");
                    Console.WriteLine("Enter your number of choice: ");
                    int optionNumber = Convert.ToInt32(Console.ReadLine());

                    while (optionNumber != 1 && optionNumber != 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Command Who writes information about creator of the program and variant");
                        Console.WriteLine("Command Announcement adds your announcement in a line from 0 to 8");
                        Console.WriteLine("Enter 1 if you choose command Who");
                        Console.WriteLine("Enter 2 if you want command Annoucement");
                        Console.WriteLine("Enter your number of choice: ");
                        optionNumber = Console.Read();
                    }

                    streamWriter.WriteLine("Sending");
                    streamWriter.WriteLine(DateTime.Now.ToLongTimeString());
                    streamWriter.Close();
                    socket.Send(ToByte(optionNumber));

                    Console.ReadKey();
                }
            }
        }

        //"Переводимо" об'єкт в масив байтів
        //Спочатку номер опції
        //Потім номер рядка, в який записувати об'яву
        //Далі сама об'ява
        static byte[] ToByte(int optionNumber)
        {
            List<byte> byteList = new List<byte>();

            if (optionNumber.Equals(1))
            {
                byteList.AddRange(BitConverter.GetBytes(optionNumber));
            }
            else
            {
                Announcement announcement = new Announcement();
                GetInformation(announcement);

                //Перетворюємо запит(об'єкт і номер опції) в масив байтів
                byteList.AddRange(BitConverter.GetBytes(optionNumber));
                byteList.AddRange(BitConverter.GetBytes(announcement.LineNumber));
                byteList.AddRange(BitConverter.GetBytes(announcement.Text.Length));
                byteList.AddRange(Encoding.ASCII.GetBytes(announcement.Text));
            }

            return byteList.ToArray();
        }

        //Дізнаємося об'яву і рядок, в який цю об'яву додати
        static void GetInformation(Announcement announcement)
        {
            Console.WriteLine("Enter your announcement you want to add: ");
            announcement.Text = Console.ReadLine();

            Console.WriteLine("Enter a line in which you want to add your announcement: ");
            announcement.LineNumber = Convert.ToInt32(Console.ReadLine());

        }
    }
}
