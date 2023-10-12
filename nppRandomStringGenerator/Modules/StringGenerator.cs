using Kbg.NppPluginNET.PluginInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nppRandomStringGenerator.Modules
{
    internal class StringGenerator
    {
        public IScintillaGateway Editor { get; set; }
        public INotepadPPGateway Notepad { get; set; }
        public string StartCharacters { get; set; }
        public string AvailableCharacters { get; set; }
        public bool DoRandom { get; set; }
        public int RandomMinimumLength { get; set; }
        public int RandomMaximumLength { get; set; }
        public int StringLength { get; set; }
        public int StringQuantity { get; set; }
        public bool UseStartCharacters { get; set; }
        public bool IsSequential { get; set; }
        public bool IsDuplicate { get; set; }
        public string Prefix { get; set; }
        public bool IsInline { get; set; }
        public string TextSeperator { get; set; }

        static object lockEditorAccess = new object();

        public void GenerateStrings()
        {
            int idx = 0;
            int previousChar = 0;
            int currentChar = 0;
            int BufferCount = 0;

            Random rnd = new Random();

            var result = Parallel.For(0, this.StringQuantity, (i, state) =>
            {
                int length = this.StringLength;

                StringBuilder sb = new StringBuilder();

                if (this.DoRandom )
                {
                    length = rnd.Next(this.RandomMinimumLength, this.RandomMaximumLength);
                }

                if (this.IsInline)
                {
                    sb.Append(this.TextSeperator);
                }

                if (this.Prefix.Length > 0)
                {
                    sb.Append(this.Prefix);
                }

                for (int y = 0; y < length; y++)
                {
                    if (y == 0 && this.UseStartCharacters)
                    {
                        idx = rnd.Next(0, this.StartCharacters.Length);
                        sb.Append(this.StartCharacters[idx]);
                        previousChar = (int)this.StartCharacters[idx];
                    }
                    else
                    {
                        idx = rnd.Next(0, this.AvailableCharacters.Length);
                        currentChar = (int)this.AvailableCharacters[idx];

                        if (this.IsSequential)
                        {
                            while (previousChar - 1 == currentChar || previousChar + 1 == currentChar)
                            {
                                idx = rnd.Next(0, this.AvailableCharacters.Length);
                                currentChar = (int)this.AvailableCharacters[idx];
                            }
                        }
                        if (this.IsDuplicate)
                        {
                            while (previousChar == currentChar)
                            {
                                idx = rnd.Next(0, this.AvailableCharacters.Length);
                                currentChar = (int)this.AvailableCharacters[idx];
                            }
                        }
                        previousChar = (int)this.AvailableCharacters[idx];
                        sb.Append(this.AvailableCharacters[idx]);
                    }
                }

                if (this.IsInline)
                {
                    lock (lockEditorAccess)
                    {
                        this.Editor.LineEnd();
                        this.Editor.AddText(sb.Length, sb.ToString());
                        this.Editor.LineDown();
                    }
                }
                else
                {
                    sb.AppendLine();
                    BufferCount++;

                    if (BufferCount >= 512)
                    {

                        lock (lockEditorAccess)
                        {
                            this.Editor.AddText(sb.Length, sb.ToString());
                        }
                    }
                }
            });
        }
    }
}
