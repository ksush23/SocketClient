using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServer
{
    //клас об'яви
    class Announcement
    {
        //текст об'яви
        private string text;
        //рядок, в який записувати
        private int lineNumber;

        public string Text
        {
            get;
            set;
        }
        public int LineNumber
        {
            get
            {
                return lineNumber;

            }
            set
            {
                //Перевірка на коректність номеру рядка, по дефолту він 0
                if (value <= 8 && value >= 0)
                    lineNumber = value;
                else
                {
                    Console.WriteLine("Incorrect line number! (Default value 0)");
                    lineNumber = 0;
                }
            }
        }

        public Announcement ToAnnouncement(byte[] bytelist)
        {
            Announcement announcement = new Announcement();

            announcement.LineNumber = BitConverter.ToInt32(bytelist, 4);
            int length = BitConverter.ToInt32(bytelist, 8);
            announcement.Text = Encoding.ASCII.GetString(bytelist, 12, length);

            return announcement;
        }
    }
}
