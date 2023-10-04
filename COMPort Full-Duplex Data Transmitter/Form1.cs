using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//

namespace COMPort_Full_duplex_Data_Transmitter
{
    public partial class Form1 : Form
    {
        byte[] fileContent;
        string selectedSize;
        int chunkSize;

        public Form1()
        {
            InitializeComponent();

        }

        enum CommunicationMode
        {
            Sender,
            Receiver
        }

        private CommunicationMode currentMode = CommunicationMode.Sender;

        private void radioSender_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSender.Checked)
            {
                currentMode = CommunicationMode.Sender;
                cBoxChunkSize.Enabled = true;
                tbxFileName.Enabled = true;
                btnSendData.Enabled = true;
                btnBrowse.Enabled = true;
                //
                btnReceiveData.Enabled = false;
                ReceivedFilePathTextBox.Enabled = false;
                cmbFileType.Enabled = false;
                InputHere.Enabled = false;
                // Remove the btnSaveHere_Click event handler when in Sender mode.
                btnSaveHere.Click -= btnSaveHere_Click;
            }
        }

        private void radioReceiver_CheckedChanged(object sender, EventArgs e)
        {
            if (radioReceiver.Checked)
            {
                currentMode = CommunicationMode.Receiver;
                btnSendData.Enabled = false;
                btnBrowse.Enabled = false;
                cBoxChunkSize.Enabled = false;
                tbxFileName.Enabled = false;
                //
                btnReceiveData.Enabled = true;
                ReceivedFilePathTextBox.Enabled = true;
                cmbFileType.Enabled = true;
                InputHere.Enabled = true;
                // Add the btnSaveHere_Click event handler when in Receiver mode.
                btnSaveHere.Click += btnSaveHere_Click;
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.AddRange(ports);
        }

        private async void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxComPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudrate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDatabit.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopbit.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParitybit.Text);

                // Disable UI elements to prevent user interaction during the operation.
                btnOpen.Enabled = false;

                // Open the serial port asynchronously.
                await Task.Run(() => serialPort1.Open());

                // Re-enable UI elements or perform other post-operation tasks.
                btnOpen.Enabled = true;
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            textBox.Text = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.Title = "Select a File Here";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                tbxFileName.Text = openFileDialog.FileName;

                try
                {
                    fileContent = LoadFileIntoByteArray(filePath);

                    if (fileContent != null)
                    {
                        // Successfully loaded the file into the byte array.
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "File Read Error 😒", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.ForeColor = Color.Red;
                }
            }
        }

        private byte[] LoadFileIntoByteArray(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    fileContent = File.ReadAllBytes(filePath);
                    return fileContent;
                }
                else
                {
                    MessageBox.Show("The selected file does not exist 🤦‍♂️", "File Not Found 🤦‍♀️", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.ForeColor = Color.Red;
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading the file: {ex.Message} 😝", "Error 😪", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.ForeColor = Color.Red;
                return null;
            }
        }

        private async void btnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    byte[] dataToSend = fileContent;

                    if (dataToSend != null)
                    {
                        int chunkSize;

                        selectedSize = cBoxChunkSize.SelectedItem.ToString();

                        if (int.TryParse(selectedSize, out chunkSize) && chunkSize > 0)
                        {
                            int totalChunks = (int)Math.Ceiling((double)dataToSend.Length / chunkSize);

                            // Define header and footer markers as byte arrays.
                            byte[] headerMarker = new byte[] { 0xAA, 0xBB }; // Example header marker.
                            byte[] footerMarker = new byte[] { 0xCC, 0xDD }; // Example footer marker.

                            // Send the header marker to signal the start of transmission.
                            await Task.Run(() => serialPort1.Write(headerMarker, 0, headerMarker.Length));

                            for (int chunkIndex = 0; chunkIndex < totalChunks; chunkIndex++)
                            {
                                int offset = chunkIndex * chunkSize;
                                int remainingBytes = dataToSend.Length - offset;
                                int bytesToSend = Math.Min(chunkSize, remainingBytes);

                                byte[] chunk = new byte[bytesToSend];
                                Array.Copy(dataToSend, offset, chunk, 0, bytesToSend);

                                // Send the current chunk of data.
                                await Task.Run(() => serialPort1.Write(chunk, 0, bytesToSend));

                                int progressPercentage = (int)(((double)(chunkIndex + 1) / totalChunks) * 100);
                                progressBar2.Value = progressPercentage;
                            }

                            // Send the footer marker to signal the end of transmission.
                            await Task.Run(() => serialPort1.Write(footerMarker, 0, footerMarker.Length));

                            textBox.Text = "•Data sent successfully! 😎 " +
                                "Well Done Bro 😘" +
                                "Proud Of You 🤗";
                            textBox.ForeColor = Color.Green;
                        }
                        else
                        {
                            textBox.Text = "•What are you doing !! 😡. Try Again";
                            textBox.ForeColor = Color.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    textBox.Text = "•Error sending data: 😭" + ex.Message;
                    textBox.ForeColor = Color.Red;
                }
            }
        }
        //
        // <-- End Sender Mode
        //

        //
        // <-- Start Receiver Mode
        //
        private List<byte> receivedDataBuffer = new List<byte>();
        private bool receivingData = false;

        // Variable to keep track of the total expected bytes to be received.
        private int expectedDataLength = 0;

        // For Data Reception Progress Bars
        private void UpdateDataReceptionProgress(int receivedBytes, int totalBytes)
        {
            double progressPercentage = (double)receivedBytes / totalBytes * 100;
            progressBar3.Value = (int)progressPercentage; // Update progressBar3 for data reception progress.
        }

        private void btnReceiveData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    // Clear any previous data.
                    receivedDataBuffer.Clear();
                    receivingData = false;

                    // Reset expectedDataLength to zero at the start of reception.
                    expectedDataLength = 0;

                    while (serialPort1.BytesToRead > 0)
                    {
                        byte receivedByte = (byte)serialPort1.ReadByte();

                        // Check for the header marker (0xAA) to start receiving data.
                        if (receivedByte == 0xAA)
                        {
                            receivingData = true;
                            receivedDataBuffer.Clear();

                            // Reset expectedDataLength to zero when a new transmission starts.
                            expectedDataLength = 0;
                        }
                        // Check for the footer marker (0xDD) to stop receiving data.
                        else if (receivedByte == 0xDD && receivingData)
                        {
                            receivingData = false;

                            // Now that we've received the data, set expectedDataLength to the received buffer's length.
                            expectedDataLength = receivedDataBuffer.Count;

                            ProcessReceivedData(receivedDataBuffer.ToArray());
                            receivedDataBuffer.Clear();
                        }
                        // If we are receiving data between header and footer markers, add it to the buffer.
                        else if (receivingData)
                        {
                            receivedDataBuffer.Add(receivedByte);

                            // Update the reception progress bars.
                            UpdateDataReceptionProgress(receivedDataBuffer.Count, expectedDataLength);
                        }
                    }
                }
                catch (Exception ex)
                {
                    textBox.Text = "•Error receiving data: 😭" + ex.Message;
                    textBox.ForeColor = Color.Red;
                }
            }
        }

        private bool IsValidFileName(string fileName)
        {
            // Define a regular expression pattern to match valid file names.
            string pattern = @"^[a-zA-Z0-9\s\(\)\[\]_-]+$";

            // Check if the fileName matches the pattern.
            bool isMatch = Regex.IsMatch(fileName, pattern);

            return isMatch;
        }

        private void ProcessReceivedData(byte[] receivedData)
        {
            if (string.IsNullOrEmpty(InputHere.Text))
            {
                this.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = "•Please enter a valid file name in InputHere.";
                    textBox.ForeColor = Color.Red;
                });
                return;
            }

            // Get the user-specified file name from the InputHere TextBox.
            string userSpecifiedFileName = InputHere.Text.Trim();

            // Validate the file name.
            if (!IsValidFileName(userSpecifiedFileName))
            {
                this.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = "•Invalid file name. File names can only contain letters, numbers, spaces, (), [], _, and -.";
                    textBox.ForeColor = Color.Red;
                });
                return;
            }

            try
            {
                // Combine the user-specified file name with the selected folder path.
                string receivedFileName = Path.Combine(selectedFolderPath, userSpecifiedFileName);

                // Save the received data to the selected directory with the user-specified file name.
                File.WriteAllBytes(receivedFileName, receivedDataBuffer.ToArray());

                // Update the ReceivedFilePathTextBox with the received file path.
                ReceivedFilePathTextBox.Text = receivedFileName;

                // Update the UI to indicate successful reception and file saving.
                this.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = "•Data received successfully! 😎 " +
                        $"Data saved as {receivedFileName}";
                    textBox.ForeColor = Color.Green;
                });
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during the file saving process.
                this.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = "•Error saving data: 😭" + ex.Message;
                    textBox.ForeColor = Color.Red;
                });
            }
        }



        // Declare selectedFolderPath at the class level
        private string selectedFolderPath;

        private void btnSaveHere_Click(object sender, EventArgs e)
        {
            
            // Show the folder browser dialog to allow the user to choose a directory.
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected directory path.
                    string selectedFolderPath = folderBrowser.SelectedPath;

                    if (!string.IsNullOrEmpty(selectedFolderPath))
                    {
                        try
                        {
                            string fileExtension = "";

                            // Determine the file extension based on the user's selection.
                            string fileType = cmbFileType.SelectedItem.ToString();
                            switch (fileType)
                            {
                                case "Image":
                                    fileExtension = ".jpg"; // Change the extension for images.
                                    break;
                                case "Text":
                                    fileExtension = ".txt"; // Change the extension for text files.
                                    break;
                                case "Audio":
                                    fileExtension = ".mp3"; // Change the extension for audio files.
                                    break;
                                case "Video":
                                    fileExtension = ".mp4"; // Change the extension for video files.
                                    break;
                                default:
                                    // Handle unsupported file types.
                                    MessageBox.Show("Unsupported file type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                            }

                            // Generate a unique file name based on the current timestamp and file extension.
                            string receivedFileName = Path.Combine(selectedFolderPath, $"received_data_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}");

                            // Save the received data to the selected directory with the generated file name.
                            File.WriteAllBytes(receivedFileName, receivedDataBuffer.ToArray());

                            // Update the ReceivedFilePathTextBox with the received file path.
                            ReceivedFilePathTextBox.Text = receivedFileName;

                            // Update the UI to indicate successful reception and file saving.
                            this.Invoke((MethodInvoker)delegate
                            {
                                textBox.Text = "•Data received successfully! 😎 " +
                                    $"Data saved as {receivedFileName}";
                                textBox.ForeColor = Color.Green;
                            });
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            // Handle unauthorized access to the selected folder.
                            MessageBox.Show("Unauthorized access to the selected folder. Please choose a different folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (IOException ex)
                        {
                            // Handle other I/O-related errors.
                            MessageBox.Show($"An I/O error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            // Handle any other unexpected errors.
                            MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

//
//
    }
//
}
// <-- End 😎