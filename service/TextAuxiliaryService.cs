using Cronos.dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.service
{
    internal class TextAuxiliaryService
    {
        public static RichText getRichText(string text, Color color) {
            RichText newText = new RichText();
            newText.text = text;
            newText.color = color;

            return newText;
            
        }

        public static  string truncate(string originalString)
        {
            if (originalString.Length > 250) {
                return originalString.Substring(0, 250);
            }
            return originalString;
        }
    }
}
