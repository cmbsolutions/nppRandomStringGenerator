using Kbg.NppPluginNET.PluginInfrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace nppRandomStringGenerator
{
    class generator
    {
        private BackgroundWorker bgWorker;
        public string AvailableChars { get; set; }
        public string StartChars { get; set; }
        public int stringLength { get; set; }
        public int quantity { get; set; }
        private IScintillaGateway Editor;
        private INotepadPPGateway Notepad;

        public void Generate(ref IScintillaGateway editor, ref INotepadPPGateway notepad)
        {
            this.Editor = editor;
            this.Notepad = notepad;

            this.bgWorker = new BackgroundWorker();
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            this.bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
            this.bgWorker.DoWork += BgWorker_DoWork;

            this.bgWorker.RunWorkerAsync();
        }

        public void CancelGenerator()
        {
            if (bgWorker.IsBusy && !bgWorker.CancellationPending) this.bgWorker.CancelAsync();
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Decimal perc = 0;
                int old_perc = 0;
                int idx = 0;
                Random rnd = new Random();

                for (int i = 0; i < quantity; i++)
                {
                    string code = "";
                    perc += quantity / 100;
                    if ((int)perc != old_perc)
                    {
                        this.bgWorker.ReportProgress((int)perc);
                        old_perc = (int)perc;
                    }

                    if (this.bgWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    for (int y = 0; y < stringLength; y++)
                    {
                        if (y == 0 && this.StartChars.Length > 0)
                        {
                            idx = rnd.Next(0, this.StartChars.Length - 1);
                            code += this.StartChars.Substring(idx, 1);
                        }
                        else
                        {
                            idx = rnd.Next(0, this.AvailableChars.Length - 1);
                            code += this.AvailableChars.Substring(idx, 1);
                        }
                    }

                    this.Editor.AddText(code.Length, code);
                    this.Editor.NewLine();
                }
                e.Result = "Completed";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }
    }
}
