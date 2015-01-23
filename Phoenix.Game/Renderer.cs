using System.Web.Configuration;
using Phoenix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Phoenix.Game
{
    public class Renderer : Module
    {
        private static Renderer _instance = new Renderer();
        public static Renderer Instance { get { return _instance ?? (_instance = new Renderer()); } }

        public Renderer() : base("SAY", "YELL", "WHISPER")
        {
            
        }

        public override void HandleMessage(Message message)
        {
            switch (message.MessageType)
            {
                case "SAY":
                    Write(message.Value.ToString());
                    break;
            }
        }

        public static string Clr(Colors color, string text)
        {
            StringBuilder s = new StringBuilder();
            s.Append("<span style=\"color: ");
            s.Append(Enum.GetName(typeof(Colors), color));
            s.Append("\">");
            s.Append(text);
            s.Append("</span>");
            return s.ToString();
        }

        public void Write(string message)
        {
            var parts = message.Split('|');
            var encodedString = new StringBuilder();
            Colors color;

            for (int i = 0; i < parts.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(parts[i]))
                {
                    color = (Colors)(int.Parse(parts[i].Substring(0, 2)));
                    parts[i] = parts[i].Remove(0, 2);
                    encodedString.Append(Clr(color, parts[i]));
                }
                if (i == parts.Length - 1)
                    encodedString.AppendLine();
                //else
                    //Console.Write(string.Format("{0}", parts[i]));
            }
            MudGame.Instance.BroadcastMessage(encodedString.ToString());
        }


        public enum Colors
        {
            Red = 0,
            Orange,
            Yellow,
            Green,
            Blue,
            Violet
        }
    }
}