using Kbg.NppPluginNET.PluginInfrastructure;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
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
        public bool IsCancelled { get; set; }
        public string TextSeperator { get; set; }
        public bool DoGuids { get; set; }
        public string GuidFormat { get; set; }
        public int GuidQuantity { get; set; }


        private TimeSpan InternalProcessTime;
        public TimeSpan ProcessTime
        {
            get { return InternalProcessTime; }
        }

        private Object LockingEditor = new Object();

        public CancellationTokenSource CancelJob { get; set; }

        public void GenerateStrings()
        {

            int cores = Environment.ProcessorCount/2;

            int WorkLoad = this.StringQuantity / cores;
            int MissingWorkload = this.StringQuantity % cores;

            if (DoGuids)
            {
                WorkLoad = this.GuidQuantity / cores;
                MissingWorkload = this.GuidQuantity % cores;
            }


            CancelJob = new CancellationTokenSource();

            ParallelOptions options = new ParallelOptions()
            {
                CancellationToken = CancelJob.Token,
                MaxDegreeOfParallelism = cores
            };

            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                Parallel.For(0, cores, options, (i, state) =>
                {
                    int idx = 0;
                    int previousChar = 0;
                    int currentChar = 0;
                    int BufferCount = 0;

                    StringBuilder sb = new StringBuilder();
                    int length = this.StringLength;
                    int internalWorkload = WorkLoad;

                    if ( i == 0 )
                    {
                        internalWorkload += MissingWorkload;
                    }

                    Random rnd = new Random(i);

                    for (int w = 0; w < internalWorkload; w++)
                    {
                        if (CancelJob.IsCancellationRequested)
                        {
                            Debug.WriteLine($"Breaking loop {i}");
                            break;
                        }

                        if (DoGuids)
                        {
                            if (this.IsInline)
                            {
                                int line = i * internalWorkload + w;

                                if (i > 0) line += MissingWorkload;

                                sb.Append(Guid.NewGuid().ToString(this.GuidFormat));

                                lock (this.LockingEditor)
                                {

                                    this.Editor.GotoLine(line);
                                    this.Editor.LineEnd();
                                    this.Editor.AddText(sb.Length, sb.ToString());
                                }

                                sb.Clear();
                            }
                            else
                            {
                                sb.AppendLine(Guid.NewGuid().ToString(this.GuidFormat));

                                BufferCount++;


                                if (BufferCount >= 1024 || w + 1 == internalWorkload)
                                {
                                    this.Editor.AddText(sb.Length, sb.ToString());

                                    BufferCount = 0;
                                    sb.Clear();
                                }
                            }
                        }
                        else
                        {
                            if (this.DoRandom)
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
                                    previousChar = this.StartCharacters[idx];
                                }
                                else
                                {
                                    idx = rnd.Next(0, this.AvailableCharacters.Length);
                                    currentChar = this.AvailableCharacters[idx];

                                    if (this.IsSequential)
                                    {
                                        while (previousChar - 1 == currentChar || previousChar + 1 == currentChar)
                                        {
                                            idx = rnd.Next(0, this.AvailableCharacters.Length);
                                            currentChar = this.AvailableCharacters[idx];
                                        }
                                    }
                                    if (this.IsDuplicate)
                                    {
                                        while (previousChar == currentChar)
                                        {
                                            idx = rnd.Next(0, this.AvailableCharacters.Length);
                                            currentChar = this.AvailableCharacters[idx];
                                        }
                                    }
                                    previousChar = this.AvailableCharacters[idx];
                                    sb.Append(this.AvailableCharacters[idx]);
                                }
                            }

                            if (this.IsInline)
                            {
                                int line = i * internalWorkload + w;

                                if (i > 0) line += MissingWorkload;

                                lock (this.LockingEditor)
                                {

                                    this.Editor.GotoLine(line);
                                    this.Editor.LineEnd();
                                    this.Editor.AddText(sb.Length, sb.ToString());
                                }

                                sb.Clear();
                            }
                            else
                            {
                                sb.AppendLine();
                                BufferCount++;

                                if (BufferCount >= 1024 || w + 1 == internalWorkload)
                                {
                                    this.Editor.AddText(sb.Length, sb.ToString());

                                    BufferCount = 0;
                                    sb.Clear();
                                }
                            }
                        }
                    }
                });
            }
            catch (OperationCanceledException oce)
            {
                Debug.WriteLine(oce.Message);
                this.IsCancelled = true;
            }
            sw.Stop();

            this.InternalProcessTime = sw.Elapsed;
        }
    }
}
