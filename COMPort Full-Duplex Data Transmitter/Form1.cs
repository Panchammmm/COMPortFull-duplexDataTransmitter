using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.XWPF.UserModel; // For .docx files
using NPOI.HWPF.UserModel; // For .doc files
using NPOI.POIFS.FileSystem;
using NPOI.HWPF.Extractor;
using NPOI.HWPF;
//

namespace COMPort_Full_duplex_Data_Transmitter
{
    public partial class Form1 : Form
    {
        byte[] fileContent;
        string selectedSize;
        string userSpecifiedFileName;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Disable user input in ComboBoxes by setting the DropDownStyle
            cBoxComPort.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxBaudrate.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxDatabit.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxStopbit.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxParitybit.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxChunkSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFileType.DropDownStyle = ComboBoxStyle.DropDownList;

            string[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.AddRange(ports);

            // Make tbxFileName, textBox and ReceivedFilePathTextBox read-only
            tbxFileName.ReadOnly = true;
            textBox.ReadOnly = true;
            ReceivedFilePathTextBox.ReadOnly = true;
        }

        private void SetSerialPortSettings()
        {
            serialPort1.PortName = cBoxComPort.Text;
            serialPort1.BaudRate = Convert.ToInt32(cBoxBaudrate.Text);
            serialPort1.DataBits = Convert.ToInt32(cBoxDatabit.Text);
            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopbit.Text);
            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParitybit.Text);
        }


        private async void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                SetSerialPortSettings();

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

        //
        // <-- Sender Mode Start
        //

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

                // Call the GetFileType method to determine the file type.
                string fileType = GetFileType(filePath);

                try
                {
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

        private string GetFileType(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            if (string.IsNullOrEmpty(fileExtension))
            {
                return "Unknown";
            }

            fileExtension = fileExtension.ToLower();

            if (fileExtension == ".txt" || fileExtension == ".doc" || fileExtension == ".docx")
            {
                // Call the EncodeTextOrDocFile function for text, .doc, or .docx files.
                byte[] encodedData = EncodeTextOrDocFile(filePath, fileExtension);
                return "Text";
            }

            else if (fileExtension == ".mp3" || fileExtension == ".wav" || fileExtension == ".ogg")
            {
                fileContent = EncodeAudio(fileContent, fileExtension);
                return "Audio";
            }

            else if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
            {
                fileContent = EncodeImage(fileContent, fileExtension);
                return "Image";
            }

            else if (fileExtension == ".mp4" || fileExtension == ".avi" || fileExtension == ".mov")
            {
                fileContent = EncodeVideo(fileContent, fileExtension);
                return "Video";
            }

            else if (fileExtension == ".pdf" || fileExtension == ".xls" || fileExtension == ".xlsx")
            {
                fileContent = EncodeDocument(fileContent, fileExtension);
                return "Document";
            }

            else if (fileExtension == ".zip" || fileExtension == ".rar")
            {
                fileContent = CompressFile(fileContent, fileExtension);
                return "Compressed";
            }

            if (fileExtension == ".csv" || fileExtension == ".tsv")
            {
                string fileType = EncodeCsvOrTsvFile(fileContent, fileExtension);
                return fileType;
            }

            if (fileExtension == ".ppt" || fileExtension == ".pptx")
            {
                string fileType = EncodePresentationFile(fileContent);
                return fileType;
            }

            else
            {
                return "Unknown";
            }
        }

        private byte[] EncodeTextOrDocFile(string filePath, string fileExtension)
        {
            // Check if the file exists.
            if (!File.Exists(filePath))
            {
                return null;
            }

            try
            {
                // Initialize a variable to hold the encoded data.
                byte[] encodedData = null;

                if (fileExtension == ".txt")
                {
                    // Encoding logic for text files (e.g., .txt)
                    // Read the text from the file using UTF-8 encoding.
                    string text = File.ReadAllText(filePath, Encoding.UTF8);

                    // Convert the text to a byte array using UTF-8 encoding.
                    encodedData = Encoding.UTF8.GetBytes(text);
                }
                else if (fileExtension == ".docx")
                {
                    // Encoding logic for .docx files
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        XWPFDocument document = new XWPFDocument(fileStream);
                        using (MemoryStream memStream = new MemoryStream())
                        {
                            document.Write(memStream);
                            encodedData = memStream.ToArray();
                        }
                    }
                }
                else if (fileExtension == ".doc")
                {
                    // Encoding logic for .doc files
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        HWPFDocument document = new HWPFDocument(fileStream);
                        WordExtractor wordExtractor = new WordExtractor(document);
                        string text = string.Join("\n", wordExtractor.Text);
                        encodedData = Encoding.Default.GetBytes(text);
                    }
                }
                else
                {
                    // Handle unsupported file types or provide specific encoding logic for other extensions.
                    Console.WriteLine("Unsupported file type: " + fileExtension);
                }

                return encodedData;
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during encoding.
                Console.WriteLine("Error encoding file: " + ex.Message);
                return null;
            }
        }

        private byte[] EncodeAudio(byte[] fileContent, string fileExtension)
        {
            // Your audio encoding logic here.
            // Replace this comment with your actual encoding code.
            // You may use libraries or methods specific to audio encoding.
            return fileContent; // For demonstration, just returning the original content.
        }

        private byte[] EncodeImage(byte[] fileContent, string fileExtension)
        {
            // Your image encoding logic here.
            // Replace this comment with your actual encoding code.
            // You may use libraries or methods specific to image encoding.
            return fileContent; // For demonstration, just returning the original content.
        }

        private byte[] EncodeVideo(byte[] fileContent, string fileExtension)
        {
            // Your video encoding logic here.
            // Replace this comment with your actual encoding code.
            // You may use libraries or methods specific to video encoding.
            return fileContent; // For demonstration, just returning the original content.
        }

        private byte[] EncodeDocument(byte[] fileContent, string fileExtension)
        {
            // Your document encoding logic here.
            // Replace this comment with your actual encoding code.
            // You may use libraries or methods specific to document encoding.
            return fileContent; // For demonstration, just returning the original content.
        }

        private byte[] CompressFile(byte[] fileContent, string fileExtension)
        {
            // Your compression logic here.
            // Replace this comment with your actual compression code.
            // You may use libraries or methods specific to file compression.
            return fileContent; // For demonstration, just returning the original content.
        }

        private string EncodeCsvOrTsvFile(byte[] fileContent, string fileExtension)
        {
            if (fileExtension == ".csv")
            {
                // Encoding logic for CSV files (Comma-Separated Values) goes here.
                // You can implement logic to handle CSV files.
                // For example, you can parse and process the CSV content.

                // Return the file type.
                return "CSV";
            }
            else if (fileExtension == ".tsv")
            {
                // Encoding logic for TSV files (Tab-Separated Values) goes here.
                // You can implement logic to handle TSV files.
                // For example, you can parse and process the TSV content.

                // Return the file type.
                return "TSV";
            }

            // If the fileExtension is neither .csv nor .tsv, you can handle it accordingly.
            return "Unknown";
        }

        private string EncodePresentationFile(byte[] fileContent)
        {
            // Encoding logic for PowerPoint presentation files goes here.
            // You can implement logic to handle .ppt and .pptx files.

            // For example, you can check the fileContent or perform encoding tasks specific to presentations.

            // Return the file type.
            return "Presentation";
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
                    SetSerialPortSettings();

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
            userSpecifiedFileName = InputHere.Text.Trim();

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
                // Generate a unique file name based on the current timestamp and file extension.
                string ReceivedFileName = Path.Combine(selectedFolderPath, userSpecifiedFileName + fileExtension);

                // Save the received data to the selected directory with the generated file name.
                File.WriteAllBytes(ReceivedFileName, receivedDataBuffer.ToArray());

                // Update the ReceivedFilePathTextBox with the received file path.
                ReceivedFilePathTextBox.Text = ReceivedFileName;

                // Update the UI to indicate successful reception and file saving.
                this.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = "•😎 " +
                        $"Data saved as {ReceivedFileName}";
                    textBox.ForeColor = Color.Green;
                });
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during the file saving process.
                this.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = "•Error saving file: 😭" + ex.Message;
                    textBox.ForeColor = Color.Red;
                });
            }
        }

        // Declare selectedFolderPath at the class level
        private string selectedFolderPath;

        string fileExtension;

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
                            fileExtension = "";

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


    }
}
// <-- End 😎