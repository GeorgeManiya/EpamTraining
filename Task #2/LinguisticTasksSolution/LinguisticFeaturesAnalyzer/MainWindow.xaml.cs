using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using LinguisticLibrary.Data;
using LinguisticFeaturesAnalyzer.Data;
using LinguisticLibrary.Interfaces;

namespace LinguisticFeaturesAnalyzer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly OpenFileDialog myOpenFileDialog = new OpenFileDialog();
        private Text _text;

        public MainWindow()
        {
            InitializeComponent();

            TerminalPunctuationTypeComboBox.ItemsSource = DefaultPunctuationMarks.TerminalPunctuationMarks;
            InternalPunctuationTypeComboBox.ItemsSource = DefaultPunctuationMarks.InternalPunctuationMarks;

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
            _text = ActiveTextComponentFactory.GetActivatedText(text);
            TextContainer.ItemsSource = _text;
        }

        private void OnFilterBySentenceTypeButtonClick(object sender, RoutedEventArgs e)
        {
            if (TextContainer.ItemsSource == null) return;

            foreach (ActiveSentence activeSentence in _text.GetSentences())
            {
                activeSentence.Deactivate();
            }

            var type = EnableByTerminalPunctuationsRadioButton.IsChecked.Value
                ? ((PunctuationMark)TerminalPunctuationTypeComboBox.SelectedItem).Type
                : ((PunctuationMark)InternalPunctuationTypeComboBox.SelectedItem).Type;

            if (EnableByTerminalPunctuationsRadioButton.IsChecked.Value)
            {
                if (TerminalPunctuationTypeComboBox.SelectedItem == null) return;

                var filteredSentences = _text.GetSentences().
                    OfType<ActiveSentence>().Where(sentence => sentence.Type == type);
                foreach (var sentence in filteredSentences)
                {
                    sentence.IsActive = true;
                }
            }

            if (EnableByInternalPunctuationsRadioButton.IsChecked.Value)
            {
                if (InternalPunctuationTypeComboBox.SelectedItem == null) return;

                var filteredSentences = _text.GetSentences().
                    OfType<ActiveSentence>().Where(sentence => sentence.OfType<PunctuationMark>().Any(mark => mark.Type == type));
                foreach (var sentence in filteredSentences)
                {
                    sentence.IsActive = true;
                }
            }
        }

        private void OnChangeWordsInTextButtonClick(object sender, RoutedEventArgs e)
        {
            if (TextContainer.ItemsSource == null) return;

            if (string.IsNullOrEmpty(OriginalWord.Text) || string.IsNullOrEmpty(NewWord.Text)) return;

            foreach (var sentence in _text.GetSentences())
            {
                foreach (var word in sentence.OfType<IWord>().Where(w => w.StringValue.Trim() == OriginalWord.Text))
                    word.StringValue = NewWord.Text;
            }

            TextContainer.Items.Refresh();
        }

        private void OnSortByButtonClick(object sender, RoutedEventArgs e)
        {
            if (SortByWordsCountRadioButton.IsChecked.Value)
            {
                var sortedSentences = _text.GetSentences().OrderBy(sentence => sentence.Count);
                var text = new Text();
                
                foreach(var sentence in sortedSentences)
                {
                    var paragraph = new Paragraph();
                    paragraph.Add(sentence);
                    text.Add(paragraph);
                }
                
                TextContainer.ItemsSource = text;
            }
            else if (SortBySentenceCountRadioButton.IsChecked.Value)
            {
                var sortedParagraphs = _text.OrderBy(paragraph => paragraph.Count);
                var text = new Text();
                foreach(var paragraph in sortedParagraphs)
                {
                    text.Add(paragraph);
                    text.Add(new Paragraph());
                }

                TextContainer.ItemsSource = text;
            }
        }
    }
}
