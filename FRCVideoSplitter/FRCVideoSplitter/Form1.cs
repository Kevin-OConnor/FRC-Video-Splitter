using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace FRCVideoSplitter
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        FrcApi api = new FrcApi();
        ProgressDialog progress;
        VideoUploader uploader = new VideoUploader();
        String playlist;
        List<VideoDetails> vidQueue;

        public class VideoDetails
        {
            public String eventCode;
            public String matchType;
            public String matchNumber;
            public String youtubeKey;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void sourceVideoBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                sourceVideoPathTextBox.Text = openFileDialog1.FileName;
            }
        }

        private void matchVideoBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                matchVideoDestinationPathTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void qualMatchesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            firstQualMatchNumberBox.Enabled = qualMatchesCheckBox.Checked;
            lastQualMatchNumberBox.Enabled = qualMatchesCheckBox.Checked;
            firstQualMatchLabel.Enabled = qualMatchesCheckBox.Checked;
            lastQualMatchLabel.Enabled = qualMatchesCheckBox.Checked;

        }

        private void elimMatchesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            elimsMatchNamesBox.Enabled = elimMatchesCheckBox.Checked;
            elimHelperLabel.Enabled = elimMatchesCheckBox.Checked;
        }

        private void matchVideoLengthOverrideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            matchLengthBox.Enabled = matchVideoLengthOverrideCheckBox.Checked;
            overrideHelperLabel.Enabled = matchVideoLengthOverrideCheckBox.Checked;
        }

        private void generateTimestampButton_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Columns.Add("MatchTitle", typeof(string));
            dt.Columns.Add("Timestamp", typeof(string));

            if (qualMatchesCheckBox.Checked)
            {
                int firstMatch = (int)firstQualMatchNumberBox.Value;
                int lastMatch = (int)lastQualMatchNumberBox.Value;
                if (firstMatch <= lastMatch)
                {
                    for (int i = firstMatch; i <= lastMatch; i++)
                    {
                        string matchName = "Q" + i.ToString();
                        dt.Rows.Add(matchName, "");
                    }

                }
                else
                {
                    MessageBox.Show("Invalid Match Numbers");
                    return;
                }
            }

            if (elimMatchesCheckBox.Checked)
            {
                string[] lines = elimsMatchNamesBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    dt.Rows.Add(line, "");
                }
            }

            timeStampsDataGridView.DataSource = dt;

            splitVideosButton.Enabled = true;
        }

        private async void uploadVideosButton_Click(object sender, EventArgs e)
        {
            if(matchVideoDestinationPathTextBox.Text.Equals("Click Browse..."))
            {
                MessageBox.Show("Error: Please specify video destination folder");
                return;
            }
            if (!useTbaCheckBox.Checked)
            {
                if (MessageBox.Show("Use TBA not checked, proceed with upload with minimal description?", "Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }
            int retVal = await uploader.SetCredentials();
            if (backgroundWorker1.IsBusy != true)
            {
                progress = new ProgressDialog();
                progress.Canceled += new EventHandler<EventArgs>(cancelAsyncButton_Click);
                progress.Show();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void splitVideosButton_Click(object sender, EventArgs e)
        {
            DateTime timeValue;
            List<string> errors = new List<string>();
            int completed = 0;

            if (!Directory.Exists(matchVideoDestinationPathTextBox.Text))
            {
                MessageBox.Show("Error: Destination not specified or invalid");
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                string timestamp = row.ItemArray[1].ToString();
                if (!DateTime.TryParse(timestamp, out timeValue))
                {
                    errors.Add(row.ItemArray[0].ToString());
                }                
            }

            if (!errors.Any())
            {
                string sourceFile = sourceVideoPathTextBox.Text;
                progress = new ProgressDialog();
                progress.SetText("Splitting video");
                progress.Chunks = dt.Rows.Count;
                progress.Show();
                foreach (DataRow row in dt.Rows)
                {
                    progress.SetText("Splitting video " + (completed + 1) + " of " + dt.Rows.Count);
                    string startTime = row.ItemArray[1].ToString();
                    string videoName = row.ItemArray[0].ToString() + " - " + eventNameTextBox.Text + Path.GetExtension(sourceFile);
                    string destinationFile = Path.Combine(matchVideoDestinationPathTextBox.Text, videoName);
                    string command = "ffmpeg.exe";
                    string args = "-ss " + startTime + " -i \"" + sourceFile + "\" -t " + matchLengthBox.Text + " -c:v copy -c:a copy \"" + destinationFile + "\"";

                    Console.WriteLine(args);

                    Process process = new Process();
                    process.StartInfo.FileName = command;
                    process.StartInfo.Arguments = args;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();                    
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    Console.WriteLine(output);
                    progress.SetCompletedChunks(++completed);
                }
                progress.Close();
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in errors)
                {
                    sb.Append(s + ", ");
                }
                MessageBox.Show("Timestamp errors detected at: " + sb.ToString());
            }
        }

        private void eventNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void useTbaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            eventCodeBox.Enabled = useTbaCheckBox.Checked;
            eventCodeLabel.Enabled = useTbaCheckBox.Checked;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            yearBox.Text = DateTime.Now.Year.ToString();
            uploader.Upload_ProgressChanged += new EventHandler<long>(vid_ProgressChanged);
            uploader.UploadCompleted += new EventHandler<string>(vid_UploadCompleted);
            uploader.Upload_Failed += new EventHandler<string>(vid_UploadFailed);
        }

        private void getTimestampsFromTbaButton_Click(object sender, EventArgs e)
        {
            progress = new ProgressDialog();
            progress.SetText("Fetching data and calculating timestamps");
            progress.Show();
            List<FrcApi.MatchResult> matchResults = api.getMatchResults(Convert.ToInt32(yearBox.Text), eventCodeBox.Text);

            DateTime firstMatchTimeStamp;
            DateTime.TryParse(dt.Rows[0].ItemArray[1].ToString(), out firstMatchTimeStamp);

            List<MatchTimeSpan> timeSpans = getMatchTimespans(matchResults);

            for (int i = 1; i < dt.Rows.Count; i++)
            {                
                try
                {
                    DateTime previousTime = DateTime.Parse(dt.Rows[i - 1][1].ToString());
                    DateTime currentTime = previousTime.Add(timeSpans.Find(x => x.matchName == dt.Rows[i][0].ToString()).timeSpan);
                    dt.Rows[i][1] = currentTime.ToString("HH:mm:ss.fff");   
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error calculating time for match " + dt.Rows[i][0]);
                }
            }
            timeStampsDataGridView.DataSource = dt;
            progress.Close();
        }

        class MatchTimeSpan
        {
            public string matchName { get; set; }
            public TimeSpan timeSpan { get; set; }

            public MatchTimeSpan() { }

            public MatchTimeSpan(string name, TimeSpan span)
            {
                this.matchName = name;
                this.timeSpan = span;
            }
        }

        private void AddToSpreadsheet(VideoDetails details)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(matchVideoDestinationPathTextBox.Text + "\\" + eventNameTextBox.Text + ".csv", true))
            {
                file.WriteLine("2015," + details.eventCode + "," + details.matchType + "," + details.matchNumber + ",https://www.youtube.com/watch?v=" + details.youtubeKey);
            }
        }

        private List<MatchTimeSpan> getMatchTimespans(List<FrcApi.MatchResult> matches)
        {
            List<MatchTimeSpan> apiMatchTimeSpans = new List<MatchTimeSpan>();
            DateTime previousMatchTime = DateTime.Parse(matches.First().autoStartTime);

            string matchTitle;
            DateTime startTime;
            TimeSpan offset;
            int lastQualMatchNumber = 0;
            int playoffStartIndex = 0;

            for (int i = 0; i < matches.Count; i++)
            {                
                if (matches[i].level == "Qualification")
                {
                    matchTitle = "Q" + matches[i].matchNumber;
                    startTime = DateTime.Parse(matches[i].autoStartTime);
                    if (Convert.ToInt32(matches[i].matchNumber) > lastQualMatchNumber)
                    {
                        lastQualMatchNumber = Convert.ToInt32(matches[i].matchNumber);
                    }
                    if (Convert.ToInt32(matches[i].matchNumber) == 1)
                    {
                        offset = startTime - startTime;
                    }
                    else
                    {
                        offset = startTime - DateTime.Parse(matches.Find(x => (x.level == "Qualification") && (Convert.ToInt32(x.matchNumber) == Convert.ToInt32(matches[i].matchNumber) - 1)).autoStartTime);
                    }
                    apiMatchTimeSpans.Add(new MatchTimeSpan(matchTitle, offset));
                }
                else
                {
                    int matchNumber = Convert.ToInt32(matches[i].matchNumber);
                    if (matchNumber <= 8)
                    {
                        matchTitle = "QF" + matchNumber.ToString();
                        startTime = DateTime.Parse(matches[i].autoStartTime);
                        if (Convert.ToInt32(matches[i].matchNumber) == 1)
                        {
                            offset = startTime - startTime;
                            playoffStartIndex = i;
                        }
                        else
                        {
                            offset = startTime - DateTime.Parse(matches.Find(x => (x.level == "Playoff") && (Convert.ToInt32(x.matchNumber) == matchNumber - 1)).autoStartTime);
                            apiMatchTimeSpans.Add(new MatchTimeSpan(matchTitle, offset));
                        }
                    }
                    else if (matchNumber <= 14 && matchNumber >= 9)
                    {
                        matchNumber = matchNumber - 8;
                        matchTitle = "SF" + matchNumber.ToString();
                        startTime = DateTime.Parse(matches[i].autoStartTime);
                        offset = startTime - DateTime.Parse(matches.Find(x => (x.level == "Playoff") && (Convert.ToInt32(x.matchNumber) == matchNumber + 7)).autoStartTime);
                        apiMatchTimeSpans.Add(new MatchTimeSpan(matchTitle, offset));
                    }
                    else
                    {
                        matchNumber = matchNumber - 14;
                        matchTitle = "F" + matchNumber.ToString();
                        startTime = DateTime.Parse(matches[i].autoStartTime);
                        offset = startTime - DateTime.Parse(matches.Find(x => (x.level == "Playoff") && (Convert.ToInt32(x.matchNumber) == matchNumber + 13 )).autoStartTime);
                        apiMatchTimeSpans.Add(new MatchTimeSpan(matchTitle, offset));
                    }
                }
                //string offsetString = offset.ToString("HH:mm:ss");
                
                
            }
            //add in the timespan between the last qual match and the first playoff match
            DateTime quarterFinalStartTime = DateTime.Parse(matches[playoffStartIndex].autoStartTime);
            apiMatchTimeSpans.Add(new MatchTimeSpan("QF1", quarterFinalStartTime - DateTime.Parse(matches.Find(x => (x.level == "Qualification") && (Convert.ToInt32(x.matchNumber) == lastQualMatchNumber)).autoStartTime)));

            return apiMatchTimeSpans;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            vidQueue = new List<VideoDetails>();
            List<FrcApi.MatchResult> matchResults = new List<FrcApi.MatchResult>();
            int chunks = 0;

            char[] delimiters = {'-'};

            if (useTbaCheckBox.Checked)
            {
                try
                {
                    matchResults = api.getMatchResults(Convert.ToInt32(yearBox.Text), eventCodeBox.Text);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("Error getting TBA data, proceed with minimal description?", "Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        worker.CancelAsync();
                    }
                }
            }
            string[] videoFiles = Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.mov");
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.mpeg4")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.mpeg")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.mp4")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.avi")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.wmv")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.mpegps")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.swf")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.flv")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.3gpp")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.webm")).ToArray();
            videoFiles = videoFiles.Concat(Directory.GetFiles(matchVideoDestinationPathTextBox.Text, "*.mkv")).ToArray();
            if (MessageBox.Show(videoFiles.Length + " Files found. This operation may take a long time. Click Yes to confirm", "Proceed?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                worker.CancelAsync();
            }
            else
            {
                progress.Chunks = videoFiles.Length;

                if (!useExistingPlaylist.Checked)
                {
                    playlist = uploader.CreatePlaylist(eventNameTextBox.Text);
                }
                else
                {
                    playlist = playlistIDBox.Text;
                }
            }
            foreach (string videoFile in videoFiles)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    string level = "Qualification";
                    Int32 matchNumber = 0;
                    string matchDescription = Path.GetFileName(videoFile).Split(delimiters).FirstOrDefault().Trim();
                    progress.SetText("Uploading file: " + Path.GetFileName(videoFile));
                    progress.ChunkSize = (new FileInfo(videoFile)).Length;
                    VideoDetails vidDetail = new VideoDetails();

                    if (matchDescription.Length > 5)
                    {
                        vidDetail.matchType = "Awards";
                        vidDetail.matchNumber = "0";
                    }
                    else if (matchDescription.Contains("Q") && !matchDescription.Contains("QF"))
                    {
                        vidDetail.matchType = "q";
                        vidDetail.matchNumber = matchDescription.Substring(1);
                        level = "Qualification";
                        matchNumber = Convert.ToInt32(matchDescription.Substring(1));
                    }
                    else
                    {
                        level = "Playoff";
                        if (matchDescription.Contains("QF"))
                        {
                            vidDetail.matchType = "qf1";
                            vidDetail.matchNumber = matchDescription.Substring(2);
                            matchNumber = Convert.ToInt32(matchDescription.Substring(2));
                        }
                        else if (matchDescription.Contains("SF"))
                        {
                            vidDetail.matchType = "sf1";
                            vidDetail.matchNumber = matchDescription.Substring(2);
                            matchNumber = Convert.ToInt32(matchDescription.Substring(2)) + 8;
                        }
                        else
                        {
                            vidDetail.matchType = "f1";
                            vidDetail.matchNumber = matchDescription.Substring(1);
                            matchNumber = Convert.ToInt32(matchDescription.Substring(1)) + 14;
                        }
                    }
                    
                    string youtubeTitle = eventNameTextBox.Text + " " + matchDescription + Environment.NewLine;
                    string youtubeDescription = youtubeTitle;
                    try
                    {
                        FrcApi.MatchResult result = matchResults.Find(x => (x.level == level) && (Convert.ToInt32(x.matchNumber) == matchNumber));
                        youtubeDescription += "Red Alliance: " + result.teams.Find(x => x.station == "Red1").teamNumber + " " + result.teams.Find(x => x.station == "Red2").teamNumber + " " + result.teams.Find(x => x.station == "Red3").teamNumber + Environment.NewLine;
                        youtubeDescription += "Blue Alliance: " + result.teams.Find(x => x.station == "Blue1").teamNumber + " " + result.teams.Find(x => x.station == "Blue2").teamNumber + " " + result.teams.Find(x => x.station == "Blue3").teamNumber + Environment.NewLine;
                        youtubeDescription += "Final Score - Red Alliance: " + result.scoreRedFinal + "   Blue Alliance: " + result.scoreBlueFinal;
                        vidDetail.eventCode = eventCodeBox.Text;
                    }
                    catch (Exception ex) { }
                    vidQueue.Add(vidDetail);
                    uploader.Upload(youtubeTitle, youtubeDescription, videoFile);
                    progress.SetCompletedChunks(++chunks);
                }
            }
        }

        private void vid_ProgressChanged(object sender, long bytes)
        {
            progress.SetChunkProgress(bytes);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progress.Close();
        }

        private void vid_UploadFailed(object sender, String error)
        {
            AddToSpreadsheet(vidQueue.FirstOrDefault());
            vidQueue.RemoveAt(0);
        }

        private void vid_UploadCompleted(object sender, string id)
        {
            if (playlist != null)
            {
                uploader.AddToPlaylist(playlist, id);
            }
            vidQueue.FirstOrDefault().youtubeKey = id;
            AddToSpreadsheet(vidQueue.FirstOrDefault());
            vidQueue.RemoveAt(0);
        }

        private void useExistingPlaylist_CheckedChanged(object sender, EventArgs e)
        {
            playlistIDBox.Enabled = useExistingPlaylist.Checked;
            playlistLabel.Enabled = useExistingPlaylist.Checked;
        }
    }
}
