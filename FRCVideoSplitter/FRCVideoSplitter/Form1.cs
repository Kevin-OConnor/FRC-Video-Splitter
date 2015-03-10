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

        private void splitVideosButton_Click(object sender, EventArgs e)
        {
            DateTime timeValue;
            List<string> errors = new List<string>();
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
                foreach (DataRow row in dt.Rows)
                {                    
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
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in errors)
                {
                    sb.Append(s + ", ");
                }
                MessageBox.Show("Timestampe errors detected at: " + sb.ToString());
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
        }

        private void getTimestampsFromTbaButton_Click(object sender, EventArgs e)
        {
            List<FrcApi.MatchResult> matchResults = api.getMatchResults(Convert.ToInt32(yearBox.Text), eventCodeBox.Text);

            DateTime firstMatchTimeStamp;
            DateTime.TryParse(dt.Rows[0].ItemArray[1].ToString(), out firstMatchTimeStamp);

            List<MatchTimeSpan> timeSpans = getMatchTimespans(matchResults);

            for (int i = 1; i < dt.Rows.Count; i++)
            {                
                DateTime previousTime = DateTime.Parse(dt.Rows[i - 1][1].ToString());
                DateTime currentTime = previousTime.Add(timeSpans.Find(x => x.matchName == dt.Rows[i][0].ToString()).timeSpan);
                dt.Rows[i][1] = currentTime.ToString("HH:mm:ss.fff");                    
            }
            timeStampsDataGridView.DataSource = dt;
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

        private List<MatchTimeSpan> getMatchTimespans(List<FrcApi.MatchResult> matches)
        {
            List<MatchTimeSpan> apiMatchTimeSpans = new List<MatchTimeSpan>();
            DateTime previousMatchTime = DateTime.Parse(matches.First().autoStartTime);

            string matchTitle;
            DateTime startTime;
            TimeSpan offset;
            for (int i = 0; i < matches.Count; i++)
            {                
                if (matches[i].level == "Qualification")
                {
                    matchTitle = "Q" + matches[i].matchNumber;
                    startTime = DateTime.Parse(matches[i].autoStartTime);
                    if (Convert.ToInt32(matches[i].matchNumber) == 1)
                    {
                        offset = startTime - startTime;
                    }
                    else
                    {
                        offset = startTime - DateTime.Parse(matches.Find(x => (x.level == "Qualification") && (Convert.ToInt32(x.matchNumber) == Convert.ToInt32(matches[i].matchNumber) - 1)).autoStartTime);
                    }
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
                        }
                        else
                        {
                            offset = startTime - DateTime.Parse(matches.Find(x => (x.level == "Playoff") && (Convert.ToInt32(x.matchNumber) == matchNumber - 1)).autoStartTime);
                        }
                    }
                    else if (matchNumber <= 14 && matchNumber >= 9)
                    {
                        matchNumber = matchNumber - 8;
                        matchTitle = "SF" + matchNumber.ToString();
                        startTime = DateTime.Parse(matches[i].autoStartTime);
                        offset = startTime - DateTime.Parse(matches.Find(x => (x.level == "Playoff") && (Convert.ToInt32(x.matchNumber) == matchNumber + 7)).autoStartTime);
                    }
                    else
                    {
                        matchNumber = matchNumber - 14;
                        matchTitle = "F" + matchNumber.ToString();
                        startTime = DateTime.Parse(matches[i].autoStartTime);
                        offset = startTime - DateTime.Parse(matches.Find(x => (x.level == "Playoff") && (Convert.ToInt32(x.matchNumber) == matchNumber + 13 )).autoStartTime);
                    }
                }
                //string offsetString = offset.ToString("HH:mm:ss");
                apiMatchTimeSpans.Add(new MatchTimeSpan(matchTitle, offset));
            }

            return apiMatchTimeSpans;
        }
    }
}
