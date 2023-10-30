using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Words.NET;
using static System.Net.Mime.MediaTypeNames;

namespace OpenListbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string selectedFileName = null;
        string selectedFileContent = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_choose_file_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //// Display OpenFileDialog by calling ShowDialog method 
            //Nullable<bool> result = dlg.ShowDialog();


            //// Get the selected file name and display in a TextBox 
            //if (result == true)
            //{
            //    // Open document 
            //    selectedFileName = dlg.FileName;
            //    selectedFileContent = System.IO.File.ReadAllText(selectedFileName);

            //    file_way.Items.Add(selectedFileName);
            //}
            //КОНЕЦ СТАРОЙ ВЕРСИИ

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();
                                    //selectedFileContent.Text = selectedFileContent.ItemsSource.TrimToBounds();

            // Get the selected file name and display it in a ListBox
            if (result == true)
            {
                // Open document
                selectedFileName = dlg.FileName;

                if (selectedFileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    selectedFileContent = System.IO.File.ReadAllText(selectedFileName);
                }
                else if (selectedFileName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                {
                    using (DocX doc = DocX.Load(selectedFileName))
                    {
                        //string line = doc.ToString(); 
                        //while((line =))
                        // selectedFileContent.TrimEnd();
                        selectedFileContent = doc.Text;

                        /* разделение построчное документа док */

                       // var obj =  doc.Text.ToString().TrimStart('.');
                       // selectedFileContent += obj;

                       // selectedFileContent = obj.ToString();
                        //string[] subwords = selectedFileContent.Split('.');

                        //foreach (string subword in subwords)
                        //{
                        //    selectedFileContent = subword;
                        //}
                        //selectedFileContent = subwords[0];


                    }
                }

                file_way.Items.Add(selectedFileName);
            }
        }

        private void button_display_info_Click(object sender, RoutedEventArgs e)
        {

            if (selectedFileName != null)
            {
                // Display the selected file information in the second ListBox (fileListBox2)
                file_info.Items.Add(selectedFileContent);
            }
            else
            {
                MessageBox.Show("Please choose file!");
                // Handle the case where no file has been selected yet.
                // You can display an error message or perform other actions as needed.
            }
        }

        private void file_dragging_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void file_dragging_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    // Add the file path to the first ListBox (pathListBox)
                    file_way.Items.Add(file);

                    // Read and display the content of the selected file in the second ListBox (contentListBox)
                    //string fileContent = System.IO.File.ReadAllText(file);
                    //file_info.Items.Add(fileContent);
                    
                    if (file.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                    {
                        using (DocX doc = DocX.Load(file))
                        {
                            string docContent = doc.Text;
                            file_info.Items.Add(docContent);
                        }
                    }
                    else if (file.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        string fileContent = System.IO.File.ReadAllText(file);
                        file_info.Items.Add(fileContent);
                    }
                    // Get file information
                    FileInfo fileInfo = new FileInfo(file);

                    // Get file size
                    long fileSizeBytes = fileInfo.Length;
                    string fileSizeString = GetFormattedFileSize(fileSizeBytes);

                    // Get file creation date
                    DateTime fileCreationDate = fileInfo.CreationTime;

                    // Add file size and creation date to the third ListBox (dragDropListBox)
                    file_dragging.Items.Add($"Розмір: {fileSizeString}\nДата створення: {fileCreationDate}");

                }
            }
        }

        private string GetFormattedFileSize(long fileSizeBytes)
        {
            string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB" };
            int i = 0;
            double size = fileSizeBytes;

            while (size >= 1024 && i < sizeSuffixes.Length - 1)
            {
                size /= 1024;
                i++;
            }

            return $"{size:N1} {sizeSuffixes[i]}";
        }

    }
}