using System;
using System.Collections.Generic;
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


using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace jslz85Assignment4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        String currentFilePath = "new.txt";
        bool modified = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_Changed(object sender, TextChangedEventArgs e)
        {
            setModifiedState();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            startSave();
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (handleSaveRequest())
            {
                startLoad();
            }
        }
            private void About_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("About");

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (handleSaveRequest())
            {
                Close();
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (handleSaveRequest())
            {
                //start new document
                textBox.Text = "";
                startNewTextDocument();
            }
        }
        private void startLoad()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text Documents (.txt)|*.txt"; // Filter files by extension

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                loadDocument(openFileDialog.FileName);
            }
        }
        private void loadDocument(String filePath)
        {
            var fStreamEmployee = new FileStream(filePath, FileMode.Open);
            var binFormatterEmployee = new BinaryFormatter();
            try
            {
            var currentEmployee = (TextDocument)binFormatterEmployee.Deserialize(fStreamEmployee);
            textBox.Text = currentEmployee.content;
            }
            catch { }
            fStreamEmployee.Close();

            
            currentFilePath = filePath;
            unsetModifiedState();
        }
        private Boolean handleSaveRequest()
        {
            if (!modified) return true;

            string messageBoxText = "The employee needs to be saved.\nDo you want to save?";
            string caption = "Employee Editor";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            // Display message box
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            // Process message box results
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes: // Save document and exit
                    startSave();
                    return true;

                case MessageBoxResult.No: // Exit without saving
                    return true;

                case MessageBoxResult.Cancel: // Don't exit
                    return false;
            }

            return false;

        }
        private void startSave()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // http://msdn.microsoft.com/en-us/library/system.io.path.getdirectoryname.aspx

            if (currentFilePath != "")
            {
                saveFileDialog.FileName = System.IO.Path.GetFileName(currentFilePath);
                saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(currentFilePath);
            }

            saveFileDialog.DefaultExt = ".txt"; // Default file extension
            saveFileDialog.Filter = "Text Documents (.txt)|*.txt"; // Filter files by extension
            // Show save file dialog box
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                saveDocument(saveFileDialog.FileName);
            }
        }
        private void saveDocument(String filePath)
        {
            var currentDocument = new TextDocument();
            currentDocument.content = textBox.Text;

            var fStreamEmployee = new FileStream(filePath, FileMode.Create);
            var binFormatterEmployee = new BinaryFormatter();
            try
            {           
            binFormatterEmployee.Serialize(fStreamEmployee, currentDocument);
            }
            catch
            {
                
            }
            fStreamEmployee.Close();

            currentFilePath = filePath;
            unsetModifiedState();
        }
        private void startNewTextDocument()
        {
            textBox.Text = "";

            unsetModifiedState();
            if (currentFilePath != "")
            {
                currentFilePath = System.IO.Path.GetDirectoryName(currentFilePath) + "/new.emp";
            }
        }
        private void unsetModifiedState()
        {
            modified = false;
            Save.IsEnabled = false;
        }
        private void setModifiedState()
        {
            modified = true;
            Save.IsEnabled = true;
        }

    }
}