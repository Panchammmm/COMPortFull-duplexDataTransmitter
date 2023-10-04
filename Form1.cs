using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace COMPort_Full_duplex_Data_Transmitter
{
    public partial class Form1 : Form
    {
        byte[] fileContent; // Declare fileContent as a field
        string selectedSize;
        int chunkSize;

        public Form1()
        {
            InitializeComponent();
        }

        // Enumeration to represent sender and receiver modes
        enum CommunicationMode
        {
            Sender,
            Receiver
        }

        private CommunicationMode currentMode = CommunicationMode.Sender; // Default mode is sender

        private void radioSender_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSender.Checked)
            {
                currentMode = CommunicationMode.Sender;
                // Enable controls relevant to sender mode
                btnSendData.Enabled = true;
                btnBrowse.Enabled = true;
                btnOpen.Enabled = true;
                btnClose.Enabled = true;
            }
        }

        private void radioReceiver_CheckedChanged(object sender, EventArgs e)
        {
            if (radioReceiver.Checked)
            {
                currentMode = CommunicationMode.Receiver;
                // Disable controls that are not applicable in receiver mode
                btnSendData.Enabled = false;
                btnBrowse.Enabled = false;
                btnOpen.Enabled = true;
                btnClose.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.AddRange(ports);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxComPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudrate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDatabit.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopbit.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParitybit.Text);
                serialPort1.Open();
                progressBar1.Value = 100;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error in Port Configuration 😥", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.ForeColor = Color.Red;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
            }
        }







        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            textBox.Text = "";

            // to open file dialog in pc
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.Title = "Select a File Here";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // selected file's path
                string filePath = openFileDialog.FileName;

                tbxFileName.Text = openFileDialog.FileName;

                try
                {
                    // Read the file's content
                    fileContent = LoadFileIntoByteArray(filePath);

                    if (fileContent != null)
                    {
                        // Successfully loaded the image into the byte array.
                        // You can now use 'imageBytes' as needed, such as sending it over the serial port.
                    }
                }
                catch (Exception ex)
                {
                    // show error if something is wrong
                    MessageBox.Show("Error: " + ex.Message, "File Read Error 😒", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.ForeColor = Color.Red;
                }
            }
        }
        private byte[] LoadFileIntoByteArray(string filePath)
        {
            try
            {
                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read the file into a byte array
                    fileContent = File.ReadAllBytes(filePath);

                    return fileContent;
                }
                else
                {
                    // Handle the case where the file doesn't exist
                    MessageBox.Show("The selected file does not exist 🤦‍♂️", "File Not Found 🤦‍♀️", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.ForeColor = Color.Red;
                    return null; // Return null to indicate that the file was not found.
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during file reading
                MessageBox.Show($"Error reading the file: {ex.Message} 😝", "Error 😪", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.ForeColor = Color.Red;
                return null; // Return null to indicate an error.
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    byte[] dataToSend = null;

                    // Use the loaded fileContent byte array
                    dataToSend = fileContent;

                    if (dataToSend != null)
                    {
                        int chunkSize;

                        // Read the selected chunk size from the ComboBox
                        selectedSize = cBoxChunkSize.SelectedItem.ToString();

                        if (int.TryParse(selectedSize, out chunkSize) && chunkSize > 0)
                        {
                            int totalChunks = (int)Math.Ceiling((double)dataToSend.Length / chunkSize);

                            for (int chunkIndex = 0; chunkIndex < totalChunks; chunkIndex++)
                            {
                                int offset = chunkIndex * chunkSize;
                                int remainingBytes = dataToSend.Length - offset;
                                int bytesToSend = Math.Min(chunkSize, remainingBytes);

                                byte[] chunk = new byte[bytesToSend];
                                Array.Copy(dataToSend, offset, chunk, 0, bytesToSend);

                                // Send the current chunk of data
                                serialPort1.Write(chunk, 0, bytesToSend);

                                // Update the progress bar
                                int progressPercentage = (int)(((double)(chunkIndex + 1) / totalChunks) * 100);
                                progressBar2.Value = progressPercentage;
                            }

                            // Display a success message in the textBox
                            textBox.Text = "•Data sent successfully! 😎 " +
                                "Well Done Bro 😘" +
                                "Proud Of You 🤗";
                            textBox.ForeColor = Color.Green;
                        }
                        else
                        {
                            // Display an error message if the chunk size is invalid
                            textBox.Text = "•What are you doing !! 😡. Try Again";
                            textBox.ForeColor = Color.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Display an error message in the textBox with red text color
                    textBox.Text = "•Error sending data: 😭" + ex.Message;
                    textBox.ForeColor = Color.Red;
                }
            }
        }



    }
}
// <-- End 😎