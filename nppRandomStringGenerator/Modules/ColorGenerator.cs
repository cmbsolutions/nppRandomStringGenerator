using Kbg.NppPluginNET.PluginInfrastructure;
using System;
using System.Diagnostics;
using System.Security.Cryptography;


namespace nppRandomStringGenerator.Modules
{
    internal class ColorGenerator
    {
        public IScintillaGateway Editor { get; set; }
        public INotepadPPGateway Notepad { get; set; }

        private readonly int COLOR_INDICATOR;

        public ColorGenerator()
        {
            this.Editor = new ScintillaGateway(PluginBase.GetCurrentScintilla());
            this.Notepad = new NotepadPPGateway();

            this.Notepad.AllocateIndicators(1, out int[] indicators);
            this.COLOR_INDICATOR = indicators[0];

            this.Editor.IndicSetStyle(this.COLOR_INDICATOR, IndicatorStyle.TEXTFORE);
            this.Editor.IndicSetFlags(this.COLOR_INDICATOR, IndicFlag.VALUEFORE);
        }

        public void GenerateColors()
        {
            Byte[] buffer = new Byte[4];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
            }
            int rndInt = BitConverter.ToInt32(buffer, 0);
            Random rnd = new Random(rndInt);

            this.Editor.AppendTextAndMoveCursor($"Hex|RGB|HSL|HSV{Environment.NewLine}");

            for (int i = 0; i < 100; i++)
            {
                int r = rnd.Next(0, 256);
                int g = rnd.Next(0, 256);
                int b = rnd.Next(0, 256);

                int colour = r | (g << 8) | (b << 16);

                string colorCode = $"#{r:X2}{g:X2}{b:X2}|{r},{g},{b}|{RgbToHsl(r, g, b)}|{RgbToHsv(r, g, b)}";

                this.Editor.TryGetLengthAsInt(out int startPosition);

                this.Editor.AppendTextAndMoveCursor(colorCode);

                this.Editor.TryGetLengthAsInt(out int endPosition);

                this.Editor.AppendTextAndMoveCursor(Environment.NewLine);

                int indicatorValue = colour | 0x1000000;

                int storedValue = 0;
                int attempts = 0;

                while (storedValue == 0)
                {
                    this.Editor.SetIndicatorCurrent(this.COLOR_INDICATOR);
                    this.Editor.SetIndicatorValue(indicatorValue);
                    this.Editor.IndicatorFillRange(startPosition, endPosition - startPosition);

                    storedValue = this.Editor.IndicatorValueAt(this.COLOR_INDICATOR, startPosition);
                    attempts++;

                    if (attempts > 10)
                    {
                        Debug.WriteLine($"Failed to set indicator value for color {colorCode} after {attempts} attempts.");
                        break;
                    }    
                }


                Debug.WriteLine($"Color: {colorCode.Trim()} | Stored Value: {storedValue:X8} | Expected Value: {indicatorValue:X8} | Range: {(int)startPosition} to {(int)endPosition} | Attempts: {attempts}");
            }
        }

        private string RgbToHsl(int r, int g, int b)
        {
            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;
            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double delta = max - min;
            double h = 0;
            double s = 0;
            double l = (max + min) / 2;
            if (delta != 0)
            {
                s = l < 0.5 ? delta / (max + min) : delta / (2 - max - min);
                if (max == rNorm)
                {
                    h = (gNorm - bNorm) / delta + (gNorm < bNorm ? 6 : 0);
                }
                else if (max == gNorm)
                {
                    h = (bNorm - rNorm) / delta + 2;
                }
                else
                {
                    h = (rNorm - gNorm) / delta + 4;
                }
                h /= 6;
            }
            return $"{(int)(h * 360)},{(int)(s * 100)}%,{(int)(l * 100)}%";
        }

        private string RgbToHsv(int r, int g, int b)
        {
            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;
            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double delta = max - min;
            double h = 0;
            double s = 0;
            double v = max;
            if (delta != 0)
            {
                s = delta / max;
                if (max == rNorm)
                {
                    h = (gNorm - bNorm) / delta + (gNorm < bNorm ? 6 : 0);
                }
                else if (max == gNorm)
                {
                    h = (bNorm - rNorm) / delta + 2;
                }
                else
                {
                    h = (rNorm - gNorm) / delta + 4;
                }
                h /= 6;
            }
            return $"{(int)(h * 360)},{(int)(s * 100)}%,{(int)(v * 100)}%";
        }
    }
}
