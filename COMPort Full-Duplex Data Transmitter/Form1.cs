using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.XWPF.UserModel;
using NPOI.HWPF; 
using NAudio.Wave;
using NAudio.Lame;
using System.Drawing.Imaging;
using Encoder = System.Drawing.Imaging.Encoder;
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
        // <-- Sender Mode Start _________________________________________________________
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

            // if TEXT file
            if (fileExtension == ".txt" || fileExtension == ".docx" || fileExtension == ".doc")
            {
                // Call the EncodeTextOrDocFile function for text, .doc, or .docx files.
                fileContent = EncodeTextOrDocFile(filePath, fileExtension);
                return "Text";
            }

            // if AUDIO file
            else if (fileExtension == ".mp3" || fileExtension == ".wav") // done 2
            {
                fileContent = EncodeAudio(fileContent, fileExtension);
                return "Audio";
            }

            // if IMAGE file
            else if (fileExtension == ".jpg" ||
                     fileExtension == ".jpeg" ||
                     fileExtension == ".png" ||
                     fileExtension == ".gif" ||
                     fileExtension == ".bmp" ||
                     fileExtension == ".tiff" ||
                     fileExtension == ".tif" ||
                     fileExtension == ".svg" ||
                     fileExtension == ".webp" ||
                     fileExtension == ".ico" ||
                     fileExtension == ".raw" ||
                     fileExtension == ".psd" ||
                     fileExtension == ".ai" ||
                     fileExtension == ".eps" ||
                     fileExtension == ".pdf" ||
                     fileExtension == ".jfif" ||
                     fileExtension == ".exif")                           // done 3
            {
                fileContent = EncodeImage(fileContent, fileExtension);
                return "Image";
            }

            // if VIDEO file
            else if (fileExtension == ".mp4" || fileExtension == ".avi" || fileExtension == ".mov") // done 4
            {
                fileContent = EncodeVideo(fileContent, fileExtension);
                return "Video";
            }

            else
            {
                return "Unknown";
            }
        } 

        //
        // Encoding Method of Text File
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
                else if (fileExtension == ".docx" || fileExtension == ".doc")
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
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        HWPFDocument document = new HWPFDocument(fileStream);
                        using (MemoryStream memStream = new MemoryStream())
                        {
                            document.Write(memStream);
                            encodedData = memStream.ToArray();
                        }
                    }
                }
                else
                {
                    // Handle unsupported file types or provide specific encoding logic for other extensions.
                    Console.WriteLine("Unsupported file type: " + fileExtension);
                }

                return encodedData; // [ encoded data is being stored in this variable ]
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during encoding.
                Console.WriteLine("Error encoding file: " + ex.Message);
                return null;
            }
        }
        //End

        //
        // Encoding Method of Audio File
        private byte[] EncodeAudio(byte[] fileContent, string fileExtension)
        {
            if (fileExtension.ToLower() == ".mp3" || fileExtension.ToLower() == ".wav")
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Assuming the input audio is in WAV format
                        using (WaveStream waveStream = new WaveFileReader(new MemoryStream(fileContent)))
                        {
                            // Create a new MP3 file
                            using (LameMP3FileWriter mp3Writer = new LameMP3FileWriter(ms, waveStream.WaveFormat, LAMEPreset.STANDARD))
                            {
                                // Convert WAV to MP3
                                waveStream.CopyTo(mp3Writer);
                            }
                        }
                        return ms.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that may occur during audio encoding.
                    Console.WriteLine("Error encoding audio: " + ex.Message);
                }
            }
            // If the file is not an audio file supported for encoding, just return the original content.
            return fileContent;
        }
        // End

        //
        // Encoding Method of Image File
        private byte[] EncodeImage(byte[] fileContent, string fileExtension, int qualityLevel = 100)
        {
            // Check for null parameters
            if (fileContent == null)
            {
                Console.WriteLine("Error: Input file content is null.");
                return null; // Or throw an ArgumentNullException.
            }

            if (IsSupportedImageFormat(fileExtension))
            {
                try
                {
                    // Load the image from the MemoryStream
                    Image image = LoadImage(fileContent);

                    // Create a new MemoryStream to store the encoded image
                    using (MemoryStream encodedMs = EncodeImageToJpeg(image, qualityLevel))
                    {
                        // Return the encoded image content
                        return encodedMs.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error encoding image: {ex}");
                    // Log the exception details to a logging system or log file.
                    // Consider rethrowing the exception if it's critical or needs further handling.
                }
            }
            // If the file is not an image file supported for encoding, just return the original content
            return fileContent;
        }
        private Image LoadImage(byte[] fileContent)
        {
            using (MemoryStream ms = new MemoryStream(fileContent))
            {
                return Image.FromStream(ms);
            }
        }
        private MemoryStream EncodeImageToJpeg(Image image, int qualityLevel)
        {
            using (MemoryStream encodedMs = new MemoryStream())
            {
                // Save the image in JPEG format with a specified quality level
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, qualityLevel);

                // Get the JPEG codec info
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Save the image using the JPEG codec
                image.Save(encodedMs, jpgEncoder, encoderParameters);

                return encodedMs;
            }
        }
        private bool IsSupportedImageFormat(string fileExtension)
        {
            // Check if the file extension corresponds to a supported image format
            fileExtension = fileExtension.ToLower();
            return fileExtension == ".jpg" ||
                   fileExtension == ".jpeg" ||
                   fileExtension == ".png" ||
                   fileExtension == ".gif" ||
                   fileExtension == ".bmp" ||
                   fileExtension == ".tiff" ||
                   fileExtension == ".tif" ||
                   fileExtension == ".svg" ||
                   fileExtension == ".webp" ||
                   fileExtension == ".ico" ||
                   fileExtension == ".raw" ||
                   fileExtension == ".psd" ||
                   fileExtension == ".ai" ||
                   fileExtension == ".eps" ||
                   fileExtension == ".pdf" ||
                   fileExtension == ".jfif" ||
                   fileExtension == ".exif";
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            // Get the codec info for a specific image format
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        // End

        //
        // Encoding Method of Video File
        private byte[] EncodeVideo(byte[] fileContent, string fileExtension)
        {
            if (fileExtension.ToLower() == ".mp4" || fileExtension.ToLower() == ".avi" || fileExtension.ToLower() == ".mov")
            {
                try
                {
                    // Convert the video content to Base64
                    string base64Encoded = Convert.ToBase64String(fileContent);

                    // Convert the Base64 string back to bytes for transmission
                    byte[] encodedBytes = Encoding.UTF8.GetBytes(base64Encoded);

                    return encodedBytes;
                }
                catch (Exception ex)
                {
                    // Handle any errors that may occur during video encoding.
                    Console.WriteLine("Error encoding video: " + ex.Message);
                }
            }

            // If the file is not a supported video format, just return the original content.
            return fileContent;
        }
        // End
        //

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