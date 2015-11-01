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
using Microsoft.Win32;
using LinguisticLibrary.Data;
using LinguisticLibrary.Interfaces;
using LinguisticFeaturesAnalyzer.Data;

namespace LinguisticFeaturesAnalyzer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly OpenFileDialog myOpenFileDialog = new OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();

            SentenceTypeComboBox.ItemsSource = new List<PunctuationMarkType>() { PunctuationMarkType.QuestionMark, PunctuationMarkType.ExclamationMark };

            myOpenFileDialog.FileOk += OnOpenFileDialogOK;
        }

        private void OnMenuItemClick(object sender, RoutedEventArgs e)
        {
            myOpenFileDialog.ShowDialog();
        }

        private void OnOpenFileDialogOK(object sender, EventArgs e)
        {
            var filePath = myOpenFileDialog.FileName;
            DisplayFile(filePath);
        }

        void DisplayFile(string filePath)
        {
            var text = TextParser.Parse(filePath);

            var activeSentences = new List<ActiveSentence>();
            foreach (var sentence in text)
            {
                activeSentences.Add(ActiveTextComponentFactory.GetActivatedSentence(sentence));
            }
            TextContainer.ItemsSource = activeSentences;
        }

        private void OnFilterBySentenceTypeButtonClick(object sender, RoutedEventArgs e)
        {
            if (SentenceTypeComboBox.SelectedItem == null || TextContainer.ItemsSource == null) return;

            var sentenceType = (PunctuationMarkType)SentenceTypeComboBox.SelectedItem;
            var filteredSentences = ((List<ActiveSentence>)TextContainer.ItemsSource).Where(sentence => sentence.Type == sentenceType);
            foreach(var sentence in filteredSentences)
            {
                sentence.IsActive = true;
            }
        }
    }
}
