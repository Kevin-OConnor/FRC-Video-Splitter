using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FRCVideoSplitter
{
    public partial class ProgressDialog : Form
    {
        long chunkSize;
        int chunks;
        int completedChunks;

        #region PROPERTIES

        public string Message
        {
            set { labelMessage.Text = value; }
        }

        public int Chunks
        {
            set { chunks = value; }
        }

        public long ChunkSize
        {
            set { chunkSize = value; }
        }
        #endregion

        public ProgressDialog()
        {
            InitializeComponent();
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {

        }

        delegate void SetTextCallback(string text);

        public void SetText(string text)
        {
            if (this.labelMessage.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelMessage.Text = text;
            }
        }

        delegate void SetProgressCallback(int progress);

        public void SetCompletedChunks(int completed)
        {
            completedChunks = completed;
            SetProgress((int)(100*((double)completedChunks / (double)chunks)));
            
        }

        public void SetChunkProgress(long progress)
        {
            SetProgress((int)(100*((double)completedChunks + ((double)progress / (double)chunkSize)) / (double)chunks));
        }

        public void SetProgress(int progress)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProgressCallback d = new SetProgressCallback(SetProgress);
                this.Invoke(d, new object[] { progress });
            }
            else
            {
                this.progressBar1.Value = progress;
            }
        }

        public event EventHandler<EventArgs> Canceled;

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            EventHandler<EventArgs> ea = Canceled;
            if (ea != null)
                ea(this, e);
        }

    }
}
