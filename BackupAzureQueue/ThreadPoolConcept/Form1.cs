using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadPoolConcept
{
    public partial class Form1 : Form
    {
        // This is the delegate that runs on the UI thread to update the bar.
        public delegate void BarDelegate();

        public Form1()
        {
            InitializeComponent();
        }

        // When a buttom is pressed, launch a new thread
        private void button1_Click(object sender, EventArgs e)
        {
            // Set progress bar length.
            prgsBar.Maximum = 6;
            prgsBar.Minimum = 0;

            // Pass these values to the thread.
            ThreadInfo threadInfo = new ThreadInfo();
            threadInfo.FileName = "file.txt";
            threadInfo.SelectedIndex = 3;

            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFile), threadInfo);
        }

        // What runs on a background thread.
        private void ProcessFile(object a)
        {
            // (Omitted)
            // Do something important using 'a'.

            // Tell the UI we are done.
            try
            {
                // Invoke the delegate on the form.
                this.Invoke(new BarDelegate(UpdateBar));
            }
            catch
            {
                // Some problem occurred but we can recover.
            }
        }

        // Update the graphical bar.
        private void UpdateBar()
        {
            prgsBar.Value++;
            if (prgsBar.Value == prgsBar.Maximum)
            {
                // We are finished and the progress bar is full.
            }
        }


        void ExampleMethod()
        {
            // Hook up the ProcessFile method to the ThreadPool.
            // Note: 'a' is an argument name. Read more on arguments.
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFile), null);
        }
    }

    // Special class that is an argument to the ThreadPool method.
    public class ThreadInfo
    {
        public string FileName { get; set; }
        public int SelectedIndex { get; set; }
    }

    public class Example
    {
        public Example()
        {
            // Declare a new argument object.
            ThreadInfo threadInfo = new ThreadInfo();
            threadInfo.FileName = "file.txt";
            threadInfo.SelectedIndex = 3;

            // Send the custom object to the threaded method.
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFile), threadInfo);
        }

        private void ProcessFile(object a)
        {
            // Constrain the number of worker threads
            // (Omitted here.)

            // We receive the threadInfo as an uncasted object.
            // Use the 'as' operator to cast it to ThreadInfo.
            ThreadInfo threadInfo = a as ThreadInfo;
            string fileName = threadInfo.FileName;
            int index = threadInfo.SelectedIndex;
        }
    }
}
