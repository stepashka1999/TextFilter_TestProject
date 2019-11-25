using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace TextFilter_TestProject
{
    public partial class MainWindow : Window
    {
        private StreamWriter streamWriter;
        private TextFragmentDeleter Deleter;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            
            OFD.Multiselect = true;
            OFD.Filter = "TXT (*.txt)|*.txt";
            
            if(OFD.ShowDialog() == true)
            {
                              
                Deleter = new TextFragmentDeleter(OFD.FileNames);

                var tempStr = OFD.FileName.Split("\\");

                FileNameLabel.Content = $"Имя файла: {tempStr[tempStr.Length-1]}";
            }
        }

        private void DoButton_Click(object sender, RoutedEventArgs e)
        {
            var listOffile = Deleter.DeletFromFile(Int32.Parse(NumOfSimbolsTextBox.Text), (bool)PunctMart_chb.IsChecked);
            
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "TXT (*.txt)|*.txt";

            foreach(var file in listOffile)
            {
                if (SFD.ShowDialog() == true)
                {
                    streamWriter = new StreamWriter(SFD.FileName);
                    streamWriter.WriteLine(file);
                    streamWriter.Close();

                    FileNameLabel.Content = "Файл сохранен";
                }
            }
        }
    }
}
