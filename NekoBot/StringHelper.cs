using System;
using System.Text;

namespace NekoBot
{
    public class StringHelper
    {
        public static string BuildMessage(params string[] messages)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string message in messages)
            {
                stringBuilder.Append(message);
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}
